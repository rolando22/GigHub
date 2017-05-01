﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.Core.Dtos
{
    public class GigDto
    {
        public int Id { get; set; }
        public string Venue { get; set; }
        public DateTime Date { get; set; }
        public ApplicationUserDto Artist { get; set; }
        public GenreDto Genre { get; set; }
    }
}