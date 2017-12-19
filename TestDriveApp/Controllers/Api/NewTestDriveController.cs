using System;
using System.Linq;
using System.Web.Http;
using TestDriveApp.Dtos;
using TestDriveApp.Models;
using Data;

namespace TestDriveApp.Controllers.Api
{
    public class NewTestDriveController : ApiController
    {
        private ApplicationDbContext _context;

        public NewTestDriveController()
        {
            _context = new ApplicationDbContext();
            
        }

        [HttpPost]
        public IHttpActionResult CreateNewRentals(NewTestDriveDto newTestDrive)
        {
            var customer = _context.Customers.Single(c => c.NumericId == newTestDrive.CustomerId);

            var vehicle = newTestDrive.Vehicle;
            
            if (!vehicle.IsAvailable)
            {
                return BadRequest("Vehicle is not available.");
            }

            vehicle.IsAvailable = false;
            Db.CreateReservation(DateTime.Now, DateTime.Now.Add(new TimeSpan(0, 45, 0)), vehicle.VehicleId, customer.NumericId);
            //var testDrive = new Reservation
            //{
            //    CustomerId = customer.NumericId,
            //    ReservationStart = DateTime.Now,
            //    Vehicle = vehicle,
            //    ReservationEnd = DateTime.Now.Add(new TimeSpan(0,45,0))
            //};

            //_context..Add(testDrive);
            
            _context.SaveChanges();

            return Ok();
        }

    }
}
