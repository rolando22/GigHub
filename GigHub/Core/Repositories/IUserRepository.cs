using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        IEnumerable<ApplicationUser> GetAllWithOutUserCurrent(string userCurrent, string query = null);
        ApplicationUser GetUserCurrent(string userCurrent);
    }
}