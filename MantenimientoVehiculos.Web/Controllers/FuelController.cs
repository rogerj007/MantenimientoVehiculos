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
    public class FuelController : Controller
    {
        private readonly DataContext _context;

        public FuelController(DataContext context)
        {
            _context = context;
        }

        // GET: Fuel
        public async Task<IActionResult> Index()
        {
             return View(await _context.Fuel.ToListAsync());
        }

        // GET: Fuel/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fuelEntity = await _context.Fuel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fuelEntity == null)
            {
                return NotFound();
            }

            return View(fuelEntity);
        }

        // GET: Fuel/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fuel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FuelEntity fuelEntity)
        {
            if (ModelState.IsValid)
            {
                fuelEntity.Fuel = fuelEntity.Fuel.ToUpper();
                fuelEntity.CreationDate = DateTime.UtcNow;
                _context.Add(fuelEntity);
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
            return View(fuelEntity);
        }

        // GET: Fuel/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fuelEntity = await _context.Fuel.FindAsync(id);
            if (fuelEntity == null)
            {
                return NotFound();
            }
            return View(fuelEntity);
        }

        // POST: Fuel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FuelEntity fuelEntity)
        {
            if (id != fuelEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var fuel = _context.Fuel.FirstOrDefaultAsync(f => f.Id.Equals(id));
                    fuel.Result.Fuel = fuel.Result.Fuel.ToUpper();
                    fuel.Result.ModificationDate = DateTime.UtcNow;
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
                    if (!FuelEntityExists(fuelEntity.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(fuelEntity);
        }

        // GET: Fuel/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fuelEntity = await _context.Fuel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fuelEntity == null)
            {
                return NotFound();
            }

            _context.Fuel.Remove(fuelEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FuelEntityExists(int id)
        {
            return _context.Fuel.Any(e => e.Id == id);
        }
    }
}
