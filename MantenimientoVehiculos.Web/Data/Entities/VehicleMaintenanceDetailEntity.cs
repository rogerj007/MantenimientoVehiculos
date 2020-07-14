using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MantenimientoVehiculos.Web.Data.Entities.Base;
using MantenimientoVehiculos.Web.Resources;

namespace MantenimientoVehiculos.Web.Data.Entities
{
    public class VehicleMaintenanceDetailEntity : BaseEntity<long>
    {
        [NotMapped]
        public override string Name { get; set; }
       
        public ComponentEntity Component { get; set; }

        [Display(Name = "NextChangeKmHr", ResourceType = typeof(Language))]
        [Required(ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = nameof(Language.Required_Message))]
        public long NextChangeKmHr { get; set; }

        [Display(Name = "ExecutedNextChange", ResourceType = typeof(Language))]
        [Required(ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = nameof(Language.Required_Message))]
        public bool ExecutedNextChange { get; set; }

        public VehicleMaintenanceEntity VehicleMaintenance { get; set; }
}
}
