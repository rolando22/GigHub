using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using GigHub.Core.Models;
using GigHub.Core.Repositories;

namespace GigHub.Persistence.Repositories
{
    public class GigRepository : Repository<Gig>, IGigRepository
    {
        public GigRepository(DbContext context) : base(context)
        {
        }

        public Gig GetWithGenreArtist(int id)
        {
            return GigHubContext.Gigs.Include(g => g.Genre).Include(g => g.Artist).First(g => g.Id.Equals(id));
        }

        public IEnumerable<Gig> GetAllWithGenreArtist(string userCurrent, string query = null)
        {
            var gigQuery = GigHubContext.Gigs.Include(g => g.Genre).Include(g => g.Artist)
                .Where(g => g.Artist.Id != userCurrent);
            if (!string.IsNullOrWhiteSpace(query))
                gigQuery = gigQuery.Where(g => g.Artist.ArtisticName.Contains(query));
            return gigQuery.OrderByDescending(g => g.Id).ToList();
        }

        public IEnumerable<Gig> GetAllMyWithGenreArtist(string user)
        {
            return GigHubContext.Gigs.Include(g => g.Genre).Include(g => g.Artist)
                .Where(g => g.Artist.Id == user).ToList();
        }

        public IEnumerable<Gig> GetAllFolloweds(IEnumerable<FollowUp> followUps)
        {
            var gigs = new List<Gig>();
            foreach (var followUp in followUps)
            {
                gigs.AddRange(GigHubContext.Gigs.Include(g => g.Genre).Include(g => g.Artist)
                    .Where(g => g.Artist.Id == followUp.Followed.Id).ToList());
            }
            return gigs;
        }

        public IEnumerable<Gig> GetMyGigs(string user)
        {
            return GigHubContext.Gigs.Include(g => g.Genre).Include(g => g.Artist)
                .Where(g => g.Artist.Id == user).ToList();
        }

        public IEnumerable<Gig> GetMyUpcomingGigs(string user, DateTime today)
        {
            return GigHubContext.Gigs.Include(g => g.Genre).Include(g => g.Artist)
                .Where(g => g.Artist.Id == user).Where(g => g.Date >= today).OrderByDescending(g => g.Date).ToList();
        }

        public ApplicationDbContext GigHubContext
        {
            get { return Context as ApplicationDbContext; }
        }
    }
}