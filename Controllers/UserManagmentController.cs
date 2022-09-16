using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using sea_booking.Data;

namespace sea_booking.Controllers
{
    public class UserManagmentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public UserManagmentController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        // GET: UserMangament
        public ActionResult Index()
        {
            var users = _context.Users.Where(x=>x.UserName!="admin").ToList();
            ViewBag.users = users;
            return View();
        }

        // GET: UserMangament/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserMangament/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserMangament/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserMangament/Edit/5
        public ActionResult Edit(string id)
        {
            ViewBag.id = id;
            return View();
        }

        // POST: UserMangament/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(string id, string password,string confirmPassword)
        {
            if (password != confirmPassword)
                ModelState.AddModelError(string.Empty, "The Password dosn't match");
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add update logic here
                    var user = _context.Users.Find(id);
                    await _userManager.RemovePasswordAsync(user);
                    var addPasswordResult = await _userManager.AddPasswordAsync(user, password);
                    if (!addPasswordResult.Succeeded)
                    {
                        foreach (var error in addPasswordResult.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                        return View();
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            return View();
           
        }

        // GET: UserMangament/Delete/5
        public ActionResult Delete(string id)
        {
            ViewBag.id=id;
            return View();
        }

        // POST: UserMangament/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                var user = _context.Users.Find(id);
                _context.Users.Remove(user);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch(Exception e)
            {
                return View();
            }
        }
    }
}