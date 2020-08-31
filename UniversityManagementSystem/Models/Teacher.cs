using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace UniversityManagementSystem.Models
{
    public class Teacher
    {
        public Teacher()
        {
           this.AssignCrouses = new List<AssignCrouse>();
        }
        public int Id { get; set; }
        [Required(ErrorMessage = "Please, Enter Your Name!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please, Enter Your Address !")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please, Enter Your Email !")]
        [RegularExpression(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", ErrorMessage = "Please, Enter A Valid Email")]
        [Remote("IsValidEmail", "Teacher", ErrorMessage = "This Email Already Exist! Try Another.")]
        public string Email { get; set; }
        [Display(Name = "Contact No.")]
        [Required(ErrorMessage = "Please, Enter Your Contact No. !")]
        [RegularExpression(@"^([0-9\(\)\/\+ \-]*)$", ErrorMessage = "Enter Valid Contact No.")]
        public string ContactNo { get; set; }
        [Required(ErrorMessage = "Please, Select Your Designation !")]
        [Display(Name = "Designation")]
        public int DesignationId { get; set; }
        [Display(Name = "Department")]
        [Required(ErrorMessage = "Please, Select Department !")]
        public int DepartmentId { get; set; }
        [Display(Name = "Credit To Be Taken")]
        [Required(ErrorMessage = "Please, Enter Credit To Be Taken !")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Please,Enter Non-negative Value")]
        public double TakenCredit { get; set; }

        public virtual Department Department { get; set; }
        public virtual Designation Designation { get; set; }
        public virtual ICollection<AssignCrouse> AssignCrouses { get; set; }

    }
}