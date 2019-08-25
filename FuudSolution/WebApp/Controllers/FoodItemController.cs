using System.Linq;
using System.Threading.Tasks;
using DAL.App.EF;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    public class FoodItemController : Controller
    {
        private readonly AppDbContext _context;

        public FoodItemController(AppDbContext context)
        {
            _context = context;
        }

        // GET: FoodItem
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.FoodItems.Include(f => f.FoodCategory).Include(f => f.Provider);
            return View(await appDbContext.ToListAsync());
        }

        // GET: FoodItem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodItem = await _context.FoodItems
                .Include(f => f.FoodCategory)
                .Include(f => f.Provider)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (foodItem == null)
            {
                return NotFound();
            }

            return View(foodItem);
        }

        // GET: FoodItem/Create
        public IActionResult Create()
        {
            ViewData["FoodCategoryId"] = new SelectList(_context.FoodCategories, "Id", "FoodCategoryValue");
            ViewData["ProviderId"] = new SelectList(_context.Providers, "Id", "Name");
            return View();
        }

        // POST: FoodItem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NameEst,NameEng,DateStart,DateEnd,FoodCategoryId,ProviderId,Id")] FoodItem foodItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(foodItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FoodCategoryId"] = new SelectList(_context.FoodCategories, "Id", "FoodCategoryValue", foodItem.FoodCategoryId);
            ViewData["ProviderId"] = new SelectList(_context.Providers, "Id", "Name", foodItem.ProviderId);
            return View(foodItem);
        }

        // GET: FoodItem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodItem = await _context.FoodItems.FindAsync(id);
            if (foodItem == null)
            {
                return NotFound();
            }
            ViewData["FoodCategoryId"] = new SelectList(_context.FoodCategories, "Id", "FoodCategoryValue", foodItem.FoodCategoryId);
            ViewData["ProviderId"] = new SelectList(_context.Providers, "Id", "Name", foodItem.ProviderId);
            return View(foodItem);
        }

        // POST: FoodItem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NameEst,NameEng,DateStart,DateEnd,FoodCategoryId,ProviderId,Id")] FoodItem foodItem)
        {
            if (id != foodItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(foodItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodItemExists(foodItem.Id))
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
            ViewData["FoodCategoryId"] = new SelectList(_context.FoodCategories, "Id", "FoodCategoryValue", foodItem.FoodCategoryId);
            ViewData["ProviderId"] = new SelectList(_context.Providers, "Id", "Name", foodItem.ProviderId);
            return View(foodItem);
        }

        // GET: FoodItem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodItem = await _context.FoodItems
                .Include(f => f.FoodCategory)
                .Include(f => f.Provider)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (foodItem == null)
            {
                return NotFound();
            }

            return View(foodItem);
        }

        // POST: FoodItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var foodItem = await _context.FoodItems.FindAsync(id);
            _context.FoodItems.Remove(foodItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodItemExists(int id)
        {
            return _context.FoodItems.Any(e => e.Id == id);
        }
    }
}
