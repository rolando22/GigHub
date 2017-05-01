using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using GigHub.Core.Models;
using GigHub.Core.Repositories;

namespace GigHub.Persistence.Repositories
{
    public class NotificationRepository : Repository<Notification>, INotificationRepository
    {
        public NotificationRepository(DbContext context) : base(context)
        {
        }

        public Notification GetNotDelivered()
        {
            return GigHubContext.Notifications.SingleOrDefault(n => n.Delivered == false);
        }

        public IEnumerable<Notification> GetNotificationsGig(int gigId)
        {
            return GigHubContext.Notifications.Where(n => n.Gig == gigId).ToList();
        }

        public ApplicationDbContext GigHubContext
        {
            get { return Context as ApplicationDbContext; }
        }
    }
}