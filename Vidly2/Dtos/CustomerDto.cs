using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly2.Models;

namespace Vidly2.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }
        
        [Required] 
        [StringLength(255)]
        public string Name { get; set; }

      //  [Min18YearsIfAMember]    
       public DateTime? BirthDate { get; set; }  

        public MembershipTypeDto MembershipType { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }

        /*we remove MembershipType property because this property is creating dependency from our DTO to 
         * our domain model. if we need to return hierarchical data strustres we would create another type 
         * called Membership type Dto*/
               
        public byte MembershipTypeId { get; set; }  
    }
}