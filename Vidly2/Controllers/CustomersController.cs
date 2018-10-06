using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly2.Models;

namespace Vidly2.Controllers
{
    public class CustomersController : Controller
    {

        // GET: Customers

        public ViewResult Index()
        {
            var customers = GetCustomers();
            return View(customers);

        }
        public IEnumerable<Customer> GetCustomers()
        {
            var customers = new List<Customer>
            {
                new Customer {Id = 1,
                    Name = "John Smith" },

                new Customer{Id = 2,
                    Name = "Mary Williams" }
            };
            return customers;
        }


        //[Route("customers/details/{id}")]

        public ActionResult Details(int id)  // !!! int? 
        {
            /* SingleOrDefault() returns record if there is some record otherwise throws exception
             * FirstOrDefault()  returns record if there is some record otherwise returns null. */
            var customer = GetCustomers().SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return HttpNotFound(); // 404 error
            }
            return View(customer);



        }
    }
}