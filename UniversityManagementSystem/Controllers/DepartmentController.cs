using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Controllers
{
    public class DepartmentController : Controller
    {
        ProjectDbContext db = new ProjectDbContext();
        public ActionResult SaveNewDept()
        {
            return View();
        }
        public JsonResult IsValidCode(string Code)
        {
            return Json(!db.Departments.Any(dept => dept.Code == Code),JsonRequestBehavior.AllowGet);
        }
        public JsonResult IsValidName(string Name)
        {
            return Json(!db.Departments.Any(dept => dept.Name == Name), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Save(Department department)
        {
            var result = false;  
            try
            {
                if (ModelState.IsValid)
                {
                    Department aDepartment = new Department();
                    aDepartment.Name = department.Name;
                    aDepartment.Code = department.Code;
                    db.Departments.Add(aDepartment);
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
        public ActionResult ShowAllDept()
        {
            List<Department> deptList = db.Departments.ToList();
            return View(deptList);
        }
	}
}