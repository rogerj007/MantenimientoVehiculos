
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using MantenimientoVehiculos.Web.Data.Entities.Base;

namespace MantenimientoVehiculos.Web.Data.Entities
{
    public class VehicleRecordActivityEntity : BaseEntity<long>
    {

        [NotMapped]
        public override string Name { get; set; } = null;

        [Display(Name = "Km - Hours")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Range(1, int.MaxValue, ErrorMessage = "Register activity 'Km/Hours'")]
        public long KmHr { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public VehicleEntity Vehicle { get; set; }
    }
}
