using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ConestogaVirtualGameStore.Web.Data;
using ConestogaVirtualGameStore.Web.Models;

namespace ConestogaVirtualGameStore.Web.Controllers
{
    public class LibraryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LibraryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Library
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ShoppingCartItems.Include(s => s.Game).Include(s => s.ShoppingCart);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Library/Details/5
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

        // GET: Library/Create
        public IActionResult Create()
        {
            ViewData["GameId"] = new SelectList(_context.Games, "RecordId", "Description");
            ViewData["ShoppingCartId"] = new SelectList(_context.ShoppingCarts, "RecordId", "User");
            return View();
        }

        // POST: Library/Create
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
            ViewData["ShoppingCartId"] = new SelectList(_context.ShoppingCarts, "RecordId", "User", shoppingCartItem.ShoppingCartId);
            return View(shoppingCartItem);
        }

        // GET: Library/Edit/5
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
            ViewData["ShoppingCartId"] = new SelectList(_context.ShoppingCarts, "RecordId", "User", shoppingCartItem.ShoppingCartId);
            return View(shoppingCartItem);
        }

        // POST: Library/Edit/5
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
            ViewData["ShoppingCartId"] = new SelectList(_context.ShoppingCarts, "RecordId", "User", shoppingCartItem.ShoppingCartId);
            return View(shoppingCartItem);
        }

        // GET: Library/Delete/5
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

        // POST: Library/Delete/5
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
