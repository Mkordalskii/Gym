using Gym.Data.Data;
using Gym.Data.Models.Cms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Gym.Admin.Controllers
{
    public class ParametersController : Controller
    {
        private readonly GymContext _context;

        public ParametersController(GymContext context)
        {
            _context = context;
        }

        // GET: Parameters
        public async Task<IActionResult> Index()
        {
            var gymContext = _context.Parameter.Include(p => p.ParameterCategory);
            return View(await gymContext.ToListAsync());
        }

        // GET: Parameters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parameter = await _context.Parameter
                .Include(p => p.ParameterCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parameter == null)
            {
                return NotFound();
            }

            return View(parameter);
        }

        // GET: Parameters/Create
        public IActionResult Create()
        {
            ViewData["ParameterCategoryId"] = new SelectList(_context.ParameterCategory, "Id", "Name");
            return View();
        }

        // POST: Parameters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Value,Description,ParameterCategoryId")] Parameter parameter)
        {
            if (ModelState.IsValid)
            {
                parameter.IsActive = true;
                parameter.CreatedAt = DateTime.UtcNow;
                parameter.CreatedBy = User.Identity?.Name ?? "Admin";
                parameter.ModifiedBy = User.Identity?.Name ?? "Admin";
                parameter.ModifiedAt = DateTime.UtcNow;

                _context.Add(parameter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParameterCategoryId"] = new SelectList(_context.ParameterCategory, "Id", "Name", parameter.ParameterCategoryId);
            return View(parameter);
        }

        // GET: Parameters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parameter = await _context.Parameter.FindAsync(id);
            if (parameter == null)
            {
                return NotFound();
            }
            ViewData["ParameterCategoryId"] = new SelectList(_context.ParameterCategory, "Id", "Name", parameter.ParameterCategoryId);
            return View(parameter);
        }

        // POST: Parameters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Value,Description,ParameterCategoryId,Id,IsActive")] Parameter parameter)
        {
            if (id != parameter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingParameter = await _context.Parameter.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
                    if (existingParameter == null)
                    {
                        return NotFound();
                    }

                    parameter.CreatedBy = existingParameter.CreatedBy;
                    parameter.CreatedAt = existingParameter.CreatedAt;
                    parameter.ModifiedBy = User.Identity?.Name ?? "Admin";
                    parameter.ModifiedAt = DateTime.UtcNow;
                    parameter.DeletedBy = existingParameter.DeletedBy;
                    parameter.DeletedAt = existingParameter.DeletedAt;
                    _context.Update(parameter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParameterExists(parameter.Id))
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
            ViewData["ParameterCategoryId"] = new SelectList(_context.ParameterCategory, "Id", "Name", parameter.ParameterCategoryId);
            return View(parameter);
        }

        // GET: Parameters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parameter = await _context.Parameter
                .Include(p => p.ParameterCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parameter == null)
            {
                return NotFound();
            }

            return View(parameter);
        }

        // POST: Parameters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parameter = await _context.Parameter.FindAsync(id);
            if (parameter != null)
            {
                _context.Parameter.Remove(parameter);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParameterExists(int id)
        {
            return _context.Parameter.Any(e => e.Id == id);
        }
    }
}
