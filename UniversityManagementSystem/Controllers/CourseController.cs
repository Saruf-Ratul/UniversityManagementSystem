using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using UniversityManagementSystem.Models;
using UniversityManagementSystem.Models.ViewModel;

namespace UniversityManagementSystem.Controllers
{
    public class CourseController : Controller
    {
        ProjectDbContext db = new ProjectDbContext();
        public ActionResult Save()
        {
            List<Department> getAllDepartments = db.Departments.ToList();
            List<Semester> getAllSemesters = db.Semesters.ToList();
            ViewBag.getAllDepartments = getAllDepartments;
            ViewBag.getAllSemesters = getAllSemesters;
            return View();
        }
        public JsonResult IsValidCode(string Code)
        {
            return Json(!db.Courses.Any(course => course.Code == Code), JsonRequestBehavior.AllowGet);
        }
        public JsonResult IsValidName(string Name)
        {
            return Json(!db.Courses.Any(course => course.Name == Name), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Save(Course course)
        {
            var result = false;
            try
            {
                if (ModelState.IsValid)
                {
                    Course aCourse = new Course();
                    aCourse.Code = course.Code;
                    aCourse.Name = course.Name;
                    aCourse.Credit = course.Credit;
                    aCourse.Description = course.Description;
                    aCourse.DepartmentId = course.DepartmentId;
                    aCourse.SemesterId = course.SemesterId;
                    db.Courses.Add(aCourse);
                    db.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCourseByDepartmentId(Department department)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var course = db.Courses.ToList();
            var courseList = course.Where(a => a.DepartmentId == department.Id).ToList();
            return Json(courseList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult IsAssignCourse(int CourseId)
        {
            return Json(!db.AssignCrouses.Any(course => course.CourseId == CourseId && course.IsUnassign == false), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCourseDetailByCourseId(Course course)
        {
            var getAllCourse = db.Courses.ToList();
            var courseDetails = getAllCourse.FirstOrDefault(a => a.Id == course.Id);
            CourseViewModel aCourseViewModel = new CourseViewModel();
            if (courseDetails != null)
            {
                aCourseViewModel.Name = courseDetails.Name;
                aCourseViewModel.Credit = courseDetails.Credit;
            }
            aCourseViewModel.IsAssignCourse = IsAssignCourse(course.Id);
            //string value = string.Empty;
            //value = JsonConvert.SerializeObject(aCourseViewModel, Formatting.Indented, new JsonSerializerSettings
            //{
            //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            //});

            return Json(aCourseViewModel, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ViewCourseStatics()
        {
            ViewBag.Departments = db.Departments.ToList();
            return View();
        }
        public JsonResult GetCourseStaticsByDepartmentId(Department department)
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


            List<CourseStaticsViewModel> aStaticsViewModels = new List<CourseStaticsViewModel>();
            foreach (var data in getCourseByDeptId)
            {
                CourseStaticsViewModel aCourseStaticsViewModel = new CourseStaticsViewModel();
                aCourseStaticsViewModel.Code = data.Code;
                aCourseStaticsViewModel.Name = data.Name;
                aCourseStaticsViewModel.SemesterNo = data.SemesterNo;
                int courseId = data.Id;
                var getTeacher = (from assignCourse in db.AssignCrouses
                                  join teacher in db.Teachers on
                                      assignCourse.TeacherId equals teacher.Id
                                  where assignCourse.CourseId == courseId
                                        && assignCourse.IsUnassign == false
                                  select new
                                  {
                                      Name = teacher.Name
                                  }).SingleOrDefault();
                if (getTeacher != null)
                {
                    string teacherName = getTeacher.Name;
                    if (string.IsNullOrEmpty(teacherName))
                    {
                        aCourseStaticsViewModel.TeacherName = "Not Assigned Yet";
                    }
                    else
                    {
                        aCourseStaticsViewModel.TeacherName = teacherName;
                    }
                }
                else
                {
                    aCourseStaticsViewModel.TeacherName = "Not Assigned Yet";
                }
                aStaticsViewModels.Add(aCourseStaticsViewModel);

            }
            return Json(aStaticsViewModels);
        }
        public ActionResult UnassignAllCourses()
        {
           return View();
        }
        public JsonResult ComfirmUnassignAllCourse()
        {
            var getAllAssignCourse = db.AssignCrouses.Where(x => x.IsUnassign == false).ToList();
            foreach (var data in getAllAssignCourse)
            {
                data.IsUnassign = true;
                db.SaveChanges();
            }
            return Json(true , JsonRequestBehavior.AllowGet);
        }
    }
}