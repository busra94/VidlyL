using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly2.Models;
using Vidly2.ViewModels;

namespace Vidly2.Controllers
{
    //THE CONTROLLER responds to the user input and performs interactions on the data model objects. The controller receives the input, optionally validates it and then passes the input to the model.
    public class MoviesController : Controller
    {
        //movies/random (controller -> movies action-> random) 
        // GET: Movies /Random when we go to movies/random this method will be called 
        /*We can set return type ViewResult but if an action have different execution paths and return different action resuts(types), in that case we set to return type ActionResult so we can 
         * return any of ActionResult subtypes.  */
        public ActionResult Random() // THIS METHOD AND VIEW NAME MUST BE SAME.
        {
            var movies = new List<Movie>
            {
                new Movie {Name = "Shrek"},
                new Movie {Name = "Wall-e"}
            };


            var customers = new List<Customer>
            {
                new Customer {Name = "Customer1"},
                new Customer {Name = "Customer2"}
            };

            var viewModel = new RandomMovieViewModel
            {
                Movies = movies,
                Customers = customers
            };
            return View(viewModel);

            /* var viewResult = new ViewResult();
             viewResult.ViewData.Model = model;
             return viewResult;
             
            instead of this we use return View(movie) this statement handle this.
            
            viewResult.ViewData.Model = model;
             in here ViewData is ViewDataDictionary -> we can use as dictionary (key-value pairs)
             or use it model property to work with an object*/


            // return View(movie);
            /* we can pass data to view 2 another ways:
             
             1. ViewData["Movie"] = movie;
             return View();
             
             2. ViewBag.Movie = movie;
             if we change Movie property to RandomMovie it wont change in view*/

            //return new ViewResult();
        }

        public ActionResult Edit(int id)
        {
            //Content does not take integer parameter.
            return Content("id = " + id); // when we use + operator int to be converted to string.
        }

        //  movies, Listing movies in the database. 
        // to make parameter optional we make it nullable -> we add ? symbol
        // string is reference type so it's nullable. 

        //public ActionResult Index(int? pageIndex, string sortBy)
        //{
        //    if (!pageIndex.HasValue)
        //        pageIndex = 1;

        //    if (String.IsNullOrWhiteSpace(sortBy))
        //        sortBy = "Name";
        //    return Content(String.Format("pageIndex={0}&sortBy={1}", pageIndex, sortBy));
        //}

        public IEnumerable<Movie> GetMovies()
        {
            var movies = new List<Movie>
            {
                new Movie{Name = "Shrek" },

                new Movie{Name = "Wall-e" }
            };
            return movies;
        }
        public ActionResult Index()
        {
            var movies = GetMovies();
            return View(movies);
        }



        [Route("movies/released/{year}/{month:regex(\\d{2}):range(1, 12)}")]  // in regex there is no string so we don't use @ sign instead of we use \\. 
        public ActionResult ByReleaseDate(int year, int month)
        {

            return Content(year + "/" + month);
        }



    }
}

/*VIEW MODEL IS A MODEL AND VIEWMODEL IS SPECIFICALLY BUILT FOR A VIEW
 IT INCLUDES ANY  DATA AND RULES SPECIFIC TO THAT VIEW   */
