﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.Core.Dtos
{
    public class FollowUpDto
    {
        public int Id { get; set; }
        public ApplicationUserDto Follower { get; set; }
        public ApplicationUserDto Followed { get; set; }
    }
}