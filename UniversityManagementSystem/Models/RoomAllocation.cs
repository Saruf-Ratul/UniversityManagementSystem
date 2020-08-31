using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using UniversityManagementSystem.Models.ViewModel;

namespace UniversityManagementSystem.Models
{
    public class RoomAllocation
    {
        public int Id { get; set; }
        [Display(Name = "Course")]
        [Required(ErrorMessage = "Please, Select Course!")]
        public int CourseId { get; set; }
        [Display(Name = "Room No.")]
        [Required(ErrorMessage = "Please, Select Room No.")]
        public int RoomId { get; set; }
        [Display(Name = "Day")]
        [Required(ErrorMessage = "Please, Select Day!")]
        public int DayId { get; set; }
        [Display(Name = "From")]
        [Required(ErrorMessage = "Please, Enter Star Time!")]
        [DataType(DataType.Time)]
        public DateTime StarTime { get; set; }
        [Display(Name = "To")]
        [Required(ErrorMessage = "Please, Enter End Time!")]
        [DataType(DataType.Time)]
        public DateTime EndTime { get; set; }
        public bool IsUnAllocated { get; set; }
        [NotMapped]
        [Display(Name = "Department")]
        [Required(ErrorMessage = "Please, Please Select Department!")]
        public int DepartmentId { get; set; }

        public virtual RoomInfo Room { get; set; }
        public virtual Day Day { get; set; }
    }
}