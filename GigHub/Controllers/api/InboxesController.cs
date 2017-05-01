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
using GigHub.Core.ViewModels;
using GigHub.Core.Repositories;

namespace GigHub.Controllers.api
{
    [Authorize]
    public class InboxesController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInboxRepository _inboxReposiroty;

        public InboxesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _inboxReposiroty = _unitOfWork.Inboxes;
        }

        public IEnumerable<InboxDto> GetInboxes()
        {
            var inboxes = _inboxReposiroty.GetAllUserCurrent(User.Identity.GetUserId());
            var inboxesDto = new List<InboxDto>();
            foreach (var inbox in inboxes)
            {
                inboxesDto.Add(Mapper.Map<Inbox, InboxDto>(inbox));
                inbox.Viewed = true;
            }
            _unitOfWork.Complete();
            return inboxesDto;
        }

        [Route("api/inboxes/notviews")]
        public IEnumerable<InboxDto> GetNotVieweds()
        {
            return _inboxReposiroty.GetViewed(User.Identity.GetUserId(), false).Select(Mapper.Map<Inbox, InboxDto>);
        }
    }
}
