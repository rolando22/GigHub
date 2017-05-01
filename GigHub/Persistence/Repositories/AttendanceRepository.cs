using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using GigHub.Core.Models;
using GigHub.Core.Repositories;

namespace GigHub.Persistence.Repositories
{
    public class AttendanceRepository : Repository<Attendance>, IAttendanceRepository
    {
        public AttendanceRepository(DbContext context) : base(context)
        {
        }

        public Attendance GetWithArtistGig(int id)
        {
            return GigHubContext.Attendances.Include(a => a.Artist).Include(a => a.Gig).First(a => a.Id.Equals(id));
        }

        public Attendance GetMyAttendance(string userCurrent, int gigId)
        {
            return GigHubContext.Attendances.Include(a => a.Artist).Include(a => a.Gig)
                .Where(a => a.Artist.Id == userCurrent && a.Gig.Id == gigId).FirstOrDefault();
        }

        public IEnumerable<Attendance> GetAllWithArtistGig(string userCurrent)
        {
            return GigHubContext.Attendances.Include(a => a.Artist).Include(a => a.Gig)
                .Include(a => a.Gig.Artist).Include(a => a.Gig.Genre)
                .Where(a => a.Artist.Id == userCurrent).OrderByDescending(a => a.Gig.Date).ToList();
        }

        public IEnumerable<Attendance> GetAllWithArtistGigCurrent(int gigId)
        {
            return GigHubContext.Attendances.Include(a => a.Artist).Include(a => a.Gig)
                .Where(a => a.Gig.Id == gigId).ToList();
        }

        public ApplicationDbContext GigHubContext
        {
            get { return Context as ApplicationDbContext; }
        }
    }
}