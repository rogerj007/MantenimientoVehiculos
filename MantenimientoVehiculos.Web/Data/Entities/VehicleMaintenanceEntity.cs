using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MantenimientoVehiculos.Web.Data.Entities.Base;
using MantenimientoVehiculos.Web.Enums;
using MantenimientoVehiculos.Web.Resources;

namespace MantenimientoVehiculos.Web.Data.Entities
{
    public class VehicleMaintenanceEntity: BaseEntity<long>
    {
        [NotMapped]
        public override string Name { get; set; }

        [Display(Name = "Maintenance Date"), DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = nameof(Language.Required_Message))]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime MaintenanceDate { get; set; }

        [Display(Name = "Maintenance Date"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
    
        public DateTime MaintenanceDatenLocal => MaintenanceDate.ToLocalTime();

        [Display(Name = "Km - Hours")]
        //[Required(ErrorMessage = "The field {0} is mandatory.")]
        [Required(ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = nameof(Language.Required_Message))]
        [Range(1, int.MaxValue, ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName= nameof(Language.RegisterActivity))]
        public long KmHrMaintenance { get; set; }

        [Display(Name = "Vehicle")]
        public VehicleEntity Vehicle { get; set; }

        [Display(Name = "Maintenance Type")]
        public MaintenanceType MaintenanceType { get; set; }

        public bool Complete { get; set; }

        public ICollection<VehicleMaintenanceDetailEntity> VehicleMaintenanceDetail { get; set; }

    }
}
