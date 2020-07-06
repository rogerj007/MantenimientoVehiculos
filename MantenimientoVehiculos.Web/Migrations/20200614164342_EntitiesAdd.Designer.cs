﻿// <auto-generated />
using System;
using MantenimientoVehiculos.Web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MantenimientoVehiculos.Web.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200614164342_EntitiesAdd")]
    partial class EntitiesAdd
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MantenimientoVehiculos.Web.Data.Entities.ColorEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Color")
                        .IsRequired();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime?>("ModifiedDate");

                    b.HasKey("Id");

                    b.HasIndex("Color")
                        .IsUnique();

                    b.ToTable("Colors");
                });

            modelBuilder.Entity("MantenimientoVehiculos.Web.Data.Entities.CountryEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Country")
                        .IsRequired();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime?>("ModifiedDate");

                    b.HasKey("Id");

                    b.HasIndex("Country")
                        .IsUnique();

                    b.ToTable("Country");
                });

            modelBuilder.Entity("MantenimientoVehiculos.Web.Data.Entities.FuelEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Fuel")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<DateTime?>("ModifiedDate");

                    b.HasKey("Id");

                    b.ToTable("Fuel");
                });

            modelBuilder.Entity("MantenimientoVehiculos.Web.Data.Entities.JobTitleEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("JobTitle")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.Property<DateTime?>("ModifiedDate");

                    b.HasKey("Id");

                    b.ToTable("JobTitle");
                });

            modelBuilder.Entity("MantenimientoVehiculos.Web.Data.Entities.VehicleTypeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("VehicleType")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.HasKey("Id");

                    b.ToTable("VehicleType");
                });

            modelBuilder.Entity("MantenimientoVehiculos.Web.Data.Entities.VehicleBrandEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("VehicleBrand")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.HasKey("Id");

                    b.ToTable("VehicleBrand");
                });

            modelBuilder.Entity("MantenimientoVehiculos.Web.Data.Entities.VehicleEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Chassis")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.Property<int>("ColorId");

                    b.Property<int>("CountryId");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("Cylinder");

                    b.Property<int>("FuelId");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("MotorSerial")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.Property<string>("Plaque")
                        .IsRequired()
                        .HasMaxLength(8);

                    b.Property<int>("VehicleTypeId");

                    b.Property<int>("VehicleBrandId");

                    b.Property<short>("Year");

                    b.HasKey("Id");

                    b.HasIndex("ColorId");

                    b.HasIndex("CountryId");

                    b.HasIndex("FuelId");

                    b.HasIndex("VehicleTypeId");

                    b.HasIndex("VehicleBrandId");

                    b.ToTable("Vehicle");
                });

            modelBuilder.Entity("MantenimientoVehiculos.Web.Data.Entities.VehicleStatusEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("VehicleStatus")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.HasKey("Id");

                    b.ToTable("VehicleStatus");
                });

            modelBuilder.Entity("MantenimientoVehiculos.Web.Data.Entities.VehicleEntity", b =>
                {
                    b.HasOne("MantenimientoVehiculos.Web.Data.Entities.ColorEntity", "Color")
                        .WithMany()
                        .HasForeignKey("ColorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MantenimientoVehiculos.Web.Data.Entities.CountryEntity", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MantenimientoVehiculos.Web.Data.Entities.FuelEntity", "Fuel")
                        .WithMany()
                        .HasForeignKey("FuelId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MantenimientoVehiculos.Web.Data.Entities.VehicleTypeEntity", "VehicleType")
                        .WithMany()
                        .HasForeignKey("VehicleTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MantenimientoVehiculos.Web.Data.Entities.VehicleBrandEntity", "VehicleBrand")
                        .WithMany()
                        .HasForeignKey("VehicleBrandId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
