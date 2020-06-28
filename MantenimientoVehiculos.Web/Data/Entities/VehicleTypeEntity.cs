using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MantenimientoVehiculos.Web.Data.Entities
{
    public class VehicleTypeEntity:BaseEntity
    {

        

        [StringLength(25, MinimumLength = 5, ErrorMessage = "The {0} field must have {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string VehicleType { get; set; }

       public ICollection<VehicleEntity> Vehicles { get; set; }
    }
}
