using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GigHub.Core.Repositories;

namespace GigHub.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IAttendanceRepository Attendances { get; }
        IFollowUpRepository FollowUps { get; }
        IGigRepository Gigs { get; }
        IInboxRepository Inboxes { get; }
        INotificationRepository Notifications { get; }
        IUserRepository Users { get; }
        int Complete();
    }
}