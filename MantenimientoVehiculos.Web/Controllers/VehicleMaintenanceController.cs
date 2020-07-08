using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MantenimientoVehiculos.Web.Data;
using MantenimientoVehiculos.Web.Data.Entities;
using MantenimientoVehiculos.Web.Enums;
using MantenimientoVehiculos.Web.Helpers;
using MantenimientoVehiculos.Web.Models;

namespace MantenimientoVehiculos.Web.Controllers
{
    public class VehicleMaintenanceController : Controller
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IUserHelper _userHelper;

        public VehicleMaintenanceController(DataContext context,
                                            ICombosHelper combosHelper,
                                            IConverterHelper converterHelper,
                                            IUserHelper userHelper)
        {
            _context = context;
            _combosHelper = combosHelper;
            _converterHelper = converterHelper;
            _userHelper = userHelper;
        }

        // GET: VehicleMaintenance
        public async Task<IActionResult> Index()
        {

            var user = await _userHelper.GetUserAsync(User.Identity.Name);
            var isAdmin = await _userHelper.IsUserInRoleAsync(user, "Admin");
            List<VehicleMaintenanceEntity> mantence;
            if (isAdmin)
                mantence = await _context.VehicleMaintenance
                                        .Include(v => v.Vehicle)
                                        .Include(v => v.VehicleMaintenanceDetail)
                                        .ToListAsync();
            else
                mantence = await _context.VehicleMaintenance
                                        .Include(v => v.Vehicle)
                                        .Include(v => v.VehicleMaintenanceDetail)
                                        .Where(u => u.CreatedBy == user)
                                        .ToListAsync();

            return View(mantence);
        }

        // GET: VehicleMaintenance/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleMaintenanceEntity = await _context.VehicleMaintenance
                                        .Include(m => m.VehicleMaintenanceDetail)
                                        .ThenInclude(v => v.Component)
                                        .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicleMaintenanceEntity == null)
            {
                return NotFound();
            }

            return View(vehicleMaintenanceEntity);
        }

        // GET: VehicleMaintenance/Create
        public IActionResult Create()
        {
            var model = new VehicleMaintenanceViewModel
            {
                MaintenanceDate = DateTime.Today,
                CreatedDate = DateTime.UtcNow,
                ListMaintenanceType = _combosHelper.GetComboListMaintenance(),
                ListVehicles = _combosHelper.GetComboVehicles()
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleMaintenanceViewModel model)
        {
            if (ModelState.IsValid)
            {

                var vehicleMantence = await _converterHelper.ToVehicleMaintenanceAsync(model);
                var user = await _userHelper.GetUserAsync(User.Identity.Name);
                vehicleMantence.CreatedDate = DateTime.UtcNow;
                vehicleMantence.CreatedBy = user;
                _context.Add(vehicleMantence);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }



            return View(model);
        }

        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleMaintenanceEntity = await _context.VehicleMaintenance
                                                    .Include(v=>v.MaintenanceType)
                                                    .Include(v=>v.Vehicle)
                                                    .FirstOrDefaultAsync(p => p.Id == id.Value);
              

            if (vehicleMaintenanceEntity == null)
            {
                return NotFound();
            }
        
            //ListMaintenanceType = _combosHelper.GetComboListMaintenance();
            //ListVehicles = _combosHelper.GetComboVehicles();

            return View(vehicleMaintenanceEntity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, VehicleMaintenanceViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var vehicleMantence = await _converterHelper.ToVehicleMaintenanceAsync(model);
                    var user = await _userHelper.GetUserAsync(User.Identity.Name);
                    vehicleMantence.ModifiedDate = DateTime.UtcNow;
                    vehicleMantence.ModifiedBy = user;
                    _context.Update(vehicleMantence);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleMaintenanceEntityExists(model.Id))
                    {
                        return NotFound();
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            model.ListVehicles = _combosHelper.GetComboVehicles();

            return View(model);
        }

        // GET: VehicleMaintenance/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleMaintenanceEntity = await _context.VehicleMaintenance
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicleMaintenanceEntity == null)
            {
                return NotFound();
            }
            _context.VehicleMaintenance.Remove(vehicleMaintenanceEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: VehicleMaintenance/DeleteComponent/5
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteComponent(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleMaintenanceEntity = await _context.VehicleMaintenanceDetail
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicleMaintenanceEntity == null)
            {
                return NotFound();
            }
            _context.Remove(vehicleMaintenanceEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AddComponent(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintenanceEntity = await _context.VehicleMaintenance.FindAsync(id);
            if (maintenanceEntity == null)
            {
                return NotFound();
            }

            var model = new VehicleMaintenanceDetailsViewModel
            {
                VehicleMaintenanceId = maintenanceEntity.Id,
                Components = _combosHelper.GetComboComponets()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComponent(VehicleMaintenanceDetailsViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var groupEntity = await _converterHelper.ToGroupEntityAsync(model, true);
                //_context.Add(groupEntity);
                await _context.SaveChangesAsync();
                //return RedirectToAction($"{nameof(Details)}/{model.TournamentId}");
            }

            return View(model);
        }




        private bool VehicleMaintenanceEntityExists(long id)
        {
            return _context.VehicleMaintenance.Any(e => e.Id == id);
        }
    }
}
