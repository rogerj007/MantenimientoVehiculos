﻿using MantenimientoVehiculos.Web.Data.Entities;
using System;
using System.Collections.Generic;
//using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace MantenimientoVehiculos.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _dataContext;

        public SeedDb(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task SeedAsync()
        {
            await _dataContext.Database.EnsureCreatedAsync();
            await CheckCountryAsync();
            await CheckColorAsync();
            await CheckFuelAsync();
            await CheckJobTitleAsync();
            await CheckTypeVehiculeAsync();
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

        private async Task CheckJobTitleAsync()
        {
            if (!_dataContext.UserType.Any())
            {
                await _dataContext.UserType.AddRangeAsync(
                    new UserTypeEntity { UserType = "SUPERVISOR DE MANTENIMIENTO MECÁNICO ",CreationDate=DateTime.UtcNow },
                    new UserTypeEntity { UserType = "JEFE DE TALLERES", CreationDate = DateTime.UtcNow },
                    new UserTypeEntity { UserType = "ASISTENTE DE SUPERVISION MTTO. MECÁNICO", CreationDate = DateTime.UtcNow },
                    new UserTypeEntity { UserType = "MECÁNICO", CreationDate = DateTime.UtcNow },
                    new UserTypeEntity { UserType = "AYUDANTE DE MECÁNICA", CreationDate = DateTime.UtcNow },
                    new UserTypeEntity { UserType = "SOLDADOR", CreationDate = DateTime.UtcNow },
                    new UserTypeEntity { UserType = "AAYUDANTE SOLDADOR", CreationDate = DateTime.UtcNow },
                    new UserTypeEntity { UserType = "VULCANIZADOR", CreationDate = DateTime.UtcNow },
                    new UserTypeEntity { UserType = "AYUDANTE DE VUCANIZADOR", CreationDate = DateTime.UtcNow }
                );
                await _dataContext.SaveChangesAsync();
            }
        }
        private async Task CheckFuelAsync()
        {
            if (!_dataContext.Fuel.Any())
            {
                await _dataContext.Fuel.AddRangeAsync(
                    new FuelEntity { Fuel = "EXTRA" ,CreationDate = DateTime.UtcNow },
                    new FuelEntity { Fuel = "SUPER" , CreationDate = DateTime.UtcNow },
                    new FuelEntity { Fuel = "ECOPAIS" , CreationDate = DateTime.UtcNow },
                    new FuelEntity { Fuel = "DIESEL", CreationDate = DateTime.UtcNow }
                );
                await _dataContext.SaveChangesAsync();

            }
        }
        private async Task CheckColorAsync()
        {
            if (!_dataContext.Colors.Any())
            {
                await _dataContext.Colors.AddRangeAsync(
                    new ColorEntity {Color = "RED", CreationDate = DateTime.UtcNow },
                                new ColorEntity {Color = "BLACK", CreationDate = DateTime.UtcNow }, 
                                new ColorEntity {Color = "BLUE", CreationDate = DateTime.UtcNow },
                                new ColorEntity { Color = "YELLOW" , CreationDate = DateTime.UtcNow }
                    );
                await _dataContext.SaveChangesAsync();

            }
        }

        private async Task CheckCountryAsync()
        {
            if (!_dataContext.Country.Any())
            {
                await _dataContext.Country.AddRangeAsync(
                    new CountryEntity { Country = "ECUADOR" , CreationDate = DateTime.UtcNow },
                    new CountryEntity { Country = "COLOMBIA", CreationDate = DateTime.UtcNow },
                    new CountryEntity { Country = "CHINA", CreationDate = DateTime.UtcNow },
                    new CountryEntity { Country = "JAPON", CreationDate = DateTime.UtcNow },
                    new CountryEntity { Country = "TAILANDIA", CreationDate = DateTime.UtcNow },
                    new CountryEntity { Country = "U.S.A", CreationDate = DateTime.UtcNow }
                );
                await _dataContext.SaveChangesAsync();

            }
        }
    }
}
