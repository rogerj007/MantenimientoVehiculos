using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MantenimientoVehiculos.Web.Enums;

namespace MantenimientoVehiculos.Web.Data.Entities
{
    public class VehicleMaintenanceEntity: BaseEntity
    {

        public ICollection<VehicleEntity> Vehicles { get; set; }

        [Display(Name = "Maintenance Date")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime MaintenanceDate { get; set; }

        [Display(Name = "Maintenance Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime MaintenanceDatenLocal => MaintenanceDate.ToLocalTime();

        [Display(Name = "Km - Hours")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Range(1, int.MaxValue, ErrorMessage = "Register activity 'Km/Hours'")]
        public long KmHrMaintenance { get; set; }



        [Display(Name = "User Type")]
        public MaintenanceType MaintenanceType { get; set; }
        public UserEntity User { get; set; }

        public ICollection<VehicleMaintenanceDetailEntity> VehicleMaintenanceDetail { get; set; }

    }
}
