using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Controllers
{
    public class StudentController : Controller
    {
        ProjectDbContext db = new ProjectDbContext();
        public ActionResult Save()
        {
            var getAllDepartments = db.Departments.ToList();
            ViewBag.getAllDepartments = getAllDepartments;
            return View();
        }
        public JsonResult IsEmailExist(string Email)
        {
            return Json(!db.Students.Any(stu => stu.Email == Email), JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveStudent(Student student)
        {
            string regNo = "";
            try
            {
                if (ModelState.IsValid)
                {
                    Student aStudent = new Student();
                    aStudent.Name = student.Name;
                    aStudent.Email = student.Email;
                    aStudent.ContactNo = student.ContactNo;
                    aStudent.Date = student.Date.Date;
                    aStudent.Address = student.Address;
                    aStudent.DepartmentId = student.DepartmentId;
                    aStudent.RegNo = GenerateRegNo(student.DepartmentId, student.Date);
                    regNo = aStudent.RegNo;
                    db.Students.Add(aStudent);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(regNo, JsonRequestBehavior.AllowGet);
        }
        public string GenerateRegNo(int deptId, DateTime date)
        {
            var dept = db.Departments.FirstOrDefault(x => x.Id == deptId);
            string regNo = "";
            if (dept != null)
            {
                var code = dept.Code;
                var year = date.Date.Year.ToString();
                string codeYear = code + "-" + year;
                var stu = db.Students.Where(x => x.RegNo.Contains(codeYear)).ToList();
                if (stu.Count > 0)
                {
                    var stuReg = stu.Last();
                    string reg = stuReg.RegNo;
                    string[] numbers = Regex.Split(reg, @"\D+");
                    string value = numbers.Last();

                    if (!string.IsNullOrEmpty(value))
                    {
                        int counter = int.Parse(value);
                        int i = ++counter;
                        regNo = codeYear + "-" + i.ToString("000");
                    }
                }
                else
                {
                    int counter = 0;
                    regNo = codeYear + "-" + (++counter).ToString("000");
                }
            }
            return regNo;
        }
        public ActionResult EnrollInACourse()
        {
            var getAllStudents = db.Students.ToList();
            ViewBag.getAllStudents = getAllStudents;
            return View();
        }
        public JsonResult IsEnrolledCourse(CourseEnroll courseEnroll)
        {
            return Json(!db.CourseEnrolls.Any(course => course.CourseId == courseEnroll.CourseId && course.StudentId == courseEnroll.StudentId), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetStudentInfoByStudentId(Student student)
        {
            var getAllstudent = db.Students.ToList();
            var studentDetails = getAllstudent.FirstOrDefault(a => a.Id == student.Id);
            CourseEnroll aCourseEnroll = new CourseEnroll();
            int deptId;
            if (studentDetails != null)
            {
                aCourseEnroll.Name = studentDetails.Name;
                aCourseEnroll.Email = studentDetails.Email;
                deptId = studentDetails.DepartmentId;
                var department = db.Departments.FirstOrDefault(x => x.Id == deptId);
                if (department != null) aCourseEnroll.Department = department.Name;
            }
            //string value = string.Empty;
            //value = JsonConvert.SerializeObject(takenAndRemainingCredit, Formatting.Indented, new JsonSerializerSettings
            //{
            //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            //});
            return Json(aCourseEnroll, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCourseByStudentDepartment(Student student)
        {
            var studentDetails = db.Students.FirstOrDefault(a => a.Id == student.Id);
            db.Configuration.ProxyCreationEnabled = false;
            var course = db.Courses.ToList();
            var courseList = course.Where(a => studentDetails != null && a.DepartmentId == studentDetails.DepartmentId).ToList();

            return Json(courseList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveCourseEnrollInfo(CourseEnroll courseEnroll)
        {
            bool result = false;
            try
            {
                if (ModelState.IsValid)
                {
                    CourseEnroll aCourseEnroll = new CourseEnroll();
                    aCourseEnroll.StudentId = courseEnroll.StudentId;
                    aCourseEnroll.CourseId = courseEnroll.CourseId;
                    aCourseEnroll.Date = courseEnroll.Date.Date;
                    db.CourseEnrolls.Add(aCourseEnroll);
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
        public ActionResult SaveResult()
        {
            var getAllStudents = db.Students.ToList();
            ViewBag.getAllStudents = getAllStudents;
            var getAllGradeLetter = db.Grades.ToList();
            ViewBag.getAllGradeLetter = getAllGradeLetter;
            return View();
        }
        public JsonResult GetStudentEnrollCourses(Student student)
        {
            var studentEnrollCourse = (from enrollCourse in db.CourseEnrolls
                                       join courseN in db.Courses on enrollCourse.CourseId equals courseN.Id
                                       where enrollCourse.StudentId == student.Id
                                       select new
                                       {
                                           Id = courseN.Id,
                                           Name = courseN.Name
                                       }).ToList();
            return Json(studentEnrollCourse, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveStudentResult(StudentResult studentResult)
        {
            string result = "";
            try
            {
                if (ModelState.IsValid)
                {
                    StudentResult aResult = new StudentResult();
                    aResult.StudentId = studentResult.StudentId;
                    aResult.CourseId = studentResult.CourseId;
                    aResult.GradeId = studentResult.GradeId;
                    var alreadySaveResult = db.StudentResults.FirstOrDefault(x => x.StudentId == studentResult.StudentId && x.CourseId == studentResult.CourseId);
                    if (alreadySaveResult == null)
                    {
                        db.StudentResults.Add(aResult);
                        db.SaveChanges();
                        result = "Successfully! Save Student Result";
                    }
                    else
                    {
                        alreadySaveResult.GradeId = studentResult.GradeId;
                        db.SaveChanges();
                        result = "Successfully! Update Student Result";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ViewResult()
        {
            var getAllStudents = db.Students.ToList();
            ViewBag.getAllStudents = getAllStudents;
            return View();
        }
        public JsonResult GetResultByStudentId(Student student)
        {
            var studentEnrollCourseByStudentId = (from enrollCourse in db.CourseEnrolls
                                                  join courseN in db.Courses on enrollCourse.CourseId equals courseN.Id
                                                  where enrollCourse.StudentId == student.Id
                                                  select new
                                                  {
                                                      Id = courseN.Id,
                                                      Code = courseN.Code,
                                                      Name = courseN.Name
                                                  }).ToList();

            List<StudentResultViewModel> aResultViewModels = new List<StudentResultViewModel>();
            foreach (var data in studentEnrollCourseByStudentId)
            {
                StudentResultViewModel resultViewModel = new StudentResultViewModel();
                resultViewModel.CourseCode = data.Code;
                resultViewModel.Name = data.Name;
                int courseId = data.Id;
                var gradeLetterFind = (from studentR in db.StudentResults
                                       join gradeLetter in db.Grades on studentR.GradeId equals gradeLetter.Id
                                       where studentR.StudentId == student.Id && studentR.CourseId == courseId
                                       select new
                                       {
                                           Name = gradeLetter.Name
                                       }).FirstOrDefault();

                if (gradeLetterFind == null)
                {
                    resultViewModel.Grade = "Not Graded Yet";
                }
                else
                {
                    resultViewModel.Grade = gradeLetterFind.Name;
                }

                aResultViewModels.Add(resultViewModel);

            }
            return Json(aResultViewModels);
        }

    }
}