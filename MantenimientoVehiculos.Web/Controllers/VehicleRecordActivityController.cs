using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MantenimientoVehiculos.Web.Data;
using MantenimientoVehiculos.Web.Data.Entities;
using MantenimientoVehiculos.Web.Enums;
using MantenimientoVehiculos.Web.Helpers;
using MantenimientoVehiculos.Web.Models;
using MantenimientoVehiculos.Web.Resources;

namespace MantenimientoVehiculos.Web.Controllers
{
    public class VehicleRecordActivityController : BaseController
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IUserHelper _userHelper;
        private readonly IMailHelper _mailHelper;

        public VehicleRecordActivityController(DataContext context,
                                                ICombosHelper combosHelper,
                                                IConverterHelper converterHelper,
                                                IUserHelper userHelper,
                                                IMailHelper mailHelper)
        {
            _context = context;
            _combosHelper = combosHelper;
            _converterHelper = converterHelper;
            _userHelper = userHelper;
            _mailHelper = mailHelper;
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

                    var query =await _context.VehicleRecordActivities
                        .Include(v => v.Vehicle)
                        .Where(v => v.Vehicle.Id == model.VehicleId).ToListAsync();
                    if (query.Any())
                    {
                        var max = query.Max(vv => vv.KmHr);
                        if (model.KmHr < max)
                        {
                            var msjValidacion = $"El registro de Km o Hr no puede ser menor al ultimo ingresado {max}";
                            model.Vehicles = _combosHelper.GetComboVehicles(true);
                            ModelState.AddModelError(string.Empty, msjValidacion);
                            return View(model);
                        }
                    }
                  

                    var user = await _userHelper.GetUserAsync(User.Identity.Name);
                    var allUser = await _userHelper.GetAllUserAsync();
                    var userToMail= allUser.Where(u => u.UserType == UserType.Admin || u.UserType == UserType.Supervisor);
                    var vehicleRecordActivity = await _converterHelper.ToVehicleRecordActivityAsync(model);
                    vehicleRecordActivity.CreatedDate = DateTime.UtcNow;
                    vehicleRecordActivity.CreatedBy = user;
                    _context.Add(vehicleRecordActivity);


                    //Register and Validation of each part or component

                    var componetsToChange =await _context.VehicleMaintenanceDetail
                                                    .Include(v => v.VehicleMaintenance)
                                                    .Include(v=>v.VehicleMaintenance.Vehicle)
                                                    .Include(c => c.Component)
                                                    .Where(m => m.VehicleMaintenance.Vehicle.Id.Equals(model.VehicleId) && !m.ExecutedNextChange)
                                                    .ToListAsync();
                    
                     var mensajeToMail = new StringBuilder();
                    //var cultureInfo = new CultureInfo();
                    var vehicle =await _context.Vehicle
                                                .Include(v=>v.VehicleBrand)
                                                .FirstAsync(v => v.Id.Equals(model.VehicleId));
                    mensajeToMail.Append($"Vehicle: Brand: {vehicle.VehicleBrand.Name.ToUpper()} Plaque: {vehicle.Name.ToUpper()} <br>");
                    foreach (var componet in componetsToChange)
                    {
                        if (componet.NextChangeKmHr < vehicleRecordActivity.KmHr)
                        {
                            mensajeToMail.Append($"{Language.ComponentChangeMessage}: {componet.Component.Name}<br>");
                        }
                        else
                        {
                            var alert = vehicleRecordActivity.KmHr - componet.NextChangeKmHr;
                            if (alert < 100)
                            {
                                mensajeToMail.Append($"Next component change over 100: {componet.Component.Name}<br>");
                            }
                        }
                    }

                    foreach (var userMail in userToMail)
                    {
                        _mailHelper.SendMail(userMail.Email, "Report", mensajeToMail.ToString());
                    }

                  




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
