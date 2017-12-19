using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using TestDriveApp.Models;
using TestDriveApp.ViewModels;
using TestDriveApp.Dtos;

namespace TestDriveApp.Controllers
{
    public class VehiclesController : Controller
    {
        private ApplicationDbContext _context;

        public VehiclesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult GetMyVehicles(string query = null)
        {
            var userId = User.Identity.GetUserId();
            var vehicleQuery = _context.Vehicles
                .Where(v => v.OwnerId == _context.Customers.Single(c => c.Id == User.Identity.GetUserId()).NumericId);

            if (!String.IsNullOrWhiteSpace(query))
            {
                vehicleQuery = vehicleQuery.Where(v => v.Make.Contains(query));
            }

            var vehicleDtos = vehicleQuery
                .ToList()
                .Select(Mapper.Map<Vehicle, VehicleDto>);

            return Json(new { data = vehicleDtos }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetVehicles(string query = null)
        {
            var vehicleQuery = _context.Vehicles
                .Where(v => v.IsAvailable == true || v.IsAvailable == false);

            if (!String.IsNullOrWhiteSpace(query))
            {
                vehicleQuery = vehicleQuery.Where(v => v.Make.Contains(query));
            }

            var vehicleDtos = vehicleQuery
                .ToList()
                .Select(Mapper.Map<Vehicle, VehicleDto>);

            return Json(new { data = vehicleDtos }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
                return View("List");
            else
                return RedirectToAction("Login", "Account");
        }

        public ActionResult SearchIndex()
        {
            return View("ReadOnlyList");
        }

        public ActionResult New()
        {
            var id = User.Identity.GetUserId();
            var viewModel = new VehicleFormViewModel()
            {
                OwnerId = _context.Customers.Single(c => c.Id == id).NumericId,
            };

            return View("VehicleForm", viewModel);
        }
        
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();

            Vehicle testVehicle = _context.Vehicles.SingleOrDefault(v => v.VehicleId == id);

            if (testVehicle == null)
            {
                return HttpNotFound();
            }

            var viewModel = new VehicleFormViewModel(testVehicle)
            {
                OwnerId = _context.Customers.Single(c => c.Id == User.Identity.GetUserId()).NumericId,
            };

            return View("VehicleForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Vehicle vehicle)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new VehicleFormViewModel(vehicle)
                {
                    OwnerId = _context.Customers.Single(c => c.Id == User.Identity.GetUserId()).NumericId,
                };
                return View("VehicleForm", viewModel);
            }
            //New Vehicle Add
            if (vehicle.VehicleId == 0)
            {
                var id = User.Identity.GetUserId();
                vehicle.OwnerId = _context.Customers.Single(c => c.Id == id).NumericId;
                _context.Vehicles.Add(vehicle);           
            }
            //Edit Vehicle Data
            else
            {
                var vehicleInDb = _context.Vehicles.Single(v => v.VehicleId == vehicle.VehicleId);
                vehicleInDb.Make = vehicle.Make;
                vehicleInDb.Model = vehicle.Model;
                vehicleInDb.Year = vehicle.Year;
                vehicleInDb.Transmission = vehicle.Transmission;
                vehicleInDb.HourlyRate = vehicle.HourlyRate;
                //vehicleInDb.Picture = vehicle.Picture;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Vehicles");
        }


        public ActionResult Details(int id)
        {
            var vehicle = _context.Vehicles
                .SingleOrDefault(v => v.VehicleId == id);

            if (vehicle == null)
            {
                return HttpNotFound();
            }

            return View(vehicle);
        }

        // GET: testvehicles/Random
        public ActionResult Random()
        {
            var vehicle = new Vehicle() { Make = "Ford", Model = "Ranger", Year = 2002, Transmission = "Automatic", OwnerId = _context.Customers.Single(c => c.Id == User.Identity.GetUserId()).NumericId, VehicleId = 0 };

            var viewModel = new VehicleFormViewModel()
            {
                Make = vehicle.Make,
                Model = vehicle.Model,
                Year = vehicle.Year,
                Transmission = vehicle.Transmission,
                OwnerId = vehicle.OwnerId,
                VehicleId = vehicle.VehicleId
            };

            return View(viewModel);
        }
    }
}