using System;
using AutoMapper;
using MantenimientoVehiculos.Web.Data.Entities;
using MantenimientoVehiculos.Web.Models;

namespace MantenimientoVehiculos.Web.Helpers
{
    public class AutoMapperConfig: Profile
    {
        public AutoMapperConfig()
        {

            CreateMap<VehicleEntity, VehicleViewModel>()
                //.ForMember(dest => dest.Version,
                //    opt => opt.MapFrom(src => Convert.FromBase64String(src.Version)))
                .ReverseMap();

            CreateMap<VehicleRecordActivityEntity, VehicleRecordActivityViewModel>().ReverseMap();

            CreateMap<VehicleMaintenanceEntity, VehicleMaintenanceViewModel>().ReverseMap();

            CreateMap<VehicleMaintenanceDetailEntity, VehicleMaintenanceDetailsViewModel>().ReverseMap();

            CreateMap<UserEntity, EditUserViewModel>().ReverseMap();

        }
    }
}
