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
    public class ColorController : Controller
    {
        private readonly DataContext _context;

        public ColorController(DataContext context)
        {
            _context = context;
        }

        // GET: Color
        public async Task<IActionResult> Index()
        {
            return View(await _context.Colors.ToListAsync());
        }

        // GET: Color/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var colorEntity = await _context.Colors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (colorEntity == null)
            {
                return NotFound();
            }

            return View(colorEntity);
        }

        // GET: Color/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Color/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ColorEntity colorEntity)
        {
            if (ModelState.IsValid)
            {
                colorEntity.Color = colorEntity.Color.ToUpper();
               // colorEntity.CreationDate = DateTime.UtcNow;
                _context.Add(colorEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(colorEntity);
        }

        // GET: Color/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var colorEntity = await _context.Colors.FindAsync(id);
            if (colorEntity == null)
            {
                return NotFound();
            }
            return View(colorEntity);
        }

        // POST: Color/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ColorEntity colorEntity)
        {
            if (id != colorEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var color = _context.Colors.SingleOrDefaultAsync(c => c.Id.Equals(id));
                    color.Result.Color= colorEntity.Color.ToUpper();
                    color.Result.ModificationDate = DateTime.UtcNow;
                    //_context.Update(colorEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ColorEntityExists(colorEntity.Id))
                    {
                        return NotFound();
                    }

                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(colorEntity);
        }

        // GET: Color/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var colorEntity = await _context.Colors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (colorEntity == null)
            {
                return NotFound();
            }

            _context.Colors.Remove(colorEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //  return View(colorEntity);
        }

        private bool ColorEntityExists(int id)
        {
            return _context.Colors.Any(e => e.Id == id);
        }
    }
}
