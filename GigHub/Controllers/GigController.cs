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
    public class GigController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GigController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Gig
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            var userCurrent = User.Identity.GetUserId();
            return View("MyUpcomingGigs", _context.Gigs.Include(g => g.Genre).Include(g => g.Artist)
                .Where(g => g.Artist.Id == userCurrent).ToList());
        }

        public ActionResult ListAll()
        {
            var userCurrent = User.Identity.GetUserId();
            var gigs = _context.Gigs.Include(g => g.Artist).Include(g => g.Genre)
                .Where(g => g.Artist.Id != userCurrent)
                .OrderByDescending(g => g.Date).ToList();
            var listAttendances = new List<AttendanceFormViewModel>();
            if (gigs.Count != 0)
            {
                var attendances = _context.Attendances.Include(a => a.Artist).Include(a => a.Gig)
                    .Where(a => a.Artist.Id == userCurrent);
                foreach (var gig in gigs)
                {
                    var attendancesForm = new AttendanceFormViewModel()
                    {
                        Gig = gig,
                        Attendance = false
                    };
                    foreach (var attendance in attendances)
                    {
                        if (gig.Id == attendance.Gig.Id)
                        {
                            attendancesForm.Id = attendance.Id;
                            attendancesForm.Attendance = true;
                        }
                    }
                    listAttendances.Add(attendancesForm);
                }
            }
            return View("GigsList", listAttendances);
        }

        public ActionResult GigFeed()
        {
            return View();
        }

        public ActionResult UpcomingGigs(string id)
        {
            return View(_context.Users.Find(id));
        }

        public ActionResult New()
        {
            return View("GigForm", new GigFormViewModel()
            {
                Gig = new Gig()
                {
                    Artist = new ApplicationUser(),
                    Genre = new Genre(),
                    Date = DateTime.Today
                },
                Genres = _context.Genres.ToList()
            });
        }

        public ActionResult Edit(int id)
        {
            var gig = _context.Gigs.Find(id);
            if (gig == null)
                return HttpNotFound();
            return View("GigForm", new GigFormViewModel()
            {
                Gig = gig,
                Genres = _context.Genres.ToList()
            });
        }

        [ValidateAntiForgeryToken]
        public ActionResult Save(Gig gig)
        {
            if (!ModelState.IsValid)
                return View("GigForm", new GigFormViewModel()
                {
                    Gig = gig,
                    Genres = _context.Genres.ToList()
                });
            var userCurrent = _context.Users.Find(User.Identity.GetUserId());
            if (gig.Id == 0)
            {
                gig.Artist = userCurrent;
                gig.Genre = _context.Genres.Find(gig.Genre.Id);
                _context.Gigs.Add(gig);
                _context.SaveChanges();
                var followUps = _context.FollowUps.Include(f => f.Follower)
                    .Where(f => f.Followed.Id == userCurrent.Id).ToList();
                if (followUps.Count != 0)
                {
                    var notification = new Notification()
                    {
                        Date = DateTime.Today,
                        Information = "A new " + userCurrent.ArtisticName + "'s concert is listed",
                        GigDate = gig.Date.ToString(),
                        Delivered = false,
                        Gig = gig.Id
                    };
                    _context.Notifications.Add(notification);
                    _context.SaveChanges();
                    foreach (var followUp in followUps)
                    {
                        _context.Inboxes.Add(new Inbox()
                        {
                            User = followUp.Follower,
                            Notification = notification
                        });
                    }
                    notification.Delivered = true;
                }
            }
            else
            {
                var gigInDb = _context.Gigs.Find(gig.Id);
                gigInDb.Venue = gig.Venue;
                gigInDb.Date = gig.Date;
                gigInDb.Genre = _context.Genres.Find(gig.Genre.Id);
                var attendances = _context.Attendances.Include(a => a.Gig).Include(a => a.Artist)
                    .Where(a => a.Gig.Id == gigInDb.Id).ToList();
                if (attendances.Count != 0)
                {
                    var notification = new Notification()
                    {
                        Date = DateTime.Today,
                        Information = "A " + userCurrent.ArtisticName + "'s concert was modified",
                        GigDate = gigInDb.Date.ToString(),
                        Delivered = false,
                        Gig = gig.Id
                    };
                    _context.Notifications.Add(notification);
                    _context.SaveChanges();
                    foreach (var attendance in attendances)
                    {
                        _context.Inboxes.Add(new Inbox()
                        {
                            User = attendance.Artist,
                            Notification = notification
                        });
                    }
                    notification.Delivered = true;
                }
            }
            _context.SaveChanges();
            return RedirectToAction("List", "Gig");
        }

        public ActionResult Delete(int id)
        {
            var gig = _context.Gigs.Find(id);
            if (gig == null)
                return HttpNotFound();           
            var attendances = _context.Attendances.Include(a => a.Gig).Include(a => a.Artist)
                .Where(a => a.Gig.Id == gig.Id).ToList();
            if (attendances.Count != 0)
            {
                var userCurrent = _context.Users.Find(User.Identity.GetUserId());
                var notification = new Notification()
                {
                    Date = DateTime.Today,
                    Information = "A " + userCurrent.ArtisticName + "'s concert was removed",
                    GigDate = gig.Date.ToString(),
                    Delivered = false,
                    Gig = 0
                };
                _context.Notifications.Add(notification);
                _context.SaveChanges();
                foreach (var attendance in attendances)
                {
                    _context.Inboxes.Add(new Inbox()
                    {
                        User = attendance.Artist,
                        Notification = notification
                    });
                    _context.Attendances.Remove(attendance);
                }
                notification.Delivered = true;
            }
            _context.Gigs.Remove(gig);
            var notifications = _context.Notifications.Where(n => n.Gig == gig.Id).ToList();
            foreach (var notification in notifications)
            {
                notification.Gig = 0;
            }
            _context.SaveChanges();
            return RedirectToAction("List", "Gig");
        }

        public ActionResult Detail(int id)
        {
            return View(id);
        }
    }
}