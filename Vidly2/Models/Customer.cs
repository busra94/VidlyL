﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly2.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
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