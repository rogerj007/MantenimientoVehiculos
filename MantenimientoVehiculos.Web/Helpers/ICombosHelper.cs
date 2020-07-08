using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MantenimientoVehiculos.Web.Helpers
{
    public interface ICombosHelper
    {
        IEnumerable<SelectListItem> GetComboRoles(bool admin=false);

        IEnumerable<SelectListItem> GetComboUserFuncion();

        IEnumerable<SelectListItem> GetComboColor();

        IEnumerable<SelectListItem> GetComboBrandVehicle();

        IEnumerable<SelectListItem> GetComboVehicleType();

        IEnumerable<SelectListItem> GetComboFuel();

        IEnumerable<SelectListItem> GetComboVehicleStatus();

        IEnumerable<SelectListItem> GetComboCountry();

        IEnumerable<SelectListItem> GetComboVehicles(bool operative=false);

        IEnumerable<SelectListItem> GetComboListMaintenance();

        IEnumerable<SelectListItem> GetComboComponets();
    }
}