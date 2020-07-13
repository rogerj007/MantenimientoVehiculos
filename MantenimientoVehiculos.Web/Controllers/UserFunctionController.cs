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
    [Authorize(Roles = "Admin,Supervisor")]
    public class UserFunctionController : Controller
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public UserFunctionController(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        // GET: JobTitle
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserFunction.ToListAsync());
        }

        // GET: JobTitle/Details/5
        public async Task<IActionResult> Details(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobTitleEntity = await _context.UserFunction
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobTitleEntity == null)
            {
                return NotFound();
            }

            return View(jobTitleEntity);
        }

        // GET: JobTitle/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JobTitle/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserFunctionEntity jobTitleEntity)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserAsync(User.Identity.Name);
                jobTitleEntity.ModifiedBy = user;
                jobTitleEntity.Name = jobTitleEntity.Name.ToUpper();
                jobTitleEntity.CreatedDate = DateTime.UtcNow;
                _context.Add(jobTitleEntity);
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    if (e.InnerException != null)
                        ModelState.AddModelError(string.Empty,
                             e.InnerException.Message.Contains("duplicate")
                                ? "Already exists name on database"
                                : e.InnerException.Message);
                }
               
            }
            return View(jobTitleEntity);
        }

        // GET: JobTitle/Edit/5
        public async Task<IActionResult> Edit(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobTitleEntity = await _context.UserFunction.FindAsync(id);
            if (jobTitleEntity == null)
            {
                return NotFound();
            }
            return View(jobTitleEntity);
        }

        // POST: JobTitle/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(byte id, UserFunctionEntity jobTitleEntity)
        {
            if (id != jobTitleEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userHelper.GetUserAsync(User.Identity.Name);
                    jobTitleEntity.ModifiedBy = user;
                    jobTitleEntity.Name = jobTitleEntity.Name.ToUpper();
                    jobTitleEntity.ModifiedDate = DateTime.UtcNow;
                    try
                    {
                        _context.Entry(jobTitleEntity).State = EntityState.Modified;
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
                    if (!JobTitleEntityExists(jobTitleEntity.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                
            }
            return View(jobTitleEntity);
        }

        // GET: JobTitle/Delete/5
        public async Task<IActionResult> Delete(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobTitleEntity = await _context.UserFunction
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobTitleEntity == null)
            {
                return NotFound();
            }

            _context.UserFunction.Remove(jobTitleEntity);
            //_context.SaveChanges();
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool JobTitleEntityExists(byte id)
        {
            return _context.UserFunction.Any(e => e.Id == id);
        }
    }
}
