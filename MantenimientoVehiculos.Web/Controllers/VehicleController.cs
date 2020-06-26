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
using Microsoft.AspNetCore.Authorization;

namespace MantenimientoVehiculos.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class VehicleController : Controller
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;

        public VehicleController(DataContext context, ICombosHelper combosHelper)
        {
            _context = context;
            _combosHelper = combosHelper;
        }

        // GET: Vehicle
        public async Task<IActionResult> Index()
        {
            return View(await _context.Vehicle.Include(c=>c.Color).Include(vb=>vb.VehicleBrand).ToListAsync());
        }

        // GET: Vehicle/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleEntity = await _context.Vehicle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicleEntity == null)
            {
                return NotFound();
            }

            return View(vehicleEntity);
        }

        // GET: Vehicle/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vehicle/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleEntity vehicleEntity)
        {
            if (ModelState.IsValid)
            {
                
                _context.Add(vehicleEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleEntity);
        }

        // GET: Vehicle/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleEntity = await _context.Vehicle.FindAsync(id);
            if (vehicleEntity == null)
            {
                return NotFound();
            }
            return View(vehicleEntity);
        }

        // POST: Vehicle/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VehicleEntity vehicleEntity)
        {
            if (id != vehicleEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var vehicle = _context.Vehicle.SingleOrDefaultAsync(v=>v.Id.Equals(vehicleEntity.Id));
                    vehicle.Result.ModificationDate = DateTime.UtcNow;
                    _context.Update(vehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleEntityExists(vehicleEntity.Id))
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
            return View(vehicleEntity);
        }

        // GET: Vehicle/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleEntity = await _context.Vehicle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicleEntity == null)
            {
                return NotFound();
            }

            _context.Vehicle.Remove(vehicleEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleEntityExists(int id)
        {
            return _context.Vehicle.Any(e => e.Id == id);
        }
    }
}
