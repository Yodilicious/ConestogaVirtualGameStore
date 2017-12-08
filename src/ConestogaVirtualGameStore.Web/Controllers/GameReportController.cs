namespace ConestogaVirtualGameStore.Web.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Data;
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    public class GameReportController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GameReportController(ApplicationDbContext context)
        {
            this._context = context;
        }

        // GET: GameReport
        public async Task<IActionResult> Index()
        {
            return View(await this._context.Games.ToListAsync());
        }
    }
}
