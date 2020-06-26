using MantenimientoVehiculos.Web.Data.Entities;
using System;
using System.Collections.Generic;
//using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using MantenimientoVehiculos.Web.Enums;
using MantenimientoVehiculos.Web.Helpers;

namespace MantenimientoVehiculos.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _dataContext;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext dataContext, IUserHelper userHelper)//,
        {
            _dataContext = dataContext;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _dataContext.Database.EnsureCreatedAsync();

            //Variables
            await CheckCountryAsync();
            await CheckColorAsync();
            await CheckFuelAsync();
            await CheckUserTypeAsync();
            await CheckTypeVehiculeAsync();
            await CheckStatusVehiculeAsync();
            await CheckBrandVehiculeAsync();

            //Roles
            await CheckRolesAsync();
            await CheckUsersAsync();
        }


        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.Supervisor.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
        }

        private async Task CheckUsersAsync()
        {
            var admin = await CheckUserAsync("1010", "Roger", "Jaimes", "rogerjh@mercapro.com", "0998585584", "Calle Luna Calle Sol", UserType.Admin);
            var supervisor = await CheckUserAsync("2020", "Cristian", "Rosado", "rogerjh@diuniversalcheck.com", "0998585584", "Calle Luna Calle Sol", UserType.Supervisor);
            var user1 = await CheckUserAsync("3030", "Mauricio", "Torres", "rogerjh@rjrecords.com", "0998585584", "Calle Luna Calle Sol", UserType.User);
        }

        private async Task<UserEntity> CheckUserAsync(
            string document,
            string firstName,
            string lastName,
            string email,
            string phone,
            string address,
            UserType userType)
        {
            var user = await _userHelper.GetUserByEmailAsync(email);
            if (user == null)
            {
                user = new UserEntity
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document,
                    UserType = userType
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());
            }

            return user;
        }


        #region Variabled
        private async Task CheckBrandVehiculeAsync()
        {
            if (!_dataContext.VehicleBrand.Any())
            {
                await _dataContext.VehicleBrand.AddRangeAsync(
                    new VehicleBrandEntity { VehicleBrand = "CHEVROLET", CreationDate = DateTime.UtcNow },
                    new VehicleBrandEntity { VehicleBrand = "KENWORTH", CreationDate = DateTime.UtcNow },
                    new VehicleBrandEntity { VehicleBrand = "HINO MOTORS", CreationDate = DateTime.UtcNow },
                    new VehicleBrandEntity { VehicleBrand = "TOYOTA", CreationDate = DateTime.UtcNow },
                    new VehicleBrandEntity { VehicleBrand = "MAZDA", CreationDate = DateTime.UtcNow },
                    new VehicleBrandEntity { VehicleBrand = "CATERPILLAR", CreationDate = DateTime.UtcNow },
                    new VehicleBrandEntity { VehicleBrand = "KOMATSU", CreationDate = DateTime.UtcNow },
                    new VehicleBrandEntity { VehicleBrand = "JOHN DEERE", CreationDate = DateTime.UtcNow }

                );
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckStatusVehiculeAsync()
        {
            if (!_dataContext.VehicleStatus.Any())
            {
                await _dataContext.VehicleStatus.AddRangeAsync(
                    new VehicleStatusEntity { VehicleStatus = "OPERATIVO", CreationDate = DateTime.UtcNow },
                    new VehicleStatusEntity { VehicleStatus = "DAÑADA", CreationDate = DateTime.UtcNow },
                    new VehicleStatusEntity { VehicleStatus = "PROCESO DE BAJA", CreationDate = DateTime.UtcNow }
                );
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckTypeVehiculeAsync()
        {
            if (!_dataContext.TypeVehicle.Any())
            {
                await _dataContext.TypeVehicle.AddRangeAsync(
                    new TypeVehicleEntity { TypeVehicle = "VOLQUETA", CreationDate = DateTime.UtcNow },
                    new TypeVehicleEntity { TypeVehicle = "BUS", CreationDate = DateTime.UtcNow },
                    new TypeVehicleEntity { TypeVehicle = "KABEZAL", CreationDate = DateTime.UtcNow },
                    new TypeVehicleEntity { TypeVehicle = "VEHICULO ESPECIAL", CreationDate = DateTime.UtcNow },
                    new TypeVehicleEntity { TypeVehicle = "ESCAVADORA", CreationDate = DateTime.UtcNow },
                    new TypeVehicleEntity { TypeVehicle = "TRACTOR", CreationDate = DateTime.UtcNow },
                    new TypeVehicleEntity { TypeVehicle = "MOTO NIVELADORA", CreationDate = DateTime.UtcNow },
                    new TypeVehicleEntity { TypeVehicle = "RODILLO", CreationDate = DateTime.UtcNow }
                );
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckUserTypeAsync()
        {
            if (!_dataContext.UserFunction.Any())
            {
                await _dataContext.UserFunction.AddRangeAsync(
                    new UserFunctionEntity { UserFunction = "SUPERVISOR DE MANTENIMIENTO MECÁNICO ", CreationDate = DateTime.UtcNow },
                    new UserFunctionEntity { UserFunction = "JEFE DE TALLERES", CreationDate = DateTime.UtcNow },
                    new UserFunctionEntity { UserFunction = "ASISTENTE DE SUPERVISION MTTO. MECÁNICO", CreationDate = DateTime.UtcNow },
                    new UserFunctionEntity { UserFunction = "MECÁNICO", CreationDate = DateTime.UtcNow },
                    new UserFunctionEntity { UserFunction = "AYUDANTE DE MECÁNICA", CreationDate = DateTime.UtcNow },
                    new UserFunctionEntity { UserFunction = "SOLDADOR", CreationDate = DateTime.UtcNow },
                    new UserFunctionEntity { UserFunction = "AYUDANTE SOLDADOR", CreationDate = DateTime.UtcNow },
                    new UserFunctionEntity { UserFunction = "VULCANIZADOR", CreationDate = DateTime.UtcNow },
                    new UserFunctionEntity { UserFunction = "AYUDANTE DE VUCANIZADOR", CreationDate = DateTime.UtcNow }
                );
                await _dataContext.SaveChangesAsync();
            }
        }
        private async Task CheckFuelAsync()
        {
            if (!_dataContext.Fuel.Any())
            {
                await _dataContext.Fuel.AddRangeAsync(
                    new FuelEntity { Fuel = "EXTRA", CreationDate = DateTime.UtcNow },
                    new FuelEntity { Fuel = "SUPER", CreationDate = DateTime.UtcNow },
                    new FuelEntity { Fuel = "ECOPAIS", CreationDate = DateTime.UtcNow },
                    new FuelEntity { Fuel = "DIESEL", CreationDate = DateTime.UtcNow }
                );
                await _dataContext.SaveChangesAsync();

            }
        }
        private async Task CheckColorAsync()
        {
            if (!_dataContext.Color.Any())
            {
                await _dataContext.Color.AddRangeAsync(
                    new ColorEntity { Color = "RED", CreationDate = DateTime.UtcNow },
                                new ColorEntity { Color = "BLACK", CreationDate = DateTime.UtcNow },
                                new ColorEntity { Color = "BLUE", CreationDate = DateTime.UtcNow },
                                new ColorEntity { Color = "YELLOW", CreationDate = DateTime.UtcNow }
                    );
                await _dataContext.SaveChangesAsync();

            }
        }

        private async Task CheckCountryAsync()
        {
            if (!_dataContext.Country.Any())
            {
                await _dataContext.Country.AddRangeAsync(
                    new CountryEntity { Country = "ECUADOR", CreationDate = DateTime.UtcNow },
                    new CountryEntity { Country = "COLOMBIA", CreationDate = DateTime.UtcNow },
                    new CountryEntity { Country = "CHINA", CreationDate = DateTime.UtcNow },
                    new CountryEntity { Country = "JAPON", CreationDate = DateTime.UtcNow },
                    new CountryEntity { Country = "TAILANDIA", CreationDate = DateTime.UtcNow },
                    new CountryEntity { Country = "U.S.A", CreationDate = DateTime.UtcNow }
                );
                await _dataContext.SaveChangesAsync();

            }
        }


        #endregion

    }
}
