using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.Core.Models
{
    public class FollowUp
    {
        public int Id { get; set; }
        public ApplicationUser Follower { get; set; }
        public ApplicationUser Followed { get; set; }
    }
}