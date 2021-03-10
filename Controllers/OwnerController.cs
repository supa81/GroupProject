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
        public ActionResult PotentialDogMatches()
        {
            var applicationDbContext = _context.Owners.Include(o => o.IdentityUser);
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var owner = _context.Owners.Where(o => o.IdentityUserId == userId).FirstOrDefault();
            if (owner == null)
            {
                return RedirectToAction("Create");
            }
            var otherDogs = _context.Dogs.Where(d => d.OwnerId != owner.Id).ToList();
            var matches = otherDogs.Where(d => d.ZipCode == owner.ZipCode).ToList();
            return View(matches);
        }
        public ActionResult FilterByAgeGreaterThan(int input)
        {
            var applicationDbContext = _context.Owners.Include(o => o.IdentityUser);
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var owner = _context.Owners.Where(o => o.IdentityUserId == userId).FirstOrDefault();
            if (owner == null)
            {
                return RedirectToAction("Create");
            }
            var otherDogs = _context.Dogs.Where(d => d.OwnerId != owner.Id).ToList();
            var matches = otherDogs.Where(d => d.ZipCode == owner.ZipCode).ToList();
            var ageFilter = matches.Where(d => d.Age >= input).ToList();
            return View(ageFilter);
        }
        public ActionResult FilterByAgeLessThan(int input)
        {
            var applicationDbContext = _context.Owners.Include(o => o.IdentityUser);
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var owner = _context.Owners.Where(o => o.IdentityUserId == userId).FirstOrDefault();
            if (owner == null)
            {
                return RedirectToAction("Create");
            }
            var otherDogs = _context.Dogs.Where(d => d.OwnerId != owner.Id).ToList();
            var matches = otherDogs.Where(d => d.ZipCode == owner.ZipCode).ToList();
            var ageFilter = matches.Where(d => d.Age <= input).ToList();
            return View(ageFilter);
        }
        public ActionResult FilterByWeightLessThan(int input)
        {
            var applicationDbContext = _context.Owners.Include(o => o.IdentityUser);
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var owner = _context.Owners.Where(o => o.IdentityUserId == userId).FirstOrDefault();
            if (owner == null)
            {
                return RedirectToAction("Create");
            }
            var otherDogs = _context.Dogs.Where(d => d.OwnerId != owner.Id).ToList();
            var matches = otherDogs.Where(d => d.ZipCode == owner.ZipCode).ToList();
            var weightFilter = matches.Where(d => d.Weight <= input).ToList();
            return View(weightFilter);
        }

        public ActionResult FilterByWeightGreaterThan(int input)
        {
            var applicationDbContext = _context.Owners.Include(o => o.IdentityUser);
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var owner = _context.Owners.Where(o => o.IdentityUserId == userId).FirstOrDefault();
            if (owner == null)
            {
                return RedirectToAction("Create");
            }
            var otherDogs = _context.Dogs.Where(d => d.OwnerId != owner.Id).ToList();
            var matches = otherDogs.Where(d => d.ZipCode == owner.ZipCode).ToList();
            var weightFilter = matches.Where(d => d.Weight >= input).ToList();
            return View(weightFilter);
        }
        public ActionResult FilterByGender(string input)
        {
            var applicationDbContext = _context.Owners.Include(o => o.IdentityUser);
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var owner = _context.Owners.Where(o => o.IdentityUserId == userId).FirstOrDefault();
            if (owner == null)
            {
                return RedirectToAction("Create");
            }
            var otherDogs = _context.Dogs.Where(d => d.OwnerId != owner.Id).ToList();
            var matches = otherDogs.Where(d => d.ZipCode == owner.ZipCode).ToList();
            var genderFilter = matches.Where(d => d.Gender == input).ToList();
            return View(genderFilter);
        }
        public ActionResult FilterByBreed(string input)
        {
            var applicationDbContext = _context.Owners.Include(o => o.IdentityUser);
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var owner = _context.Owners.Where(o => o.IdentityUserId == userId).FirstOrDefault();
            if (owner == null)
            {
                return RedirectToAction("Create");
            }
            var otherDogs = _context.Dogs.Where(d => d.OwnerId != owner.Id).ToList();
            var matches = otherDogs.Where(d => d.ZipCode == owner.ZipCode).ToList();
            var breedFilter = matches.Where(d => d.Breed == input).ToList();
            return View(breedFilter);
        }
        public ActionResult FilterByTemperment(string input)
        {
            var applicationDbContext = _context.Owners.Include(o => o.IdentityUser);
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var owner = _context.Owners.Where(o => o.IdentityUserId == userId).FirstOrDefault();
            if (owner == null)
            {
                return RedirectToAction("Create");
            }
            var otherDogs = _context.Dogs.Where(d => d.OwnerId != owner.Id).ToList();
            var matches = otherDogs.Where(d => d.ZipCode == owner.ZipCode).ToList();
            var tempermentFilter = matches.Where(d => d.Temperment == input).ToList();
            return View(tempermentFilter);
        }
        // GET: OwnerController/Details/5
        public  ActionResult Details()
        {
            var applicationDbContext = _context.Owners.Include(o => o.IdentityUser);
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var owner = _context.Owners.Where(o => o.IdentityUserId == userId).SingleOrDefault();
            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
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
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var owner = _context.Owners.Find(id);

            if (owner == null)
            {

                return NotFound();
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", owner.IdentityUserId);
            return View(owner);
        }

        // POST: OwnerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Owner owner)
        {
            if (id != owner.Id)
            {
                return NotFound();
            }
            else if (ModelState.IsValid)
            {
                try
                {
                    Owner ownerToEdit = _context.Owners.Find(id);
                    ownerToEdit.Username = owner.Username;
                    ownerToEdit.ZipCode = owner.ZipCode;
                    ownerToEdit.SlackUserId = owner.SlackUserId;
                    ownerToEdit.PictureURL = owner.PictureURL;
                    _context.Update(ownerToEdit);
                    var ownersDogs = _context.Dogs.Where(d => d.OwnerId == ownerToEdit.Id).ToList();
                    foreach (var dog in ownersDogs)
                    {
                        dog.ZipCode = ownerToEdit.ZipCode;
                        _context.Update(dog);
                    }
                    _context.SaveChanges();
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OwnerExists(owner.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                    
                }

                return RedirectToAction("Details");
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", owner.IdentityUserId);
            return View(owner);
        }
        public ActionResult EditDog(int? id)
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
        public ActionResult EditDog(int id, Dog dog)
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
        private bool OwnerExists(int id)
        {
            return _context.Owners.Any(o => o.Id == id);
        }
    }
}
