using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Vidly2.Dtos;
using Vidly2.Models;

namespace Vidly2.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext context;

        public CustomersController()
        {
            context = new ApplicationDbContext();
        }
        // GET /api/customers -- all Customers
        public IHttpActionResult GetCustomers(string query = null)
        {
            var customersQuery = context.Customers
                .Include(c=>c.MembershipType);

            if (!String.IsNullOrWhiteSpace(query))
                customersQuery = customersQuery.Where(c => c.Name.Contains(query));
                
            var customersDtos = customersQuery
                .ToList()
                .Select(Mapper.Map<Customer,CustomerDto>);

            return Ok(customersDtos);
        }

        // GET /api/customers/1 -- return a single Customer
        
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return NotFound();//throw new HttpResponseException(HttpStatusCode.NotFound);

            return Ok(Mapper.Map<Customer,CustomerDto>(customer));

        }

        // POST /api/customers
        [HttpPost]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
                //throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            context.Customers.Add(customer);
            context.SaveChanges();

            customerDto.Id = customer.Id;
            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto); //customerDto;
        }

        // PUT /api/customers/1
        [HttpPut]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();//throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customerInDb = context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                return NotFound();//throw  new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(customerDto, customerInDb);
            //customerInDb.Name = customerDto.Name;
            //customerInDb.Birthdate = customerDto.Birthdate;
            //customerInDb.IsSubscribedToNewsletter = customerDto.IsSubscribedToNewsletter;
            //customerInDb.MembershipTypeId = customerDto.MembershipTypeId;

            context.SaveChanges();

            return Ok();
        }

        // DELETE /api/customers/1
        [HttpDelete]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var customerInDb = context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                return NotFound();//throw  new HttpResponseException(HttpStatusCode.NotFound);

            context.Customers.Remove(customerInDb);
            context.SaveChanges();

            return Ok();
        }
    }
}
