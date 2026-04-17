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
    public class MembershipPlansController : Controller
    {
        private readonly GymContext _context;

        public MembershipPlansController(GymContext context)
        {
            _context = context;
        }

        // GET: MembershipPlans
        public async Task<IActionResult> Index()
        {
            return View(await _context.MembershipPlan.ToListAsync());
        }

        // GET: MembershipPlans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membershipPlan = await _context.MembershipPlan
                .FirstOrDefaultAsync(m => m.Id == id);
            if (membershipPlan == null)
            {
                return NotFound();
            }

            return View(membershipPlan);
        }

        // GET: MembershipPlans/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MembershipPlans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,DurationInDays,Price")] MembershipPlan membershipPlan)
        {
            if (ModelState.IsValid)
            {
                membershipPlan.IsActive = true;
                membershipPlan.CreatedAt = DateTime.UtcNow;
                membershipPlan.CreatedBy = User.Identity?.Name ?? "Admin";
                membershipPlan.ModifiedBy = User.Identity?.Name ?? "Admin";
                membershipPlan.ModifiedAt = DateTime.UtcNow;

                _context.Add(membershipPlan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(membershipPlan);
        }

        // GET: MembershipPlans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membershipPlan = await _context.MembershipPlan.FindAsync(id);
            if (membershipPlan == null)
            {
                return NotFound();
            }
            membershipPlan.ModifiedBy = User.Identity?.Name ?? "Admin";
            membershipPlan.ModifiedAt = DateTime.UtcNow;
            return View(membershipPlan);
        }

        // POST: MembershipPlans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,DurationInDays,Price,Id,IsActive")] MembershipPlan membershipPlan)
        {
            if (id != membershipPlan.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingMembershipPlan = await _context.MembershipPlan.AsNoTracking().FirstOrDefaultAsync(mp => mp.Id == id);
                    if (existingMembershipPlan == null)
                    {
                        return NotFound();
                    }

                    membershipPlan.CreatedBy = existingMembershipPlan.CreatedBy;
                    membershipPlan.CreatedAt = existingMembershipPlan.CreatedAt;
                    membershipPlan.DeletedBy = existingMembershipPlan.DeletedBy;
                    membershipPlan.DeletedAt = existingMembershipPlan.DeletedAt;
                    membershipPlan.ModifiedBy = User.Identity?.Name ?? "Admin";
                    membershipPlan.ModifiedAt = DateTime.UtcNow;

                    _context.Update(membershipPlan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MembershipPlanExists(membershipPlan.Id))
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
            return View(membershipPlan);
        }

        // GET: MembershipPlans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membershipPlan = await _context.MembershipPlan
                .FirstOrDefaultAsync(m => m.Id == id);
            if (membershipPlan == null)
            {
                return NotFound();
            }

            return View(membershipPlan);
        }

        // POST: MembershipPlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var membershipPlan = await _context.MembershipPlan.FindAsync(id);
            if (membershipPlan != null)
            {
                _context.MembershipPlan.Remove(membershipPlan);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MembershipPlanExists(int id)
        {
            return _context.MembershipPlan.Any(e => e.Id == id);
        }
    }
}
