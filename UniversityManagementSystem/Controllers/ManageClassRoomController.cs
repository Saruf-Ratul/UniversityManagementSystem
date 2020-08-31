using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystem.Models;
using UniversityManagementSystem.Models.ViewModel;

namespace UniversityManagementSystem.Controllers
{
    public class ManageClassRoomController : Controller
    {
        ProjectDbContext db = new ProjectDbContext();
        public ActionResult Index()
        {
            List<Department> getAllDepartments = db.Departments.ToList();
            ViewBag.getAllDepartments = getAllDepartments;
            List<RoomInfo> getAllRoom = db.RoomInfos.ToList();
            ViewBag.getAllRoom = getAllRoom;
            List<Day> getAllDay = db.Days.ToList();
            ViewBag.getAllDay = getAllDay;
            return View();
        }
        public JsonResult SaveRoomAllocationInfo(RoomAllocation rAllocation)
        {
            string result = "";
            //try
            //{
            if (ModelState.IsValid)
            {
                result = CheckTimeOverlapOrNot(rAllocation);
                if (result == "NotOverlap")
                {
                    RoomAllocation allocation = new RoomAllocation();
                    allocation.CourseId = rAllocation.CourseId;
                    allocation.RoomId = rAllocation.RoomId;
                    allocation.DayId = rAllocation.DayId;
                    allocation.StarTime = rAllocation.StarTime;
                    allocation.EndTime = rAllocation.EndTime;
                    allocation.IsUnAllocated = false;
                    db.RoomAllocations.Add(allocation);
                    db.SaveChanges();
                }
            }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public string CheckTimeOverlapOrNot(RoomAllocation rAllocation)
        {
            var allocatedRoomByDay = db.RoomAllocations.Where(x => x.RoomId == rAllocation.RoomId && x.DayId == rAllocation.DayId && x.IsUnAllocated == false).ToList();
            if (allocatedRoomByDay.Count > 0)
            {
                foreach (var aTime in allocatedRoomByDay)
                {
                    if (aTime.StarTime.TimeOfDay < rAllocation.EndTime.TimeOfDay && rAllocation.StarTime.TimeOfDay < aTime.EndTime.TimeOfDay)
                    {
                        string overlap = "";
                        var course = db.Courses.FirstOrDefault(x => x.Id == aTime.CourseId);
                        var roomNo = db.RoomInfos.FirstOrDefault(x => x.Id == aTime.RoomId);
                        var day = db.Days.FirstOrDefault(x => x.Id == aTime.DayId);
                        if (roomNo != null && course != null && day != null)
                        {
                            overlap = "Overlapping Schedule! Try Another ): " + "Coure : " + course.Name + "," + " R. No : " + roomNo.RoomNo + "," + " Day : " +
                                      day.DayName + ", " + aTime.StarTime.ToString("h:mm tt") + " - " + aTime.EndTime.ToString("h:mm tt");
                        }
                        return overlap;
                    }
                }
            }
            return "NotOverlap";
        }
        public ActionResult ViewClassSchedule()
        {
            ViewBag.Departments = db.Departments.ToList();
            return View();
        }
        public JsonResult GetClassSchedulByDepartmentId(Department department)
        {
            var getCourseByDeptId = (from course in db.Courses
                                     join semester in
                                         db.Semesters on course.SemesterId equals semester.Id
                                     where course.DepartmentId == department.Id
                                     select new
                                     {
                                         Id = course.Id,
                                         Code = course.Code,
                                         Name = course.Name,
                                         SemesterNo = semester.SemesterNo
                                     }).ToList();


            List<ClassScheduleViewModel> aClassScheduleViewModels = new List<ClassScheduleViewModel>();
            foreach (var data in getCourseByDeptId)
            {
                //CourseStaticsViewModel aCourseStaticsViewModel = new CourseStaticsViewModel();
                ClassScheduleViewModel aClassScheduleViewModel = new ClassScheduleViewModel();
                aClassScheduleViewModel.Code = data.Code;
                aClassScheduleViewModel.Name = data.Name;

                int courseId = data.Id;
                var getScheduleInfo = (from allocatedRoom in db.RoomAllocations
                                       join room in db.RoomInfos on allocatedRoom.RoomId equals room.Id
                                       join day in db.Days on allocatedRoom.DayId equals day.Id
                                       where allocatedRoom.CourseId == courseId
                                             && allocatedRoom.IsUnAllocated == false
                                       select new
                                       {
                                           RoomNo = room.RoomNo,
                                           DayName = day.DayName,
                                           StarTime = allocatedRoom.StarTime,
                                           EndTime = allocatedRoom.EndTime
                                       }).ToList();
                List<string> scheduleList = new List<string>();
                if (getScheduleInfo.Count > 0)
                {
                    foreach (var schedule in getScheduleInfo)
                    {
                        string ganerateSchedule = " R. No : " + schedule.RoomNo + "," + " Day : " +
                                       schedule.DayName + ", " + schedule.StarTime.ToString("h:mm tt") + " - " + schedule.EndTime.ToString("h:mm tt") + ";" + "<br/>";
                        scheduleList.Add(ganerateSchedule);
                    }
                }
                else
                {
                    string ganerateSchedule = "Not Scheduled Yet";
                    scheduleList.Add(ganerateSchedule);
                }
                aClassScheduleViewModel.Schedule = scheduleList;
                aClassScheduleViewModels.Add(aClassScheduleViewModel);

            }
            return Json(aClassScheduleViewModels);
        }
        public ActionResult UnallocateAllClassRoom()
        {
            return View();
        }
        public JsonResult ComfirmUnallocateAllClassRoom()
        {
            var getAllAllocatedClassrooms = db.RoomAllocations.Where(x => x.IsUnAllocated == false).ToList();
            foreach (var data in getAllAllocatedClassrooms)
            {
                data.IsUnAllocated = true;
                db.SaveChanges();
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}