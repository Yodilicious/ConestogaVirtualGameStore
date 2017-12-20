namespace ConestogaVirtualGameStore.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Http.Headers;
    using Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Models.ViewModels;
    using Repository;

    [Authorize]
    public class EventController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public EventController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Event
        public IActionResult Index()
        {
            var me = this._context.ApplicationUser.FirstOrDefault(u => u.UserName == this.User.Identity.Name);
            var events = this._context.Events.ToList();
            var myEvents = this._context.EventRegistrations.Where(evt => evt.UserId == me.Id).ToList();

            var vms = new List<EventViewModel>();
            foreach (var evt in events)
            {
                var vm = new EventViewModel();

                vm.RecordId = evt.RecordId;
                vm.Date = evt.Date;
                vm.Description = evt.Description;
                vm.ImagePath = evt.ImagePath;
                vm.Title = evt.Title;
                vm.IsRegistered = myEvents.Exists(m => m.EventId == evt.RecordId);

                vms.Add(vm);
            }
            
            return View(vms);
        }

        public IActionResult Register(long id)
        {
            var me = this._context.ApplicationUser.FirstOrDefault(u => u.UserName == this.User.Identity.Name);

            var registration = new EventRegistration();

            registration.EventId = id;
            registration.UserId = me.Id;
            registration.RegisteredOn = DateTime.Now;

            this._context.EventRegistrations.Add(registration);
            this._context.SaveChanges();
            
            var events = this._context.Events.ToList();
            var myEvents = this._context.EventRegistrations.Where(evt => evt.UserId == me.Id).ToList();

            var vms = new List<EventViewModel>();
            foreach (var evt in events)
            {
                var vm = new EventViewModel();

                vm.RecordId = evt.RecordId;
                vm.Date = evt.Date;
                vm.Description = evt.Description;
                vm.ImagePath = evt.ImagePath;
                vm.Title = evt.Title;
                vm.IsRegistered = myEvents.Exists(m => m.EventId == evt.RecordId);

                vms.Add(vm);
            }

            return View("Index", vms);
        }

        public IActionResult Unregister(long id)
        {
            var me = this._context.ApplicationUser.FirstOrDefault(u => u.UserName == this.User.Identity.Name);

            var registration =
                this._context.EventRegistrations.FirstOrDefault(er => er.EventId == id && er.UserId == me.Id);
            
            this._context.EventRegistrations.Remove(registration);
            this._context.SaveChanges();

            var events = this._context.Events.ToList();
            var myEvents = this._context.EventRegistrations.Where(evt => evt.UserId == me.Id).ToList();

            var vms = new List<EventViewModel>();
            foreach (var evt in events)
            {
                var vm = new EventViewModel();

                vm.RecordId = evt.RecordId;
                vm.Date = evt.Date;
                vm.Description = evt.Description;
                vm.ImagePath = evt.ImagePath;
                vm.Title = evt.Title;
                vm.IsRegistered = myEvents.Exists(m => m.EventId == evt.RecordId);

                vms.Add(vm);
            }

            return View("Index", vms);
        }

        // GET: Event/Details/5
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = _context.Events
                .SingleOrDefault(m => m.RecordId == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // GET: Event/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Event/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Title,Description,Date,RecordId,File")] EventCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var evt = new Event();

                evt.Date = vm.Date;
                evt.Description = vm.Description;
                evt.Title = vm.Title;
                evt.RecordId = vm.RecordId;

                var file = vm.File;
                var parsedContentDisposition =
                    ContentDispositionHeaderValue.Parse(file.ContentDisposition);
                var filename = Path.Combine(_hostingEnvironment.WebRootPath, "images", "events", parsedContentDisposition.FileName.Trim('"'));
                using (var stream = System.IO.File.OpenWrite(filename))
                {
                    file.CopyTo(stream);
                }

                evt.ImagePath = parsedContentDisposition.FileName.Trim('"');

                _context.Add(evt);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: Event/Edit/5
        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = _context.Events.SingleOrDefault(m => m.RecordId == id);
            if (@event == null)
            {
                return NotFound();
            }
            return View(@event);
        }

        // POST: Event/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, [Bind("Title,Description,Date,RecordId")] Event @event)
        {
            if (id != @event.RecordId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var evt = this._context.Events.FirstOrDefault(e => e.RecordId == @event.RecordId);

                    evt.Date = @event.Date;
                    evt.Description = @event.Description;
                    evt.Title = @event.Title;
                    
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.RecordId))
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
            return View(@event);
        }

        // GET: Event/Delete/5
        public IActionResult Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = _context.Events
                .SingleOrDefault(m => m.RecordId == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Event/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(long id)
        {
            var @event = _context.Events.SingleOrDefault(m => m.RecordId == id);
            _context.Events.Remove(@event);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(long id)
        {
            return _context.Events.Any(e => e.RecordId == id);
        }
        public IActionResult JoinEvent(long id)
        {
            var user = HttpContext.User.Identity.Name;

            return null;
        }
    }
}
