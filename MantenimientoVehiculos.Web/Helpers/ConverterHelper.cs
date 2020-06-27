using MantenimientoVehiculos.Web.Data;
using MantenimientoVehiculos.Web.Data.Entities;
using MantenimientoVehiculos.Web.Models;
using Org.BouncyCastle.Asn1.IsisMtt.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace MantenimientoVehiculos.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;

        public ConverterHelper(DataContext context, ICombosHelper combosHelper)
        {
            _context = context;
            _combosHelper = combosHelper;
        }
        public async Task<VehicleEntity> ToVehicleAsync(VehicleViewModel model, string path, bool isNew)
        {
            return new VehicleEntity
            {
                Id = isNew ? 0 : model.Id,
                Plaque = model.Plaque,
                MotorSerial = model.MotorSerial,
                Chassis = model.Chassis,
                Year = model.Year,
                ImageUrl=path,
               // VehicleBrand = model.VehicleBrands,
            };
        }

        public VehicleViewModel ToVehicleViewModel(VehicleEntity vehicle)
        {
            return new VehicleViewModel
            {
                Id = vehicle.Id,
                Plaque = vehicle.Plaque,
                MotorSerial = vehicle.MotorSerial,
                Chassis = vehicle.Chassis,
                Year = vehicle.Year,
                ImageUrl = vehicle.ImageUrl,
                VehicleBrandId = vehicle.VehicleBrand.Id,
                VehicleBrands = _combosHelper.GetComboBrandVehicle()
            };
        }
    }
}
