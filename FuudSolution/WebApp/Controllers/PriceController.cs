using System.Linq;
using System.Threading.Tasks;
using DAL.App.EF;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    public class PriceController : Controller
    {
        private readonly AppDbContext _context;

        public PriceController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Price
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Prices.Include(p => p.FoodItem);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Price/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var price = await _context.Prices
                .Include(p => p.FoodItem)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (price == null)
            {
                return NotFound();
            }

            return View(price);
        }

        // GET: Price/Create
        public IActionResult Create()
        {
            ViewData["FoodItemId"] = new SelectList(_context.FoodItems, "Id", "NameEst");
            return View();
        }

        // POST: Price/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PriceValue,ModifierName,FoodItemId,Id")] Price price)
        {
            if (ModelState.IsValid)
            {
                _context.Add(price);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FoodItemId"] = new SelectList(_context.FoodItems, "Id", "NameEst", price.FoodItemId);
            return View(price);
        }

        // GET: Price/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var price = await _context.Prices.FindAsync(id);
            if (price == null)
            {
                return NotFound();
            }
            ViewData["FoodItemId"] = new SelectList(_context.FoodItems, "Id", "NameEst", price.FoodItemId);
            return View(price);
        }

        // POST: Price/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PriceValue,ModifierName,FoodItemId,Id")] Price price)
        {
            if (id != price.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(price);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PriceExists(price.Id))
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
            ViewData["FoodItemId"] = new SelectList(_context.FoodItems, "Id", "NameEst", price.FoodItemId);
            return View(price);
        }

        // GET: Price/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var price = await _context.Prices
                .Include(p => p.FoodItem)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (price == null)
            {
                return NotFound();
            }

            return View(price);
        }

        // POST: Price/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var price = await _context.Prices.FindAsync(id);
            _context.Prices.Remove(price);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PriceExists(int id)
        {
            return _context.Prices.Any(e => e.Id == id);
        }
    }
}
