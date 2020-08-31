using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversityManagementSystem.Models.ViewModel
{
    public class AssignCourseViewModel
    {
        [Display(Name = "Department")]
        [Required(ErrorMessage = "Please, Select Department !")] 
        public int DepartmentId { get; set; }
        [Display(Name = "Teacher")]
        [Required(ErrorMessage = "Please, Select Teacher !")]
        public int TeacherId { get; set; }
        [Display(Name = "Credit To Be Taken")]
        public string TakenCredit { get; set; }
        [Display(Name = "Remaining Credit")]
        public string RemainingCredit { get; set; }
        [Display(Name = "Course Code")]
        [Required(ErrorMessage = "Please, Select Course Code !")]
        [Remote("IsAssignCourse", "Course", ErrorMessage = "This Course Already Assign! Try Another.")]
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string Credit { get; set; }
    }
}