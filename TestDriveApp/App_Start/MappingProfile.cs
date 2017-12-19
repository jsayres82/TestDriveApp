using AutoMapper;
using TestDriveApp.Dtos;
using TestDriveApp.Models;

namespace TestDriveApp.App_Start
{
    public class MappingProfile : Profile
    {        public MappingProfile()
        {
            // Domain to Dto
            Mapper.CreateMap<Customer, CustomerDto>();
            Mapper.CreateMap<Reservation, NewTestDriveDto>();
            Mapper.CreateMap<MembershipType, MembershipTypeDto>();
            Mapper.CreateMap<Vehicle, VehicleDto>();
            
            // Dto to Domain
            Mapper.CreateMap<CustomerDto, Customer>()
                .ForMember(c => c.NumericId, opt => opt.Ignore());

            // Dto to Domain
            Mapper.CreateMap<VehicleDto, Vehicle>()
                .ForMember(c => c.VehicleId, opt => opt.Ignore());

            Mapper.CreateMap<NewTestDriveDto, Reservation>()
                .ForMember(c => c.ReservationId, opt => opt.Ignore());
        }
    }
}