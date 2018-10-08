using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly2.Models
{
    public class MembershipType
    {
        // Id prop-> every entity must have a key which will be mapped to the primary key of the corresponding table in the database.
        // id is a conventional name.
        public byte Id { get; set; } //byte -> again byte, because we have only a few mwmbership types.
        public short SignUpFee { get; set; }  // we use 'short' type because we don't need any values more than 32000
        public byte DurationInMonths { get; set; } // we use byte here because the largest number we wanna store is 12 for 12 monts 
        public byte DiscountRate { get; set; } // we use byte again for discount rate because this going to be percentage between 0-100 
        [Required]
        public string Name { get; set; }
    
}

}

/* ALL CHANGES WE MAKE ON DATABASE STORED IN MIGRATIONS, SO IF WE  WANNA DEPLOY OUR DATABASE WE CAN GET
ALL MIGRATIONS AND USING A COMMAND IN PACKAGE MANAGER CONSOLE WE ASK TO ENTITY FRAMEWORK TO GENERATE A SQL SCRIPT
WHICH WOULD INCLUDE ALL THE CHANGES AND THEN RUN ON THE PRODUCTION DATABASE.

 AND WE CAN (FIND) MIGRATION AND CREATE SCRIPT FROM THAT MIGRATION   */

//    using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

