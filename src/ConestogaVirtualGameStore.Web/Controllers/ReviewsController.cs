namespace ConestogaVirtualGameStore.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using Data;
    using Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;

    [Authorize]
    public class ReviewsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReviewsController(ApplicationDbContext context)
        {
            this._context = context;
        }

        // GET: Reviews
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = this._context.Reviews.Include(r => r.Game);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Reviews/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await this._context.Reviews
                .Include(r => r.Game)
                .SingleOrDefaultAsync(m => m.RecordId == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        public async Task<IActionResult> Approve(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await this._context.Reviews
                .Include(r => r.Game)
                .SingleOrDefaultAsync(m => m.RecordId == id);

            if (review == null)
            {
                return NotFound();
            }

            review.IsApproved = true;

            this._context.SaveChanges();

            return RedirectToAction("Details", "Game", new {@id = review.Game.RecordId});
        }
        
        // GET: Reviews/Create
        public IActionResult Create()
        {
            this.ViewData["GameId"] = this.HttpContext.Session.GetInt32("game_id");
            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Author,Title,ReviewText,Rating,Date,GameId,RecordId")] Review review)
        {
            this.ViewData["GameId"] = this.HttpContext.Session.GetInt32("game_id");

            if (this.ModelState.IsValid)
            {
                review.IsApproved = false;
                review.Author = this.User.Identity.Name;
                review.Date = DateTime.Now;

                if (review.Rating < 0)
                {
                    review.Rating = 1;
                }

                if (review.Rating > 5)
                {
                    review.Rating = 5;
                }

                if (string.IsNullOrWhiteSpace(review.Title) || string.IsNullOrWhiteSpace(review.ReviewText))
                {
                    return View(review);
                }

                this._context.Add(review);
                await this._context.SaveChangesAsync();
                return RedirectToAction("Details", "Game", new { @id = review.GameId });
            }

            return View(review);
        }

        // GET: Reviews/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await this._context.Reviews.SingleOrDefaultAsync(m => m.RecordId == id);
            if (review == null)
            {
                return NotFound();
            }
            this.ViewData["GameId"] = new SelectList(this._context.Games, "RecordId", "Description", review.GameId);
            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Author,Title,ReviewText,Rating,Date,GameId,RecordId")] Review review)
        {
            if (id != review.RecordId)
            {
                return NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this._context.Update(review);
                    await this._context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewExists(review.RecordId))
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
            this.ViewData["GameId"] = new SelectList(this._context.Games, "RecordId", "Description", review.GameId);
            return View(review);
        }

        // GET: Reviews/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await this._context.Reviews
                .Include(r => r.Game)
                .SingleOrDefaultAsync(m => m.RecordId == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var review = await this._context.Reviews.SingleOrDefaultAsync(m => m.RecordId == id);
            this._context.Reviews.Remove(review);
            await this._context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReviewExists(long id)
        {
            return this._context.Reviews.Any(e => e.RecordId == id);
        }
    }
}
