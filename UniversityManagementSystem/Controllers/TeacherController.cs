using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using UniversityManagementSystem.Models;
using UniversityManagementSystem.Models.ViewModel;

namespace UniversityManagementSystem.Controllers
{
    public class TeacherController : Controller
    {
        ProjectDbContext db = new ProjectDbContext();
        public ActionResult Save()
        {
            List<Department> getAllDepartments = db.Departments.ToList();
            List<Designation> getAllDesignations = db.Designations.ToList();
            ViewBag.getAllDepartments = getAllDepartments;
            ViewBag.getAllDesignations = getAllDesignations;
            return View();
        }
        public JsonResult IsValidEmail(string Email)
        {
            return Json(!db.Teachers.Any(teacher => teacher.Email == Email), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Save(Teacher teacher)
        {
            var result = false;
            try
            {
                if (ModelState.IsValid)
                {
                    Teacher aTeacher = new Teacher();
                    aTeacher.Name = teacher.Name;
                    aTeacher.Address = teacher.Address;
                    aTeacher.Email = teacher.Email;
                    aTeacher.ContactNo = teacher.ContactNo;
                    aTeacher.DesignationId = teacher.DesignationId;
                    aTeacher.DepartmentId = teacher.DepartmentId;
                    aTeacher.TakenCredit = teacher.TakenCredit;
                    db.Teachers.Add(aTeacher);
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
        public ActionResult CourseAssign()
        {
            List<Department> getAllDepartments = db.Departments.ToList();
            ViewBag.getAllDepartments = getAllDepartments;
            return View();
        }
        public JsonResult GetTeacherByDepartmentId(Department department)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var teacher = db.Teachers.ToList();
            var teacherList = teacher.Where(a => a.DepartmentId == department.Id).ToList();
            return Json(teacherList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTeacherDetailByTeacherId(Teacher teacher)
        {
            var getAllTeacher = db.Teachers.ToList();
            var teacherDetails = getAllTeacher.FirstOrDefault(a => a.Id == teacher.Id);
            var takenCredit=0.0;
            if (teacherDetails != null)
            {
                takenCredit = teacherDetails.TakenCredit;
            }
            var selectCredit = (from assignC in db.AssignCrouses
                               join course in db.Courses
                                   on assignC.CourseId equals course.Id
                               where assignC.TeacherId == teacher.Id && assignC.IsUnassign == false
                               select new
                               {
                                   Credit = course.Credit
                               }).ToList();

            var totalCourseAssignCredit = selectCredit.Select(c => c.Credit).Sum();
            var remainingCredit = takenCredit - totalCourseAssignCredit;
            var takenAndRemainingCredit = new TeacherViewModel();
            takenAndRemainingCredit.TakenCredit = takenCredit;
            takenAndRemainingCredit.RemainingCredit = remainingCredit;
            string value = string.Empty;
            value = JsonConvert.SerializeObject(takenAndRemainingCredit, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult CourseAssign(AssignCourseViewModel assignCourse)
        {
            var result = false;
            try
            {
                if (ModelState.IsValid)
                {
                    AssignCrouse aAssignCrouse = new AssignCrouse();
                    aAssignCrouse.CourseId = assignCourse.CourseId;
                    aAssignCrouse.TeacherId = assignCourse.TeacherId;
                    aAssignCrouse.IsUnassign = false;
                    db.AssignCrouses.Add(aAssignCrouse);
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
    }
}