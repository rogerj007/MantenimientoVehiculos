using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MantenimientoVehiculos.Web.Data.Entities
{
    public class ColorEntity: BaseEntity
    {
    
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "The {0} field must have {1} characters.")]
        public string Color { get; set; }

        public ICollection<VehicleEntity> Vehicles { get; set; }
    }
}
