using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MantenimientoVehiculos.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MantenimientoVehiculos.Web.Models
{
    public class VehicleMaintenanceDetailsViewModel: VehicleMaintenanceDetailEntity
    {
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Vehicle Maintenance")]
        [Range(1, long.MaxValue, ErrorMessage = "You must select a Vehicle Maintenance.")]
        public long VehicleMaintenanceId { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Vehicle Type")]
        [Range(1, byte.MaxValue, ErrorMessage = "You must select a Vehicle Type.")]
        public byte ComponentId { get; set; }

        public IEnumerable<SelectListItem> Components { get; set; }
    }
}
