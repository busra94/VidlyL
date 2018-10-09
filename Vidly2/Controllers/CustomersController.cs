using System;
using System.Collections.Generic;
using System.Data.Entity; // Include method's library.
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly2.Models;
using Vidly2.ViewModels;

namespace Vidly2.Controllers
{
    public class CustomersController : Controller
    {

        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }
        //DbContext object is a disposable object, so we need to sispose it properly 

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();

        }
        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new NewCustomerViewModel
            {
                MembershipTypes = membershipTypes
            };
            return View(viewModel);  // future we implement editing a customer, so we need to pass a customer object to this view in that time because of this we create view model 
        }

        [HttpPost] // with this attribute, we make sure this action only be called using HttpPost and not HttpGet.   
        // AS A BEST PRACTICE if your actions modify data they should never be accessible by a HttpGet
        public ActionResult Create(Customer customer)
        {
            /* Because the model behind our view is of type NewCustomerViewModel we pass this parameter to Create action MVC framework will automatically map 
            request data to this object THIS IS CALLED MODEL BINDING. IF WE CHANGE OUR PARAMETER NewCustomerViewModel to Customer, MVC framework will understand that because in view all parameters prefixed with 'Customer'. */
            /*MVC framework binds this data to the request data. When reqest goes to our application MVC framework will use (form) properties to initialize to parameter ot our action. */
            //TO ADD CUSTOMER TO DATABASE first we add it to DbContext(database gateway) Customers => table. DbContext ha a change tracking mechanism when we make changes in datas it will mark them deleted added or modified.
            _context.Customers.Add(customer);
            _context.SaveChanges(); /* we make to persist changes. 
            DbContext goes through all modified objects and based on the kind of modification it will generate SQL statement at runtime and it will run them on the database all these statements wrapped in a transaction so either all changes get persisted together or nothing will get persisted. */

            return RedirectToAction("Index","Customers"); // redirect to index action in customers controller
        }

        // GET: Customers
        public ViewResult Index()
        {
            /* when executed in below statement entity framework will not query the database. this is called DEFERRED EXECTION.
             Queries executed when we iterate over this customers(var customers) object. We can immediately execute this query by calling the ToList() method */
            var customers = _context.Customers.Include(c => c.MembershipType).ToList(); // this Customers property is a DbSet defined in our DBContext, we can get all customers in the database.
            return View(customers);

        }
        //public IEnumerable<Customer> GetCustomers()
        //{
        //    var customers = new List<Customer>
        //    {
        //        new Customer {Id = 1,
        //            Name = "John Smith" },

        //        new Customer{Id = 2,
        //            Name = "Mary Williams" }
        //    };
        //    return customers;
        //} 


        //[Route("customers/details/{id}")]

        public ActionResult Details(int id)
        {
            /* SingleOrDefault() returns record if there is some record otherwise throws exception
             * FirstOrDefault()  returns record if there is some record otherwise returns null. */
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);//Take membership types and in with these id's compare customers table membershipId when find them equal return just that MembershipType.

            if (customer == null)
            {
                return HttpNotFound(); // 404 error
            }
            return View(customer);

        }

    }
}

// when we add some records to tables directly, -because we wouldnt use migration, only migrations deployed- records won't be deployed to target database