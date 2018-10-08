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
        [Required] //with this attribute our column name will no longer be nullable.
        [StringLength(255)]
        public string Name { get; set; }

        public DateTime? BirthDate { get; set; }  // DateTime? is a nullable DateTime
        public bool IsSubscribedToNewsletter { get; set; }
        public MembershipType MembershipType { get; set; }
        public byte MembershipTypeId { get; set; } // this is a convention, entity framework treats as foreign key to that property. 
        
        
        /*public MembershipType MembershipType { get; set; } -->
         
        this is what we called a navigation property because it allows us to navigate from one type to another type.
         in this case from customer to it's membership type. These navigation properties are useful when you want 
         to load an object and it's related objects together from the database. For example we can load the customer and it's membership type together. */
        // public byte MembershipTypeId { get; set; } --> framework recognizes this and treats this property as a foreign key.
    }
}