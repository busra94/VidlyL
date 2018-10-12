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
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;
        List<CustomerDto> customersDto;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
            customersDto = new List<CustomerDto>();
        }
        //GET /api/customers this action by convention will respond to this url
        public IHttpActionResult GetCustomers()
        {
            var customersInDb = _context.Customers.ToList();
            var customer = new CustomerDto();
            foreach (var customers in customersInDb)
            {
                customer.Id = customers.Id;
                customer.Name = customers.Name;
                customer.BirthDate = customers.BirthDate;
                customer.IsSubscribedToNewsletter = customers.IsSubscribedToNewsletter;
                customer.MembershipTypeId = customers.MembershipTypeId;

                customersDto.Add(customer);
            }
            return Ok(customersDto);
        }
        //Get /api/customers/1(id) get one record from database
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = new CustomerDto();
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            customer.Name = customerInDb.Name;
            customer.Id = customerInDb.Id;
            customer.BirthDate = customerInDb.BirthDate;
            customer.IsSubscribedToNewsletter = customerInDb.IsSubscribedToNewsletter;
            customer.MembershipTypeId = customerInDb.MembershipTypeId;

            if (customer == null)
            {
                NotFound();
            }
            return Ok(customer);
        }
        //POST /api/customers we post(add) a customer to customers collection 
        [HttpPost] // with applying this attribute here this action only be called if we send an http post request. By convention if we set name to this action PostCustomer no need add [HttpPost] attribute
        public IHttpActionResult CreateCustomer(CustomerDto customerDto) //action parameter will be in request body and asp.net mvc will automatically initializes this. 
        {
            if (!ModelState.IsValid)
                return BadRequest(); // this is a class that implements IHttpActionResult.


            //var customer = new Customer();
            //customer.Name = customerDto.Name;
            //customer.BirthDate = customerDto.BirthDate;
            //customer.IsSubscribedToNewsletter = customerDto.IsSubscribedToNewsletter;
            //customer.MembershipTypeId = customerDto.MembershipTypeId;
           
            _context.Customers.Add(customer);
            _context.SaveChanges(); // id property will be set based on the ID generated from the database

            // add ID to DTO for return it to client
            customerDto.Id = customer.Id;

            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto); // uri -> unified resource identifier something like this /api/customers/10 (newly created id) 
        }

        //PUT /api/customers/1
        [HttpPut]
        public void UpdateCustomer(int id, CustomerDto customerDto) // either we can return Customer object or void. id from the url(database) and customer from request body
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null) // if client sends an invalid ID throw exception 
                throw new HttpResponseException(HttpStatusCode.NotFound);

            customerInDb.Name = customerDto.Name;
            customerInDb.BirthDate = customerDto.BirthDate;
            customerInDb.IsSubscribedToNewsletter = customerDto.IsSubscribedToNewsletter;
            customerInDb.MembershipTypeId = customerDto.MembershipTypeId;

            _context.SaveChanges();
        }

        // DELETE /api/customers/1
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null) // if client sends an invalid ID throw exception 
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();
        }
    }
}

/*We check all fields valid for Create and update actions because it is possible to client does not
 * send invalid data*/

/*Data Transfer Object: 

If we change(rename or remove a property) the domain model(Customer), 
these can impact the clients that are dependent on that property
and these changes can potentially break existing clients that are dependent on the customer object.

So we need to make the contract of this API as stable as possible. To solve this issue we have a 
different model which we call Data Transfer Object(DTO)
 */
