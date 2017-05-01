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
    public class UsersController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFollowUpRepository _followUpRepository;
        private readonly IGigRepository _gigRepository;
        private readonly IUserRepository _userRepository;

        public UsersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _followUpRepository = _unitOfWork.FollowUps;
            _gigRepository = _unitOfWork.Gigs;
            _userRepository = _unitOfWork.Users;
        }

        public IEnumerable<ApplicationUserDto> GetUsers(string query = null)
        {
            return _userRepository.GetAllWithOutUserCurrent(User.Identity.GetUserId(), query)
                .Select(Mapper.Map<ApplicationUser, ApplicationUserDto>);
        }
    }
}
