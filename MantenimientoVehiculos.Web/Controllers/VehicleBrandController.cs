using System;
using System.Linq;
using System.Threading.Tasks;
using MantenimientoVehiculos.Web.Data;
using MantenimientoVehiculos.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MantenimientoVehiculos.Web.Controllers
{
    public class VehicleBrandController : Controller
    {
        private readonly DataContext _context;

        public VehicleBrandController(DataContext context)
        {
            _context = context;
        }

        // GET: VehicleBrand
        public async Task<IActionResult> Index()
        {
            return View(await _context.VehicleBrand.ToListAsync());
        }

        // GET: VehicleBrand/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleBrandEntity = await _context.VehicleBrand
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicleBrandEntity == null)
            {
                return NotFound();
            }

            return View(vehicleBrandEntity);
        }

        // GET: VehicleBrand/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VehicleBrand/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleBrandEntity vehicleBrandEntity)
        {
            if (ModelState.IsValid)
            {
                vehicleBrandEntity.VehicleBrand = vehicleBrandEntity.VehicleBrand.ToUpper();
                _context.Add(vehicleBrandEntity);
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
            return View(vehicleBrandEntity);
        }

        // GET: VehicleBrand/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleBrandEntity = await _context.VehicleBrand.FindAsync(id);
            if (vehicleBrandEntity == null)
            {
                return NotFound();
            }
            return View(vehicleBrandEntity);
        }

        // POST: VehicleBrand/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  VehicleBrandEntity vehicleBrandEntity)
        {
            if (id != vehicleBrandEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var brand = _context.VehicleBrand.FirstOrDefaultAsync(b=>b.Id.Equals(id));
                    brand.Result.VehicleBrand = vehicleBrandEntity.VehicleBrand;
                    brand.Result.ModificationDate = DateTime.UtcNow;
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
                    if (!VehicleBrandEntityExists(vehicleBrandEntity.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(vehicleBrandEntity);
        }

        // GET: VehicleBrand/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleBrandEntity = await _context.VehicleBrand
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicleBrandEntity == null)
            {
                return NotFound();
            }

            _context.VehicleBrand.Remove(vehicleBrandEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleBrandEntityExists(int id)
        {
            return _context.VehicleBrand.Any(e => e.Id == id);
        }
    }
}
