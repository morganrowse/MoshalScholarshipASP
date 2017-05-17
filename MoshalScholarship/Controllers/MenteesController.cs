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
    public class MenteesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MenteesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Mentees
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Mentee.Include(m => m.Mentor).Include(m => m.Student);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Mentees/Invites
        public async Task<IActionResult> Invites()
        {
            var applicationDbContext = _context.Mentee.Include(m => m.Mentor).Include(ms => ms.Student).Where(m => m.Accepted == false).Where(m => m.MentorId == this.User.FindFirstValue(ClaimTypes.NameIdentifier));
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Mentees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mentee = await _context.Mentee
                .Include(m => m.Mentor)
                .Include(m => m.Student)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (mentee == null)
            {
                return NotFound();
            }

            return View(mentee);
        }

        // GET: Mentees/Create
        public IActionResult Create()
        {
            ViewData["MentorId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["StudentId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Mentees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,MentorId,StudentId,StartAt,EndedAt,Accepted,CreatedAt")] Mentee mentee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mentee);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["MentorId"] = new SelectList(_context.Users, "Id", "Id", mentee.MentorId);
            ViewData["StudentId"] = new SelectList(_context.Users, "Id", "Id", mentee.StudentId);
            return View(mentee);
        }

        // GET: Mentees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mentee = await _context.Mentee.SingleOrDefaultAsync(m => m.ID == id);
            if (mentee == null)
            {
                return NotFound();
            }
            ViewData["MentorId"] = new SelectList(_context.Users, "Id", "Id", mentee.MentorId);
            ViewData["StudentId"] = new SelectList(_context.Users, "Id", "Id", mentee.StudentId);
            return View(mentee);
        }

        // POST: Mentees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,MentorId,StudentId,StartAt,EndedAt,Accepted,CreatedAt")] Mentee mentee)
        {
            if (id != mentee.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mentee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenteeExists(mentee.ID))
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
            ViewData["MentorId"] = new SelectList(_context.Users, "Id", "Id", mentee.MentorId);
            ViewData["StudentId"] = new SelectList(_context.Users, "Id", "Id", mentee.StudentId);
            return View(mentee);
        }

        // GET: Mentees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mentee = await _context.Mentee
                .Include(m => m.Mentor)
                .Include(m => m.Student)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (mentee == null)
            {
                return NotFound();
            }

            return View(mentee);
        }

        // POST: Mentees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mentee = await _context.Mentee.SingleOrDefaultAsync(m => m.ID == id);
            _context.Mentee.Remove(mentee);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool MenteeExists(int id)
        {
            return _context.Mentee.Any(e => e.ID == id);
        }

        public async Task<IActionResult> SendInvite(string id)
        {
           

            var MentorUser = _context.Users.Find(id);

            var mentor = _context.Mentor.SingleOrDefault(d => d.UserId == id);

            if (_context.Mentee.Where(m => m.StudentId == this.User.FindFirstValue(ClaimTypes.NameIdentifier) && m.Accepted==false).Count()>0)
            {
                TempData["message"] = "Invite already sent";
                return RedirectToAction("Details/" + mentor.ID.ToString(), "Mentors");
            }

            var mentee = new Mentee { MentorId = MentorUser.Id, StudentId = this.User.FindFirstValue(ClaimTypes.NameIdentifier), Accepted = false, CreatedAt = DateTime.Now };

            _context.Add(mentee);
            await _context.SaveChangesAsync();

            TempData["message"] = "Invite sent successfully";
            return RedirectToAction("Details/" + mentor.ID.ToString(), "Mentors");
        }

        public async Task<IActionResult> Respond(int id, int response)
        {
            var mentee = await _context.Mentee.SingleOrDefaultAsync(m => m.ID == id);

            if (mentee != null)
            {
                if (response == 1)
                {
                    mentee.Accepted = true;
                    await _context.SaveChangesAsync();
                    TempData["message"] = "Invite accepted";
                } else
                {
                    _context.Mentee.Remove(mentee);
                    await _context.SaveChangesAsync();
                    TempData["message"] = "Invite rejected";
                }
            }

            return RedirectToAction("Invites", "Mentees");
        }
    }
}
