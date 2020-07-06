using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MantenimientoVehiculos.Web.Data.Entities.Base;

namespace MantenimientoVehiculos.Web.Data.Entities
{
    public class CountryEntity : BaseEntity<byte>
    {


        [Display(Name = "Country")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public override string Name { get; set; }
        public ICollection<VehicleEntity> Vehicles { get; set; }
    }
}
