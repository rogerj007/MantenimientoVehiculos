using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MantenimientoVehiculos.Web.Data.Entities.Base;
using MantenimientoVehiculos.Web.Resources;

namespace MantenimientoVehiculos.Web.Data.Entities
{
    public class VehicleEntity: BaseEntity<short>
    {

        [RegularExpression(@"^([A-Za-z]{3}\d{4})$", ErrorMessage = "The field {0} must starts with three characters and ends with numbers.")]
        [Column("Plaque")]
        [Display(Name = "Plaque")]
        [Required(ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = nameof(Language.Required_Message))]
        public override string Name { get; set; }

        [StringLength(25, MinimumLength = 4, ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = nameof(Language.MaxLength_Message))]
        [MaxLength(25)]
        [Required(ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = nameof(Language.Required_Message))]
        public string MotorSerial  { get; set; }

        [StringLength(25, MinimumLength = 4, ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = nameof(Language.MaxLength_Message))]
        [Required(ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = nameof(Language.Required_Message))]
        public string Chassis { get; set; }

        [Required(ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = nameof(Language.Required_Message))]
        [Range(1000, int.MaxValue, ErrorMessage = "Year must be from 1000")]
        public int Cylinder { get; set; }
        
        [Display(Name = "Release Date")]
        [Required(ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = nameof(Language.Required_Message))]
        [Range(1980, int.MaxValue, ErrorMessage = "Year must be from 2000")]
        public short Year { get; set; }

        //[Display(Name = "Vehicle Info")]
        //public string? VehicleInfoWithColor => $"{VehicleBrand.Name} {Name} - {Color.Name}";

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }
     
        public VehicleBrandEntity VehicleBrand { get; set; }
        public VehicleTypeEntity VehicleType { get; set; }
        public CountryEntity Country { get; set; }
        public FuelEntity Fuel { get; set; }
        public ColorEntity Color { get; set; }
        public VehicleStatusEntity VehicleStatus { get; set; }

        public ICollection<VehicleRecordActivityEntity> VehicleRecordActivities { get; set; }

        public ICollection<VehicleMaintenanceEntity> VehicleMaintenance { get; set; }

    }
}
