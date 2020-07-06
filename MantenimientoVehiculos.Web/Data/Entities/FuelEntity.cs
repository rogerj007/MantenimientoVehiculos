using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MantenimientoVehiculos.Web.Data.Entities.Base;

namespace MantenimientoVehiculos.Web.Data.Entities
{
    public class FuelEntity:BaseEntity<byte>
    {

        [Display(Name = "Fuel")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public override string Name { get; set; }


        public ICollection<VehicleEntity> Vehicles { get; set; }

    }
}
