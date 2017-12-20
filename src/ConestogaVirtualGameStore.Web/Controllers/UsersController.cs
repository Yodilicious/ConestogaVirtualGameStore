namespace ConestogaVirtualGameStore.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Data;
    using Microsoft.AspNetCore.Authorization;
    using Models;
    using Models.ViewModels;

    [Authorize]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var me = await this._context.ApplicationUser.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
            var users = await this._context.ApplicationUser.Where(u => u.UserName != User.Identity.Name).ToListAsync();
            var friends = await this._context.Friends.Where(f => f.UserId == me.Id).ToListAsync();

            var vms = new List<FriendViewModel>();
            foreach (var user in users)
            {
                var vm = new FriendViewModel();

                vm.Id = user.Id;
                vm.Email = user.Email;

                if (friends.Exists(u => u.FriendId == user.Id))
                {
                    vm.IsFriend = true;
                }

                vms.Add(vm);
            }

            return View(vms);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.ApplicationUser
                .SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return View(applicationUser);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(applicationUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(applicationUser);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }
            return View(applicationUser);
        }

        // POST: Users/Edit/5
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

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicationUser);
                    await _context.SaveChangesAsync();
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

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.ApplicationUser
                .SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return View(applicationUser);
        }

        public async Task<IActionResult> AddFriend(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var me = this._context.ApplicationUser.FirstOrDefault(f => f.UserName == User.Identity.Name);

            if (me != null)
            {
                var friend = new Friend();

                friend.UserId = me.Id;
                friend.FriendId = id;

                this._context.Friends.Add(friend);
                this._context.SaveChanges();
            }

            var users = await this._context.ApplicationUser.Where(u => u.UserName != User.Identity.Name).ToListAsync();
            var friends = await this._context.Friends.Where(f => f.UserId == me.Id).ToListAsync();

            var vms = new List<FriendViewModel>();
            foreach (var user in users)
            {
                var vm = new FriendViewModel();

                vm.Id = user.Id;
                vm.Email = user.Email;

                if (friends.Exists(u => u.FriendId == user.Id))
                {
                    vm.IsFriend = true;
                }

                vms.Add(vm);
            }

            return View("Index", vms);
        }

        public IActionResult RemoveFriend(string id)
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
                var vm = new FriendViewModel();

                vm.Id = user.Id;
                vm.Email = user.Email;

                if (friends.Exists(u => u.FriendId == user.Id))
                {
                    vm.IsFriend = true;
                }

                vms.Add(vm);
            }

            return View("Index", vms);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var applicationUser = await _context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == id);
            _context.ApplicationUser.Remove(applicationUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationUserExists(string id)
        {
            return _context.ApplicationUser.Any(e => e.Id == id);
        }
    }
}
