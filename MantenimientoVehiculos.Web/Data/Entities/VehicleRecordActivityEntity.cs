
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using MantenimientoVehiculos.Web.Data.Entities.Base;
using MantenimientoVehiculos.Web.Resources;

namespace MantenimientoVehiculos.Web.Data.Entities
{
    public class VehicleRecordActivityEntity : BaseEntity<long>
    {

        [NotMapped]
        public override string Name { get; set; } = null;

        [Display(Name = "Km - Hours")]
        //[Required(ErrorMessage = "The field {0} is mandatory.")]
        [Required(ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = nameof(Language.Required_Message))]
        [Range(1, int.MaxValue, ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = nameof(Language.RegisterActivity))]
        public long KmHr { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public VehicleEntity Vehicle { get; set; }
    }
}
