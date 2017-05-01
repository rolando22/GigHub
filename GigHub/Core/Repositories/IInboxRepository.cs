using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IInboxRepository : IRepository<Inbox>
    {
        IEnumerable<Inbox> GetAllUserCurrent(string userCurrent);
        IEnumerable<Inbox> GetViewed(string userCurrent, bool viewed);
    }
}