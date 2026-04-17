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
    public class PortalPagesController : Controller
    {
        private readonly GymContext _context;

        public PortalPagesController(GymContext context)
        {
            _context = context;
        }

        // GET: PortalPages
        public async Task<IActionResult> Index()
        {
            return View(await _context.PortalPage.ToListAsync());
        }

        // GET: PortalPages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portalPage = await _context.PortalPage
                .FirstOrDefaultAsync(m => m.Id == id);
            if (portalPage == null)
            {
                return NotFound();
            }

            return View(portalPage);
        }

        // GET: PortalPages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PortalPages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Slug,Title,Content,IsPublished")] PortalPage portalPage)
        {
            if (ModelState.IsValid)
            {
                portalPage.IsActive = true;
                portalPage.CreatedAt = DateTime.UtcNow;
                portalPage.CreatedBy = User.Identity?.Name ?? "Admin";
                portalPage.ModifiedBy = User.Identity?.Name ?? "Admin";
                portalPage.ModifiedAt = DateTime.UtcNow;

                _context.Add(portalPage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(portalPage);
        }

        // GET: PortalPages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portalPage = await _context.PortalPage.FindAsync(id);
            if (portalPage == null)
            {
                return NotFound();
            }
            portalPage.ModifiedBy = User.Identity?.Name ?? "Admin";
            portalPage.ModifiedAt = DateTime.UtcNow;
            return View(portalPage);
        }

        // POST: PortalPages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Slug,Title,Content,IsPublished,Id,IsActive")] PortalPage portalPage)
        {
            if (id != portalPage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingPortalPage = await _context.PortalPage.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
                    if (existingPortalPage == null)
                    {
                        return NotFound();
                    }

                    portalPage.CreatedBy = existingPortalPage.CreatedBy;
                    portalPage.CreatedAt = existingPortalPage.CreatedAt;
                    portalPage.DeletedBy = existingPortalPage.DeletedBy;
                    portalPage.DeletedAt = existingPortalPage.DeletedAt;
                    portalPage.ModifiedBy = User.Identity?.Name ?? "Admin";
                    portalPage.ModifiedAt = DateTime.UtcNow;

                    _context.Update(portalPage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PortalPageExists(portalPage.Id))
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
            return View(portalPage);
        }

        // GET: PortalPages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portalPage = await _context.PortalPage
                .FirstOrDefaultAsync(m => m.Id == id);
            if (portalPage == null)
            {
                return NotFound();
            }

            return View(portalPage);
        }

        // POST: PortalPages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var portalPage = await _context.PortalPage.FindAsync(id);
            if (portalPage != null)
            {
                _context.PortalPage.Remove(portalPage);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PortalPageExists(int id)
        {
            return _context.PortalPage.Any(e => e.Id == id);
        }
    }
}
