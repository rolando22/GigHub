using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GigHub.Core.Models;

namespace GigHub.Core.ViewModels
{
    public class FollowUpFormViewModel
    {
        public int Id { get; set; }
        public ApplicationUser Artist { get; set; }
        public int TotalGigs { get; set; }
        public int UpcomingGigs { get; set; }
        public Boolean Follow { get; set; }
    }
}