using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using GigHub.Core.Models;
using GigHub.Persistence;

namespace GigHub.Controllers
{
    [Authorize]
    public class InboxController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InboxController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Inbox
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            var userCurrent = User.Identity.GetUserId();
            return View("Inbox", _context.Inboxes.Include(i => i.Notification)
                .Where(i => i.User.Id == userCurrent).OrderByDescending(i => i.Id).ToList());
        }
    }
}