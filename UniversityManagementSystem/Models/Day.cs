using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityManagementSystem.Models
{
    public class Day
    {
        public int Id { get; set; }
        public string DayName { get; set; }
        public virtual List<RoomAllocation> RoomAllocations { get; set; }
    }
}