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
    public class ShipClassesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShipClassesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ShipClasses
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ShipClasses.Include(s => s.Class).Include(s => s.Ship);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ShipClasses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipClasses = await _context.ShipClasses
                .Include(s => s.Class)
                .Include(s => s.Ship)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shipClasses == null)
            {
                return NotFound();
            }

            return View(shipClasses);
        }

        // GET: ShipClasses/Create
        public IActionResult Create()
        {
            ViewData["ClassId"] = new SelectList(_context.Class, "Id", "ClassName");
            ViewData["ShipId"] = new SelectList(_context.Ship, "Id", "ShipName");
            return View();
        }

        // POST: ShipClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ShipId,ClassId,MaxSeatsNo")] ShipClasses shipClasses)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shipClasses);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassId"] = new SelectList(_context.Class, "Id", "ClassName", shipClasses.ClassId);
            ViewData["ShipId"] = new SelectList(_context.Ship, "Id", "ShipName", shipClasses.ShipId);
            return View(shipClasses);
        }
        public IActionResult addClassToShip(int id)
        {
            bool isExsist = false;
            List<Class> classes = new List<Class>();
            var listItems = _context.ShipClasses.ToList();
            var selectListItems = _context.Class.ToList();
            foreach(var lstclass in selectListItems)
            {
                isExsist = false;
                foreach(var shipclass in listItems)
                {
                    if(lstclass.Id==shipclass.ClassId && id == shipclass.ShipId)
                    {
                        isExsist = true;
                        break;
                    }
                }
                if (!isExsist)
                    classes.Add(lstclass);
            }
            if (classes.Count == 0)
                return Content("There is no class can added!");
            ViewData["ClassId"] = new SelectList(classes, "Id", "ClassName");
            ViewData["ShipId"] = new SelectList(_context.Ship.Where(x=>x.Id==id).ToList(), "Id", "ShipName",id);

            return View();
        }

        // POST: ShipClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> addClassToShip([Bind("Id,ShipId,ClassId,MaxSeatsNo")] ShipClasses shipClasses)
        {
            if (shipClasses.ClassId == null)
                ModelState.AddModelError(string.Empty, "The class is Empty");
            bool isExsist = false;
            List<Class> classes = new List<Class>();
            
            if (ModelState.IsValid)
            {

                try
                {
                    shipClasses.Id = 0;
                    _context.Add(shipClasses);
                    await _context.SaveChangesAsync();
                    createSeat(shipClasses.Id, shipClasses.MaxSeatsNo);
                    ViewBag.feedback = "Seccesfully";

                }
                catch (Exception e) 
                {
                }
                var listItems = _context.ShipClasses.ToList();
                var selectListItems = _context.Class.ToList();
                foreach (var lstclass in selectListItems)
                {
                    isExsist = false;
                    foreach (var shipclass in listItems)
                    {
                        if (lstclass.Id == shipclass.ClassId && shipClasses.ShipId == shipclass.ShipId)
                        {
                            isExsist = true;
                            break;
                        }
                    }
                    if (!isExsist)
                        classes.Add(lstclass);
                }

                ViewData["ClassId"] = new SelectList(classes, "Id", "ClassName", shipClasses.ClassId);
                ViewData["ShipId"] = new SelectList(_context.Ship, "Id", "ShipName", shipClasses.ShipId);
                if(classes.Count()!=0)
                    return View( shipClasses);
                return RedirectToAction("Index","Ships");
            }
            var listItems2 = _context.ShipClasses.ToList();
            var selectListItems2 = _context.Class.ToList();
            foreach (var lstclass in selectListItems2)
            {
                isExsist = false;
                foreach (var shipclass in listItems2)
                {
                    if (lstclass.Id == shipclass.ClassId && shipClasses.ShipId == shipclass.ShipId)
                    {
                        isExsist = true;
                        break;
                    }
                }
                if (!isExsist)
                    classes.Add(lstclass);
            }
            ViewData["ClassId"] = new SelectList(classes, "Id", "ClassName", shipClasses.ClassId);
            ViewData["ShipId"] = new SelectList(_context.Ship, "Id", "ShipName", shipClasses.ShipId);
            return View(shipClasses);
        }
        public async void  createSeat(int id,int seatNo)
        {
            List<Seat> seats = new List<Seat>();
            try {
                for (int i = 1; i <= seatNo; i++)
                {
                    seats.Add(new Seat { Id = 0, ShipClassId = id, SeatNumber = i.ToString() });
                }
              await  _context.Seat.AddRangeAsync(seats);
                _context.SaveChanges();
            }
            catch(Exception e)
            {

            }
            
        }

        // GET: ShipClasses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipClasses = await _context.ShipClasses.FindAsync(id);
            if (shipClasses == null)
            {
                return NotFound();
            }
            ViewData["ClassId"] = new SelectList(_context.Class, "Id", "ClassName", shipClasses.ClassId);
            ViewData["ShipId"] = new SelectList(_context.Ship, "Id", "ShipName", shipClasses.ShipId);
            return View(shipClasses);
        }

        // POST: ShipClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ShipId,ClassId,MaxSeatsNo")] ShipClasses shipClasses)
        {
            if (id != shipClasses.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shipClasses);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShipClassesExists(shipClasses.Id))
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
            ViewData["ClassId"] = new SelectList(_context.Class, "Id", "ClassName", shipClasses.ClassId);
            ViewData["ShipId"] = new SelectList(_context.Ship, "Id", "ShipName", shipClasses.ShipId);
            return View(shipClasses);
        }

        // GET: ShipClasses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipClasses = await _context.ShipClasses
                .Include(s => s.Class)
                .Include(s => s.Ship)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shipClasses == null)
            {
                return NotFound();
            }

            return View(shipClasses);
        }

        // POST: ShipClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shipClasses = await _context.ShipClasses.FindAsync(id);
            _context.ShipClasses.Remove(shipClasses);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShipClassesExists(int id)
        {
            return _context.ShipClasses.Any(e => e.Id == id);
        }
    }
}
