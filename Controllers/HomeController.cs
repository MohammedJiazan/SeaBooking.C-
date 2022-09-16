using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using sea_booking.Data;
using sea_booking.Models;

namespace sea_booking.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult IndexTest()
        {
            return View();
        }
        public  IActionResult Report()
        {
            List<string> status = new List<string>() { "Open Trip", "Close Trip" };
            ViewData["tripId"] = new SelectList(_context.Trip, "Id", "Id");
            ViewData["passangerId"] = new SelectList(_context.Passanger, "Id", "Name");
            ViewData["classId"] = new SelectList(_context.Class, "Id", "ClassName");
            ViewData["shipId"] = new SelectList(_context.Ship, "Id", "ShipName");
            ViewData["fromStationId"] = new SelectList(_context.Station, "Id", "Name");
            ViewData["toStationId"] = new SelectList(_context.Station, "Id", "Name");
            ViewData["tripStatus"] = new SelectList(status);
            List<Booking>  bookings = new List<Booking>();

            return View(bookings);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Report(int tripId,int passangerId,int classId,int shipId,DateTime fromDate,DateTime toDate,int fromStationId,int toStationId,string tripStatus)
        {
            var bookings =await  _context.Booking.Include(b => b.Passanger).Include(b => b.Trip).Include(x=>x.ShipClasses.Class).Include(x => x.ShipClasses.Ship).ToListAsync();

            bookings = bookings.Where(x => x.BookingDateTime >= fromDate && x.BookingDateTime <= toDate).ToList();
            if (tripId != 0)
                bookings = bookings.Where(x => x.TripId == tripId).ToList();
            else
            {
                if(tripStatus== "Open Trip")
                    bookings = bookings.Where(x => x.Trip.isOpen == true).ToList();
                else if(tripStatus== "Close Trip")
                    bookings = bookings.Where(x => x.Trip.isOpen == false).ToList();
            }
            if (passangerId != 0)
                bookings = bookings.Where(x => x.PassangerId == passangerId).ToList();
            if (classId != 0)
                bookings = bookings.Where(x => x.ShipClasses.ClassId == classId).ToList();
            if (shipId != 0)
                bookings = bookings.Where(x => x.ShipClasses.ShipId == shipId).ToList();
            if (fromStationId != 0)
                bookings = bookings.Where(x => x.Trip.FromId == fromStationId).ToList();
            if (toStationId != 0)
                bookings = bookings.Where(x => x.Trip.ToId == toStationId).ToList();
            List<string> status = new List<string>() { "Open Trip", "Close Trip" };
            ViewData["tripId"] = new SelectList(_context.Trip, "Id", "Id",tripId);
            ViewData["passangerId"] = new SelectList(_context.Passanger, "Id", "Name",passangerId);
            ViewData["classId"] = new SelectList(_context.Class, "Id", "ClassName",classId);
            ViewData["shipId"] = new SelectList(_context.Ship, "Id", "ShipName",classId);
            ViewData["fromStationId"] = new SelectList(_context.Station, "Id", "Name",fromStationId);
            ViewData["toStationId"] = new SelectList(_context.Station, "Id", "Name",toStationId);
            ViewData["tripStatus"] = new SelectList(status,tripStatus);

            return View(bookings.OrderByDescending(x=>x.Id));
        }

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
