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
            var viewModel = new CustomerFormViewModel
            {
                MembershipTypes = membershipTypes
            };
            return View("CustomerForm", viewModel);  // future we implement editing a customer, so we need to pass a customer object to this view in that time because of this we create view model 
        }

        [HttpPost] // with this attribute, we make sure this action only be called using HttpPost and not HttpGet.   
        // AS A BEST PRACTICE if your actions modify data they should never be accessible by a HttpGet
        public ActionResult Save(Customer customer)
        {
            /*MVC also uses data annotations to validate action parameters, example, in customer controller in Save action we have Customer object as a parameter. when asp.net MVC populates this costumer object
       * using requests data it checks to see if this object is valid based on the data annotations applied on various properties of this customer class.*/

            if (!ModelState.IsValid) // accessing to validation data.
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()

                };
                return View("CustomerForm", viewModel);
            }

            if (customer.Id == 0) // condition means is this is a new customer so we should add to the database. otherwise we should update it.
            {
                /* Because the model behind our view is of type NewCustomerViewModel we pass this parameter to Create action MVC framework will automatically map 
                request data to this object THIS IS CALLED MODEL BINDING. IF WE CHANGE OUR PARAMETER NewCustomerViewModel to Customer, MVC framework will understand that because in view all parameters prefixed with 'Customer'. */
                /*MVC framework binds this data to the request data. When reqest goes to our application MVC framework will use (form) properties to initialize to parameter ot our action. */
                //TO ADD CUSTOMER TO DATABASE first we add it to DbContext(database gateway) Customers => table. DbContext ha a change tracking mechanism when we make changes in datas it will mark them deleted added or modified.
                _context.Customers.Add(customer);
            }
            else
            {
                //Mapper.Map(customer, customerInDb) => library AutoMap function. it looks at same properties and updates them, first looks at customer. 
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id); // we use Single method because when customer is not found it throws an exception  we don't wanna handle this exception, because this action should only be called as a result of posting our customer form.   
                customerInDb.Name = customer.Name;
                customerInDb.BirthDate = customer.BirthDate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;

            }
            _context.SaveChanges(); /* we make to persist changes. 
            DbContext goes through all modified objects and based on the kind of modification it will generate SQL statement at runtime and it will run them on the database all these statements wrapped in a transaction so either all changes get persisted together or nothing will get persisted. */
            return RedirectToAction("Index", "Customers"); // redirect to index action in customers controller
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

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            var viewModel = new CustomerFormViewModel // The model behind this view is CustomerFormViewModel 
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };
            return View("CustomerForm", viewModel); // if we use View() MVC framework look for Edit view
        }

    }
}

// when we add some records to tables directly, -because we wouldnt use migration, only migrations deployed- records won't be deployed to target database

    /*ADD VALIDATION TO AN ACTION:
     * 1. add data annotation to entities.
     * 2. Use ModelState.IsValid to change the flow of the program, if is not valid return same view
     * 3. Add validation messages to CustomerForm (to view), we put a placeholder for validation message
     * next to each field that requires validation.*/