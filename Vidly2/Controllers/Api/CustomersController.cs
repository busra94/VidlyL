using AutoMapper;
using System;
using System.Data.Entity;
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
      

        public CustomersController()
        {
            _context = new ApplicationDbContext();
          
        }
        //GET /api/customers this action by convention will respond to this url
        public IHttpActionResult GetCustomers(string query = null)
        {
            var customersQuery = _context.Customers
                .Include(c => c.MembershipType);

            if (!String.IsNullOrWhiteSpace(query)) // if there is a query is returning from typeahead return that contains query 
                customersQuery = customersQuery.Where(c => c.Name.Contains(query));

            var customersInDb = customersQuery         
                .ToList()
                .Select(Mapper.Map<Customer,CustomerDto>);
      
            return Ok(customersInDb);
        }
        //Get /api/customers/1(id) get one record from database
        public IHttpActionResult GetCustomer(int id)
        {    
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
                       
            if (customerInDb == null)
            {
                NotFound();
            }
            return Ok(Mapper.Map<Customer,CustomerDto>(customerInDb));
        }
        //POST /api/customers we post(add) a customer to customers collection 
        [HttpPost] // with applying this attribute here this action only be called if we send an http post request. By convention if we set name to this action PostCustomer no need add [HttpPost] attribute
        public IHttpActionResult CreateCustomer(CustomerDto customerDto) //action parameter will be in request body and asp.net mvc will automatically initializes this. 
        {
            if (!ModelState.IsValid)
                return BadRequest(); // this is a class that implements IHttpActionResult.

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
           
            _context.Customers.Add(customer);
            _context.SaveChanges(); // id property will be set based on the ID generated from the database

            // add ID to DTO for return it to client
            customerDto.Id = customer.Id;

            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto); // uri -> unified resource identifier something like this /api/customers/10 (newly created id) uti -> restful convention
        }

        //PUT /api/customers/1
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, CustomerDto customerDto) // either we can return Customer object or void. id from the url(database) and customer from request body
        {
            if (!ModelState.IsValid)
                BadRequest();

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null) // if client sends an invalid ID throw exception 
                NotFound();

            //Mapper.Map<CustomerDto,Customer>(customerDto, customerInDb);
            Mapper.Map(customerDto, customerInDb); //customerInDb object loaded into context, so dbContext to be able to track changes in this object 
            //here no need to specify source and target because compiler can understand source and target which is.                        
            _context.SaveChanges();

            return Ok();
        }

        // DELETE /api/customers/1
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null) // if client sends an invalid ID throw exception 
                return NotFound();

            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();

            return Ok();
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

    /* in RESTful convention when we create a resource the Http status code should be 201 (created)
     * to make this happen instead of returning CustomerDto we return IHttpActionResult    
     */