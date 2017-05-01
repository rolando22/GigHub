using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IFollowUpRepository : IRepository<FollowUp>
    {
        FollowUp GetWithFollowerFollowed(int id);
        IEnumerable<FollowUp> GetAllWithFollowerFollowed(string userCurrent);
    }
}