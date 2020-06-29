using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MantenimientoVehiculos.Web.Data.Entities
{
    public class VehicleEntity: BaseEntity
    {
      


        [StringLength(7, MinimumLength = 7, ErrorMessage = "The {0} field must have {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [RegularExpression(@"^([A-Za-z]{3}\d{4})$", ErrorMessage = "The field {0} must starts with three characters and ends with numbers.")]
        public string Plaque { get; set; }

        [StringLength(25, MinimumLength = 4, ErrorMessage = "The {0} field must have {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string MotorSerial  { get; set; }

        [StringLength(25, MinimumLength = 6, ErrorMessage = "The {0} field must have {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Chassis { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Range(1000, int.MaxValue, ErrorMessage = "Year must be from 1000")]
        public int Cylinder { get; set; }
        
        [Display(Name = "Release Date")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Range(1980, int.MaxValue, ErrorMessage = "Year must be from 2000")]
        public short Year { get; set; }

        [Display(Name = "Vehicle Info")]
        public string VehicleInfoWithColor => $"{VehicleBrand.VehicleBrand} {Plaque} - {Color.Color}";

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }
     
        public VehicleBrandEntity VehicleBrand { get; set; }
        public VehicleTypeEntity VehicleType { get; set; }
        public CountryEntity Country { get; set; }
        public FuelEntity Fuel { get; set; }
        public ColorEntity Color { get; set; }
        public VehicleStatusEntity VehicleStatus { get; set; }

        public ICollection<VehicleRecordActivityEntity> VehicleRecordActivities { get; set; }
        

    }
}
