namespace ConestogaVirtualGameStore.Web.Controllers
{
    using Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System.Linq;
    using System.Threading.Tasks;

    [Authorize]
    public class WishlistController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WishlistController(ApplicationDbContext context)
        {
            this._context = context;
        }

        // GET: Wishlist
        public async Task<IActionResult> Index(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                var applicationDbContext = this._context.Wishlist.Include(w => w.Game).Where(w => w.User == this.User.Identity.Name);
                return View(await applicationDbContext.ToListAsync());
            }
            else
            {
                var applicationDbContext = this._context.Wishlist.Include(w => w.Game).Where(w => w.User == id);
                return View(await applicationDbContext.ToListAsync());
            }
        }

        // GET: Wishlist/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wishlist = await this._context.Wishlist
                .Include(w => w.Game)
                .SingleOrDefaultAsync(m => m.RecordId == id);
            if (wishlist == null)
            {
                return NotFound();
            }

            return View(wishlist);
        }

        // GET: Wishlist/Create
        public IActionResult Create()
        {
            this.ViewData["GameId"] = new SelectList(this._context.Games, "RecordId", "Description");
            return View();
        }

        // POST: Wishlist/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("User,GameId,RecordId")] Wishlist wishlist)
        {
            if (this.ModelState.IsValid)
            {
                this._context.Add(wishlist);
                await this._context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            this.ViewData["GameId"] = new SelectList(this._context.Games, "RecordId", "Description", wishlist.GameId);
            return View(wishlist);
        }

        // GET: Wishlist/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wishlist = await this._context.Wishlist.SingleOrDefaultAsync(m => m.RecordId == id);
            if (wishlist == null)
            {
                return NotFound();
            }
            this.ViewData["GameId"] = new SelectList(this._context.Games, "RecordId", "Description", wishlist.GameId);
            return View(wishlist);
        }

        // POST: Wishlist/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("User,GameId,RecordId")] Wishlist wishlist)
        {
            if (id != wishlist.RecordId)
            {
                return NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this._context.Update(wishlist);
                    await this._context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WishlistExists(wishlist.RecordId))
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
            this.ViewData["GameId"] = new SelectList(this._context.Games, "RecordId", "Description", wishlist.GameId);
            return View(wishlist);
        }

        // GET: Wishlist/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wishlist = await this._context.Wishlist
                .Include(w => w.Game)
                .SingleOrDefaultAsync(m => m.RecordId == id);
            if (wishlist == null)
            {
                return NotFound();
            }

            return View(wishlist);
        }

        // POST: Wishlist/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var wishlist = await this._context.Wishlist.SingleOrDefaultAsync(m => m.RecordId == id);
            this._context.Wishlist.Remove(wishlist);
            await this._context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WishlistExists(long id)
        {
            return this._context.Wishlist.Any(e => e.RecordId == id);
        }
    }
}
