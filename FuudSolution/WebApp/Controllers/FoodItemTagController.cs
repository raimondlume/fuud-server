using System.Linq;
using System.Threading.Tasks;
using DAL.App.EF;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    public class FoodItemTagController : Controller
    {
        private readonly AppDbContext _context;

        public FoodItemTagController(AppDbContext context)
        {
            _context = context;
        }

        // GET: FoodItemTag
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.FoodItemTags.Include(f => f.FoodItem).Include(f => f.FoodTag);
            return View(await appDbContext.ToListAsync());
        }

        // GET: FoodItemTag/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodItemTag = await _context.FoodItemTags
                .Include(f => f.FoodItem)
                .Include(f => f.FoodTag)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (foodItemTag == null)
            {
                return NotFound();
            }

            return View(foodItemTag);
        }

        // GET: FoodItemTag/Create
        public IActionResult Create()
        {
            ViewData["FoodItemId"] = new SelectList(_context.FoodItems, "Id", "NameEst");
            ViewData["FoodTagId"] = new SelectList(_context.FoodTags, "Id", "FoodTagValue");
            return View();
        }

        // POST: FoodItemTag/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FoodItemId,FoodTagId,Id")] FoodItemTag foodItemTag)
        {
            if (ModelState.IsValid)
            {
                _context.Add(foodItemTag);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FoodItemId"] = new SelectList(_context.FoodItems, "Id", "NameEst", foodItemTag.FoodItemId);
            ViewData["FoodTagId"] = new SelectList(_context.FoodTags, "Id", "FoodTagValue", foodItemTag.FoodTagId);
            return View(foodItemTag);
        }

        // GET: FoodItemTag/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodItemTag = await _context.FoodItemTags.FindAsync(id);
            if (foodItemTag == null)
            {
                return NotFound();
            }
            ViewData["FoodItemId"] = new SelectList(_context.FoodItems, "Id", "NameEst", foodItemTag.FoodItemId);
            ViewData["FoodTagId"] = new SelectList(_context.FoodTags, "Id", "FoodTagValue", foodItemTag.FoodTagId);
            return View(foodItemTag);
        }

        // POST: FoodItemTag/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FoodItemId,FoodTagId,Id")] FoodItemTag foodItemTag)
        {
            if (id != foodItemTag.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(foodItemTag);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodItemTagExists(foodItemTag.Id))
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
            ViewData["FoodItemId"] = new SelectList(_context.FoodItems, "Id", "NameEst", foodItemTag.FoodItemId);
            ViewData["FoodTagId"] = new SelectList(_context.FoodTags, "Id", "FoodTagValue", foodItemTag.FoodTagId);
            return View(foodItemTag);
        }

        // GET: FoodItemTag/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodItemTag = await _context.FoodItemTags
                .Include(f => f.FoodItem)
                .Include(f => f.FoodTag)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (foodItemTag == null)
            {
                return NotFound();
            }

            return View(foodItemTag);
        }

        // POST: FoodItemTag/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var foodItemTag = await _context.FoodItemTags.FindAsync(id);
            _context.FoodItemTags.Remove(foodItemTag);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodItemTagExists(int id)
        {
            return _context.FoodItemTags.Any(e => e.Id == id);
        }
    }
}
