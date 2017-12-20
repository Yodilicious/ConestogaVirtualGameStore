namespace ConestogaVirtualGameStore.Web.Controllers
{
    using System.Collections.Generic;
    using Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System.Linq;
    using System.Threading.Tasks;
    using Models.ViewModels;

    [Authorize]
    public class FriendsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FriendsController(ApplicationDbContext context)
        {
            this._context = context;
        }

        // GET: Friends
        public async Task<IActionResult> Index(string id)
        {
            var me = await this._context.ApplicationUser.FirstOrDefaultAsync(u => u.UserName == this.User.Identity.Name);
            var users = await this._context.ApplicationUser.Where(u => u.UserName != this.User.Identity.Name).ToListAsync();
            var friends = await this._context.Friends.Where(f => f.UserId == me.Id).ToListAsync();

            var vms = new List<FriendViewModel>();
            foreach (var user in users)
            {
                if (friends.Exists(u => u.FriendId == user.Id))
                {
                    var vm = new FriendViewModel();

                    vm.Id = user.Id;
                    vm.Email = user.Email;
                    vm.IsFriend = true;

                    vms.Add(vm);
                }
            }

            return View(vms);
        }

        public IActionResult Remove(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var me = this._context.ApplicationUser.FirstOrDefault(f => f.UserName == this.User.Identity.Name);

            if (me != null)
            {
                var friend = this._context.Friends.FirstOrDefault(f => f.FriendId == id && f.UserId == me.Id);

                this._context.Friends.Remove(friend);
                this._context.SaveChanges();
            }

            var users = this._context.ApplicationUser.Where(u => u.UserName != this.User.Identity.Name).ToList();
            var friends = this._context.Friends.Where(f => f.UserId == me.Id).ToList();

            var vms = new List<FriendViewModel>();
            foreach (var user in users)
            {
                if (friends.Exists(u => u.FriendId == user.Id))
                {
                    var vm = new FriendViewModel();

                    vm.Id = user.Id;
                    vm.Email = user.Email;
                    vm.IsFriend = true;

                    vms.Add(vm);
                }
            }

            return View("Index", vms);
        }

        // GET: Friends/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await this._context.ApplicationUser
                .SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return View(applicationUser);
        }

        // GET: Friends/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Friends/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] ApplicationUser applicationUser)
        {
            if (this.ModelState.IsValid)
            {
                this._context.Add(applicationUser);
                await this._context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(applicationUser);
        }

        // GET: Friends/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await this._context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }
            return View(applicationUser);
        }

        // POST: Friends/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] ApplicationUser applicationUser)
        {
            if (id != applicationUser.Id)
            {
                return NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this._context.Update(applicationUser);
                    await this._context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationUserExists(applicationUser.Id))
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
            return View(applicationUser);
        }

        // GET: Friends/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await this._context.ApplicationUser
                .SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return View(applicationUser);
        }

        // POST: Friends/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var applicationUser = await this._context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == id);
            this._context.ApplicationUser.Remove(applicationUser);
            await this._context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationUserExists(string id)
        {
            return this._context.ApplicationUser.Any(e => e.Id == id);
        }
    }
}
