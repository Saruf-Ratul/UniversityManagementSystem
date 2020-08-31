using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversityManagementSystem.Models
{
    public class CourseEnroll
    {
        public int Id { get; set; }
        [Display(Name = "Student Reg. No.")]
        [Required(ErrorMessage = "Please, Select Reg. No.")]
        public int StudentId { get; set; }
        [Display(Name = "Select Course")]
        [Required(ErrorMessage = "Please, Select Course")]
        [Remote("IsEnrolledCourse", "Student", AdditionalFields = "StudentId", ErrorMessage = "You Already Enroll This Course! Try Another.", HttpMethod = "POST")]
        public int CourseId { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Please, Select Date")]
        public DateTime Date { get; set; }

        [NotMapped]
        public string Name { get; set; }
        [NotMapped]
        public string Email { get; set; }
        [NotMapped]
        public string Department { get; set; }


       
        public virtual Student Student { get; set; }
        public virtual Course Course { get; set; }
    }
}