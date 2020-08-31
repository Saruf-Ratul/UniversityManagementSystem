using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace UniversityManagementSystem.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please, Enter Department Code !")]
        [Remote("IsValidCode", "Department", ErrorMessage = "This Code Already Exist! Try Another.")]
        [StringLength(7, MinimumLength = 2, ErrorMessage = "Code Must Be 2 to 7 Characters Long")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Please, Enter Department Name !")]
        [Remote("IsValidName", "Department", ErrorMessage = "This Name Already Exist! Try Another.")]
        public string Name { get; set; }
        public virtual List<Course> Courses { get; set; } 
        public virtual List<Student> Students { get; set; }
        public virtual List<Teacher> Teachers { get; set; } 
    }
}