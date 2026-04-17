using Gym.Data.Data;
using Gym.Data.Models.Cms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gym.Admin.Controllers
{
    public class ParameterCategoriesController : Controller
    {
        private readonly GymContext _context;

        public ParameterCategoriesController(GymContext context)
        {
            _context = context;
        }

        // GET: ParameterCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.ParameterCategory.ToListAsync());
        }

        // GET: ParameterCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parameterCategory = await _context.ParameterCategory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parameterCategory == null)
            {
                return NotFound();
            }

            return View(parameterCategory);
        }

        // GET: ParameterCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ParameterCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description")] ParameterCategory parameterCategory)
        {
            if (ModelState.IsValid)
            {
                parameterCategory.IsActive = true;
                parameterCategory.CreatedAt = DateTime.UtcNow;
                parameterCategory.CreatedBy = User.Identity?.Name ?? "Admin";
                parameterCategory.ModifiedBy = User.Identity?.Name ?? "Admin";
                parameterCategory.ModifiedAt = DateTime.UtcNow;

                _context.Add(parameterCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(parameterCategory);
        }

        // GET: ParameterCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parameterCategory = await _context.ParameterCategory.FindAsync(id);
            if (parameterCategory == null)
            {
                return NotFound();
            }
            return View(parameterCategory);
        }

        // POST: ParameterCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Description,Id,IsActive")] ParameterCategory parameterCategory)
        {
            if (id != parameterCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingParameterCategory = await _context.ParameterCategory.AsNoTracking().FirstOrDefaultAsync(pc => pc.Id == id);
                    if (existingParameterCategory == null)
                    {
                        return NotFound();
                    }

                    parameterCategory.CreatedBy = existingParameterCategory.CreatedBy;
                    parameterCategory.CreatedAt = existingParameterCategory.CreatedAt;
                    parameterCategory.ModifiedBy = User.Identity?.Name ?? "Admin";
                    parameterCategory.ModifiedAt = DateTime.UtcNow;
                    parameterCategory.DeletedBy = existingParameterCategory.DeletedBy;
                    parameterCategory.DeletedAt = existingParameterCategory.DeletedAt;
                    _context.Update(parameterCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParameterCategoryExists(parameterCategory.Id))
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
            return View(parameterCategory);
        }

        // GET: ParameterCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parameterCategory = await _context.ParameterCategory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parameterCategory == null)
            {
                return NotFound();
            }

            return View(parameterCategory);
        }

        // POST: ParameterCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parameterCategory = await _context.ParameterCategory.FindAsync(id);
            if (parameterCategory != null)
            {
                _context.ParameterCategory.Remove(parameterCategory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParameterCategoryExists(int id)
        {
            return _context.ParameterCategory.Any(e => e.Id == id);
        }
    }
}
