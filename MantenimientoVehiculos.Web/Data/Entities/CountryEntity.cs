using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MantenimientoVehiculos.Web.Data.Entities
{
    public class CountryEntity : BaseEntity
    {
      
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "The {0} field must have {1} characters.")]
        public string Country { get; set; }

        public ICollection<VehicleEntity> Vehicles { get; set; }
    }
}
