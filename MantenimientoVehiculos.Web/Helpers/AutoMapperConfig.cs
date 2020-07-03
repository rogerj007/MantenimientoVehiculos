using AutoMapper;
using MantenimientoVehiculos.Web.Data.Entities;
using MantenimientoVehiculos.Web.Models;

namespace MantenimientoVehiculos.Web.Helpers
{
    public class AutoMapperConfig: Profile
    {
        public AutoMapperConfig()
        {

            CreateMap<VehicleEntity, VehicleViewModel>().ReverseMap();

            CreateMap<VehicleRecordActivityEntity, VehicleRecordActivityViewModel>().ReverseMap();

            CreateMap<UserEntity, EditUserViewModel>().ReverseMap();

        }
    }
}
