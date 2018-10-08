using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly2.Models
{
    //THE MODEL is responsible for managing the data of the application.It receives user input from the controller.

    /*client side assets is defined in bundleConfig.cs 
     * when we combine multiple js css files into a bundle and this way we reduce the number of http request required to get these assets when a page loaded THIS RESULTS IN A FASTER PAGE LOAD.   */
    public class Movie
    {

        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Required]
        public Genre Genre { get; set; }
        public byte GenreId { get; set; }
        public DateTime ReleaseDate { get; set; }
        public byte NumberInStock { get; set; }
    }





}

/*PARTIAL VIEW is a re-usable widget we can use on different pages. */
