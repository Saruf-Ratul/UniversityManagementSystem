using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversityManagementSystem.Models
{
    public class StudentResult
    {
        public int Id { get; set; }
        [Display(Name = "Student Reg. No.")]
        [Required(ErrorMessage = "Please, Select Reg. No.")]
        public int StudentId { get; set; }
        [Display(Name = "Select Course")]
        [Required(ErrorMessage = "Please, Select Course")]
        public int CourseId { get; set; }
        [Display(Name = "Select Grade Letter")]
        [Required(ErrorMessage = "Please, Select Grade Letter")]
        public int GradeId { get; set; }

        [NotMapped]
        public string Name { get; set; }
        [NotMapped]
        public string Email { get; set; }
        [NotMapped]
        public string Department { get; set; }

        public virtual Student Student { get; set; }
        public virtual Course Course { get; set; }
        public virtual Grade Grade { get; set; }
    }
}