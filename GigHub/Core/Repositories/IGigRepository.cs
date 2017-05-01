using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IGigRepository : IRepository<Gig>
    {
        Gig GetWithGenreArtist(int id);
        IEnumerable<Gig> GetAllWithGenreArtist(string userCurrent, string query = null);
        IEnumerable<Gig> GetAllMyWithGenreArtist(string userCurrent);
        IEnumerable<Gig> GetAllFolloweds(IEnumerable<FollowUp> followUps);
        IEnumerable<Gig> GetMyGigs(string user);
        IEnumerable<Gig> GetMyUpcomingGigs(string user, DateTime today);
    }
}
