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
    public class TypeVehicleController : Controller
    {
        private readonly DataContext _context;

        public TypeVehicleController(DataContext context)
        {
            _context = context;
        }

        // GET: TypeVehicle
        public async Task<IActionResult> Index()
        {
            return View(await _context.TypeVehicle.ToListAsync());
        }

        // GET: TypeVehicle/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeVehicleEntity = await _context.TypeVehicle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeVehicleEntity == null)
            {
                return NotFound();
            }

            return View(typeVehicleEntity);
        }

        // GET: TypeVehicle/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypeVehicle/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TypeVehicleEntity typeVehicleEntity)
        {
            if (ModelState.IsValid)
            {
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

        // GET: TypeVehicle/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeVehicleEntity = await _context.TypeVehicle.FindAsync(id);
            if (typeVehicleEntity == null)
            {
                return NotFound();
            }
            return View(typeVehicleEntity);
        }

        // POST: TypeVehicle/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TypeVehicleEntity typeVehicleEntity)
        {
            if (id != typeVehicleEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var typeVehicle = _context.TypeVehicle.FirstOrDefaultAsync(tv => tv.Id.Equals(id));
                    typeVehicle.Result.ModificationDate = DateTime.UtcNow;
                    typeVehicle.Result.TypeVehicle = typeVehicle.Result.TypeVehicle.ToUpper();
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
                    if (!TypeVehicleEntityExists(typeVehicleEntity.Id))
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

        // GET: TypeVehicle/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeVehicleEntity = await _context.TypeVehicle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeVehicleEntity == null)
            {
                return NotFound();
            }

            _context.TypeVehicle.Remove(typeVehicleEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeVehicleEntityExists(int id)
        {
            return _context.TypeVehicle.Any(e => e.Id == id);
        }
    }
}
