using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MantenimientoVehiculos.Web.Data.Entities.Base;
using MantenimientoVehiculos.Web.Resources;

namespace MantenimientoVehiculos.Web.Data.Entities
{
    public class CountryEntity : BaseEntity<byte>
    {


        [Display(Name = "Country")]
        //[Required(ErrorMessage = "The field {0} is mandatory.")]
        [Required(ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = nameof(Language.Required_Message))]
        [StringLength(50, MinimumLength = 2, ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = nameof(Language.MaxLength_Message))]
        [MaxLength(50)]
        public override string Name { get; set; }
        public ICollection<VehicleEntity> Vehicles { get; set; }
    }
}
