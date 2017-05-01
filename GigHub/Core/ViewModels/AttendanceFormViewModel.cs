using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GigHub.Core.Models;

namespace GigHub.Core.ViewModels
{
    public class AttendanceFormViewModel
    {
        public int Id { get; set; }
        public Gig Gig { get; set; }
        public Boolean Attendance { get; set; }
    }
}