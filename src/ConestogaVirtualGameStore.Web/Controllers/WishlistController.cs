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
    public class WishlistController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WishlistController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Wishlist
        public async Task<IActionResult> Index()
        {
            return View(await _context.Wishlist.ToListAsync());
        }

        // GET: Wishlist/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wishlist = await _context.Wishlist
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
            return View();
        }

        // POST: Wishlist/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,ImageFileName,Price,RecordId")] Wishlist wishlist)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wishlist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(wishlist);
        }

        // GET: Wishlist/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wishlist = await _context.Wishlist.SingleOrDefaultAsync(m => m.RecordId == id);
            if (wishlist == null)
            {
                return NotFound();
            }
            return View(wishlist);
        }

        // POST: Wishlist/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Title,ImageFileName,Price,RecordId")] Wishlist wishlist)
        {
            if (id != wishlist.RecordId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wishlist);
                    await _context.SaveChangesAsync();
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
            return View(wishlist);
        }

        // GET: Wishlist/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wishlist = await _context.Wishlist
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
            var wishlist = await _context.Wishlist.SingleOrDefaultAsync(m => m.RecordId == id);
            _context.Wishlist.Remove(wishlist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WishlistExists(long id)
        {
            return _context.Wishlist.Any(e => e.RecordId == id);
        }
        public async Task<IActionResult> Add(String Title)
        {
            Wishlist wishlist;

            if (Title == null)
            {
                return NotFound();
            }
            var game = await _context.Games.SingleOrDefaultAsync(a => a.Title == Title);

            if (wishlist == null)
            {
                return NotFound();
            }

            return View(wishlist);
        }
    }
}
