using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly2.Models
{
    //THE MODEL is responsible for managing the data of the application.It receives user input from the controller.

    /*client side assets is defined in bundleConfig.cs 
     * when we combine multiple js css files into a bundle and this way we reduce the number of http request required to get these assets when a page loaded THIS RESULTS IN A FASTER PAGE LOAD.   */
    public class Movie
    {
        public int id { get; set; }
        public string Name { get; set; }








    }
}

/*PARTIAL VIEW is a re-usable widget we can use on different pages. */
