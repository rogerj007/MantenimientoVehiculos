using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MantenimientoVehiculos.Web.Data.Entities
{
    public class ColorEntity: BaseEntity
    {
    
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Color { get; set; }

        public ICollection<VehicleEntity> Vehicles { get; set; }
    }
}
