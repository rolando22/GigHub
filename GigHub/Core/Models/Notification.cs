using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.Core.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Information { get; set; }
        public string GigDate { get; set; }
        public Boolean Delivered { get; set; }
        public int Gig { get; set; }
    }
}