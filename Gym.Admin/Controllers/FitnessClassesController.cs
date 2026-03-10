using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gym.Data.Models.Core;
using Gym.Data.Data;

namespace Gym.Admin.Controllers
{
    public class FitnessClassesController : Controller
    {
        private readonly GymContext _context;

        public FitnessClassesController(GymContext context)
        {
            _context = context;
        }

        // GET: FitnessClasses
        public async Task<IActionResult> Index()
        {
            return View(await _context.FitnessClass.ToListAsync());
        }

        // GET: FitnessClasses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fitnessClass = await _context.FitnessClass
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fitnessClass == null)
            {
                return NotFound();
            }

            return View(fitnessClass);
        }

        // GET: FitnessClasses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FitnessClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,StartTime,DurationInMinutes,Room,Capacity,Id,IsActive,CreatedBy,CreatedAt,ModifiedBy,ModifiedAt,DeletedBy,DeletedAt")] FitnessClass fitnessClass)
        {
            if (ModelState.IsValid)
            {
                fitnessClass.IsActive = true;
                fitnessClass.CreatedAt = DateTime.UtcNow;
                fitnessClass.CreatedBy = User.Identity?.Name ?? "Admin";
                fitnessClass.ModifiedBy = User.Identity?.Name ?? "Admin";
                fitnessClass.ModifiedAt = DateTime.UtcNow;

                _context.Add(fitnessClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fitnessClass);
        }

        // GET: FitnessClasses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fitnessClass = await _context.FitnessClass.FindAsync(id);
            if (fitnessClass == null)
            {
                return NotFound();
            }
            fitnessClass.ModifiedBy = User.Identity?.Name ?? "Admin";
            fitnessClass.ModifiedAt = DateTime.UtcNow;
            return View(fitnessClass);
        }

        // POST: FitnessClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,StartTime,DurationInMinutes,Room,Capacity,Id,IsActive,CreatedBy,CreatedAt,ModifiedBy,ModifiedAt,DeletedBy,DeletedAt")] FitnessClass fitnessClass)
        {
            if (id != fitnessClass.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    fitnessClass.ModifiedBy = User.Identity?.Name ?? "Admin";
                    fitnessClass.ModifiedAt = DateTime.UtcNow;

                    _context.Update(fitnessClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FitnessClassExists(fitnessClass.Id))
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
            return View(fitnessClass);
        }

        // GET: FitnessClasses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fitnessClass = await _context.FitnessClass
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fitnessClass == null)
            {
                return NotFound();
            }

            return View(fitnessClass);
        }

        // POST: FitnessClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fitnessClass = await _context.FitnessClass.FindAsync(id);
            if (fitnessClass != null)
            {
                _context.FitnessClass.Remove(fitnessClass);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FitnessClassExists(int id)
        {
            return _context.FitnessClass.Any(e => e.Id == id);
        }
    }
}
