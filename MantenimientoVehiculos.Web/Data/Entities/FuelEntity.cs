using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MantenimientoVehiculos.Web.Data.Entities
{
    public class FuelEntity:BaseEntity
    {
     

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "The {0} field must have {1} characters.")]
        public string Fuel { get; set; }

       public ICollection<VehicleEntity> Vehicles { get; set; }

    }
}
