using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityManagementSystem.Models
{
    public class Designation
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual List<Teacher> Teachers { get; set; } 
    }
}