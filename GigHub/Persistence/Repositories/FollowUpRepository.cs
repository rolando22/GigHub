using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using GigHub.Core.Models;
using GigHub.Core.Repositories;

namespace GigHub.Persistence.Repositories
{
    public class FollowUpRepository : Repository<FollowUp>, IFollowUpRepository
    {
        public FollowUpRepository(DbContext context) : base(context)
        {
        }

        public FollowUp GetWithFollowerFollowed(int id)
        {
            return GigHubContext.FollowUps.Include(f => f.Followed).First(f => f.Id.Equals(id));
        }

        public IEnumerable<FollowUp> GetAllWithFollowerFollowed(string userCurrent)
        {
            return GigHubContext.FollowUps.Include(f => f.Follower).Include(f => f.Followed)
                .Where(f => f.Follower.Id == userCurrent).ToList();
        }

        public ApplicationDbContext GigHubContext
        {
            get { return Context as ApplicationDbContext; }
        }
    }
}