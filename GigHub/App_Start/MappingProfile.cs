using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using GigHub.Core.ViewModels;

namespace GigHub.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserDto>();
            CreateMap<Attendance, AttendanceDto>();
            CreateMap<FollowUp, FollowUpDto>();
            CreateMap<Genre, GenreDto>();
            CreateMap<Gig, GigDto>();
            CreateMap<Inbox, InboxDto>();
            CreateMap<Notification, NotificationDto>();
            CreateMap<AttendanceFormViewModel, AttendanceFormViewModelDto>();
            CreateMap<FollowUpFormViewModel, FollowUpFormViewModelDto>();

            CreateMap<ApplicationUserDto, ApplicationUser>().ForMember(a => a.Id, opt => opt.Ignore());
            CreateMap<AttendanceDto, Attendance>().ForMember(a => a.Id, opt => opt.Ignore());
            CreateMap<FollowUpDto, FollowUp>().ForMember(f => f.Id, opt => opt.Ignore());
            CreateMap<GenreDto, Genre>().ForMember(g => g.Id, opt => opt.Ignore());
            CreateMap<GigDto, Gig>().ForMember(g => g.Id, opt => opt.Ignore());
            CreateMap<InboxDto, Inbox>().ForMember(i => i.Id, opt => opt.Ignore());
            CreateMap<NotificationDto, Notification>().ForMember(n => n.Id, opt => opt.Ignore());
            CreateMap<AttendanceFormViewModelDto, AttendanceFormViewModel>().ForMember(a => a.Id, opt => opt.Ignore());
            CreateMap<FollowUpFormViewModelDto, FollowUpFormViewModel>().ForMember(f => f.Id, opt => opt.Ignore());
        }
    }
}