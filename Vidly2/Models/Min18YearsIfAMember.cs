using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly2.Models
{
    public class Min18YearsIfAMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            /*check selected membership type and if is pay as you go there is no necessary to check birthdate valid 
             * is success otherwise chack birthdate.
            */
            //ObjectInstance gives us access to the containing class. Because this is a customer we need to cast it to Customer
            var customer = (Customer) validationContext.ObjectInstance;
            if (customer.MembershipTypeId == 0 || customer.MembershipTypeId == 1)  // pay as you go id is 1, later we change this code to make more maintainable. 0 is value of select membership type (mean is membership type does not selected.) 0 is the default value of numeric properties.
                return ValidationResult.Success; // Success is a static field on the ValidationResult class. , we won't initalizethis field!
            if (customer.BirthDate == null)
                return new ValidationResult("Birthdate is required"); // to indicate an error we instantiate a new validation result.

            var age = DateTime.Today.Year - customer.BirthDate.Value.Year; // because datetime is nullable we use .Year

            return (age >=  18) 
                ? ValidationResult.Success 
                : new ValidationResult("Customer should at least 18 years old to go on a membership"); 
        }

    }
}