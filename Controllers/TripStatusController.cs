using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sea_booking.Data;
using sea_booking.Models;

namespace sea_booking.Controllers
{
    public class TripStatusController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TripStatusController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TripStatus
        public async Task<IActionResult> Index()
        {
            return View(await _context.TripStatus.ToListAsync());
        }

        // GET: TripStatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tripStatus = await _context.TripStatus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tripStatus == null)
            {
                return NotFound();
            }

            return View(tripStatus);
        }

        // GET: TripStatus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TripStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StatusTitle,Note")] TripStatus tripStatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tripStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tripStatus);
        }

        // GET: TripStatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tripStatus = await _context.TripStatus.FindAsync(id);
            if (tripStatus == null)
            {
                return NotFound();
            }
            return View(tripStatus);
        }

        // POST: TripStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StatusTitle,Note")] TripStatus tripStatus)
        {
            if (id != tripStatus.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tripStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TripStatusExists(tripStatus.Id))
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
            return View(tripStatus);
        }

        // GET: TripStatus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tripStatus = await _context.TripStatus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tripStatus == null)
            {
                return NotFound();
            }

            return View(tripStatus);
        }

        // POST: TripStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tripStatus = await _context.TripStatus.FindAsync(id);
            _context.TripStatus.Remove(tripStatus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TripStatusExists(int id)
        {
            return _context.TripStatus.Any(e => e.Id == id);
        }
    }
}
