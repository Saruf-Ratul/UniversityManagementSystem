using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityManagementSystem.Models.ViewModel
{
    public class ClassScheduleViewModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string SemesterNo { get; set; }
        public List<string> Schedule { get; set; }
    }
}