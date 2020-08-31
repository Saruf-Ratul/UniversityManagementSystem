using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityManagementSystem.Models.ViewModel
{
    public class RoomInfo
    {
        public int Id { get; set; }
        public string RoomNo { get; set; }

        public virtual List<RoomAllocation> RoomAllocations { get; set; }
    }
}