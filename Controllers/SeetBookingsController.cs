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
    public class SeetBookingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SeetBookingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SeetBookings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SeetBooking.Include(s => s.Booking).Include(s => s.Seet).Include(s => s.Trip);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SeetBookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seetBooking = await _context.SeetBooking
                .Include(s => s.Booking)
                .Include(s => s.Seet)
                .Include(s => s.Trip)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (seetBooking == null)
            {
                return NotFound();
            }

            return View(seetBooking);
        }

        // GET: SeetBookings/Create
        public IActionResult Create()
        {
            ViewData["BookingId"] = new SelectList(_context.Booking, "Id", "Id");
            ViewData["SeetId"] = new SelectList(_context.Seat, "Id", "SeatNumber");
            ViewData["TripId"] = new SelectList(_context.Trip, "Id", "Id");
            return View();
        }

        // POST: SeetBookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BookingId,SeetId,TripId")] SeetBooking seetBooking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(seetBooking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookingId"] = new SelectList(_context.Booking, "Id", "Id", seetBooking.BookingId);
            ViewData["SeetId"] = new SelectList(_context.Seat, "Id", "SeatNumber", seetBooking.SeetId);
            ViewData["TripId"] = new SelectList(_context.Trip, "Id", "Id", seetBooking.TripId);
            return View(seetBooking);
        }

        public IActionResult SelectSeat(int bookingId,int tripId)
        {
           var trip= _context.Trip.Find(tripId);
            var booking = _context.Booking.Include(x=>x.ShipClasses.Class).Where(x=>x.Id== bookingId).FirstOrDefault();
            List<Seat> seatList = new List<Seat>();
            var seetBookings = _context.SeetBooking.Where(x=>x.TripId==tripId).ToList();
            var seats = (from seat in _context.Seat join shipCl in _context.ShipClasses on seat.ShipClassId equals shipCl.Id join ship in _context.Ship on shipCl.ShipId equals ship.Id where (ship.Id == trip.ShipId && shipCl.ClassId == booking.ShipClasses.ClassId) select seat).ToList();
           foreach(var i in seats)
            {
                if (!seetBookings.Exists(item => item.SeetId == i.Id))
                {
                    seatList.Add(i);
                }
            }
            ViewData["SeetId"] = new SelectList(seatList, "Id", "SeatNumber");
            ViewBag.totalSalary = trip.Price +booking.ShipClasses.Class.ClassSeatPrice;
            var seatBookine =new SeetBooking();
            seatBookine.BookingId = bookingId;
            seatBookine.Booking = booking;
            seatBookine.Trip = trip;
            seatBookine.TripId = tripId;
            return View(seatBookine);
        }

        // POST: SeetBookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SelectSeat(SeetBooking seetBooking)
        {
            var trip = _context.Trip.Find(seetBooking.TripId);
            var booking = _context.Booking.Include(x => x.ShipClasses.Class).Where(x => x.Id == seetBooking.BookingId).FirstOrDefault();
            List<Seat> seatList = new List<Seat>();
            var seetBookings = _context.SeetBooking.Where(x => x.TripId == seetBooking.TripId).ToList();
            var seats = (from seat in _context.Seat join shipCl in _context.ShipClasses on seat.ShipClassId equals shipCl.Id join ship in _context.Ship on shipCl.ShipId equals ship.Id where (ship.Id == trip.ShipId && shipCl.ClassId == booking.ShipClasses.ClassId) select seat).ToList();
            foreach (var i in seats)
            {
                if (!seetBookings.Exists(item => item.SeetId == i.Id))
                {
                    seatList.Add(i);
                }
            }
            if (seetBooking.Booking.TotalPayment != trip.Price + booking.ShipClasses.Class.ClassSeatPrice)
                ModelState.AddModelError(string.Empty, "The total payed isn't equal the totla price");
            if (ModelState.IsValid)
            {
                booking.TotalPayment = seetBooking.Booking.TotalPayment;
                seetBooking.Booking = booking;
                if (seetBooking.Booking.TotalPayment == trip.Price + booking.ShipClasses.Class.ClassSeatPrice)
                    seetBooking.Booking.isConfirmed = true;
                _context.Add(seetBooking);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Bookings",new {id=seetBooking.BookingId });
            }
            ViewBag.totalSalary = trip.Price + booking.ShipClasses.Class.ClassSeatPrice;

            ViewData["SeetId"] = new SelectList(seatList, "Id", "SeatNumber", seetBooking.SeetId);
            return View(seetBooking);
        }

        // GET: SeetBookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seetBooking = await _context.SeetBooking.FindAsync(id);
            if (seetBooking == null)
            {
                return NotFound();
            }
            ViewData["BookingId"] = new SelectList(_context.Booking, "Id", "Id", seetBooking.BookingId);
            ViewData["SeetId"] = new SelectList(_context.Seat, "Id", "SeatNumber", seetBooking.SeetId);
            ViewData["TripId"] = new SelectList(_context.Trip, "Id", "Id", seetBooking.TripId);
            return View(seetBooking);
        }

        // POST: SeetBookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BookingId,SeetId,TripId")] SeetBooking seetBooking)
        {
            if (id != seetBooking.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(seetBooking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SeetBookingExists(seetBooking.Id))
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
            ViewData["BookingId"] = new SelectList(_context.Booking, "Id", "Id", seetBooking.BookingId);
            ViewData["SeetId"] = new SelectList(_context.Seat, "Id", "SeatNumber", seetBooking.SeetId);
            ViewData["TripId"] = new SelectList(_context.Trip, "Id", "Id", seetBooking.TripId);
            return View(seetBooking);
        }

        // GET: SeetBookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seetBooking = await _context.SeetBooking
                .Include(s => s.Booking)
                .Include(s => s.Seet)
                .Include(s => s.Trip)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (seetBooking == null)
            {
                return NotFound();
            }

            return View(seetBooking);
        }

        // POST: SeetBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var seetBooking = await _context.SeetBooking.FindAsync(id);
            _context.SeetBooking.Remove(seetBooking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SeetBookingExists(int id)
        {
            return _context.SeetBooking.Any(e => e.Id == id);
        }
    }
}
