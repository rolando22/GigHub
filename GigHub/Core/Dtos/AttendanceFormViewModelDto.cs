using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.Core.Dtos
{
    public class AttendanceFormViewModelDto
    {
        public int Id { get; set; }
        public GigDto Gig { get; set; }
        public Boolean Attendance { get; set; }
    }
}