using System.Collections.Generic;
using System.Linq;
using MantenimientoVehiculos.Web.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MantenimientoVehiculos.Web.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _context;

        public CombosHelper(DataContext context)
        {
            _context = context;
        }
        public IEnumerable<SelectListItem> GetComboBrandVehicle()
        {
            var list = _context.VehicleBrand.Select(t => new SelectListItem
                {
                    Text = t.VehicleBrand,
                    Value = $"{t.Id}"
                })
                .OrderBy(t => t.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Select a Vehicle Brand...]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboColor()
        {
            var list = _context.Color.Select(t => new SelectListItem
                {
                    Text = t.Color,
                    Value = $"{t.Id}"
                })
                .OrderBy(t => t.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Select a Color...]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboRoles()
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem { Value = "0", Text = "[Select a role...]" },
                new SelectListItem { Value = "1", Text = "Supervisor" },
                new SelectListItem { Value = "2", Text = "User" }
            };

            return list;
        }

        public IEnumerable<SelectListItem> GetComboVehicleType()
        {
            var list = _context.VehicleType.Select(t => new SelectListItem
                {
                    Text = t.VehicleType,
                    Value = $"{t.Id}"
                })
                .OrderBy(t => t.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Select a Type Vehicle...]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboFuel()
        {
            var list = _context.Fuel.Select(t => new SelectListItem
                {
                    Text = t.Fuel,
                    Value = $"{t.Id}"
                })
                .OrderBy(t => t.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Select a Fuel...]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboVehicleStatus()
        {
            var list = _context.VehicleStatus.Select(t => new SelectListItem
                {
                    Text = t.VehicleStatus,
                    Value = $"{t.Id}"
                })
                .OrderBy(t => t.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Select a Vehicle Status...]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboCountry()
        {
            var list = _context.Country.Select(t => new SelectListItem
                {
                    Text = t.Country,
                    Value = $"{t.Id}"
                })
                .OrderBy(t => t.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Select a Country...]",
                Value = "0"
            });

            return list;
        }
    }

}