using System;
using System.Collections.Generic;
using System.Linq;
using MantenimientoVehiculos.Web.Data;
using MantenimientoVehiculos.Web.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MantenimientoVehiculos.Web.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _context;

        public CombosHelper(DataContext context)
        {
            _context = context;
        }

        #region Users

        public IEnumerable<SelectListItem> GetComboRoles(bool admin=false)
        {
            if(admin)
                return new List<SelectListItem>
                {
                    new SelectListItem { Value = "0", Text = "[Select a role...]" },
                    new SelectListItem { Value = "1", Text = "Admin" },
                    new SelectListItem { Value = "2", Text = "Supervisor" },
                    new SelectListItem { Value = "3", Text = "User" }
                };
            return new List<SelectListItem>
            {
                new SelectListItem { Value = "0", Text = "[Select a role...]" },
                new SelectListItem { Value = "1", Text = "Supervisor" },
                new SelectListItem { Value = "2", Text = "User" }
            };


        }

        public IEnumerable<SelectListItem> GetComboUserFuncion()
        {
            var list = _context.UserFunction.Select(t => new SelectListItem
                {
                    Text = t.Name,
                    Value = $"{t.Id}"
                })
                .OrderBy(t => t.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Select a User Funcion...]",
                Value = "0"
            });

            return list;
        }

        #endregion

        public IEnumerable<SelectListItem> GetComboListMaintenance()
        {
            //var list = (from MaintenanceType item in Enum.GetValues(typeof(MaintenanceType))
            //    select new SelectListItem
            //    {
            //        Value = item.ToString(),
            //        Text= (MaintenanceType)item.ToString()
            //        //  Text= ((MaintenanceType)item.ToString()).ToString()
            //    }).ToList();

            //list.Insert(0, new SelectListItem
            //{
            //    Text = "[Select a Maintenance...]",
            //    Value = "0"
            //});

            //return list;
            return new List<SelectListItem>
            {
                new SelectListItem { Value = "0", Text = "[Select a Maintenance...]" },
                new SelectListItem { Value = "1", Text = "Corrective" },
                new SelectListItem { Value = "2", Text = "Preventive" }
            };
        }

        public IEnumerable<SelectListItem> GetComboBrandVehicle()
        {
            var list = _context.VehicleBrand.Select(t => new SelectListItem
                {
                    Text = t.Name,
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
                    Text = t.Name,
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

        public IEnumerable<SelectListItem> GetComboVehicleType()
        {
            var list = _context.VehicleType.Select(t => new SelectListItem
                {
                    Text = t.Name,
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
                    Text = t.Name,
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
                    Text = t.Name,
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
                    Text = t.Name,
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

        public IEnumerable<SelectListItem> GetComboVehicles(bool operative=false)
        {
            List<SelectListItem> list;
            

            if(operative)
                list = _context.Vehicle
                    .Include(v=>v.VehicleStatus)
                    .Where(v => v.VehicleStatus.Id==1)
                    .Select(t => new SelectListItem
                    {
                        Text = t.Name,
                        Value = $"{t.Id}"
                    })
                    
                    .OrderBy(t => t.Text)
                    .ToList();
            else
                list = _context.Vehicle.Select(t => new SelectListItem
                {
                    Text = t.Name,
                    Value = $"{t.Id}"
                })
                .OrderBy(t => t.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Select a Vehicle...]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboComponets()
        {
            var list = _context.Component.Select(t => new SelectListItem
                {
                    Text = t.Name,
                    Value = $"{t.Id}"
                })
                .OrderBy(t => t.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Select a Component...]",
                Value = "0"
            });

            return list;
        }

    }

}