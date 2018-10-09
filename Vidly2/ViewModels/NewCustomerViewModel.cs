using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly2.Models;

namespace Vidly2.ViewModels
{
    /* we can use List<MembershipType> or IEnumerable<MembershipType>(this is an interface), we dont need any functionality provided by the list class(editing adding,removing,accessing an object by index), 
     * we need just list of membership type. In the future if we want to replace list with another collection we do not need come back here and modify this view model AS LONG AS collection implements I|Enumerable.*/
    public class NewCustomerViewModel
    {
        public IEnumerable<MembershipType> MembershipTypes { get; set; } 
        public Customer Customer { get; set;} // we need properties of our new customer
        /*we can either use customer type here or explicitly add it's properties like Name Birtdate ad so on. which approach should be use? it depends with our scenario.
         * Sometimes in large complex applications how you present an entity in your view can be different from how it's defined in the domain model of your application. 
         * In those situation if you want to use existing entity you may end up pollutin that entity by adding additional properties which are only required by a view.
         In that case we need to separate that domain and view moodels and let each model evolve independently.
         But in our case, we don't need this*/

    }
}