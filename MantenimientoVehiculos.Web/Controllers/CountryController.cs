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
using Microsoft.AspNetCore.Authorization;

namespace MantenimientoVehiculos.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CountryController : Controller
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public CountryController(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        // GET: Country
        public async Task<IActionResult> Index()
        {
            return View(await _context.Country.ToListAsync());
        }

        // GET: Country/Details/5
        public async Task<IActionResult> Details(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var countryEntity = await _context.Country
                .FirstOrDefaultAsync(m => m.Id == id);
            if (countryEntity == null)
            {
                return NotFound();
            }
            return View(countryEntity);
        }

        // GET: Country/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CountryEntity countryEntity)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserAsync(User.Identity.Name);
                countryEntity.CreatedBy = user;
                countryEntity.Name = countryEntity.Name.ToUpper();
                countryEntity.CreatedDate = DateTime.UtcNow;
                _context.Add(countryEntity);
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
            return View(countryEntity);
        }

        // GET: Country/Edit/5
        public async Task<IActionResult> Edit(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var countryEntity = await _context.Country.FindAsync(id);
            if (countryEntity == null)
            {
                return NotFound();
            }
            return View(countryEntity);
        }

        // POST: Country/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(byte id, CountryEntity countryEntity)
        {
            if (id != countryEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var country = _context.Country.SingleOrDefaultAsync(c => c.Id.Equals(id)).Result;
                    var user = await _userHelper.GetUserAsync(User.Identity.Name);
                    country.ModifiedBy = user;
                    country.Name = countryEntity.Name.ToUpper();
                    country.ModifiedDate = DateTime.UtcNow;
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
                    if (!CountryEntityExists(countryEntity.Id))
                    {
                        return NotFound();
                    }

                    throw;
                }
              
            }
            return View(countryEntity);
        }

        // GET: Country/Delete/5
        public async Task<IActionResult> Delete(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var countryEntity = await _context.Country
                .FirstOrDefaultAsync(m => m.Id == id);
            if (countryEntity == null)
            {
                return NotFound();
            }

            _context.Country.Remove(countryEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

       

        private bool CountryEntityExists(byte id)
        {
            return _context.Country.Any(e => e.Id == id);
        }
    }
}
