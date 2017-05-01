using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using GigHub.Core.Models;
using GigHub.Core.Repositories;

namespace GigHub.Persistence.Repositories
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<ApplicationUser> GetAllWithOutUserCurrent(string userCurrent, string query = null)
        {
            var users = GigHubContext.Users.Where(u => u.Id != userCurrent).OrderBy(u => u.ArtisticName).ToList();
            if (!string.IsNullOrWhiteSpace(query))
            {
                users = users.Where(u => u.ArtisticName.ToUpper().Contains(query.ToUpper())).ToList();
            }
            return users;
        }

        public ApplicationUser GetUserCurrent(string userCurrent)
        {
            return GigHubContext.Users.Find(userCurrent);
        }

        public ApplicationDbContext GigHubContext
        {
            get { return Context as ApplicationDbContext; }
        }
    }
}