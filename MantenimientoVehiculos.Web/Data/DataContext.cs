using MantenimientoVehiculos.Web.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MantenimientoVehiculos.Web.Data
{
    public class DataContext :  IdentityDbContext<UserEntity>//DbContext
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

            builder.Entity<UserFunctionEntity>()
                .HasIndex(t => t.UserFunction)
                .IsUnique();

            builder.Entity<VehicleTypeEntity>()
                .HasIndex(t => t.VehicleType)
                .IsUnique();

            builder.Entity<VehicleBrandEntity>()
                .HasIndex(t => t.VehicleBrand)
                .IsUnique();

            builder.Entity<VehicleStatusEntity>()
                .HasIndex(t => t.VehicleStatus)
                .IsUnique();
        }


        public DbSet<ColorEntity> Color { get; set; }
        public DbSet<CountryEntity> Country { get; set; }
        public DbSet<FuelEntity> Fuel { get; set; }
        public DbSet<UserFunctionEntity> UserFunction { get; set; }
        public DbSet<VehicleTypeEntity> VehicleType { get; set; }
        public DbSet<VehicleBrandEntity> VehicleBrand { get; set; }
        public DbSet<VehicleEntity> Vehicle { get; set; }
        public DbSet<VehicleStatusEntity> VehicleStatus { get; set; }
        public DbSet<ComponentEntity> Component { get; set; }
        public DbSet<VehicleMaintenanceEntity> VehicleMaintenance { get; set; }
        public DbSet<VehicleMaintenanceDetailEntity> VehicleMaintenanceDetail { get; set; }
        public DbSet<VehicleRecordActivityEntity> VehicleRecordActivities { get; set; }
    }
}
