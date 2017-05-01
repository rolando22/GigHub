using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using GigHub.Core.Models;

namespace GigHub.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Gig> Gigs { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<FollowUp> FollowUps { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Inbox> Inboxes { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}