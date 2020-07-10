using System;
using System.Reflection;
using MantenimientoVehiculos.Web.Data;
using MantenimientoVehiculos.Web.Data.Entities;
using MantenimientoVehiculos.Web.Models;
using System.Threading.Tasks;
using AutoMapper;
using MantenimientoVehiculos.Web.Enums;
using Microsoft.EntityFrameworkCore;

namespace MantenimientoVehiculos.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;
        private readonly IMapper _mapper;

        public ConverterHelper(DataContext context, ICombosHelper combosHelper, IMapper mapper)
        {
            _context = context;
            _combosHelper = combosHelper;
            _mapper = mapper;
        }
        public async Task<VehicleEntity> ToVehicleAsync(VehicleViewModel model, string path)
        {
            var dto = _mapper.Map<VehicleEntity>(model);
            dto.VehicleBrand = await _context.VehicleBrand.FindAsync(model.VehicleBrandId);
            dto.VehicleType = await _context.VehicleType.FindAsync(model.VehicleTypeId);
            dto.VehicleStatus = await _context.VehicleStatus.FindAsync(model.VehicleStatusId);
            dto.Country = await _context.Country.FindAsync(model.CountryId);
            dto.Fuel = await _context.Fuel.FindAsync(model.FuelId);
            dto.Color = await _context.Color.FindAsync(model.ColorId);
            if (!string.IsNullOrEmpty(path)) dto.ImageUrl = path;
            return dto;
        }

        public VehicleViewModel ToVehicleViewModel(VehicleEntity vehicle)
        {
            var dto = _mapper.Map<VehicleViewModel>(vehicle);
            dto.VehicleBrands = _combosHelper.GetComboBrandVehicle();
            dto.VehicleTypes = _combosHelper.GetComboVehicleType();
            dto.VehicleStatu = _combosHelper.GetComboVehicleStatus();
            dto.Countries = _combosHelper.GetComboCountry();
            dto.Fuels = _combosHelper.GetComboFuel();
            dto.Colors = _combosHelper.GetComboColor();
            return dto;
        }

        public async Task<VehicleRecordActivityEntity> ToVehicleRecordActivityAsync(VehicleRecordActivityViewModel model)
        {
            var dto = _mapper.Map<VehicleRecordActivityEntity>(model);
            dto.Vehicle = await _context.Vehicle.FindAsync(model.VehicleId);
            return dto;
        }

        public VehicleRecordActivityViewModel ToVehicleRecordActivityViewModel(VehicleRecordActivityEntity model)
        {
            var dto = _mapper.Map<VehicleRecordActivityViewModel>(model);
            dto.Vehicles = _combosHelper.GetComboVehicles();
            return dto;
        }

        public async Task<VehicleMaintenanceEntity> ToVehicleMaintenanceAsync(VehicleMaintenanceViewModel model)
        {
            var dto = _mapper.Map<VehicleMaintenanceEntity>(model);
            dto.MaintenanceType = Enum.Parse<MaintenanceType>(model.MaintenanceTypeId.ToString());
            dto.Vehicle = await _context.Vehicle.FindAsync(model.VehicleId);
            return dto;
        }

        public VehicleMaintenanceViewModel ToVehicleMaintenanceViewModel(VehicleMaintenanceEntity model)
        {
            var dto = _mapper.Map<VehicleMaintenanceViewModel>(model);
            dto.MaintenanceTypeId = (int) model.MaintenanceType;
            dto.VehicleId =model.Vehicle.Id;
            dto.ListMaintenanceType = _combosHelper.GetComboListMaintenance();
            dto.ListVehicles = _combosHelper.GetComboVehicles();
            return dto;
        }


        public async Task<VehicleMaintenanceDetailEntity> ToVehicleMaintenanceDetailsAsync(VehicleMaintenanceDetailsViewModel model, bool isNew)
        {
            var dto = _mapper.Map<VehicleMaintenanceDetailEntity>(model);
            dto.Id = isNew ? 0 : model.Id;
            dto.VehicleMaintenance = await _context.VehicleMaintenance.FindAsync(model.VehicleMaintenanceId);
            dto.Component = await _context.Component.FindAsync(model.ComponentId);
            return dto;
        }

        public VehicleMaintenanceDetailsViewModel ToVehicleMaintenanceDetailsViewModel(VehicleMaintenanceDetailEntity model)
        {
            var dto = _mapper.Map<VehicleMaintenanceDetailsViewModel>(model);
            //dto.MaintenanceTypeId = (int)model.MaintenanceType;
            //dto.VehicleMaintenanceId = model.ma
            dto.Components = _combosHelper.GetComboComponets();
         
            return dto;
        }


        public async Task<UserEntity> ToUserAsync(EditListUserViewModel model, string path)
        {
            try
            {

                var user = _context.Users.SingleOrDefaultAsync(c => c.Id.Equals(model.Id)).Result;
                user.ModifiedDate = DateTime.UtcNow;
                user.Address = model.Address;
                user.Document = model.Document;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.PhoneNumber = model.PhoneNumber;
                user.PicturePath = model.PicturePath;
                user.Enable = model.Enable;
                user.UserType = Enum.Parse<UserType>(model.UserTypeId.ToString());
                user.UserFunction = await _context.UserFunction.FindAsync(model.UserFuncionId);
                return user;
            }
            catch (AutoMapperMappingException ex)
            {
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public EditListUserViewModel ToEditListUserViewModel(UserEntity model)
        {
            try
            {

                var uwm= new EditListUserViewModel
                {
                    Id = model.Id,
                    Address = model.Address,
                    Document = model.Document,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    PicturePath = model.PicturePath,
                    Enable = model.Enable,
                    UserFuncionId=model.UserFunction?.Id,
                    //UserTypeId=model.UserType,
                    UserTypes = _combosHelper.GetComboRoles(true),
                    UserFuncion = _combosHelper.GetComboUserFuncion()
                };
              
                return uwm;
            }
            catch (AutoMapperMappingException)
            {
                return null;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
