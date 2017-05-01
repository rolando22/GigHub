using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.Core.Dtos
{
    public class ApplicationUserDto
    {
        public string Id { get; set; }
        public string ArtisticName { get; set; }
        public string Image { get; set; }
        public string Email { get; set; }
    }
}