using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MantenimientoVehiculos.Web.Data.Entities
{
    public class VehicleRecordActivityEntity : BaseEntity
    {
        [Display(Name = "Km - Hours")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Range(1, int.MaxValue, ErrorMessage = "Register activity 'Km/Hours'")]
        public long KmHr { get; set; }
        public UserEntity User { get; set; }
        public VehicleEntity Vehicle { get; set; }
    }
}
