using System.ComponentModel.DataAnnotations;
using MantenimientoVehiculos.Web.Data.Entities.Base;
using MantenimientoVehiculos.Web.Resources;

namespace MantenimientoVehiculos.Web.Data.Entities
{
    public class ComponentEntity: BaseEntity<short>
    {


        [Display(Name = "Component")]
        [Required(ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = nameof(Language.Required_Message))]
        [StringLength(25, MinimumLength = 4, ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = nameof(Language.MaxLength_Message))]
        [MaxLength(100)]
        public override string Name { get; set; }

       
        [Required(ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = nameof(Language.Required_Message))]
        [StringLength(25, MinimumLength = 4, ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = nameof(Language.MaxLength_Message))]
        [MaxLength(25)]
        public string Code { get; set; }

    }
}
