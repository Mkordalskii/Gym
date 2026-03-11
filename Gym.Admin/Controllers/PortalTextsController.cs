using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gym.Data.Models.Cms;
using Gym.Data.Data;

namespace Gym.Admin.Controllers
{
    public class PortalTextsController : Controller
    {
        private readonly GymContext _context;

        public PortalTextsController(GymContext context)
        {
            _context = context;
        }

        // GET: PortalTexts
        public async Task<IActionResult> Index()
        {
            return View(await _context.PortalText.ToListAsync());
        }

        // GET: PortalTexts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portalText = await _context.PortalText
                .FirstOrDefaultAsync(m => m.Id == id);
            if (portalText == null)
            {
                return NotFound();
            }

            return View(portalText);
        }

        // GET: PortalTexts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PortalTexts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Key,Value,Language,Id,IsActive,CreatedBy,CreatedAt,ModifiedBy,ModifiedAt,DeletedBy,DeletedAt")] PortalText portalText)
        {
            if (ModelState.IsValid)
            {
                portalText.IsActive = true;
                portalText.CreatedAt = DateTime.UtcNow;
                portalText.CreatedBy = User.Identity?.Name ?? "Admin";
                portalText.ModifiedBy = User.Identity?.Name ?? "Admin";
                portalText.ModifiedAt = DateTime.UtcNow;

                _context.Add(portalText);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(portalText);
        }

        // GET: PortalTexts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portalText = await _context.PortalText.FindAsync(id);
            if (portalText == null)
            {
                return NotFound();
            }
            portalText.ModifiedBy = User.Identity?.Name ?? "Admin";
            portalText.ModifiedAt = DateTime.UtcNow;
            return View(portalText);
        }

        // POST: PortalTexts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Key,Value,Language,Id,IsActive,CreatedBy,CreatedAt,ModifiedBy,ModifiedAt,DeletedBy,DeletedAt")] PortalText portalText)
        {
            if (id != portalText.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    portalText.ModifiedBy = User.Identity?.Name ?? "Admin";
                    portalText.ModifiedAt = DateTime.UtcNow;

                    _context.Update(portalText);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PortalTextExists(portalText.Id))
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
            return View(portalText);
        }

        // GET: PortalTexts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portalText = await _context.PortalText
                .FirstOrDefaultAsync(m => m.Id == id);
            if (portalText == null)
            {
                return NotFound();
            }

            return View(portalText);
        }

        // POST: PortalTexts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var portalText = await _context.PortalText.FindAsync(id);
            if (portalText != null)
            {
                _context.PortalText.Remove(portalText);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PortalTextExists(int id)
        {
            return _context.PortalText.Any(e => e.Id == id);
        }
    }
}
