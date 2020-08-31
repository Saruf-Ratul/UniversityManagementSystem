using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace UniversityManagementSystem.Models
{
    public class Course
    {
        public Course()
        {
            AssignCrouses = new List<AssignCrouse>();
        }
        public int Id { get; set; }
        [Required(ErrorMessage = "Please, Enter Course Code !")]
        [Remote("IsValidCode", "Course", ErrorMessage = "This Code Already Exist! Try Another.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Code Must Be At Least 5 Characters Long")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Please, Enter Course Name !")]
        [Remote("IsValidName", "Course", ErrorMessage = "This Name Already Exist! Try Another.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please, Enter Course Credit !")]
        [Range(0.5, 5.0, ErrorMessage = "Credit Cannot Be Less Than 0.5 And More Than 5.0")]
        public double Credit { get; set; }
        public string Description { get; set; }
        [Display(Name = "Department")]
        [Required(ErrorMessage = "Please, Select Department !")]        
        public int DepartmentId { get; set; }
        [Display(Name = "Semester")]
        [Required(ErrorMessage = "Please, Select Semester !")]
        public int SemesterId { get; set; }

        public virtual Department Department { get; set; }
        public virtual Semester Semester { get; set; }
        public virtual ICollection<AssignCrouse> AssignCrouses { get; set; }
        public virtual List<CourseEnroll> CourseEnrolls { get; set; } 
    }
}