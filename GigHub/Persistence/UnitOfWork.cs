using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GigHub.Core;
using GigHub.Core.Repositories;
using GigHub.Persistence.Repositories;

namespace GigHub.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IAttendanceRepository Attendances { get; private set; }
        public IFollowUpRepository FollowUps { get; private set; }
        public IGigRepository Gigs { get; private set; }
        public IInboxRepository Inboxes { get; private set; }
        public INotificationRepository Notifications { get; private set; }
        public IUserRepository Users { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Attendances = new AttendanceRepository(_context);
            FollowUps = new FollowUpRepository(_context);
            Gigs = new GigRepository(_context);
            Inboxes = new InboxRepository(_context);
            Notifications = new NotificationRepository(_context);
            Users = new UserRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }
    }
}