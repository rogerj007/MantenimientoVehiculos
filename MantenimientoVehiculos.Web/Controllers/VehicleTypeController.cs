using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MantenimientoVehiculos.Web.Data;
using MantenimientoVehiculos.Web.Data.Entities;
using Microsoft.AspNetCore.Authorization;

namespace MantenimientoVehiculos.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class VehicleTypeController : Controller
    {
        private readonly DataContext _context;

        public VehicleTypeController(DataContext context)
        {
            _context = context;
        }

        // GET: VehicleType
        public async Task<IActionResult> Index()
        {
            return View(await _context.VehicleType.ToListAsync());
        }

        // GET: VehicleType/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeVehicleEntity = await _context.VehicleType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeVehicleEntity == null)
            {
                return NotFound();
            }

            return View(typeVehicleEntity);
        }

        // GET: VehicleType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VehicleType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleTypeEntity typeVehicleEntity)
        {
            if (ModelState.IsValid)
            {
                typeVehicleEntity.VehicleType = typeVehicleEntity.VehicleType.ToUpper();
                typeVehicleEntity.CreationDate = DateTime.UtcNow;
                _context.Add(typeVehicleEntity);
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
            return View(typeVehicleEntity);
        }

        // GET: VehicleType/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeVehicleEntity = await _context.VehicleType.FindAsync(id);
            if (typeVehicleEntity == null)
            {
                return NotFound();
            }
            return View(typeVehicleEntity);
        }

        // POST: VehicleType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VehicleTypeEntity typeVehicleEntity)
        {
            if (id != typeVehicleEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var typeVehicle = _context.VehicleType.FirstOrDefaultAsync(tv => tv.Id.Equals(id));
                    typeVehicle.Result.ModificationDate = DateTime.UtcNow;
                    typeVehicle.Result.VehicleType = typeVehicle.Result.VehicleType.ToUpper();
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
                    if (!VehicleTypeEntityExists(typeVehicleEntity.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                
            }
            return View(typeVehicleEntity);
        }

        // GET: VehicleType/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeVehicleEntity = await _context.VehicleType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeVehicleEntity == null)
            {
                return NotFound();
            }

            _context.VehicleType.Remove(typeVehicleEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleTypeEntityExists(int id)
        {
            return _context.VehicleType.Any(e => e.Id == id);
        }
    }
}
