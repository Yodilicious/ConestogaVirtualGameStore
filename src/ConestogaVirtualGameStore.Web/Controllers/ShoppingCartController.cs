using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ConestogaVirtualGameStore.Web.Data;
using ConestogaVirtualGameStore.Web.Models;

namespace ConestogaVirtualGameStore.Web.Controllers
{
    using System;

    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShoppingCartController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ShoppingCart
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ShoppingCartItems.Include(s => s.Game).Include(s => s.ShoppingCart)
                .Where(s => s.ShoppingCart.HasPaid == false);

            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> Checkout()
        {
            var cart = this._context.ShoppingCarts.FirstOrDefault(s => s.HasPaid == false);

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

            var shoppingCartItem = await _context.ShoppingCartItems
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
            ViewData["GameId"] = new SelectList(_context.Games, "RecordId", "Description");
            ViewData["ShoppingCartId"] = new SelectList(_context.ShoppingCarts, "RecordId", "RecordId");
            return View();
        }

        // POST: ShoppingCart/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShoppingCartId,GameId,Price,AddedOn,RecordId")] ShoppingCartItem shoppingCartItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shoppingCartItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GameId"] = new SelectList(_context.Games, "RecordId", "Description", shoppingCartItem.GameId);
            ViewData["ShoppingCartId"] = new SelectList(_context.ShoppingCarts, "RecordId", "RecordId", shoppingCartItem.ShoppingCartId);
            return View(shoppingCartItem);
        }

        // GET: ShoppingCart/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingCartItem = await _context.ShoppingCartItems.SingleOrDefaultAsync(m => m.RecordId == id);
            if (shoppingCartItem == null)
            {
                return NotFound();
            }
            ViewData["GameId"] = new SelectList(_context.Games, "RecordId", "Description", shoppingCartItem.GameId);
            ViewData["ShoppingCartId"] = new SelectList(_context.ShoppingCarts, "RecordId", "RecordId", shoppingCartItem.ShoppingCartId);
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

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shoppingCartItem);
                    await _context.SaveChangesAsync();
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
            ViewData["GameId"] = new SelectList(_context.Games, "RecordId", "Description", shoppingCartItem.GameId);
            ViewData["ShoppingCartId"] = new SelectList(_context.ShoppingCarts, "RecordId", "RecordId", shoppingCartItem.ShoppingCartId);
            return View(shoppingCartItem);
        }

        // GET: ShoppingCart/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingCartItem = await _context.ShoppingCartItems
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
            var shoppingCartItem = await _context.ShoppingCartItems.SingleOrDefaultAsync(m => m.RecordId == id);
            _context.ShoppingCartItems.Remove(shoppingCartItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShoppingCartItemExists(long id)
        {
            return _context.ShoppingCartItems.Any(e => e.RecordId == id);
        }
    }
}
