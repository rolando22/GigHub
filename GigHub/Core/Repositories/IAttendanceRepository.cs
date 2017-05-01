using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IAttendanceRepository : IRepository<Attendance>
    {
        Attendance GetWithArtistGig(int id);
        Attendance GetMyAttendance(string userCurrent, int gigId);
        IEnumerable<Attendance> GetAllWithArtistGig(string userCurrent);
        IEnumerable<Attendance> GetAllWithArtistGigCurrent(int gigId);
    }
}