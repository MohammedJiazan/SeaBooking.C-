using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sea_booking.Data;
using sea_booking.Models;

namespace sea_booking.Controllers
{
    public class ShipsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ShipsController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }
        

        // GET: Ships
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ship.ToListAsync());
        }

        // GET: Ships/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ship = await _context.Ship
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ship == null)
            {
                return NotFound();
            }

            return View(ship);
        }

        // GET: Ships/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ships/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Ship ship)
        {
            try
            {
                string wwwrootPath = _hostEnvironment.WebRootPath;
                string filename = Path.GetFileNameWithoutExtension(ship.ImageFile.FileName);
                string extension = Path.GetExtension(ship.ImageFile.FileName);
                filename = filename + DateTime.Now.ToString("yymmssfff") + extension;

                string path = Path.Combine(wwwrootPath + "/Uploads", filename);
                ship.Image = filename;
                using (var filestream = new FileStream(path, FileMode.Create))
                {
                    await ship.ImageFile.CopyToAsync(filestream);
                }
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Somthing went wrong");
            }
            if (ModelState.IsValid)
            {
                
                try
                {
                    _context.Ship.Add(ship);
                    _context.SaveChanges();
                    return RedirectToAction("addClassToShip","ShipClasses",new {id = ship.Id } );
                }
                catch
                {
                    ModelState.AddModelError("err", "Somthing went wrong");
                }
               
            }
            return View(ship);
        }

        // GET: Ships/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ship = await _context.Ship.FindAsync(id);
            if (ship == null)
            {
                return NotFound();
            }
            return View(ship);
        }

        // POST: Ships/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Ship ship)
        {
            if (id != ship.Id)
            {
                return NotFound();
            }
            if(ship.ImageFile.FileName=="")
            {
                ship.Image = _context.Ship.Find(id).Image;
            }
            else
            {
                try { 
                string wwwrootPath = _hostEnvironment.WebRootPath;
                string filename = Path.GetFileNameWithoutExtension(ship.ImageFile.FileName);
                string extension = Path.GetExtension(ship.ImageFile.FileName);
                filename = filename + DateTime.Now.ToString("yymmssfff") + extension;

                string path = Path.Combine(wwwrootPath + "/Uploads", filename);
                ship.Image = filename;
                using (var filestream = new FileStream(path, FileMode.Create))
                {
                    await ship.ImageFile.CopyToAsync(filestream);
                    }
                }
                catch
                {
                    ModelState.AddModelError(string.Empty, "قم باختيار صوره");
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ship);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShipExists(ship.Id))
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
            return View(ship);
        }

        // GET: Ships/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ship = await _context.Ship
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ship == null)
            {
                return NotFound();
            }

            return View(ship);
        }

        // POST: Ships/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ship = await _context.Ship.FindAsync(id);
            _context.Ship.Remove(ship);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShipExists(int id)
        {
            return _context.Ship.Any(e => e.Id == id);
        }
    }
}
