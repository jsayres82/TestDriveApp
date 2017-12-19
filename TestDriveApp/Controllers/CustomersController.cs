using AutoMapper;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using TestDriveApp.Models;
using TestDriveApp.ViewModels;
using TestDriveApp.Dtos;

namespace TestDriveApp.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Customers
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetCustomers(string query = null)
        {
            var customersQuery = _context.Customers
                .Include(c => c.MembershipType);

            if (!string.IsNullOrWhiteSpace(query))
            {
                customersQuery = customersQuery.Where(c => c.Name.Contains(query));
            }

            var customerDtos = customersQuery
                .ToList()
                .Select(Mapper.Map<Customer, CustomerDto>);

            return Json(new {data = customerDtos }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult New(string UserId)
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            
            var viewModel = new CustomerFormViewModel(UserId)
            {
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm", viewModel);
        }

        public ActionResult Edit(int UserId)
        {
            
            var customer = _context.Customers.SingleOrDefault(c => c.NumericId == UserId);

            if (customer == null)
            {
                return HttpNotFound();
            }

            var viewModel = new CustomerFormViewModel(customer)
            {
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", viewModel);
        }

        public ActionResult EditAccount(string AppUserId)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.ApplicationUserId == AppUserId);

            if (customer == null)
            {
                return HttpNotFound();
            }

            var viewModel = new CustomerFormViewModel(customer)
            {
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", viewModel);
        }

        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel(customer)
                {
                    MembershipTypes = _context.MembershipTypes.ToList()
                };

                return View("CustomerForm", viewModel);
            }

            if (customer.NumericId == 0)
            {
                _context.Customers.Add(customer);
            }
            else
            {
                var customerInDb = _context.Customers.Single(c => c.NumericId == customer.NumericId);
                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
                customerInDb.MembershipType = customer.MembershipType;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Details(int id)
        {
            var customer = _context.Customers
                .Include(c => c.MembershipType)
                .SingleOrDefault(c => c.NumericId == id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            return View(customer);
        }
    }
}