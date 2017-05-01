﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface INotificationRepository : IRepository<Notification>
    {
        Notification GetNotDelivered();
        IEnumerable<Notification> GetNotificationsGig(int gigId);
    }
}