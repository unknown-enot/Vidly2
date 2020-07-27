using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly2.Models;
using Vidly2.ViewModels;
using System.Runtime.Caching;

namespace Vidly2.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext context;

        public CustomersController()
        {
            context=new ApplicationDbContext(); // Disposable object
        }

        protected override void Dispose(bool disposing)
        {
            context.Dispose();
        }

        public ActionResult New()
        {
            var membershipTypes = context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel()
            {
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = context.MembershipTypes.ToList()
                };

                return View("CustomerForm", viewModel);
            }

            if (customer.Id == 0)
                context.Customers.Add(customer);
            else
            {
                var customerInDb = context.Customers.Single(c => c.Id == customer.Id);

                // Mapper.Map(customer, customerInDb);
                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;

            }

            context.SaveChanges();

            return RedirectToAction("Index","Customers");
        }

        /*
        private IEnumerable<Customer> GetCustomers()
        {
            
            return new List<Customer>
            {
                new Customer {Id = 1, Name = "John Smith"},
                new Customer {Id = 2, Name = "Mary Williams"}
            };
            
        }
        */


        // GET: Customers
        public ActionResult Index()
        {
            //var customers = context.Customers.Include(c=>c.MembershipType).ToList(); // get all customers in the database
            //return View(customers);
            if (MemoryCache.Default["Genres"] == null)
            {
                MemoryCache.Default["Genres"] = context.Genres.ToList();
            }

            var genres = MemoryCache.Default["Genres"] as IEnumerable<Genre>;

            return View();
        }

        public ActionResult Details(int id)
        {
            var customer = context.Customers.Include(c=>c.MembershipType).SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return HttpNotFound();
            return View(customer);
        }

        public ActionResult Edit(int id)
        {
            var customer = context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = context.MembershipTypes.ToList()
            };
            return View("CustomerForm", viewModel);

        }
    }
}