using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoshalScholarship.Data;
using MoshalScholarship.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace MoshalScholarship.Controllers
{
    [Authorize]
    public class MentorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MentorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Mentors
        public async Task<IActionResult> Index()
        {
            var mentors = _context.Mentor.Include(m => m.User);

            var view = new MentorIndexViewModel
            {
                Mentors = new List<Mentor>(mentors) { },
                MentorStatus = new Array[1, 0]
            };

            return View(view);
        }

        // GET: Mentors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mentor = await _context.Mentor
                .Include(m => m.User)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (mentor == null)
            {
                return NotFound();
            }

            var messages = _context.Message.Include(m => m.FromUser).Include(m => m.ToUser).Where(m => (m.FromUserId == this.User.FindFirstValue(ClaimTypes.NameIdentifier) && m.ToUserId == mentor.UserId) || (m.FromUserId == mentor.UserId && m.ToUserId == this.User.FindFirstValue(ClaimTypes.NameIdentifier)));

            var view = new MentorDetailViewModel
            {
                Mentor = mentor,
                Messages = new List<Message>(messages) { }
            };

            return View(view);
        }

        // GET: Mentors/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Mentors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,UserId,Degree,Institution,Faculty,IdentityNumber,PassportNumber,WorkLocation,CurrentCompany,CurrentJob,StudentCount,AddressLine1,AddressLine2,AddressLine3,AddressLine4")] Mentor mentor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mentor);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", mentor.UserId);
            return View(mentor);
        }

        // GET: Mentors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mentor = await _context.Mentor.SingleOrDefaultAsync(m => m.ID == id);
            if (mentor == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", mentor.UserId);
            return View(mentor);
        }

        // POST: Mentors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,UserId,Degree,Institution,Faculty,IdentityNumber,PassportNumber,WorkLocation,CurrentCompany,CurrentJob,StudentCount,AddressLine1,AddressLine2,AddressLine3,AddressLine4")] Mentor mentor)
        {
            if (id != mentor.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mentor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MentorExists(mentor.ID))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", mentor.UserId);
            return View(mentor);
        }

        // GET: Mentors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mentor = await _context.Mentor
                .Include(m => m.User)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (mentor == null)
            {
                return NotFound();
            }

            return View(mentor);
        }

        // POST: Mentors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mentor = await _context.Mentor.SingleOrDefaultAsync(m => m.ID == id);
            _context.Mentor.Remove(mentor);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool MentorExists(int id)
        {
            return _context.Mentor.Any(e => e.ID == id);
        }
    }
}
