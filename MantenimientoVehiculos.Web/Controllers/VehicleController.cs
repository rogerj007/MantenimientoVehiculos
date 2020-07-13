using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IUserHelper _userHelper;
        private readonly IMapper _mapper;

        public VehicleController(DataContext context,
                                ICombosHelper combosHelper,
                                IConverterHelper converterHelper,
                                IUserHelper userHelper,
                                IMapper mapper)
        {
            _context = context;
            _combosHelper = combosHelper;
            _converterHelper = converterHelper;
            _userHelper = userHelper;
            _mapper = mapper;
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
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleEntity = await _context.Vehicle
                .Include(c => c.Color)
                .Include(vb => vb.VehicleBrand)
                .Include(vb => vb.VehicleStatus)
                .Include(vb => vb.Country)
                .Include(vb => vb.Fuel)
                .Include(vb => vb.VehicleType)
                .FirstOrDefaultAsync(p => p.Id == id.Value);
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
                CreatedDate=DateTime.UtcNow
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
                            await model.ImageFile.CopyToAsync(stream).ConfigureAwait(false);
                        }
                        path = $"~/images/Vechicles/{file}";
                    }

                    var vehicle = await _converterHelper.ToVehicleAsync(model, path).ConfigureAwait(false);
                    var user = await _userHelper.GetUserAsync(User.Identity.Name).ConfigureAwait(false);
                    vehicle.Name = vehicle.Name.ToUpper();
                    vehicle.Chassis = vehicle.Chassis.ToUpper();
                    vehicle.CreatedBy = user;
                    vehicle.CreatedDate = DateTime.UtcNow;
                    _context.Add(vehicle);
                    await _context.SaveChangesAsync().ConfigureAwait(false);
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
        public async Task<IActionResult> Edit(short? id)
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
                    var user = await _userHelper.GetUserAsync(User.Identity.Name);
                    var vehicle = await _converterHelper.ToVehicleAsync(model, path);
                    vehicle.Name = model.Name.ToUpper();
                    vehicle.Chassis = model.Chassis.ToUpper();
                    vehicle.ModifiedDate = DateTime.UtcNow;
                    vehicle.ModifiedBy = user;
                   
                    _context.Entry(vehicle).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    foreach (var entry in ex.Entries)
                    {
                        if (entry.Entity is VehicleEntity)
                        {
                            var proposedValues = entry.CurrentValues;
                            var databaseValues = await entry.GetDatabaseValuesAsync();

                            foreach (var property in proposedValues.Properties)
                            {
                                var proposedValue = proposedValues[property];
                                var databaseValue = databaseValues[property];

                                // TODO: decide which value should be written to database
                                // proposedValues[property] = <value to be saved>;
                            }

                            // Refresh original values to bypass next concurrency check
                            entry.OriginalValues.SetValues(databaseValues);
                        }
                        else
                        {
                            throw new NotSupportedException(
                                "Don't know how to handle concurrency conflicts for "
                                + entry.Metadata.Name);
                        }
                    }

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
        public async Task<IActionResult> Delete(short? id)
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

        private bool VehicleEntityExists(short id)
        {
            return _context.Vehicle.Any(e => e.Id == id);
        }
    }
}
