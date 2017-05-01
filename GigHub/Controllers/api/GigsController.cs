using AutoMapper;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GigHub.Core;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using GigHub.Core.ViewModels;
using GigHub.Persistence;

namespace GigHub.Controllers.api
{
    [Authorize]
    public class GigsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IGigRepository _gigRepository;
        private readonly IInboxRepository _inboxRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly IUserRepository _userRepository;

        public GigsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _attendanceRepository = _unitOfWork.Attendances;
            _gigRepository = _unitOfWork.Gigs;
            _inboxRepository = _unitOfWork.Inboxes;
            _notificationRepository = _unitOfWork.Notifications;
            _userRepository = _unitOfWork.Users;
        }

        public IEnumerable<GigDto> GetGigs()
        {
            return _gigRepository.GetAllMyWithGenreArtist(User.Identity.GetUserId())
                .Select(Mapper.Map<Gig, GigDto>);
        }

        public IHttpActionResult GetGig(int id)
        {
            var gig = _gigRepository.GetWithGenreArtist(id);
            if (gig == null)
                return BadRequest();
            var attendanceForm = new AttendanceFormViewModelDto()
            {
                Id = 0,
                Gig = Mapper.Map<Gig, GigDto>(gig),
                Attendance = false
            };
            var attendance = _attendanceRepository.GetMyAttendance(User.Identity.GetUserId(), gig.Id);
            if (attendance != null)
            {
                attendanceForm.Id = attendance.Id;
                attendanceForm.Attendance = true;
            }
            return Ok(attendanceForm);
        }

        //[HttpPost]
        //public IHttpActionResult CreateGig(GigDto gigDto)
        //{
        //    var gig = Mapper.Map<GigDto, Gig>(gigDto);
        //    gig.Artist = _context.Users.Find(gigDto.Artist.Id);
        //    gig.Genre = _context.Genres.Find(gigDto.Genre.Id);
        //    _context.Gigs.Add(gig);
        //    _context.SaveChanges();
        //    gigDto.Id = gig.Id;
        //    return Created(new Uri(Request.RequestUri + "/" + gig.Id), gigDto);
        //}

        //[HttpPut]
        //public IHttpActionResult UpdateGig(int id, GigDto gigDto)
        //{
        //    var gig = _context.Gigs.Find(id);
        //    if (gig == null)
        //        return BadRequest();
        //    //Mapper.Map(gigDto, gig);
        //    gig.Venue = gigDto.Venue;
        //    gig.Date = gigDto.Date;
        //    gig.Artist = _context.Users.Find(gigDto.Artist.Id);
        //    gig.Genre = _context.Genres.Find(gigDto.Genre.Id);
        //    _context.SaveChanges();
        //    return Ok();
        //}

        [HttpDelete]
        public IHttpActionResult DeleteGig(int id)
        {
            var gig = _gigRepository.Get(id);
            if (gig == null)
                return BadRequest();
            var attendances = _attendanceRepository.GetAllWithArtistGigCurrent(gig.Id);
            if (attendances.Count() != 0)
            {
                var userCurrent = _userRepository.GetUserCurrent(User.Identity.GetUserId());
                var notification = new Notification()
                {
                    Date = DateTime.Today,
                    Information = "A " + userCurrent.ArtisticName + "'s concert was removed",
                    GigDate = gig.Date.ToString(),
                    Delivered = false,
                    Gig = 0
                };
                _notificationRepository.Add(notification);
                _unitOfWork.Complete();
                foreach (var attendance in attendances)
                {
                    _inboxRepository.Add(new Inbox()
                    {
                        User = attendance.Artist,
                        Notification = notification
                    });
                    _attendanceRepository.Remove(attendance);
                }
                notification.Delivered = true;
            }
            _gigRepository.Remove(gig);
            var notifications = _notificationRepository.GetNotificationsGig(gig.Id);
            foreach (var notification in notifications)
            {
                notification.Gig = 0;
            }
            _unitOfWork.Complete();
            return Ok();
        }
    }
}
