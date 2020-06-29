using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MantenimientoVehiculos.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MantenimientoVehiculos.Web.Models
{
    public class VehicleRecordActivityViewModel: VehicleRecordActivityEntity
    {

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Vehicle")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a Vehicle ")]
        public int VehicleId { get; set; }

        public IEnumerable<SelectListItem> Vehicles { get; set; }
    }
}
