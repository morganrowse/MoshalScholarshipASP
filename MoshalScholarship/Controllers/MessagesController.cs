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
    public class MessagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MessagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Messages
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Message.Include(m => m.FromUser).Include(m => m.ToUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Messages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Message
                .Include(m => m.FromUser)
                .Include(m => m.ToUser)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // GET: Messages/Create
        public IActionResult Create()
        {
            ViewData["FromUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["ToUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Messages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string body, string toUserId, string type)
        {
            var message = new Message { Body = body, FromUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier), ToUserId = toUserId, CreatedAt = DateTime.Now };

            switch (type)
            {
                case "Student":
                    var student = _context.Student.SingleOrDefault(d => d.UserId == toUserId);

                    if (_context.Mentee.Where(m => m.MentorId == this.User.FindFirstValue(ClaimTypes.NameIdentifier) && m.Accepted == true).Count()==0)
                    {
                        TempData["message"] = "Invite this student first";
                        return RedirectToAction("Details/" + student.ID.ToString(), "Students");
                    }

                    _context.Add(message);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details/" + student.ID.ToString(), "Students");
                case "Mentor":
                    var mentor = _context.Mentor.SingleOrDefault(d => d.UserId == toUserId);

                    if (_context.Mentee.Where(m => m.StudentId == this.User.FindFirstValue(ClaimTypes.NameIdentifier) && m.Accepted == true).Count() == 0)
                    {
                        TempData["message"] = "Invite this mentor first";
                        return RedirectToAction("Details/" + mentor.ID.ToString(), "Mentors");
                    }

                    _context.Add(message);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details/" + mentor.ID.ToString(), "Mentors");
            }

            return NotFound();

        }

        // GET: Messages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Message.SingleOrDefaultAsync(m => m.ID == id);
            if (message == null)
            {
                return NotFound();
            }
            ViewData["FromUserId"] = new SelectList(_context.Users, "Id", "Id", message.FromUserId);
            ViewData["ToUserId"] = new SelectList(_context.Users, "Id", "Id", message.ToUserId);
            return View(message);
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Body,FromUserId,ToUserId,CreatedAt")] Message message)
        {
            if (id != message.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(message);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MessageExists(message.ID))
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
            ViewData["FromUserId"] = new SelectList(_context.Users, "Id", "Id", message.FromUserId);
            ViewData["ToUserId"] = new SelectList(_context.Users, "Id", "Id", message.ToUserId);
            return View(message);
        }

        // GET: Messages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Message
                .Include(m => m.FromUser)
                .Include(m => m.ToUser)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var message = await _context.Message.SingleOrDefaultAsync(m => m.ID == id);
            _context.Message.Remove(message);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool MessageExists(int id)
        {
            return _context.Message.Any(e => e.ID == id);
        }
    }
}
