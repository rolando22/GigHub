using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GigHub.Core.Models
{
    public class GigDateValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var gig = (Gig)validationContext.ObjectInstance;
            if (gig.Date == null)
                return new ValidationResult("Date is required.");
            var comparteTo = gig.Date.CompareTo(DateTime.Today);
            return (comparteTo >= 0) 
                ? ValidationResult.Success 
                : new ValidationResult("Gig's date should be higher to current date");
        }
    }
}