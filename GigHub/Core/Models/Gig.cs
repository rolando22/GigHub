using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GigHub.Core.Models
{
    public class Gig
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Venue Invalided")]
        public string Venue { get; set; }
        [GigDateValidation]
        public DateTime Date { get; set; }
        public ApplicationUser Artist { get; set; }
        [Required(ErrorMessage = "Genre Invalided")]
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}