using System.Collections.Generic;

namespace UniversityManagementSystem.Models
{
    public class Semester
    {
        public int Id { get; set; }
        public string SemesterNo { get; set; }
        public virtual List<Course> Courses { get; set; } 
    }

}