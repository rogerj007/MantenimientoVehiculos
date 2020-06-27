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
    public class ComponentController : Controller
    {
        private readonly DataContext _context;

        public ComponentController(DataContext context)
        {
            _context = context;
        }

        // GET: Component
        public async Task<IActionResult> Index()
        {
            return View(await _context.Component.ToListAsync());
        }

        // GET: Component/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var componentEntity = await _context.Component
                .FirstOrDefaultAsync(m => m.Id == id);
            if (componentEntity == null)
            {
                return NotFound();
            }

            return View(componentEntity);
        }

        // GET: Component/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Component/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ComponentEntity componentEntity)
        {
            if (ModelState.IsValid)
            {
               
                componentEntity.Component = componentEntity.Component.ToUpper();
                _context.Add(componentEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(componentEntity);
        }

        // GET: Component/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var componentEntity = await _context.Component.FindAsync(id);
            if (componentEntity == null)
            {
                return NotFound();
            }
            return View(componentEntity);
        }

        // POST: Component/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ComponentEntity componentEntity)
        {
            if (id != componentEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                   
                    componentEntity.Component = componentEntity.Component.ToUpper();
                    componentEntity.ModificationDate = DateTime.UtcNow;
                    _context.Update(componentEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComponentEntityExists(componentEntity.Id))
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
            return View(componentEntity);
        }

        // GET: Component/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var componentEntity = await _context.Component
                .FirstOrDefaultAsync(m => m.Id == id);
            if (componentEntity == null)
            {
                return NotFound();
            }

            _context.Component.Remove(componentEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComponentEntityExists(int id)
        {
            return _context.Component.Any(e => e.Id == id);
        }
    }
}
