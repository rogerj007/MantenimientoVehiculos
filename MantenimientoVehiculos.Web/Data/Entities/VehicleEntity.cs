using System;
using System.ComponentModel.DataAnnotations;

namespace MantenimientoVehiculos.Web.Data.Entities
{
    public class VehicleEntity
    {
        public int Id { get; set; }


        [StringLength(8, MinimumLength = 7, ErrorMessage = "The {0} field must have {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Plaque { get; set; }

        [StringLength(25, MinimumLength = 4, ErrorMessage = "The {0} field must have {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string MotorSerial  { get; set; }

        [StringLength(25, MinimumLength = 6, ErrorMessage = "The {0} field must have {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Chassis { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public int Cylinder { get; set; }
        
        [Display(Name = "Release Date")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Range(2000, int.MaxValue, ErrorMessage = "Year must be from 2000")]
        public short Year { get; set; }


        [DataType(DataType.DateTime)]
        [Display(Name = "Creation Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm}", ApplyFormatInEditMode = false)]
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public DateTime CreationDateLocal => CreationDate.ToLocalTime();


        [DataType(DataType.DateTime)]
        [Display(Name = "Modification Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm}", ApplyFormatInEditMode = false)]
        public DateTime? ModificationDate { get; set; }
        public DateTime? ModificationDateLocal => ModificationDate?.ToLocalTime();



        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public VehicleBrandEntity VehicleBrand { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public TypeVehicleEntity TypeVehicle { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public CountryEntity Country { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public FuelEntity Fuel { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public ColorEntity Color { get; set; }

        

    }
}
