using AutoMapper;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using TestDriveApp.Dtos;
using TestDriveApp.Models;

namespace TestDriveApp.Controllers.Api
{
    public class VehicleController : ApiController
    {
        private ApplicationDbContext _context;

        public VehicleController()
        {
            _context = new ApplicationDbContext();
        }

        // GET api/testvehicles
        public IHttpActionResult GetVehicles(string query = null)
        {
            var vehicleQuery = _context.Vehicles
                .Where(v => v.IsAvailable == true);

            if (!String.IsNullOrWhiteSpace(query))
            {
                vehicleQuery = vehicleQuery.Where(v => v.Make.Contains(query));
            }

            var vehicleDtos = vehicleQuery
                .ToList()
                .Select(Mapper.Map<Vehicle, VehicleDto>); 

            return Ok(vehicleDtos);
        }

        // GET api/testvehicles/1
        public IHttpActionResult GetVehicle(int id)
        {
            var vehicle = _context.Vehicles.SingleOrDefault(v => v.VehicleId == id);

            if (vehicle == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Vehicle, VehicleDto>(vehicle));
        }

        // POST api/testvehicles
        [Authorize(Roles = RoleName.CanManageTestDrives)]
        public IHttpActionResult CreateVehicle(VehicleDto vehicleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var vehicle = Mapper.Map<VehicleDto, Vehicle>(vehicleDto);

            _context.Vehicles.Add(vehicle);
            _context.SaveChanges();

            vehicleDto.VehicleId = vehicle.VehicleId;

            return Created(Request.RequestUri + "/" + vehicle.VehicleId, vehicleDto);
        }

        // PUT api/testvehicles/1
        [HttpPut]
        [Authorize(Roles = RoleName.CanManageTestDrives)]
        public IHttpActionResult UpdateVehicle(int id, VehicleDto vehicleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var vehicleInDb = _context.Vehicles.SingleOrDefault(v => v.VehicleId == id);

            if (vehicleInDb == null)
            {
                return NotFound();
            }

            Mapper.Map(vehicleDto, vehicleInDb);

            _context.SaveChanges();

            return Ok();
        }

        // DELETE api/testvehicles/1
        [HttpDelete]
        [Authorize(Roles = RoleName.CanManageTestDrives)]
        public IHttpActionResult DeleteVehicle(int id)
        {
            var vehicleInDb = _context.Vehicles.SingleOrDefault(v => v.VehicleId == id);

            if (vehicleInDb == null)
            {
                return NotFound();
            }

            _context.Vehicles.Remove(vehicleInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}
