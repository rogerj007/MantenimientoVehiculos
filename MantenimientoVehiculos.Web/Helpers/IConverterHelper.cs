using System.Threading.Tasks;
using MantenimientoVehiculos.Web.Data.Entities;
using MantenimientoVehiculos.Web.Models;

namespace MantenimientoVehiculos.Web.Helpers
{
    public interface IConverterHelper
    {
        Task<VehicleEntity> ToVehicleAsync(VehicleViewModel model, string path);

        VehicleViewModel ToVehicleViewModel(VehicleEntity vehicle);

        Task<VehicleRecordActivityEntity> ToVehicleRecordActivityAsync(VehicleRecordActivityViewModel model);

        VehicleRecordActivityViewModel ToVehicleRecordActivityViewModel(VehicleRecordActivityEntity vehicle);


        Task<VehicleMaintenanceEntity> ToVehicleMaintenanceAsync(VehicleMaintenanceViewModel model);

        VehicleMaintenanceViewModel ToVehicleMaintenanceViewModel(VehicleMaintenanceEntity model);


        Task<UserEntity> ToUserAsync(EditListUserViewModel model,string path);

        EditListUserViewModel ToEditListUserViewModel(UserEntity model);
    }
}