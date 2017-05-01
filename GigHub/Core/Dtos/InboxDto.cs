using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.Core.Dtos
{
    public class InboxDto
    {
        public int Id { get; set; }
        public ApplicationUserDto User { get; set; }
        public NotificationDto Notification { get; set; }
        public Boolean Viewed { get; set; }
    }
}