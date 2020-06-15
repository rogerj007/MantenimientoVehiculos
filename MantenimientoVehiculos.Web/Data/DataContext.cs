using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MantenimientoVehiculos.Web.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MantenimientoVehiculos.Web.Data
{
    public class DataContext :IdentityUserContext<UserEntity> //DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ColorEntity>()
                .HasIndex(t => t.Color)
                .IsUnique();

            builder.Entity<CountryEntity>()
                .HasIndex(t => t.Country)
                .IsUnique();

            builder.Entity<FuelEntity>()
                .HasIndex(t => t.Fuel)
                .IsUnique();

            builder.Entity<UserTypeEntity>()
                .HasIndex(t => t.UserType)
                .IsUnique();

            builder.Entity<TypeVehicleEntity>()
                .HasIndex(t => t.TypeVehicle)
                .IsUnique();

            builder.Entity<VehicleBrandEntity>()
                .HasIndex(t => t.VehicleBrand)
                .IsUnique();

            builder.Entity<VehicleStatusEntity>()
                .HasIndex(t => t.VehicleStatus)
                .IsUnique();
        }


        public DbSet<ColorEntity> Colors { get; set; }
        public DbSet<CountryEntity> Country { get; set; }
        public DbSet<FuelEntity> Fuel { get; set; }
        public DbSet<UserTypeEntity> UserType { get; set; }
        public DbSet<TypeVehicleEntity> TypeVehicle { get; set; }
        public DbSet<VehicleBrandEntity> VehicleBrand { get; set; }
        public DbSet<VehicleEntity> Vehicle { get; set; }
        public DbSet<VehicleStatusEntity> VehicleStatus { get; set; }
    }
}
