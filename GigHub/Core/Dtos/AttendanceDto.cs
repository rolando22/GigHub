﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.Core.Dtos
{
    public class AttendanceDto
    {
        public int Id { get; set; }
        public ApplicationUserDto Artist { get; set; }
        public GigDto Gig { get; set; }
    }
}