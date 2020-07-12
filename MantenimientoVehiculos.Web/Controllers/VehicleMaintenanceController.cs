using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MantenimientoVehiculos.Web.Data;
using MantenimientoVehiculos.Web.Data.Entities;
using MantenimientoVehiculos.Web.Enums;
using MantenimientoVehiculos.Web.Helpers;
using MantenimientoVehiculos.Web.Models;

namespace MantenimientoVehiculos.Web.Controllers
{
    public class VehicleMaintenanceController : Controller
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IUserHelper _userHelper;
        


        public VehicleMaintenanceController(DataContext context,
                                            ICombosHelper combosHelper,
                                            IConverterHelper converterHelper,
                                            IUserHelper userHelper)
        {
            _context = context;
            _combosHelper = combosHelper;
            _converterHelper = converterHelper;
            _userHelper = userHelper;
        }

        // GET: VehicleMaintenance
        public async Task<IActionResult> Index()
        {

            var user = await _userHelper.GetUserAsync(User.Identity.Name);
            var isAdmin = await _userHelper.IsUserInRoleAsync(user, "Admin");
            List<VehicleMaintenanceEntity> mantence;
            if (isAdmin)
                mantence = await _context.VehicleMaintenance
                                        .Include(v => v.Vehicle)
                                        //.Include(v => v.VehicleMaintenanceDetail)
                                        .ToListAsync();
            else
                mantence = await _context.VehicleMaintenance
                                        .Include(v => v.Vehicle)
                                        //.Include(v => v.VehicleMaintenanceDetail)
                                        .Where(u => u.CreatedBy == user)
                                        .ToListAsync();

            return View(mantence);
        }

        // GET: VehicleMaintenance/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleMaintenanceEntity = await _context.VehicleMaintenance
                                        .Include(m => m.VehicleMaintenanceDetail)
                                        .ThenInclude(v => v.Component)
                                        .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicleMaintenanceEntity == null)
            {
                return NotFound();
            }

            return View(vehicleMaintenanceEntity);
        }

        // GET: VehicleMaintenance/Create
        public IActionResult Create()
        {
            var model = new VehicleMaintenanceViewModel
            {
                MaintenanceDate = DateTime.Today,
                CreatedDate = DateTime.UtcNow,
                ListMaintenanceType = _combosHelper.GetComboListMaintenance(),
                ListVehicles = _combosHelper.GetComboVehicles()
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleMaintenanceViewModel model)
        {
            if (ModelState.IsValid)
            {

                var vehicleMantence = await _converterHelper.ToVehicleMaintenanceAsync(model);
                var user = await _userHelper.GetUserAsync(User.Identity.Name);
                vehicleMantence.CreatedDate = DateTime.UtcNow;
                vehicleMantence.CreatedBy = user;
                _context.Add(vehicleMantence);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }



            return View(model);
        }

        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleMaintenanceEntity = await _context.VehicleMaintenance
                                                    .Include(md=>md.VehicleMaintenanceDetail)
                                                    .Include(v=>v.Vehicle)
                                                    .FirstOrDefaultAsync(p => p.Id == id.Value);
              

            if (vehicleMaintenanceEntity == null)
            {
                return NotFound();
            }

            var model=_converterHelper.ToVehicleMaintenanceViewModel(vehicleMaintenanceEntity);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, VehicleMaintenanceViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var vehicleMantence = _context.VehicleMaintenance
                                                                        .FirstOrDefaultAsync(m => m.Id.Equals(model.Id)).Result;

                    var user = await _userHelper.GetUserAsync(User.Identity.Name);
                    vehicleMantence.MaintenanceType = Enum.Parse<MaintenanceType>(model.MaintenanceTypeId.ToString());
                    vehicleMantence.Vehicle = await _context.Vehicle.FindAsync(model.VehicleId);
                    vehicleMantence.KmHrMaintenance = model.KmHrMaintenance;

                    vehicleMantence.ModifiedDate = DateTime.UtcNow;
                    vehicleMantence.Complete = model.Complete;
                    vehicleMantence.ModifiedBy = user;

                    _context.Update(vehicleMantence);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleMaintenanceEntityExists(model.Id))
                    {
                        return NotFound();
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            model.ListVehicles = _combosHelper.GetComboVehicles();

            return View(model);
        }

        // GET: VehicleMaintenance/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleMaintenanceEntity = await _context.VehicleMaintenance
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicleMaintenanceEntity == null)
            {
                return NotFound();
            }
            _context.VehicleMaintenance.Remove(vehicleMaintenanceEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: VehicleMaintenance/DeleteComponent/5
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteComponent(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleMaintenanceDetails = await _context.VehicleMaintenanceDetail
                .Include(m=>m.VehicleMaintenance)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicleMaintenanceDetails == null)
            {
                return NotFound();
            }
            _context.Remove(vehicleMaintenanceDetails);
            await _context.SaveChangesAsync();
            return RedirectToAction($"{nameof(Details)}/{vehicleMaintenanceDetails.VehicleMaintenance.Id}");
        }

        public async Task<IActionResult> AddComponent(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintenanceEntity = await _context.VehicleMaintenance.FindAsync(id);
            if (maintenanceEntity == null)
            {
                return NotFound();
            }

            var model = new VehicleMaintenanceDetailsViewModel
            {
                VehicleMaintenanceId = maintenanceEntity.Id,
                Components = _combosHelper.GetComboComponets()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComponent(VehicleMaintenanceDetailsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var md = await _converterHelper.ToVehicleMaintenanceDetailsAsync(model,true);
                var user = await _userHelper.GetUserAsync(User.Identity.Name);
                md.CreatedBy = user;
                md.CreatedDate = DateTime.UtcNow;
                _context.Add(md);
                await _context.SaveChangesAsync();
                return RedirectToAction($"{nameof(Details)}/{model.VehicleMaintenanceId}");
            }

            model.Components = _combosHelper.GetComboComponets();
            return View(model);
        }




        public IActionResult Report()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Report(string maintenanceDateBegin, string maintenanceDateEnd)
        {

            var begin = DateTime.Parse(maintenanceDateBegin);
            var end = DateTime.Parse(maintenanceDateEnd);
            var query = await _context.VehicleMaintenance
                                                        .Include(v => v.Vehicle)
                                                        .Include(v => v.VehicleMaintenanceDetail)
                                                        .ThenInclude(c=>c.Component)
                                                        .Where(d => d.MaintenanceDate.Date >= begin && d.MaintenanceDate.Date <= end && d.Complete)
                                                        .ToListAsync();

            var model = (from v in query from vm in v.VehicleMaintenanceDetail select new ReportViewModel {Plaque = v.Vehicle.Name, Date = v.MaintenanceDate.Date, KmHrMaintenance = v.KmHrMaintenance, ComponentName = vm.Component.Name}).ToList();

            
            return View(model);
        }

        [HttpPost]
        public async Task<FileContentResult> DownloadExcelDocument(string maintenanceDateBegin, string maintenanceDateEnd)
        {
            if (string.IsNullOrEmpty(maintenanceDateBegin) || string.IsNullOrEmpty(maintenanceDateEnd)) return null;
            var begin = DateTime.Parse(maintenanceDateBegin);
            var end = DateTime.Parse(maintenanceDateEnd);
            var query = await _context.VehicleMaintenance
                .Include(v => v.Vehicle)
                .Include(v => v.VehicleMaintenanceDetail)
                .ThenInclude(c => c.Component)
                .Where(d => d.MaintenanceDate.Date >= begin && d.MaintenanceDate.Date <= end && d.Complete)
                .ToListAsync();

            var model = (from v in query from vm in v.VehicleMaintenanceDetail select new ReportViewModel { Plaque = v.Vehicle.Name, Date = v.MaintenanceDate.Date, KmHrMaintenance = v.KmHrMaintenance, ComponentName = vm.Component.Name }).ToList();
            
            
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Report");
            var currentRow = 1;
            worksheet.Cell(currentRow, 1).Style.Fill.BackgroundColor = XLColor.BabyBlue;
            worksheet.Cell(currentRow, 1).Style.Font.FontColor = XLColor.Red;
            worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
            worksheet.Cell(currentRow, 1).Value = "Plaque";
            worksheet.Cell(currentRow, 2).Style.Fill.BackgroundColor = XLColor.BabyBlue;
            worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
            worksheet.Cell(currentRow, 2).Value = "Date";
            worksheet.Cell(currentRow, 3).Style.Fill.BackgroundColor = XLColor.BabyBlue;
            worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
            worksheet.Cell(currentRow, 3).Value = "Km / Hr";
            worksheet.Cell(currentRow, 4).Style.Fill.BackgroundColor = XLColor.BabyBlue;
            worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
            worksheet.Cell(currentRow, 4).Value = "Component Name";
            worksheet.Cell(currentRow, 4).Worksheet.ColumnWidth = 50;

            foreach (var user in model)
            {
                currentRow++;
                worksheet.Cell(currentRow, 1).Value = user.Plaque;
                worksheet.Cell(currentRow, 2).Value = user.Date;
                worksheet.Cell(currentRow, 3).Value = user.KmHrMaintenance;
                worksheet.Cell(currentRow, 4).Value = user.ComponentName;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();

            return File(
                content,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Report.xlsx");
        }

        private bool VehicleMaintenanceEntityExists(long id)
        {
            return _context.VehicleMaintenance.Any(e => e.Id == id);
        }
    }
}
