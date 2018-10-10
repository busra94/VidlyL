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
        //[Required]  //we removed this annotation because we don't add genre to movie table, with this annotation we have to add this to table.
        public Genre Genre { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public byte GenreId { get; set; }

        [Required]
        [Display(Name = "Release Date")]     
        public DateTime ReleaseDate { get; set; }

        [Required]
        [Display(Name = "Number In Stock")]
        [Range(1,20)]
        public byte NumberInStock { get; set; }
    }





}

/*PARTIAL VIEW is a re-usable widget we can use on different pages. */
