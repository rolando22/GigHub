using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GigHub.Core.Models;

namespace GigHub.Core.ViewModels
{
    public class GigFormViewModel
    {
        public Gig Gig { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
    }
}