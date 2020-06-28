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
            await CheckComponetsAsync();
            await CheckColorAsync();
            await CheckFuelAsync();
            await CheckUserTypeAsync();
            await CheckVehiculeTypeAsync();
            await CheckVehiculeStatusAsync();
            await CheckVehiculeBrandAsync();

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

        private async Task CheckComponetsAsync()
        {
            if (!_dataContext.Component.Any())
            {
                await _dataContext.Component.AddRangeAsync(
                    new ComponentEntity { Component = "FILTRO TRAMPA DE AGUA", CreationDate = DateTime.UtcNow },
                    new ComponentEntity { Component = "FILTRO COMBUSTIBLE", CreationDate = DateTime.UtcNow },
                    new ComponentEntity { Component = "FILTRO DE ACEITE", CreationDate = DateTime.UtcNow },
                    new ComponentEntity { Component = "FILTRO ACEITE HIDRAULICO", CreationDate = DateTime.UtcNow },
                    new ComponentEntity { Component = "FILTRO DE AIRE", CreationDate = DateTime.UtcNow },
                    new ComponentEntity { Component = "FILTRO DE AIRE SECUNDARIO", CreationDate = DateTime.UtcNow },
                    new ComponentEntity { Component = "FILTRO COMBUSTIBLE PRIMARIO", CreationDate = DateTime.UtcNow },
                    new ComponentEntity { Component = "FILTRO COMBUSTIBLE SECUNDARIO", CreationDate = DateTime.UtcNow },
                    new ComponentEntity { Component = "FILTRO RACOR", CreationDate = DateTime.UtcNow },
                    new ComponentEntity { Component = "FILTRO SECADOR", CreationDate = DateTime.UtcNow }

                );
                await _dataContext.SaveChangesAsync();
            }
        }
        private async Task CheckVehiculeBrandAsync()
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

        private async Task CheckVehiculeStatusAsync()
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

        private async Task CheckVehiculeTypeAsync()
        {
            if (!_dataContext.VehicleType.Any())
            {
                await _dataContext.VehicleType.AddRangeAsync(
                    new VehicleTypeEntity { VehicleType = "VOLQUETA", CreationDate = DateTime.UtcNow },
                    new VehicleTypeEntity { VehicleType = "BUS", CreationDate = DateTime.UtcNow },
                    new VehicleTypeEntity { VehicleType = "KABEZAL", CreationDate = DateTime.UtcNow },
                    new VehicleTypeEntity { VehicleType = "VEHICULO ESPECIAL", CreationDate = DateTime.UtcNow },
                    new VehicleTypeEntity { VehicleType = "ESCAVADORA", CreationDate = DateTime.UtcNow },
                    new VehicleTypeEntity { VehicleType = "TRACTOR", CreationDate = DateTime.UtcNow },
                    new VehicleTypeEntity { VehicleType = "MOTO NIVELADORA", CreationDate = DateTime.UtcNow },
                    new VehicleTypeEntity { VehicleType = "RODILLO", CreationDate = DateTime.UtcNow }
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
                    new ColorEntity { Color = "ROJO", CreationDate = DateTime.UtcNow },
                                new ColorEntity { Color = "NEGRO", CreationDate = DateTime.UtcNow },
                                new ColorEntity { Color = "AZUL", CreationDate = DateTime.UtcNow },
                                new ColorEntity { Color = "AMARILLO", CreationDate = DateTime.UtcNow },
                                new ColorEntity { Color = "NARANJA", CreationDate = DateTime.UtcNow },
                                new ColorEntity { Color = "BLANCO", CreationDate = DateTime.UtcNow },
                                new ColorEntity { Color = "MORADO", CreationDate = DateTime.UtcNow },
                                new ColorEntity { Color = "PLOMO", CreationDate = DateTime.UtcNow },
                                new ColorEntity { Color = "MARRON", CreationDate = DateTime.UtcNow }
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

        private async Task CheckVehicleAsync()
        {
            if (!_dataContext.Vehicle.Any())
            {
                var vehicleStatus = _dataContext.VehicleStatus.FirstOrDefault();
                var vehicleType = _dataContext.VehicleType.FirstOrDefault();
                var vehicleBrand = _dataContext.VehicleBrand.FirstOrDefault();
                var country = _dataContext.Country.FirstOrDefault();
                var color = _dataContext.Color.FirstOrDefault();
                var fuel = _dataContext.Fuel.FirstOrDefault();
                await _dataContext.Vehicle.AddRangeAsync(
                    new VehicleEntity
                    {
                        VehicleType= vehicleType,
                        VehicleStatus=vehicleStatus,
                        VehicleBrand=vehicleBrand,
                        Country = country,
                        Color=color, 
                        Fuel=fuel,
                        Plaque="XXX696",
                        Chassis="XDXDXDXD",
                        Year=2020,
                        MotorSerial="ASDFGHJKL",
                        Cylinder =1515,
                        CreationDate = DateTime.UtcNow
                    }
                );
                await _dataContext.SaveChangesAsync();

            }
        }
    }
}
