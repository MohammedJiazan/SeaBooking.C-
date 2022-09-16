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
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Bookings
        public async Task<IActionResult> Index()
        {
            var planReservationContext = _context.Booking.Include(b => b.Passanger).Include(b => b.Trip);
            
            return View(await planReservationContext.ToListAsync());
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .Include(b => b.Passanger)
                .Include(b => b.Trip)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // GET: Bookings/Create
        public IActionResult Create()
        {
            ViewData["PassangerId"] = new SelectList(_context.Passanger, "Id", "Name");
            ViewData["StatusId"] = new SelectList(_context.BookingStatus, "Id", "StatusTitle");
            ViewData["TripId"] = new SelectList(_context.Trip.Where(x=>x.isOpen), "Id", "Id");
            ViewData["ClassId"] = new SelectList(_context.Class, "Id", "ClassName");

            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Booking booking)
        {
            //باقي تكملة انشاء راكب تلقائي
            int passangerId;
            if (ModelState.IsValid )
            {
                //if (booking.PassangerId == 0)
                //{
                //    passangerId = CreateNewPassanger(PassangerName);
                //    booking.PassangerId = passangerId;
                //}
                booking.isConfirmed = false;
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction("SelectSeat","SeetBookings",new { bookingId =booking.Id, tripId =booking.TripId});
            }
            ViewData["PassangerId"] = new SelectList(_context.Passanger, "Id", "Name", booking.PassangerId);
            ViewData["TripId"] = new SelectList(_context.Trip.Where(x=>x.isOpen), "Id", "Id", booking.TripId);
            ViewData["ClassId"] = new SelectList(_context.Class, "Id", "ClassName",booking.ShipClasses.ClassId);
            return View(booking);
        }
        public IActionResult CreateFromTrip(int tripId,int shipClassId)
        {
            ViewData["PassangerId"] = new SelectList(_context.Passanger, "Id", "Name");
            ViewData["StatusId"] = new SelectList(_context.BookingStatus, "Id", "StatusTitle");
            ViewData["TripId"] = new SelectList(_context.Trip.Where(x=>x.Id==tripId), "Id", "Id",tripId);
            Booking booking = new Booking();
            booking.ShipClassesId = shipClassId;

            return View(booking);
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFromTrip(Booking booking)
        {
            //باقي تكملة انشاء راكب تلقائي
            if (ModelState.IsValid)
            {
                //if (booking.PassangerId == 0)
                //{
                //    passangerId = CreateNewPassanger(PassangerName);
                //    booking.PassangerId = passangerId;
                //}
                booking.isConfirmed = false;
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction("SelectSeat", "SeetBookings", new { bookingId = booking.Id, tripId = booking.TripId });
            }
            ViewData["PassangerId"] = new SelectList(_context.Passanger, "Id", "Name", booking.PassangerId);
            ViewData["TripId"] = new SelectList(_context.Trip.Where(x=>x.Id==booking.TripId), "Id", "Id", booking.TripId);
           // ViewData["ClassId"] = new SelectList(_context.Class, "Id", "ClassName", booking.ShipClasses.ClassId);
            return View(booking);
        }

        public JsonResult GetPassanger()
        {
            var passangers = _context.Passanger.ToList();
            //if (search != null)
            //     clints = clints.Where(x => x.Name.Contains(search)).ToList();
            var modefideData = passangers.Select(x => new
            {
                x.Id,
                x.Name
            });
            return Json(modefideData);
        }
        public int CreateNewPassanger(string name)
        {
            Passanger passanger = new Passanger() { Name = name};
            _context.Passanger.Add(passanger);
            _context.SaveChanges();
            int getNew = passanger.Id;
            return getNew;
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            ViewData["PassangerId"] = new SelectList(_context.Passanger, "Id", "Name", booking.PassangerId);
            ViewData["TripId"] = new SelectList(_context.Trip, "Id", "Id", booking.TripId);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BookingDateTime,TicketId,TripId,ShipId,PassangerId,ClassId,TotalPayment,StatusId")] Booking booking)
        {
            if (id != booking.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.Id))
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
            ViewData["PassangerId"] = new SelectList(_context.Passanger, "Id", "Name", booking.PassangerId);
            ViewData["TripId"] = new SelectList(_context.Trip, "Id", "Id", booking.TripId);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .Include(b => b.Passanger)
                .Include(b => b.Trip)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Booking.FindAsync(id);
            _context.Booking.Remove(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(int id)
        {
            return _context.Booking.Any(e => e.Id == id);
        }
    }
}
