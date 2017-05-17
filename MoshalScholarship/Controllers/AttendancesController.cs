using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoshalScholarship.Data;
using MoshalScholarship.Models;
using Microsoft.AspNetCore.Authorization;

namespace MoshalScholarship.Controllers
{
    [Authorize]
    public class AttendancesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AttendancesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Attendances
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Attendance.Include(a => a.Event);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Attendances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendance
                .Include(a => a.Event)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (attendance == null)
            {
                return NotFound();
            }

            return View(attendance);
        }

        // GET: Attendances/Create
        public IActionResult Create()
        {
            ViewData["EventId"] = new SelectList(_context.Event, "ID", "ID");
            return View();
        }

        // POST: Attendances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,EventId,UserId,CreatedAt")] Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(attendance);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["EventId"] = new SelectList(_context.Event, "ID", "ID", attendance.EventId);
            return View(attendance);
        }

        // GET: Attendances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendance.SingleOrDefaultAsync(m => m.ID == id);
            if (attendance == null)
            {
                return NotFound();
            }
            ViewData["EventId"] = new SelectList(_context.Event, "ID", "ID", attendance.EventId);
            return View(attendance);
        }

        // POST: Attendances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,EventId,UserId,CreatedAt")] Attendance attendance)
        {
            if (id != attendance.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attendance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttendanceExists(attendance.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["EventId"] = new SelectList(_context.Event, "ID", "ID", attendance.EventId);
            return View(attendance);
        }

        // GET: Attendances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendance
                .Include(a => a.Event)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (attendance == null)
            {
                return NotFound();
            }

            return View(attendance);
        }

        // POST: Attendances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var attendance = await _context.Attendance.SingleOrDefaultAsync(m => m.ID == id);
            _context.Attendance.Remove(attendance);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool AttendanceExists(int id)
        {
            return _context.Attendance.Any(e => e.ID == id);
        }
    }
}
