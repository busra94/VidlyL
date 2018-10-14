using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly2.Dtos;
using Vidly2.Models;

namespace Vidly2.Controllers.Api
{
    public class NewRentalsController : ApiController
    {
        private ApplicationDbContext _context;

        public NewRentalsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult CreateNewRentals(NewRentalDto newRentalDto)
        {
            //if (newRentalDto.MovieIds.Count == 0)
            //    return BadRequest("No MoviIds have been given!");

            //var customer = _context.Customers.SingleOrDefault(c => c.Id == newRentalDto.CustomerId);
            var customer = _context.Customers.Single(c => c.Id == newRentalDto.CustomerId);

            //if (customer == null)
            //    return BadRequest("CustomerId is invalid!");

            var movies = _context.Movies.Where(m => newRentalDto.MovieIds.Contains(m.Id)).ToList(); // in newRentalDto get MovisIds and get all movies that matches with movies table id's 

            //if (movies.Count != newRentalDto.MovieIds.Count)
            //    return BadRequest("One or more MovieIds invalid!");          
                      
            foreach(var movie in movies)
            {
                if (movie.NumberAvailable == 0)
                    return BadRequest("Movie is not available");
                if (movie.NumberInStock == 0)
                {
                    return BadRequest("This movie is not available now, sorry for that!");
                }

                movie.NumberAvailable--;
                var rental = new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now
                };

                _context.Rentals.Add(rental);
                _context.SaveChanges();

            }
            return Ok();

        }
        
                  

        }
    }

