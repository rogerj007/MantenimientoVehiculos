using MantenimientoVehiculos.Web.Data;
using MantenimientoVehiculos.Web.Data.Entities;
using MantenimientoVehiculos.Web.Models;
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
                Cylinder=model.Cylinder,
                Year = model.Year,
                ImageUrl=path,
                VehicleBrand = await _context.VehicleBrand.FindAsync(model.VehicleBrandId),
                VehicleType = await _context.VehicleType.FindAsync(model.VehicleTypeId),
                VehicleStatus = await _context.VehicleStatus.FindAsync(model.VehicleStatusId),
                Country = await _context.Country.FindAsync(model.CountryId),
                Fuel = await _context.Fuel.FindAsync(model.FuelId),
                Color = await _context.Color.FindAsync(model.ColorId)

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
                Cylinder=vehicle.Cylinder,
                Year = vehicle.Year,
                ImageUrl = vehicle.ImageUrl,
                VehicleBrandId = vehicle.VehicleBrand.Id,
                VehicleBrands = _combosHelper.GetComboBrandVehicle(),
                VehicleTypeId = vehicle.VehicleType.Id,
                VehicleTypes = _combosHelper.GetComboVehicleType(),
                VehicleStatusId=vehicle.VehicleStatus.Id,
                VehicleStatu=_combosHelper.GetComboVehicleStatus(),
                CountryId = vehicle.Country.Id,
                Countries = _combosHelper.GetComboCountry(),
                FuelId = vehicle.Fuel.Id,
                Fuels = _combosHelper.GetComboFuel(),
                ColorId=vehicle.Color.Id,
                Colors=_combosHelper.GetComboColor()
            };
        }

        public async Task<VehicleRecordActivityEntity> ToVehicleRecordActivityAsync(VehicleRecordActivityViewModel model, bool isNew)
        {
            return new VehicleRecordActivityEntity
            {
                Id = isNew ? 0 : model.Id,
                KmHr=model.KmHr,
                Vehicle= await _context.Vehicle.FindAsync(model.VehicleId)
                //User= await _context.Users.FindAsync(model.User.Id),
            };

        }

        public VehicleRecordActivityViewModel ToVehicleRecordActivityViewModel(VehicleRecordActivityEntity vehicle)
        {
            return new VehicleRecordActivityViewModel
            {
                Id = vehicle.Id,
                KmHr = vehicle.KmHr,
                Vehicles = _combosHelper.GetComboVehicles()
                //User= await _context.Users.FindAsync(model.User.Id),
            };
        }
    }
}
