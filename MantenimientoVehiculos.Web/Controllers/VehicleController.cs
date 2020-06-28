using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MantenimientoVehiculos.Web.Data;
using MantenimientoVehiculos.Web.Data.Entities;
using MantenimientoVehiculos.Web.Helpers;
using MantenimientoVehiculos.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MantenimientoVehiculos.Web.Controllers
{
    [Authorize(Roles = "Admin,Supervisor,User")]
    public class VehicleController : Controller
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;
        private readonly IConverterHelper _converterHelper;

        public VehicleController(DataContext context, ICombosHelper combosHelper,IConverterHelper converterHelper)
        {
            _context = context;
            _combosHelper = combosHelper;
            _converterHelper = converterHelper;
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        // GET: Vehicle
        public async Task<IActionResult> Index()
        {
            return View(await _context.Vehicle
                                            .Include(c=>c.Color)
                                            .Include(vb=>vb.VehicleBrand)
                                            .Include(vb => vb.VehicleStatus)
                                            .Include(vb => vb.Country)
                                            .Include(vb => vb.Fuel)
                                            .Include(vb => vb.VehicleType)
                                            .ToListAsync());
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

            var model = new VehicleViewModel
            {
                VehicleBrands = _combosHelper.GetComboBrandVehicle(),
                VehicleTypes = _combosHelper.GetComboVehicleType(),
                VehicleStatu = _combosHelper.GetComboVehicleStatus(),
                Countries = _combosHelper.GetComboCountry(),
                Fuels = _combosHelper.GetComboFuel(),
                Colors=_combosHelper.GetComboColor(),
                CreationDate=DateTime.UtcNow
            };

            return View(model);
        }

        // POST: Vehicle/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var path = string.Empty;

                    if (model.ImageFile != null && model.ImageFile.Length > 0)
                    {
                        var guid = Guid.NewGuid().ToString();
                        var file = $"{guid}.jpg";

                        path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\Vechicles", file);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await model.ImageFile.CopyToAsync(stream);
                        }
                        path = $"~/images/Vechicles/{file}";
                    }
                    var vehicle = await _converterHelper.ToVehicleAsync(model, path, true);
                    _context.Add(vehicle);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                    foreach (var error in errors)
                    {
                        ModelState.AddModelError(string.Empty, error.ErrorMessage);
                    }
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

            model.VehicleBrands = _combosHelper.GetComboBrandVehicle();
            model.VehicleTypes = _combosHelper.GetComboVehicleType();
            model.VehicleStatu = _combosHelper.GetComboVehicleStatus();
            model.Countries = _combosHelper.GetComboCountry();
            model.Fuels = _combosHelper.GetComboFuel();
            model.Colors = _combosHelper.GetComboColor();
            return View(model);
        }

        // GET: Vehicle/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle=await _context.Vehicle
                .Include(c => c.Color)
                .Include(vb => vb.VehicleBrand)
                .Include(vb => vb.VehicleStatus)
                .Include(vb => vb.Country)
                .Include(vb => vb.Fuel)
                .Include(vb => vb.VehicleType)
                .FirstOrDefaultAsync(p => p.Id == id.Value);

            if (vehicle == null)
            {
                return NotFound();
            }
            var vehicleView = _converterHelper.ToVehicleViewModel(vehicle);
            return View(vehicleView);
        }

        // POST: Vehicle/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(VehicleViewModel model)
        {
           

            if (ModelState.IsValid)
            {

                var path = model.ImageUrl;

                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    var guid = Guid.NewGuid().ToString();
                    var file = $"{guid}.jpg";

                    path = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot\\images\\Vechicles",
                        file);
                    //if (!Directory.Exists(path))
                    //    Directory.CreateDirectory(path);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await model.ImageFile.CopyToAsync(stream);
                    }

                    path = $"~/images/Vechicles/{file}";
                }

                try
                {
                    var vehicle = await _converterHelper.ToVehicleAsync(model, path, false);
                    _context.Update(vehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleEntityExists(model.Id))
                    {
                        return NotFound();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            model.VehicleBrands = _combosHelper.GetComboBrandVehicle();
            model.VehicleTypes = _combosHelper.GetComboVehicleType();
            model.VehicleStatu = _combosHelper.GetComboVehicleStatus();
            model.Countries = _combosHelper.GetComboCountry();
            model.Fuels = _combosHelper.GetComboFuel();
            model.Colors = _combosHelper.GetComboColor();
            return View(model);
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
