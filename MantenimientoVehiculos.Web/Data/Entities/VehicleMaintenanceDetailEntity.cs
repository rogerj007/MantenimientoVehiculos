using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MantenimientoVehiculos.Web.Data.Entities
{
    public class VehicleMaintenanceDetailEntity : BaseEntity
    {
        public VehicleMaintenanceEntity Trip { get; set; }

        public ComponentEntity Component { get; set; }

    }
}
