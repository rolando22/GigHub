using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GigHub.Core.Models;

namespace GigHub.Core.ViewModels
{
    public class InboxFormViewModel
    {
        public List<Inbox> Vieweds { get; set; }
        public List<Inbox> NotVieweds { get; set; }

        public InboxFormViewModel()
        {
            Vieweds = new List<Inbox>();
            NotVieweds = new List<Inbox>();
        }
    }
}