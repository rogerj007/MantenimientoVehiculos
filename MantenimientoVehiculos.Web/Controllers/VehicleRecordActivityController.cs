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
        private readonly IUserHelper _userHelper;

        public VehicleRecordActivityController(DataContext context,
                                                ICombosHelper combosHelper,
                                                IConverterHelper converterHelper,
                                                IUserHelper userHelper)
        {
            _context = context;
            _combosHelper = combosHelper;
            _converterHelper = converterHelper;
            _userHelper = userHelper;
        }

        // GET: VehicleRecordActivity
        public async Task<IActionResult> Index()
        {
            return View(await _context.VehicleRecordActivities
                                        .Include(v=>v.Vehicle).ThenInclude(c=>c.Color)
                                        .Include(v => v.Vehicle).ThenInclude(c => c.VehicleBrand)
                                        //.Where(v=>v.Vehicle.VehicleStatus.Id.Equals(1))//Solo Operativo
                                        .ToListAsync());
        }

        // GET: VehicleRecordActivity/Details/5
        public async Task<IActionResult> Details(long? id)
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
                Vehicles=_combosHelper.GetComboVehicles(true)
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
                    var user = await _userHelper.GetUserAsync(User.Identity.Name);
                    var vehicleRecordActivity = await _converterHelper.ToVehicleRecordActivityAsync(model);
                    vehicleRecordActivity.CreatedDate = DateTime.UtcNow;
                    vehicleRecordActivity.ModifiedBy = user;
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

            model.Vehicles = _combosHelper.GetComboVehicles(true);
            return View(model);
        }

        // GET: VehicleRecordActivity/Edit/5
        public async Task<IActionResult> Edit(long? id)
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
        public async Task<IActionResult> Edit(long id, VehicleRecordActivityEntity model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return View(model);
            try
            {
                var user = await _userHelper.GetUserAsync(User.Identity.Name);
                model.ModifiedBy = user;
                model.ModifiedDate = DateTime.UtcNow;
                _context.Entry(model).State = EntityState.Modified;
                await _context.SaveChangesAsync().ConfigureAwait(true);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!VehicleRecordActivityEntityExists(model.Id))
                {
                    return NotFound();
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: VehicleRecordActivity/Delete/5
        public async Task<IActionResult> Delete(long? id)
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

        private bool VehicleRecordActivityEntityExists(long id)
        {
            return _context.VehicleRecordActivities.Any(e => e.Id == id);
        }
    }
}
