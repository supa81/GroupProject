using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PawMates.Data;
using PawMates.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PawMates.Controllers
{
    public class DogController : Controller
    {
        private readonly ApplicationDbContext _context;
        public DogController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: DogController
        public ActionResult Index()
        {
            return View();
        }

        // GET: DogController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DogController/Create
        public ActionResult AddNewDog()
        {

            return View();
        }

        // POST: DogController/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewDog(Dog dog )
        {
            var applicationDbContext = _context.Owners.Include(o => o.IdentityUser);
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var owner = _context.Owners.Where(o => o.IdentityUserId == userId).FirstOrDefault();
            dog.OwnerId = owner.Id;
            dog.Owner = owner;
            _context.Dogs.Add(dog);
            _context.SaveChanges();
            return RedirectToAction("DogList", "Owner");
        }

        // GET: DogController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var dog = _context.Dogs.Find(id);

            if (dog == null)
            {

                return NotFound();
            }
            return View(dog);
        }

        // POST: DogController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Dog dog)
        {
            if (id != dog.DogId)
            {
                return NotFound();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    _context.Update(dog);
                    _context.SaveChanges();
                    return RedirectToAction("DogList", "Owner");
                }

            }
            return View(dog);
            
        }

        // GET: DogController/Delete/5
        public ActionResult RemoveDog(int? id)
        {
            if ( id == null)
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
