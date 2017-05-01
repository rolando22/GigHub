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
using GigHub.Persistence;

namespace GigHub.Controllers.api
{
    [Authorize]
    public class FollowUpsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFollowUpRepository _followUpRepository;
        private readonly IGigRepository _gigRepository;
        private readonly IUserRepository _userRepository;

        public FollowUpsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _followUpRepository = _unitOfWork.FollowUps;
            _gigRepository = _unitOfWork.Gigs;
            _userRepository = _unitOfWork.Users;
        }

        public IEnumerable<FollowUpFormViewModelDto> GetUsers()
        {
            var artists = _userRepository.GetAllWithOutUserCurrent(User.Identity.GetUserId())
                .Select(Mapper.Map<ApplicationUser, ApplicationUserDto>);
            var listFollowed = new List<FollowUpFormViewModelDto>();
            if (artists != null)
            {
                var followUps = _followUpRepository.GetAllWithFollowerFollowed(User.Identity.GetUserId())
                    .Select(Mapper.Map<FollowUp, FollowUpDto>);
                foreach (var artist in artists)
                {
                    var followUpForm = new FollowUpFormViewModelDto()
                    {
                        Artist = artist,
                        TotalGigs = _gigRepository.GetMyGigs(artist.Id).Count(),
                        UpcomingGigs = _gigRepository.GetMyUpcomingGigs(artist.Id, DateTime.Today).Count(),
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
            return listFollowed;
        }

        [HttpPost]
        public IHttpActionResult CreateFollowUp(string id)
        {
            var followed = _userRepository.GetUserCurrent(id);
            if (followed == null)
                return BadRequest();
            var followUp = new FollowUp()
            {
                Follower = _userRepository.GetUserCurrent(User.Identity.GetUserId()),
                Followed = followed
            };
            _followUpRepository.Add(followUp);
            _unitOfWork.Complete();
            return Ok(followUp.Id);
        }

        [HttpDelete]
        public IHttpActionResult DeleteFollowUp(int id)
        {
            var followUp = _followUpRepository.GetWithFollowerFollowed(id);
            if (followUp == null)
                return BadRequest();
            var followedId = followUp.Followed.Id;
            _followUpRepository.Remove(followUp);
            _unitOfWork.Complete();
            return Ok(followedId);
        }
    }
}
