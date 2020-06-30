using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MantenimientoVehiculos.Web.Data;
using MantenimientoVehiculos.Web.Data.Entities;
using MantenimientoVehiculos.Web.Helpers;
using MantenimientoVehiculos.Web.Models;

namespace MantenimientoVehiculos.Web.Controllers
{
    public class VehicleRecordActivityController : Controller
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;
        private readonly IConverterHelper _converterHelper;

        public VehicleRecordActivityController(DataContext context, ICombosHelper combosHelper, IConverterHelper converterHelper)
        {
            _context = context;
            _combosHelper = combosHelper;
            _converterHelper = converterHelper;
        }

        // GET: VehicleRecordActivity
        public async Task<IActionResult> Index()
        {
            return View(await _context.VehicleRecordActivities
                                        .Include(v=>v.Vehicle).ThenInclude(c=>c.Color)
                                        .Include(v => v.Vehicle).ThenInclude(c => c.VehicleBrand)
                                        .ToListAsync());
        }

        // GET: VehicleRecordActivity/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleRecordActivityEntity = await _context.VehicleRecordActivities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicleRecordActivityEntity == null)
            {
                return NotFound();
            }

            return View(vehicleRecordActivityEntity);
        }

        // GET: VehicleRecordActivity/Create
        public IActionResult Create()
        {
            var model = new VehicleRecordActivityViewModel
            {
                Vehicles=_combosHelper.GetComboVehicles()
            };

            return View(model);
        }

        // POST: VehicleRecordActivity/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleRecordActivityViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var vehicleRecordActivity = await _converterHelper.ToVehicleRecordActivityAsync(model, true);
                    vehicleRecordActivity.CreationDate = DateTime.UtcNow;
                    _context.Add(vehicleRecordActivity);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                }
               
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    ModelState.AddModelError(string.Empty, error.ErrorMessage);
                }
            }

            model.Vehicles = _combosHelper.GetComboVehicles();
            return View(model);
        }

        // GET: VehicleRecordActivity/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleRecordActivityEntity = await _context.VehicleRecordActivities.FindAsync(id);

            if (vehicleRecordActivityEntity == null)
            {
                return NotFound();
            }
            return View(vehicleRecordActivityEntity);
        }

        // POST: VehicleRecordActivity/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VehicleRecordActivityEntity vehicleRecordActivityEntity)
        {
            if (id != vehicleRecordActivityEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicleRecordActivityEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleRecordActivityEntityExists(vehicleRecordActivityEntity.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleRecordActivityEntity);
        }

        // GET: VehicleRecordActivity/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleRecordActivityEntity = await _context.VehicleRecordActivities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicleRecordActivityEntity == null)
            {
                return NotFound();
            }
            _context.VehicleRecordActivities.Remove(vehicleRecordActivityEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleRecordActivityEntityExists(int id)
        {
            return _context.VehicleRecordActivities.Any(e => e.Id == id);
        }
    }
}
