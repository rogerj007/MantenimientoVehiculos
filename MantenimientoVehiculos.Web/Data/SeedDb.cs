using MantenimientoVehiculos.Web.Data.Entities;
using System;
using System.Collections.Generic;
//using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using MantenimientoVehiculos.Web.Enums;
using MantenimientoVehiculos.Web.Helpers;
using Microsoft.EntityFrameworkCore;

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

            //Roles
            await CheckRolesAsync();
            await CheckUsersAsync();


            //Variables
            await CheckCountryAsync();
            await CheckComponetsAsync();
            await CheckColorAsync();
            await CheckFuelAsync();
            await CheckUserTypeAsync();
            await CheckVehiculeTypeAsync();
            await CheckVehiculeStatusAsync();
            await CheckVehiculeBrandAsync();

            //Create Events
            await CheckVehicleAsync();

        }




        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.Supervisor.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
        }

        private async Task CheckUsersAsync()
        {
           await CheckUserAsync("1010", "Roger", "Jaimes", "rogerjh@mercapro.com", "0998585584", "Calle Luna Calle Sol",true,1, UserType.Admin);
           await CheckUserAsync("2020", "Cristian", "Rosado", "rogerjh@diuniversalcheck.com", "0998585584", "Calle Luna Calle Sol",true,2, UserType.Supervisor);
           await CheckUserAsync("3030", "Mauricio", "Torres", "rogerjh@rjrecords.com", "0998585584", "Calle Luna Calle Sol", true,3,UserType.User);
        }

        private async Task<UserEntity> CheckUserAsync(
            string document,
            string firstName,
            string lastName,
            string email,
            string phone,
            string address,
            bool enable,
            byte userFunctionId,
            UserType userType
            
            )
        {
            var user = await _userHelper.GetUserByEmailAsync(email);
          //  var userFunction = await _dataContext.UserFunction.FirstAsync(u => u.Id.Equals(userFunctionId));
            if (user != null) return user;
            user = new UserEntity
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                UserName = email,
                PhoneNumber = phone,
                Address = address,
                Document = document,
                Enable=enable,
                //UserFunction= userFunction,
                UserType = userType
            };

            await _userHelper.AddUserAsync(user, "123456");
            await _userHelper.AddUserToRoleAsync(user, userType.ToString());

            return user;
        }


        #region Variables

        private async Task CheckComponetsAsync()
        {
            if (!_dataContext.Component.Any())
            {
                await _dataContext.Component.AddRangeAsync(
                    new ComponentEntity { Name = "FILTRO TRAMPA DE AGUA",Code="xxxx", CreatedDate = DateTime.UtcNow },
                    new ComponentEntity { Name = "FILTRO COMBUSTIBLE", Code = "xxxx", CreatedDate = DateTime.UtcNow },
                    new ComponentEntity { Name = "FILTRO DE ACEITE", Code = "xxxx", CreatedDate = DateTime.UtcNow },
                    new ComponentEntity { Name = "FILTRO ACEITE HIDRAULICO", Code = "xxxx", CreatedDate = DateTime.UtcNow },
                    new ComponentEntity { Name = "FILTRO DE AIRE", Code = "xxxx", CreatedDate = DateTime.UtcNow },
                    new ComponentEntity { Name = "FILTRO DE AIRE SECUNDARIO", Code = "xxxx", CreatedDate = DateTime.UtcNow },
                    new ComponentEntity { Name = "FILTRO COMBUSTIBLE PRIMARIO", Code = "xxxx", CreatedDate = DateTime.UtcNow },
                    new ComponentEntity { Name = "FILTRO COMBUSTIBLE SECUNDARIO", Code = "xxxx", CreatedDate = DateTime.UtcNow },
                    new ComponentEntity { Name = "FILTRO RACOR", Code = "xxxx", CreatedDate = DateTime.UtcNow },
                    new ComponentEntity { Name = "FILTRO SECADOR", Code = "xxxx", CreatedDate = DateTime.UtcNow }

                );
                await _dataContext.SaveChangesAsync();
            }
        }
        private async Task CheckVehiculeBrandAsync()
        {
            if (!_dataContext.VehicleBrand.Any())
            {
                await _dataContext.VehicleBrand.AddRangeAsync(
                    new VehicleBrandEntity { Name = "CHEVROLET", CreatedDate = DateTime.UtcNow },
                    new VehicleBrandEntity { Name = "KENWORTH", CreatedDate = DateTime.UtcNow },
                    new VehicleBrandEntity { Name = "HINO MOTORS", CreatedDate = DateTime.UtcNow },
                    new VehicleBrandEntity { Name = "TOYOTA", CreatedDate = DateTime.UtcNow },
                    new VehicleBrandEntity { Name = "MAZDA", CreatedDate = DateTime.UtcNow },
                    new VehicleBrandEntity { Name = "CATERPILLAR", CreatedDate = DateTime.UtcNow },
                    new VehicleBrandEntity { Name = "KOMATSU", CreatedDate = DateTime.UtcNow },
                    new VehicleBrandEntity { Name = "JOHN DEERE", CreatedDate = DateTime.UtcNow }

                );
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckVehiculeStatusAsync()
        {
            if (!_dataContext.VehicleStatus.Any())
            {
                await _dataContext.VehicleStatus.AddRangeAsync(
                    new VehicleStatusEntity { Name = "OPERATIVO", CreatedDate = DateTime.UtcNow },
                    new VehicleStatusEntity { Name = "DAÑADA", CreatedDate = DateTime.UtcNow },
                    new VehicleStatusEntity { Name = "PROCESO DE BAJA", CreatedDate = DateTime.UtcNow }
                );
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckVehiculeTypeAsync()
        {
            if (!_dataContext.VehicleType.Any())
            {
                await _dataContext.VehicleType.AddRangeAsync(
                    new VehicleTypeEntity { Name = "VOLQUETA", CreatedDate = DateTime.UtcNow },
                    new VehicleTypeEntity { Name = "BUS", CreatedDate = DateTime.UtcNow },
                    new VehicleTypeEntity { Name = "KABEZAL", CreatedDate = DateTime.UtcNow },
                    new VehicleTypeEntity { Name = "VEHICULO ESPECIAL", CreatedDate = DateTime.UtcNow },
                    new VehicleTypeEntity { Name = "ESCAVADORA", CreatedDate = DateTime.UtcNow },
                    new VehicleTypeEntity { Name = "TRACTOR", CreatedDate = DateTime.UtcNow },
                    new VehicleTypeEntity { Name = "MOTO NIVELADORA", CreatedDate = DateTime.UtcNow },
                    new VehicleTypeEntity { Name = "RODILLO", CreatedDate = DateTime.UtcNow }
                );
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckUserTypeAsync()
        {
            if (!_dataContext.UserFunction.Any())
            {
                await _dataContext.UserFunction.AddRangeAsync(
                    new UserFunctionEntity { Name = "SUPERVISOR DE MANTENIMIENTO MECÁNICO ", CreatedDate = DateTime.UtcNow },
                    new UserFunctionEntity { Name = "JEFE DE TALLERES", CreatedDate = DateTime.UtcNow },
                    new UserFunctionEntity { Name = "ASISTENTE DE SUPERVISION MTTO. MECÁNICO", CreatedDate = DateTime.UtcNow },
                    new UserFunctionEntity { Name = "MECÁNICO", CreatedDate = DateTime.UtcNow },
                    new UserFunctionEntity { Name = "AYUDANTE DE MECÁNICA", CreatedDate = DateTime.UtcNow },
                    new UserFunctionEntity { Name = "SOLDADOR", CreatedDate = DateTime.UtcNow },
                    new UserFunctionEntity { Name = "AYUDANTE SOLDADOR", CreatedDate = DateTime.UtcNow },
                    new UserFunctionEntity { Name = "VULCANIZADOR", CreatedDate = DateTime.UtcNow },
                    new UserFunctionEntity { Name = "AYUDANTE DE VUCANIZADOR", CreatedDate = DateTime.UtcNow }
                );
                await _dataContext.SaveChangesAsync();
            }
        }
        private async Task CheckFuelAsync()
        {
            if (!_dataContext.Fuel.Any())
            {
                await _dataContext.Fuel.AddRangeAsync(
                    new FuelEntity { Name = "EXTRA", CreatedDate = DateTime.UtcNow },
                    new FuelEntity { Name = "SUPER", CreatedDate = DateTime.UtcNow },
                    new FuelEntity { Name = "ECOPAIS", CreatedDate = DateTime.UtcNow },
                    new FuelEntity { Name = "DIESEL", CreatedDate = DateTime.UtcNow }
                );
                await _dataContext.SaveChangesAsync();

            }
        }
        private async Task CheckColorAsync()
        {
            if (!_dataContext.Color.Any())
            {
                await _dataContext.Color.AddRangeAsync(
                    new ColorEntity { Name = "ROJO", CreatedDate = DateTime.UtcNow },
                                new ColorEntity { Name = "NEGRO", CreatedDate = DateTime.UtcNow },
                                new ColorEntity { Name = "AZUL", CreatedDate = DateTime.UtcNow },
                                new ColorEntity { Name = "AMARILLO", CreatedDate = DateTime.UtcNow },
                                new ColorEntity { Name = "NARANJA", CreatedDate = DateTime.UtcNow },
                                new ColorEntity { Name = "BLANCO", CreatedDate = DateTime.UtcNow },
                                new ColorEntity { Name = "MORADO", CreatedDate = DateTime.UtcNow },
                                new ColorEntity { Name = "PLOMO", CreatedDate = DateTime.UtcNow },
                                new ColorEntity { Name = "MARRON", CreatedDate = DateTime.UtcNow }
                    );
                await _dataContext.SaveChangesAsync();

            }
        }

        private async Task CheckCountryAsync()
        {
            if (!_dataContext.Country.Any())
            {
                await _dataContext.Country.AddRangeAsync(
                    new CountryEntity { Name = "ECUADOR", CreatedDate = DateTime.UtcNow },
                    new CountryEntity { Name = "COLOMBIA", CreatedDate = DateTime.UtcNow },
                    new CountryEntity { Name = "CHINA", CreatedDate = DateTime.UtcNow },
                    new CountryEntity { Name = "JAPON", CreatedDate = DateTime.UtcNow },
                    new CountryEntity { Name = "TAILANDIA", CreatedDate = DateTime.UtcNow },
                    new CountryEntity { Name = "U.S.A", CreatedDate = DateTime.UtcNow }
                );
                await _dataContext.SaveChangesAsync();

            }
        }


        #endregion

        private async Task CheckVehicleAsync()
        {
            if (!_dataContext.Vehicle.Any())
            {
                var users = await _dataContext.Users.ToListAsync() ;
                var user = users.FirstOrDefault();
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
                        Name="XXX696",
                        Chassis="XDXDXDXD",
                        Year=2020,
                        MotorSerial="ASDFGHJKL",
                        Cylinder =1515,
                        CreatedDate = DateTime.UtcNow,
                        CreatedBy= user
                    }
                );
                await _dataContext.SaveChangesAsync();

            }
        }
    }
}
