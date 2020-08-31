using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace UniversityManagementSystem.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please, Enter Your Name!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please, Enter Your Email !")]
        [RegularExpression(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", ErrorMessage = "Please, Enter A Valid Email")]
        [Remote("IsEmailExist", "Student", ErrorMessage = "This Email Already Exist! Try Another.")]
        public string Email { get; set; }
        [Display(Name = "Contact No.")]
        [Required(ErrorMessage = "Please, Enter Your Contact No !")]
        [RegularExpression(@"^([0-9\(\)\/\+ \-]*)$", ErrorMessage = "Enter Valid Contact No.")]
        public string ContactNo { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Please, Enter Your Address !")]
        public string Address { get; set; }
        [Display(Name = "Department")]
        [Required(ErrorMessage = "Please, Select Department !")]
        public int DepartmentId { get; set; }
        public string RegNo { get; set; }
        public virtual Department Department { get; set; }
        public virtual List<CourseEnroll> CourseEnrolls { get; set; } 
    }
}