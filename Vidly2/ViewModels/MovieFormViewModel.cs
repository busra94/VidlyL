using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly2.Models;

namespace Vidly2.ViewModels
{
    public class MovieFormViewModel
    {
        
        public IEnumerable<Genre> Genres { get; set; }  // because we have GenreId in movies table we use this. 

        public int? Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        //[Required]  //we removed this annotation because we don't add genre to movie table, with this annotation we have to add this to table.

        [Required]
        [Display(Name = "Genre")]
        public byte? GenreId { get; set; }

        [Required]
        [Display(Name = "Release Date")]
        public DateTime? ReleaseDate { get; set; }

        [Required]
        [Display(Name = "Number In Stock")]
        [Range(1, 20)]
        public byte? NumberInStock { get; set; }

        public string Title // with this we can change title edit or new movie of the form
        {
            get
            {
                return Id != 0 ? "Edit Movie"  : "New Movie";
            }
        }

        public MovieFormViewModel()
        {
            Id = 0; // for the new movie so we make sure hidden field of the movie is populated 
        }
        public MovieFormViewModel(Movie movie)
        {
            Id = movie.Id;
            Name = movie.Name;
            ReleaseDate = movie.ReleaseDate;
            NumberInStock = movie.NumberInStock;
            GenreId = movie.GenreId;
        }
    }
}