using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly2.Dtos;

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



            //Customer customer = new Customer(); DERS 70 SON SORUDA 
            //if (validationContext.ObjectType == typeof(Customer))
            //    customer = (Customer)validationContext.ObjectInstance;
            //else
            //    customer = Mapper.Map((CustomerDto)validationContext.ObjectInstance, customer);

            //Customer customer = new Customer();
            //CustomerDto customerDto = new CustomerDto();

            //if (validationContext.ObjectType == typeof(Customer))
            //    customer = (Customer)validationContext.ObjectInstance;
            //else
            //{
            //    //customerDto.Name = customer.Name;
            //    //customerDto.BirthDate = customer.BirthDate;
            //    //customerDto.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            //    //customerDto.MembershipTypeId = customer.MembershipTypeId;
                
            ////customerDto =(Customer) validationContext.ObjectInstance;
               
            //}

               
            if (customer.MembershipTypeId == MembershipType.Unknown || customer.MembershipTypeId == MembershipType.PayAsYouGo)  // pay as you go id is 1, later we change this code to make more maintainable. 0 is value of select membership type (mean is membership type does not selected.) 0 is the default value of numeric properties.
                // to make 0 and 1 values maintainable we create two read-only fields in MembershipType class.
                return ValidationResult.Success; // Success is a static field on the ValidationResult class. , we won't initalize this field!
            if (customer.BirthDate == null)
                return new ValidationResult("Birthdate is required"); // to indicate an error we instantiate a new validation result.

            var age = DateTime.Today.Year - customer.BirthDate.Value.Year; // because datetime is nullable we use .Year

            return (age >=  18) 
                ? ValidationResult.Success 
                : new ValidationResult("Customer should at least 18 years old to go on a membership"); 
        }

    }
}