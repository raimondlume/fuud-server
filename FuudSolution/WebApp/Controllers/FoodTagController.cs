using System.Linq;
using System.Threading.Tasks;
using DAL.App.EF;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    public class FoodTagController : Controller
    {
        private readonly AppDbContext _context;

        public FoodTagController(AppDbContext context)
        {
            _context = context;
        }

        // GET: FoodTag
        public async Task<IActionResult> Index()
        {
            return View(await _context.FoodTags.ToListAsync());
        }

        // GET: FoodTag/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodTag = await _context.FoodTags
                .FirstOrDefaultAsync(m => m.Id == id);
            if (foodTag == null)
            {
                return NotFound();
            }

            return View(foodTag);
        }

        // GET: FoodTag/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FoodTag/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FoodTagValue,Id")] FoodTag foodTag)
        {
            if (ModelState.IsValid)
            {
                _context.Add(foodTag);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(foodTag);
        }

        // GET: FoodTag/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodTag = await _context.FoodTags.FindAsync(id);
            if (foodTag == null)
            {
                return NotFound();
            }
            return View(foodTag);
        }

        // POST: FoodTag/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FoodTagValue,Id")] FoodTag foodTag)
        {
            if (id != foodTag.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(foodTag);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodTagExists(foodTag.Id))
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
            return View(foodTag);
        }

        // GET: FoodTag/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodTag = await _context.FoodTags
                .FirstOrDefaultAsync(m => m.Id == id);
            if (foodTag == null)
            {
                return NotFound();
            }

            return View(foodTag);
        }

        // POST: FoodTag/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var foodTag = await _context.FoodTags.FindAsync(id);
            _context.FoodTags.Remove(foodTag);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodTagExists(int id)
        {
            return _context.FoodTags.Any(e => e.Id == id);
        }
    }
}
