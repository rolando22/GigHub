using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using GigHub.Core.Models;
using GigHub.Core.ViewModels;
using GigHub.Persistence;

namespace GigHub.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            var userCurrent = User.Identity.GetUserId();
            var artists = _context.Users.Where(a => a.Id != userCurrent).OrderBy(a => a.ArtisticName).ToList();                    
            var listFollowed = new List<FollowUpFormViewModel>();
            if (artists != null)
            {
                var followUps = _context.FollowUps.Include(f => f.Follower).Include(f => f.Followed)
                    .Where(f => f.Follower.Id == userCurrent).ToList();
                foreach (var artist in artists)
                {
                    var followUpForm = new FollowUpFormViewModel()
                    {
                        Artist = artist,
                        TotalGigs = _context.Gigs.Where(g => g.Artist.Id == artist.Id).Count(),
                        UpcomingGigs = _context.Gigs.Where(g => g.Artist.Id == artist.Id)
                        .Where(g => g.Date >= DateTime.Now).Count(),
                        Follow = false
                    };
                    foreach (var followUp in followUps)
                    {
                        if (followUp.Followed.Id == artist.Id)
                        {
                            followUpForm.Id = followUp.Id;
                            followUpForm.Follow = true;
                        }
                    }
                    listFollowed.Add(followUpForm);
                }
            }
            return View("UsersList", listFollowed);
        }

        public ActionResult Follow(string followedId)
        {
            var followed = _context.Users.Find(followedId);
            if (followed == null)
                return HttpNotFound();
            _context.FollowUps.Add(new FollowUp()
            {
                Follower = _context.Users.Find(User.Identity.GetUserId()),
                Followed = followed
            });
            _context.SaveChanges();
            return RedirectToAction("List", "User");
        }

        public ActionResult Unfollow(int followUpId)
        {
            var followUp = _context.FollowUps.Find(followUpId);
            if (followUp == null)
                return HttpNotFound();
            _context.FollowUps.Remove(followUp);
            _context.SaveChanges();
            return RedirectToAction("List", "User");
        }

        public ActionResult Attendance(int gigId)
        {
            var gigInDb = _context.Gigs.Find(gigId);
            if (gigInDb == null)
                return HttpNotFound();
            _context.Attendances.Add(new Attendance()
            {
                Artist = _context.Users.Find(User.Identity.GetUserId()),
                Gig = gigInDb
            });
            _context.SaveChanges();
            return RedirectToAction("ListAll", "Gig");
        }

        public ActionResult Unattendance(int attendanceId)
        {
            var attendance = _context.Attendances.Find(attendanceId);
            if (attendance == null)
                return HttpNotFound();
            _context.Attendances.Remove(attendance);
            _context.SaveChanges();
            return RedirectToAction("ListAll", "Gig");
        }
    }
}