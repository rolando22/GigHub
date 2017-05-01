using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.Core.Dtos
{
    public class FollowUpFormViewModelDto
    {
        public int Id { get; set; }
        public ApplicationUserDto Artist { get; set; }
        public int TotalGigs { get; set; }
        public int UpcomingGigs { get; set; }
        public Boolean Follow { get; set; }
    }
}