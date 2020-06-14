using System;
using System.ComponentModel.DataAnnotations;

namespace MantenimientoVehiculos.Web.Data.Entities
{
    public class FuelEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "The {0} field must have {1} characters.")]
        public string Fuel { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Creation date")]
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;

        public DateTime CreationDateLocal => CreationDate.ToLocalTime();


        [DataType(DataType.DateTime)]
        [Display(Name = "Modification date")]
        public DateTime? ModificationDate { get; set; }

        public DateTime? ModificationDateLocal => ModificationDate?.ToLocalTime();

    }
}
