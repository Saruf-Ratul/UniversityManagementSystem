using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityManagementSystem.Models
{
    public class Grade
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<StudentResult> StudentResults { get; set; } 
    }
}