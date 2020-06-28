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
        public string Country { get; set; }

        public ICollection<VehicleEntity> Vehicles { get; set; }
    }
}
