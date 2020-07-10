using System.ComponentModel.DataAnnotations.Schema;
using MantenimientoVehiculos.Web.Data.Entities.Base;

namespace MantenimientoVehiculos.Web.Data.Entities
{
    public class VehicleMaintenanceDetailEntity : BaseEntity<long>
    {
        [NotMapped]
        public override string Name { get; set; }
        public ComponentEntity Component { get; set; }

        public VehicleMaintenanceEntity VehicleMaintenance { get; set; }
}
}
