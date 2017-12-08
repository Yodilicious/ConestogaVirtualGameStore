namespace ConestogaVirtualGameStore.Web.Controllers
{
    using System;
    using Microsoft.AspNetCore.Authorization;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using Data;
    using Models;

    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShoppingCartController(ApplicationDbContext context)
        {
            this._context = context;
        }

        // GET: ShoppingCart
        public async Task<IActionResult> Index()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return NotFound();
            }

            var applicationDbContext = this._context.ShoppingCartItems.Include(s => s.Game).Include(s => s.ShoppingCart)
                .Where(s => s.ShoppingCart.HasPaid == false && s.ShoppingCart.User == this.User.Identity.Name);

            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> Checkout()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return NotFound();
            }

            var cart = this._context.ShoppingCarts.FirstOrDefault(s => s.HasPaid == false && s.User == this.User.Identity.Name);

            if (cart == null)
            {
                return NotFound();
            }

            cart.HasPaid = true;
            cart.PurcheasedOn = DateTime.Now;

            await this._context.SaveChangesAsync();

            return View();
        }

        // GET: ShoppingCart/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingCartItem = await this._context.ShoppingCartItems
                .Include(s => s.Game)
                .Include(s => s.ShoppingCart)
                .SingleOrDefaultAsync(m => m.RecordId == id);
            if (shoppingCartItem == null)
            {
                return NotFound();
            }

            return View(shoppingCartItem);
        }

        // GET: ShoppingCart/Create
        public IActionResult Create()
        {
            this.ViewData["GameId"] = new SelectList(this._context.Games, "RecordId", "Description");
            this.ViewData["ShoppingCartId"] = new SelectList(this._context.ShoppingCarts, "RecordId", "RecordId");
            return View();
        }

        // POST: ShoppingCart/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShoppingCartId,GameId,Price,AddedOn,RecordId")] ShoppingCartItem shoppingCartItem)
        {
            if (this.ModelState.IsValid)
            {
                this._context.Add(shoppingCartItem);
                await this._context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            this.ViewData["GameId"] = new SelectList(this._context.Games, "RecordId", "Description", shoppingCartItem.GameId);
            this.ViewData["ShoppingCartId"] = new SelectList(this._context.ShoppingCarts, "RecordId", "RecordId", shoppingCartItem.ShoppingCartId);
            return View(shoppingCartItem);
        }

        // GET: ShoppingCart/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingCartItem = await this._context.ShoppingCartItems.SingleOrDefaultAsync(m => m.RecordId == id);
            if (shoppingCartItem == null)
            {
                return NotFound();
            }
            this.ViewData["GameId"] = new SelectList(this._context.Games, "RecordId", "Description", shoppingCartItem.GameId);
            this.ViewData["ShoppingCartId"] = new SelectList(this._context.ShoppingCarts, "RecordId", "RecordId", shoppingCartItem.ShoppingCartId);
            return View(shoppingCartItem);
        }

        // POST: ShoppingCart/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("ShoppingCartId,GameId,Price,AddedOn,RecordId")] ShoppingCartItem shoppingCartItem)
        {
            if (id != shoppingCartItem.RecordId)
            {
                return NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this._context.Update(shoppingCartItem);
                    await this._context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShoppingCartItemExists(shoppingCartItem.RecordId))
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
            this.ViewData["GameId"] = new SelectList(this._context.Games, "RecordId", "Description", shoppingCartItem.GameId);
            this.ViewData["ShoppingCartId"] = new SelectList(this._context.ShoppingCarts, "RecordId", "RecordId", shoppingCartItem.ShoppingCartId);
            return View(shoppingCartItem);
        }

        // GET: ShoppingCart/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingCartItem = await this._context.ShoppingCartItems
                .Include(s => s.Game)
                .Include(s => s.ShoppingCart)
                .SingleOrDefaultAsync(m => m.RecordId == id);
            if (shoppingCartItem == null)
            {
                return NotFound();
            }

            return View(shoppingCartItem);
        }

        // POST: ShoppingCart/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var shoppingCartItem = await this._context.ShoppingCartItems.SingleOrDefaultAsync(m => m.RecordId == id);
            this._context.ShoppingCartItems.Remove(shoppingCartItem);
            await this._context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShoppingCartItemExists(long id)
        {
            return this._context.ShoppingCartItems.Any(e => e.RecordId == id);
        }
    }
}
