using System.ComponentModel.DataAnnotations;

namespace MantenimientoVehiculos.Web.Enums
{
    public enum MaintenanceType
    {
        [Display(Name = "Corrective")]
        Corrective =1,
        [Display(Name = "Preventive")]
        Preventive =2
    }
}
