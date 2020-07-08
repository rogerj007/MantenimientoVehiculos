using MantenimientoVehiculos.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MantenimientoVehiculos.Web.Models
{
    public class VehicleMaintenanceViewModel:VehicleMaintenanceEntity
    {

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Vehicle")]
        [Range(1, short.MaxValue, ErrorMessage = "You must select a Vehicle.")]
        public short VehicleId { get; set; }


        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Maintenance Type")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a Maintenance Type.")]
        public int MaintenanceTypeId { get; set; }


        public IEnumerable<SelectListItem> ListVehicles { get; set; }

        public IEnumerable<SelectListItem> ListMaintenanceType { get; set; }

    }
}
