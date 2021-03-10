using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PawMates.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using PawMates.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PawMates.Controllers
{
    public class OwnerController : Controller
    {
        private readonly ApplicationDbContext _context;
        public OwnerController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: OwnerController
        public IActionResult DogList()
        {
            var applicationDbContext = _context.Owners.Include(o => o.IdentityUser);
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var owner = _context.Owners.Where(o => o.IdentityUserId == userId).FirstOrDefault();
            if (owner == null)
            {
                 return RedirectToAction("Create");
            }
            var ownersDogs = _context.Dogs.Where(d => d.OwnerId == owner.Id).ToList();
            return View(ownersDogs);

        }

        // GET: OwnerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        public ActionResult DogDetails(int? id)
        {
            var dog = _context.Dogs.Find(id);
            if (dog == null)
            {
                return NotFound();
            }
            return View(dog);
        }

        // GET: OwnerController/Create
        public ActionResult Create()
        {
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: OwnerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Owner owner)
        {
            if (ModelState.IsValid)
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                owner.IdentityUserId = userId;
                _context.Add(owner);
                _context.SaveChanges();
                return RedirectToAction("DogList");
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", owner.IdentityUserId);
            return View("DogList");
        }
        public ActionResult NavigateToAddDog()
        {
            return RedirectToAction("AddNewDog", "Dog");
        }
        //public ActionResult NavigateToRemoveDog(int id)
        //{
        //    string ID = id.ToString();
        //    return RedirectToAction( "RemoveDog", "Dog", ID);
        //}

        // GET: OwnerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OwnerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OwnerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OwnerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult RemoveDog(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var dog = _context.Dogs.FirstOrDefault(d => d.DogId == id);

            if (dog == null)
            {
                return NotFound();
            }
            return View(dog);
        }

        // POST: DogController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveDog(int id)
        {
            try
            {
                var dog = _context.Dogs.Find(id);
                _context.Dogs.Remove(dog);
                _context.SaveChanges();
                return RedirectToAction("DogList", "Owner");

            }
            catch
            {
                return View();
            }
        }
    }
}
