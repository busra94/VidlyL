using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
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
        public ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ViewResult Index()
        {
            var movies = _context.Movies.Include(m => m.Genre).ToList();
            return View(movies);
        }

        public ViewResult New()
        {
            var viewModel = new MovieFormViewModel
            {
                Genres = _context.Genres.ToList()
            };
            return View("MovieForm", viewModel);

        }

        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return HttpNotFound();
            }

            var viewModel = new MovieFormViewModel
            {
                Movie = movie,
                Genres = _context.Genres.ToList()
            };


            return View("MovieForm", viewModel);
            ////Content does not take integer parameter.
            //return Content("id = " + id); // when we use + operator int to be converted to string.
        }
        [HttpPost]
        public ActionResult Save(Movie movie)
        {

            if (movie.Id == 0) // add movie
            {
                _context.Movies.Add(movie);
            }
            else // added database to form values ? 
            {
                var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.GenreId = movie.GenreId;
            }
            _context.SaveChanges();


            return RedirectToAction("Index", "Movies");
        }
        //public ActionResult Random() // THIS METHOD(ACTION) AND VIEW NAME MUST BE SAME.
        //{
        //    var movies = new List<Movie>
        //    {
        //        new Movie {Name = "Shrek"},
        //        new Movie {Name = "Wall-e"}
        //    };


        //    var customers = new List<Customer>
        //    {
        //        new Customer {Name = "Customer1"},
        //        new Customer {Name = "Customer2"}
        //    };

        //    var viewModel = new MovieFormViewModel
        //    {
        //        Movies = movies,
        //        Customers = customers
        //    };
        //    return View(viewModel);

        //    /* var viewResult = new ViewResult();
        //     viewResult.ViewData.Model = model;
        //     return viewResult;

        //    instead of this we use return View(movie) this statement handle this.

        //    viewResult.ViewData.Model = model;
        //     in here ViewData is ViewDataDictionary -> we can use as dictionary (key-value pairs)
        //     or use it model property to work with an object*/


        //    // return View(movie);
        //    /* we can pass data to view 2 another ways:

        //     1. ViewData["Movie"] = movie;
        //     return View();

        //     2. ViewBag.Movie = movie;
        //     if we change Movie property to RandomMovie it wont change in view*/

        //    //return new ViewResult();
        //}


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

        public ViewResult MovieDetails(int id)
        {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);
            return View(movie);

        }

        [Route("movies/released/{year}/{month:regex(\\d{2}):range(1, 12)}")]  // in regex there is no string so we don't use @ sign instead of we use \\. 
        public ActionResult ByReleaseDate(int year, int month)
        {

            return Content(year + "/" + month);
        }
     
    }
}

///*VIEW MODEL IS A MODEL AND VIEWMODEL IS SPECIFICALLY BUILT FOR A VIEW
// IT INCLUDES ANY  DATA AND RULES SPECIFIC TO THAT VIEW   */

///*  */
