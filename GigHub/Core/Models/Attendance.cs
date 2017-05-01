using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.Core.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        public ApplicationUser Artist { get; set; }
        public Gig Gig { get; set; }
    }
}