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
    public class MembershipsController : Controller
    {
        private readonly GymContext _context;

        public MembershipsController(GymContext context)
        {
            _context = context;
        }

        // GET: Memberships
        public async Task<IActionResult> Index()
        {
            var gymContext = _context.Membership.Include(m => m.Member).Include(m => m.MembershipPlan);
            return View(await gymContext.ToListAsync());
        }

        // GET: Memberships/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membership = await _context.Membership
                .Include(m => m.Member)
                .Include(m => m.MembershipPlan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (membership == null)
            {
                return NotFound();
            }

            return View(membership);
        }

        // GET: Memberships/Create
        public IActionResult Create()
        {
            ViewData["MemberId"] = new SelectList(_context.Member, "Id", "Email");
            ViewData["MembershipPlanId"] = new SelectList(_context.Set<MembershipPlan>(), "Id", "Name");
            return View();
        }

        // POST: Memberships/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MemberId,MembershipPlanId,StartDate,EndDate,Status")] Membership membership)
        {
            if (ModelState.IsValid)
            {
                membership.IsActive = true;
                membership.CreatedAt = DateTime.UtcNow;
                membership.CreatedBy = User.Identity?.Name ?? "Admin";
                membership.ModifiedBy = User.Identity?.Name ?? "Admin";
                membership.ModifiedAt = DateTime.UtcNow;

                _context.Add(membership);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MemberId"] = new SelectList(_context.Member, "Id", "Email", membership.MemberId);
            ViewData["MembershipPlanId"] = new SelectList(_context.Set<MembershipPlan>(), "Id", "Name", membership.MembershipPlanId);
            return View(membership);
        }

        // GET: Memberships/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membership = await _context.Membership.FindAsync(id);
            if (membership == null)
            {
                return NotFound();
            }
            membership.ModifiedBy = User.Identity?.Name ?? "Admin";
            membership.ModifiedAt = DateTime.UtcNow;
            ViewData["MemberId"] = new SelectList(_context.Member, "Id", "Email", membership.MemberId);
            ViewData["MembershipPlanId"] = new SelectList(_context.Set<MembershipPlan>(), "Id", "Name", membership.MembershipPlanId);
            return View(membership);
        }

        // POST: Memberships/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MemberId,MembershipPlanId,StartDate,EndDate,Status,Id,IsActive")] Membership membership)
        {
            if (id != membership.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingMembership = await _context.Membership.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
                    if (existingMembership == null)
                    {
                        return NotFound();
                    }

                    membership.CreatedBy = existingMembership.CreatedBy;
                    membership.CreatedAt = existingMembership.CreatedAt;
                    membership.DeletedBy = existingMembership.DeletedBy;
                    membership.DeletedAt = existingMembership.DeletedAt;
                    membership.ModifiedBy = User.Identity?.Name ?? "Admin";
                    membership.ModifiedAt = DateTime.UtcNow;

                    _context.Update(membership);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MembershipExists(membership.Id))
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
            ViewData["MemberId"] = new SelectList(_context.Member, "Id", "Email", membership.MemberId);
            ViewData["MembershipPlanId"] = new SelectList(_context.Set<MembershipPlan>(), "Id", "Name", membership.MembershipPlanId);
            return View(membership);
        }

        // GET: Memberships/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membership = await _context.Membership
                .Include(m => m.Member)
                .Include(m => m.MembershipPlan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (membership == null)
            {
                return NotFound();
            }

            return View(membership);
        }

        // POST: Memberships/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var membership = await _context.Membership.FindAsync(id);
            if (membership != null)
            {
                _context.Membership.Remove(membership);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MembershipExists(int id)
        {
            return _context.Membership.Any(e => e.Id == id);
        }
    }
}
