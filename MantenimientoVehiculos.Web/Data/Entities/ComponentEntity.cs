using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MantenimientoVehiculos.Web.Data.Entities.Base;

namespace MantenimientoVehiculos.Web.Data.Entities
{
    public class ComponentEntity: BaseEntity<short>
    {


        [Display(Name = "Component")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public override string Name { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [StringLength(25, MinimumLength = 4, ErrorMessage = "The {0} field must have {1} characters.")]
        public string Code { get; set; }

    }
}
