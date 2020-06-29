using System.Threading.Tasks;
using MantenimientoVehiculos.Web.Data.Entities;
using MantenimientoVehiculos.Web.Models;

namespace MantenimientoVehiculos.Web.Helpers
{
    public interface IConverterHelper
    {
        Task<VehicleEntity> ToVehicleAsync(VehicleViewModel model, string path, bool isNew);

        VehicleViewModel ToVehicleViewModel(VehicleEntity vehicle);

        Task<VehicleRecordActivityEntity> ToVehicleRecordActivityAsync(VehicleRecordActivityViewModel model, bool isNew);

        VehicleRecordActivityViewModel ToVehicleRecordActivityViewModel(VehicleRecordActivityEntity vehicle);
    }
}