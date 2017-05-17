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
    public class BuddiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BuddiesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Buddies
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Buddy.Include(b => b.FromUser).Include(b => b.ToUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Buddies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buddy = await _context.Buddy
                .Include(b => b.FromUser)
                .Include(b => b.ToUser)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (buddy == null)
            {
                return NotFound();
            }

            return View(buddy);
        }

        // GET: Buddies/Create
        public IActionResult Create()
        {
            ViewData["FromUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["ToUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Buddies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FromUserId,ToUserId,Accepted,CreatedAt")] Buddy buddy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(buddy);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["FromUserId"] = new SelectList(_context.Users, "Id", "Id", buddy.FromUserId);
            ViewData["ToUserId"] = new SelectList(_context.Users, "Id", "Id", buddy.ToUserId);
            return View(buddy);
        }

        // GET: Buddies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buddy = await _context.Buddy.SingleOrDefaultAsync(m => m.ID == id);
            if (buddy == null)
            {
                return NotFound();
            }
            ViewData["FromUserId"] = new SelectList(_context.Users, "Id", "Id", buddy.FromUserId);
            ViewData["ToUserId"] = new SelectList(_context.Users, "Id", "Id", buddy.ToUserId);
            return View(buddy);
        }

        // POST: Buddies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FromUserId,ToUserId,Accepted,CreatedAt")] Buddy buddy)
        {
            if (id != buddy.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(buddy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BuddyExists(buddy.ID))
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
            ViewData["FromUserId"] = new SelectList(_context.Users, "Id", "Id", buddy.FromUserId);
            ViewData["ToUserId"] = new SelectList(_context.Users, "Id", "Id", buddy.ToUserId);
            return View(buddy);
        }

        // GET: Buddies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buddy = await _context.Buddy
                .Include(b => b.FromUser)
                .Include(b => b.ToUser)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (buddy == null)
            {
                return NotFound();
            }

            return View(buddy);
        }

        // POST: Buddies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var buddy = await _context.Buddy.SingleOrDefaultAsync(m => m.ID == id);
            _context.Buddy.Remove(buddy);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool BuddyExists(int id)
        {
            return _context.Buddy.Any(e => e.ID == id);
        }
    }
}
