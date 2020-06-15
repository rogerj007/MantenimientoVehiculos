using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MantenimientoVehiculos.Web.Data;
using MantenimientoVehiculos.Web.Data.Entities;

namespace MantenimientoVehiculos.Web.Controllers
{
    public class VehicleStatusController : Controller
    {
        private readonly DataContext _context;

        public VehicleStatusController(DataContext context)
        {
            _context = context;
        }

        // GET: VehicleStatus
        public async Task<IActionResult> Index()
        {
            return View(await _context.VehicleStatus.ToListAsync());
        }

        // GET: VehicleStatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleStatusEntity = await _context.VehicleStatus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicleStatusEntity == null)
            {
                return NotFound();
            }

            return View(vehicleStatusEntity);
        }

        // GET: VehicleStatus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VehicleStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleStatusEntity vehicleStatusEntity)
        {
            if (ModelState.IsValid)
            {
                vehicleStatusEntity.VehicleStatus = vehicleStatusEntity.VehicleStatus.ToUpper();
                _context.Add(vehicleStatusEntity);
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    if (e.InnerException != null)
                        ModelState.AddModelError(string.Empty,
                            e.InnerException != null && e.InnerException.Message.Contains("duplicate")
                                ? "Already exists name on database"
                                : e.InnerException.Message);
                }
            }
            return View(vehicleStatusEntity);
        }

        // GET: VehicleStatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleStatusEntity = await _context.VehicleStatus.FindAsync(id);
            if (vehicleStatusEntity == null)
            {
                return NotFound();
            }
            return View(vehicleStatusEntity);
        }

        // POST: VehicleStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VehicleStatusEntity vehicleStatusEntity)
        {
            if (id != vehicleStatusEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var status = _context.VehicleStatus.FirstOrDefaultAsync(s => s.Id.Equals(id));
                    status.Result.ModificationDate = vehicleStatusEntity.ModificationDate;
                    status.Result.VehicleStatus = vehicleStatusEntity.VehicleStatus.ToUpper();
                    try
                    {
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception e)
                    {
                        if (e.InnerException != null)
                            ModelState.AddModelError(string.Empty,
                                e.InnerException != null && e.InnerException.Message.Contains("duplicate")
                                    ? "Already exists name on database"
                                    : e.InnerException.Message);
                    }
                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleStatusEntityExists(vehicleStatusEntity.Id))
                    {
                        return NotFound();
                    }
                }
                
            }
            return View(vehicleStatusEntity);
        }

        // GET: VehicleStatus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleStatusEntity = await _context.VehicleStatus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicleStatusEntity == null)
            {
                return NotFound();
            }
            _context.VehicleStatus.Remove(vehicleStatusEntity);
            await _context.SaveChangesAsync();
            return View(vehicleStatusEntity);
        }


        private bool VehicleStatusEntityExists(int id)
        {
            return _context.VehicleStatus.Any(e => e.Id == id);
        }
    }
}
