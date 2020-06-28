using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MantenimientoVehiculos.Web.Data.Entities
{
    public class VehicleStatusEntity: BaseEntity
    {
       
        [StringLength(15, MinimumLength = 4, ErrorMessage = "The {0} field must have {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string VehicleStatus { get; set; }

       public ICollection<VehicleEntity> Vehicles { get; set; }
    }
}
