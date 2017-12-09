namespace ConestogaVirtualGameStore.Web.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Data;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;
    using Models;
    using Microsoft.AspNetCore.Http;
    using System;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Authorization;

    [Authorize(Roles = "Employee")]
    public class RolesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private RoleManager<IdentityRole> RoleManager;
        private UserManager<ApplicationUser> UserManager;

        public RolesController(ApplicationDbContext context,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            this._context = context;
            this.RoleManager = roleManager;
            this.UserManager = userManager;

        }

        public async Task<IActionResult> Index()
        {

            return View(await this._context.Roles.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(IFormCollection collection)

        {
            try
            {
                this._context.Roles.Add(new IdentityRole
                {
                    Name = collection["RoleName"],
                    NormalizedName = collection["RoleName"].ToString().ToUpperInvariant()
                });
                await this._context.SaveChangesAsync();
                return RedirectToAction("Index", "Roles");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(string roleName)
        {
            var role = this._context.Roles.FirstOrDefault(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase));
            this._context.Roles.Remove(role);
            this._context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ManageUsers()
        {
            var roleList = this._context.Roles.OrderBy(b => b.Name).ToList()
                .Select(bb => new SelectListItem { Value = bb.Name.ToString(), Text = bb.Name }).ToList();


            this.ViewBag.Roles = roleList;
            return View("ManageUsers");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRoleToUser(string UserName, string RoleName)
        {
            ApplicationUser user = this._context.Users.FirstOrDefault(a => a.Email.Equals(UserName, StringComparison.CurrentCultureIgnoreCase));
            await this.UserManager.AddToRoleAsync(user, RoleName);

            this.ViewBag.Results = UserName + "has been added to the " + RoleName + "group";

            //this._context.SaveChanges();

            //var roleList = this._context.Roles.OrderBy(b => b.Name).ToList()
            //   .Select(bb => new SelectListItem { Value = bb.Name.ToString(), Text = bb.Name }).ToList();
            //this.ViewBag.Roles = roleList;

            return RedirectToAction("Index");
        }
    }
}