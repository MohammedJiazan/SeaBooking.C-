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
    public class TripsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TripsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Trips
        public async Task<IActionResult> Index()
        {
            List<ShipClasses> shipClasses = new List<ShipClasses>();
            List<SeetBooking> seetBooking = new List<SeetBooking>();

            var trips = _context.Trip.Include(t => t.From).Include(t => t.To).Where(x=>x.isOpen).Include(x=>x.Ship.ShipClasses).Include(x=>x.SeetBookings).ToList();
            for (var i= 0 ;i <trips.Count; i++)
            {
                shipClasses = trips[i].Ship.ShipClasses.ToList();
                for (var j = 0; j < trips[i].Ship.ShipClasses.Count;j++) 
                {
                    shipClasses[j].Class = _context.Class.Find(shipClasses[j].ClassId);
                }
                trips[i].Ship.ShipClasses = shipClasses;
            }

            for (var i = 0; i < trips.Count; i++)
            {
                seetBooking = trips[i].SeetBookings.ToList();
                for (var j = 0; j < trips[i].SeetBookings.Count; j++)
                {
                    seetBooking[j].Seet = _context.Seat.Find(seetBooking[j].SeetId);
                }
                trips[i].SeetBookings = seetBooking;
            }
            return View(trips);
        }

        // GET: Trips
        public async Task<IActionResult> IndexClosed()
        {
            List<ShipClasses> shipClasses = new List<ShipClasses>();
            List<SeetBooking> seetBooking = new List<SeetBooking>();

            var trips = _context.Trip.Include(t => t.From).Include(t => t.To).Where(x => !x.isOpen).Include(x => x.Ship.ShipClasses).Include(x => x.SeetBookings).ToList();
            for (var i = 0; i < trips.Count; i++)
            {
                shipClasses = trips[i].Ship.ShipClasses.ToList();
                for (var j = 0; j < trips[i].Ship.ShipClasses.Count; j++)
                {
                    shipClasses[j].Class = _context.Class.Find(shipClasses[j].ClassId);
                }
                trips[i].Ship.ShipClasses = shipClasses;
            }

            for (var i = 0; i < trips.Count; i++)
            {
                seetBooking = trips[i].SeetBookings.ToList();
                for (var j = 0; j < trips[i].SeetBookings.Count; j++)
                {
                    seetBooking[j].Seet = _context.Seat.Find(seetBooking[j].SeetId);
                }
                trips[i].SeetBookings = seetBooking;
            }
            return View(nameof(Index),trips);
        }

        // GET: Trips/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trip = await _context.Trip
                .Include(t => t.From)
                .Include(t => t.To)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trip == null)
            {
                return NotFound();
            }

            return View(trip);
        }

        // GET: Trips/Create
        public IActionResult Create()
        {
            ViewData["FromId"] = new SelectList(_context.Station, "Id", "Name");
            ViewData["StatusId"] = new SelectList(_context.TripStatus, "Id", "StatusTitle");
            ViewData["ShipId"] = new SelectList(_context.Ship, "Id", "ShipName");
            ViewData["ToId"] = new SelectList(_context.Station, "Id", "Name");
            return View();
        }

        // POST: Trips/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Trip trip)
        {
            if (ModelState.IsValid)
            {
                trip.Id = 0;
                trip.isOpen = true; 
                _context.Add(trip);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FromId"] = new SelectList(_context.Station, "Id", "Name", trip.From);
            ViewData["ShipId"] = new SelectList(_context.Ship, "Id", "ShipName");
            ViewData["ToId"] = new SelectList(_context.Station, "Id", "Name", trip.To);
            return View(trip);
        }

        // GET: Trips/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trip = await _context.Trip.FindAsync(id);
            if (trip == null)
            {
                return NotFound();
            }
            ViewData["From"] = new SelectList(_context.Station, "Id", "Name", trip.From);
            ViewData["ShipId"] = new SelectList(_context.Ship, "Id", "ShipName");
            ViewData["To"] = new SelectList(_context.Station, "Id", "Name", trip.To);
            return View(trip);
        }

        // POST: Trips/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Price,StatuesId,TripDateTime,From,To,DelayDateTime,StatusId,Note")] Trip trip)
        {
            if (id != trip.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trip);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TripExists(trip.Id))
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
            ViewData["From"] = new SelectList(_context.Station, "Id", "Name", trip.From);
            ViewData["ShipId"] = new SelectList(_context.Ship, "Id", "ShipName");
            ViewData["To"] = new SelectList(_context.Station, "Id", "Name", trip.To);
            return View(trip);
        }
        public async Task<IActionResult> OpenOrCloseTrip(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trip = await _context.Trip.FindAsync(id);
            trip.isOpen = !trip.isOpen;
            _context.SaveChanges();
            if (trip == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index");
        }

        // GET: Trips/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trip = await _context.Trip
                .Include(t => t.From)
                .Include(t => t.To)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trip == null)
            {
                return NotFound();
            }

            return View(trip);
        }

        // POST: Trips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trip = await _context.Trip.FindAsync(id);
            _context.Trip.Remove(trip);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TripExists(int id)
        {
            return _context.Trip.Any(e => e.Id == id);
        }
    }
}
