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
                                        .Include(v=>v.VehicleMaintenanceDetail)
                                        .ToListAsync();
            else
                mantence = await _context.VehicleMaintenance
                                        .Include(v => v.VehicleMaintenanceDetail)
                                        .Where(u=>u.CreatedBy==user)
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
                MaintenanceDate=DateTime.Today,
                CreatedDate = DateTime.UtcNow,
                ListMaintenanceType= _combosHelper.GetComboListMaintenance(),
                ListVehicles=_combosHelper.GetComboVehicles()
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
                vehicleMantence.CreatedBy= user;
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

            var vehicleMaintenanceEntity = await _context.VehicleMaintenance.FindAsync(id);
            if (vehicleMaintenanceEntity == null)
            {
                return NotFound();
            }
            return View(vehicleMaintenanceEntity);
        }

        // POST: VehicleMaintenance/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        private bool VehicleMaintenanceEntityExists(long id)
        {
            return _context.VehicleMaintenance.Any(e => e.Id == id);
        }
    }
}
