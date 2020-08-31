using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityManagementSystem.Models
{
    public class AssignCrouse
    {
        public int Id { get; set; }
        [Required]
        public int TeacherId { get; set; }
        [Required]
        public int CourseId { get; set; }
        public bool IsUnassign { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual Course Course { get; set; }

    }
}