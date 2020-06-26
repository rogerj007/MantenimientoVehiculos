using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MantenimientoVehiculos.Web.Helpers
{
    public interface ICombosHelper
    {
        IEnumerable<SelectListItem> GetComboRoles();

        IEnumerable<SelectListItem> GetComboColor();

        IEnumerable<SelectListItem> GetComboBrandVehicle();

        IEnumerable<SelectListItem> GetComboTypeVehicle();

        IEnumerable<SelectListItem> GetComboFuel();

        IEnumerable<SelectListItem> GetComboVehicleStatus();

        IEnumerable<SelectListItem> GetComboCountry();
    }
}