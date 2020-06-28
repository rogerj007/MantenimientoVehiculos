using System;
using System.ComponentModel.DataAnnotations;

namespace MantenimientoVehiculos.Web.Data.Entities
{
    public class UserFunctionEntity: BaseEntity
    {
     

        [StringLength(50, MinimumLength = 5, ErrorMessage = "The {0} field must have {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string UserFunction { get; set; }

    }
}
