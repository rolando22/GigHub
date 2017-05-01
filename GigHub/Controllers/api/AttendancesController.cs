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

namespace GigHub.Controllers.api
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IFollowUpRepository _followUpRepository;
        private readonly IGigRepository _gigRepository;
        private readonly IUserRepository _userRepository;

        public AttendancesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _attendanceRepository = _unitOfWork.Attendances;
            _followUpRepository = _unitOfWork.FollowUps;
            _gigRepository = _unitOfWork.Gigs;
            _userRepository = _unitOfWork.Users;
        }

        public IEnumerable<AttendanceFormViewModelDto> GetAttendances()
        {
            var userCurrent = User.Identity.GetUserId();
            var gigs = _gigRepository.GetAllWithGenreArtist(userCurrent).Select(Mapper.Map<Gig, GigDto>);
            var listAttendances = new List<AttendanceFormViewModelDto>();
            if (gigs.Count() != 0)
            {
                var attendances = _attendanceRepository.GetAllWithArtistGig(userCurrent)
                    .Select(Mapper.Map<Attendance, AttendanceDto>);
                foreach (var gig in gigs)
                {
                    var attendancesForm = new AttendanceFormViewModelDto()
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
            return listAttendances;
        }

        [Route("api/attendances/myattendances")]
        public IEnumerable<AttendanceFormViewModelDto> GetMyAttendances()
        {
            var userCurrent = User.Identity.GetUserId();
            var gigs = _gigRepository.GetAllFolloweds(_followUpRepository
                .GetAllWithFollowerFollowed(userCurrent)).Select(Mapper.Map<Gig, GigDto>);
            var listAttendances = new List<AttendanceFormViewModelDto>();
            if (gigs.Count() != 0)
            {
                var attendances = _attendanceRepository.GetAllWithArtistGig(userCurrent)
                    .Select(Mapper.Map<Attendance, AttendanceDto>);
                foreach (var gig in gigs)
                {
                    var attendancesForm = new AttendanceFormViewModelDto()
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
            return listAttendances;
        }

        [Route("api/attendances/myattendances/{id}")]
        public IEnumerable<AttendanceFormViewModelDto> GetMyAttendances(string id)
        {
            var userCurrent = User.Identity.GetUserId();
            var gigs = _gigRepository.GetMyUpcomingGigs(id, DateTime.Today).Select(Mapper.Map<Gig, GigDto>);
            var listAttendances = new List<AttendanceFormViewModelDto>();
            if (gigs.Count() != 0)
            {
                var attendances = _attendanceRepository.GetAllWithArtistGig(userCurrent)
                    .Select(Mapper.Map<Attendance, AttendanceDto>);
                foreach (var gig in gigs)
                {
                    var attendancesForm = new AttendanceFormViewModelDto()
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
            return listAttendances.OrderByDescending(vm => vm.Gig.Date);
        }

        [HttpPost]
        public IHttpActionResult CreateAttendance(int id)
        {
            var gig = _gigRepository.Get(id);
            if (gig == null)
                return BadRequest();
            var attendance = new Attendance()
            {
                Artist = _userRepository.GetUserCurrent(User.Identity.GetUserId()),
                Gig = gig
            };
            _attendanceRepository.Add(attendance);
            _unitOfWork.Complete();
            return Ok(attendance.Id);
        }

        [HttpDelete]
        public IHttpActionResult DeleteAttendance(int id)
        {
            var attendance = _attendanceRepository.GetWithArtistGig(id);
            if (attendance == null)
                return BadRequest();
            var gigId = attendance.Gig.Id;
            _attendanceRepository.Remove(attendance);
            _unitOfWork.Complete();
            return Ok(gigId);
        }
    }
}
