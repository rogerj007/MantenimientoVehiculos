using System;
using System.ComponentModel.DataAnnotations;

namespace MantenimientoVehiculos.Web.Data.Entities
{
    public class VehicleMaintenance: BaseEntity
    {
        

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Color { get; set; }

    }
}
