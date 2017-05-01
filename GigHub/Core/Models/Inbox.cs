using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.Core.Models
{
    public class Inbox
    {
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
        public Notification Notification { get; set; }
        public Boolean Viewed { get; set; }
    }
}