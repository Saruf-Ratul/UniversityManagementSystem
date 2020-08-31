using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversityManagementSystem.Models.ViewModel
{
    public class CourseViewModel
    {
        public string Name { get; set; }
        public double Credit { get; set; }
        public JsonResult IsAssignCourse { get; set; }
    }
}