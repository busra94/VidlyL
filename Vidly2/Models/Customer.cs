using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; // for required attribute
using System.Linq;
using System.Web;

namespace Vidly2.Models
{
    public class Customer
    {
        //Overriding default conventions is called DATA ANNOTATIONS (you can search for fluent API)
        
        public int Id { get; set; }
        //[Required(ErrorMessage = "Please enter customer's name")] // error message 
        [Required] //with this attribute our column name will no longer be nullable.
        [StringLength(255)]
        public string Name { get; set; }
        [Min18YearsIfAMember]
        [Display(Name = "Date of Birth")]  // displaying in label Date of Birth
        public DateTime? BirthDate { get; set; }  // DateTime? is a nullable DateTime

        public bool IsSubscribedToNewsletter { get; set; }

        public MembershipType MembershipType { get; set;}

        [Display(Name = "Membership Type")] // displaying in label Membership type.
        public byte MembershipTypeId { get; set; } // this is a convention, entity framework treats as foreign key to that property. 
        /*because MemebershipTypeId type is byte, this property implicitly required, if i wanna to make this optional i use byte? (nullable)
         *Select memebership type does not have a value, and mvc framework cannot find a value for membership type (more accurately value will be an empty string) into form data and does not know how to translate empty string to a byte, so it marks this field invalid. */
        
        /*public MembershipType MembershipType { get; set; } -->
         
        this is what we called a navigation property because it allows us to navigate from one type to another type.
         in this case from customer to it's membership type. These navigation properties are useful when you want 
         to load an object and it's related objects together from the database. For example we can load the customer and it's membership type together. */
        // public byte MembershipTypeId { get; set; } --> framework recognizes this and treats this property as a foreign key.
    }
}