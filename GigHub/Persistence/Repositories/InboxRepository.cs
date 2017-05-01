using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using GigHub.Core.Models;
using GigHub.Core.Repositories;

namespace GigHub.Persistence.Repositories
{
    public class InboxRepository : Repository<Inbox>, IInboxRepository
    {
        public InboxRepository(DbContext context) : base (context)
        {
        }

        public IEnumerable<Inbox> GetAllUserCurrent(string userCurrent)
        {
            return GigHubContext.Inboxes.Include(i => i.User).Include(i => i.Notification)
                .Where(i => i.User.Id == userCurrent).OrderByDescending(i => i.Id).ToList();
        }

        public IEnumerable<Inbox> GetViewed(string userCurrent, bool viewed)
        {
            return GigHubContext.Inboxes.Include(i => i.User).Include(i => i.Notification)
                .Where(i => i.User.Id == userCurrent).Where(i => i.Viewed == viewed).ToList();
        }

        public ApplicationDbContext GigHubContext
        {
            get { return Context as ApplicationDbContext; }
        }
    }
}