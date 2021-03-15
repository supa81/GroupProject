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
using PawMates.Services;

namespace PawMates.Controllers
{
    public class OwnerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly GeocodingService _geocodingService;
        private readonly DistanceMatrixService _distanceMatrixService;
        public OwnerController(ApplicationDbContext context, GeocodingService geocodingService, DistanceMatrixService distanceMatrixService)
        {
            _context = context;
            _geocodingService = geocodingService;
            _distanceMatrixService = distanceMatrixService;
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
            var ownersDogs = _context.Dogs.Where(d => d.Id == owner.Id).ToList();
            return View(ownersDogs);

        }
        //public ActionResult PotentialDogMatches()
        //{
        //    var applicationDbContext = _context.Owners.Include(o => o.IdentityUser);
        //    var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    var owner = _context.Owners.Where(o => o.IdentityUserId == userId).FirstOrDefault();
        //    if (owner == null)
        //    {
        //        return RedirectToAction("Create");
        //    }
        //    var otherDogs = _context.Dogs.Where(d => d.OwnerId != owner.Id).ToList();
        //    var matches = otherDogs.Where(d => d.ZipCode == owner.ZipCode).ToList();
        //    return View(matches);
        //}
        //public ActionResult FilterByAgeGreaterThan(int input)
        //{
        //    var applicationDbContext = _context.Owners.Include(o => o.IdentityUser);
        //    var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    var owner = _context.Owners.Where(o => o.IdentityUserId == userId).FirstOrDefault();
        //    if (owner == null)
        //    {
        //        return RedirectToAction("Create");
        //    }
        //    var otherDogs = _context.Dogs.Where(d => d.OwnerId != owner.Id).ToList();
        //    var matches = otherDogs.Where(d => d.ZipCode == owner.ZipCode).ToList();
        //    var ageFilter = matches.Where(d => d.Age >= input).ToList();
        //    return View(ageFilter);
        //}
        //public ActionResult FilterByAgeLessThan(int input)
        //{
        //    var applicationDbContext = _context.Owners.Include(o => o.IdentityUser);
        //    var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    var owner = _context.Owners.Where(o => o.IdentityUserId == userId).FirstOrDefault();
        //    if (owner == null)
        //    {
        //        return RedirectToAction("Create");
        //    }
        //    var otherDogs = _context.Dogs.Where(d => d.OwnerId != owner.Id).ToList();
        //    var matches = otherDogs.Where(d => d.ZipCode == owner.ZipCode).ToList();
        //    var ageFilter = matches.Where(d => d.Age <= input).ToList();
        //    return View(ageFilter);
        //}
        //public ActionResult FilterByWeightLessThan(int input)
        //{
        //    var applicationDbContext = _context.Owners.Include(o => o.IdentityUser);
        //    var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    var owner = _context.Owners.Where(o => o.IdentityUserId == userId).FirstOrDefault();
        //    if (owner == null)
        //    {
        //        return RedirectToAction("Create");
        //    }
        //    var otherDogs = _context.Dogs.Where(d => d.OwnerId != owner.Id).ToList();
        //    var matches = otherDogs.Where(d => d.ZipCode == owner.ZipCode).ToList();
        //    var weightFilter = matches.Where(d => d.Weight <= input).ToList();
        //    return View(weightFilter);
        //}

        //public ActionResult FilterByWeightGreaterThan(int input)
        //{
        //    var applicationDbContext = _context.Owners.Include(o => o.IdentityUser);
        //    var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    var owner = _context.Owners.Where(o => o.IdentityUserId == userId).FirstOrDefault();
        //    if (owner == null)
        //    {
        //        return RedirectToAction("Create");
        //    }
        //    var otherDogs = _context.Dogs.Where(d => d.OwnerId != owner.Id).ToList();
        //    var matches = otherDogs.Where(d => d.ZipCode == owner.ZipCode).ToList();
        //    var weightFilter = matches.Where(d => d.Weight >= input).ToList();
        //    return View(weightFilter);
        //}
        public IActionResult SeeOwnersDogs(int id)
        {
            var owner = _context.Owners.Find(id);
            var ownersDogs = _context.Dogs.Where(d => d.Id == owner.Id).ToList();
            return View(ownersDogs);

        }
        public IActionResult MatchedOwners()
        {

            var applicationDbContext = _context.Owners.Include(o => o.IdentityUser);
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var owner = _context.Owners.Where(o => o.IdentityUserId == userId).FirstOrDefault();
            var ownersDogs = _context.Dogs.Where(d => d.Id == owner.Id).ToList();
            var otherDogs = _context.Dogs.Where(d => d.Id != owner.Id);
            List<Owner> matchedOwners = new List<Owner>();
            foreach (var ownedDog in ownersDogs)
            {
                var likedDogs = LikedDogs(ownedDog);
                foreach (var likedDog in likedDogs)
                {
                    var likedDogsLikes = LikedDogs(likedDog);
                    foreach (var dog in likedDogsLikes)
                    {
                        if (dog.Id == owner.Id)
                        {
                            var matchedOwner = _context.Owners.Find(likedDog.Id);
                            matchedOwners.Add(matchedOwner);
                        }
                    }
                }
            }
            matchedOwners = matchedOwners.Distinct().ToList();
            
            return View(matchedOwners);
        }
        //public IActionResult ConnectWithOwner( int? id)
        //{

        //}
        public IActionResult YourLikedDogs(int? id)
        {
            var applicationDbContext = _context.Owners.Include(o => o.IdentityUser);
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var owner = _context.Owners.Where(o => o.IdentityUserId == userId).FirstOrDefault();
            var ownerDog = _context.Dogs.Find(id);
            var matchedDogs = LikedDogs(ownerDog);
            return View(matchedDogs);
        }
        public List<Dog> LikedDogs(Dog ownerDog)
        {
            List<Dog> matchedDogs = new List<Dog>();
            if (ownerDog.PotentialMatches != null)
            {
                var dogMatch1 = _context.Dogs.Find(ownerDog.PotentialMatches);
                matchedDogs.Add(dogMatch1);
            }
            if (ownerDog.PotentialMatches2 != null)
            {
                var dogMatch2 = _context.Dogs.Find(ownerDog.PotentialMatches2);
                matchedDogs.Add(dogMatch2);
            }
            if (ownerDog.PotentialMatches3 != null)
            {
                var dogMatch3 = _context.Dogs.Find(ownerDog.PotentialMatches3);
                matchedDogs.Add(dogMatch3);
            }
            if (ownerDog.PotentialMatches4 != null)
            {
                var dogMatch4 = _context.Dogs.Find(ownerDog.PotentialMatches4);
                matchedDogs.Add(dogMatch4);
            }
            if (ownerDog.PotentialMatches5 != null)
            {
                var dogMatch5 = _context.Dogs.Find(ownerDog.PotentialMatches5);
                matchedDogs.Add(dogMatch5);
            }
            return matchedDogs;
        }
        public IActionResult AddDogToLikes(int id)
        {
            var applicationDbContext = _context.Owners.Include(o => o.IdentityUser);
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var owner = _context.Owners.Where(o => o.IdentityUserId == userId).FirstOrDefault();
            var ownersDogs = _context.Dogs.Where(d => d.Id == owner.Id).ToList();
            foreach (var dog in ownersDogs)
            {
                Likes(id, dog.DogId);
            }
            _context.SaveChanges();
            return RedirectToAction("PotentialDogMatches");
        }
        public async Task<IActionResult> PotentialDogMatches()
        {
            var applicationDbContext = _context.Owners.Include(o => o.IdentityUser);
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var owner = _context.Owners.Where(o => o.IdentityUserId == userId).FirstOrDefault();
            if (owner == null)
            {
                return RedirectToAction("Create");
            }
            var otherDogs = _context.Dogs.Where(d => d.Id != owner.Id).ToList();
            List<Dog> matches = new List<Dog>();
            foreach (var dog in otherDogs)
            {
                var dogDistance = await _distanceMatrixService.GetDistanceInMeters(owner, dog);
                if (dogDistance < owner.FilterDistance)
                {
                    matches.Add(dog);
                }
            }
            if(owner.FilterGender != null)
            {
                matches = matches.Where(d => d.Gender == owner.FilterGender).ToList();
            }
            if (owner.FilterBreed != null)
            {
                matches = matches.Where(d => d.Breed == owner.FilterBreed).ToList();
            }
            if(owner.FilterTemperment != null)
            {
                matches = matches.Where(d => d.Temperment == owner.FilterTemperment).ToList();
            }
            if(owner.FilterAge != null)
            {
                matches = matches.Where(d => d.Age == owner.FilterAge).ToList();
            }
            if (owner.FilterWeight != null)
            {
                matches = matches.Where(d => d.Weight == owner.FilterWeight).ToList();
            }
            return View(matches);
          
        }
        public void Likes(int? idLikedDog, int? idOwnedDog)
        {
            var likedDog = _context.Dogs.Find(idLikedDog);
            var ownedDog = _context.Dogs.Find(idOwnedDog);
            while(ownedDog.PotentialMatches != idLikedDog && ownedDog.PotentialMatches2 != idLikedDog && ownedDog.PotentialMatches3 != idLikedDog && ownedDog.PotentialMatches4 != idLikedDog && ownedDog.PotentialMatches5 != idLikedDog)
            {
                if (ownedDog.PotentialMatches == null && ownedDog.PotentialMatches != idLikedDog)
                {
                    ownedDog.PotentialMatches = likedDog.DogId;
                }
                else if (ownedDog.PotentialMatches2 == null && ownedDog.PotentialMatches2 != idLikedDog)
                {
                    ownedDog.PotentialMatches2 = likedDog.DogId;
                }
                else if (ownedDog.PotentialMatches3 == null && ownedDog.PotentialMatches3 != idLikedDog)
                {
                    ownedDog.PotentialMatches3 = likedDog.DogId;
                }
                else if (ownedDog.PotentialMatches4 == null && ownedDog.PotentialMatches4 != idLikedDog)
                {
                    ownedDog.PotentialMatches4 = likedDog.DogId;
                }
                else if (ownedDog.PotentialMatches5 == null && ownedDog.PotentialMatches5 != idLikedDog)
                {
                    ownedDog.PotentialMatches5 = likedDog.DogId;
                }
                else
                {
                    Console.WriteLine();
                }
            }
        }
        
        public ActionResult RemoveDogFromLikes(int id)
        {
            var applicationDbContext = _context.Owners.Include(o => o.IdentityUser);
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var owner = _context.Owners.Where(o => o.IdentityUserId == userId).FirstOrDefault();
            var ownersDogs = _context.Dogs.Where(d => d.Id == owner.Id).ToList();
            foreach (var dog in ownersDogs)
            {
                RemoveFromLikes(id, dog.DogId);
            }
            _context.SaveChanges();
            return RedirectToAction("PotentialDogMatches");
        }
        public void RemoveFromLikes(int? idRemoveDog, int? idOwnedDog)
        {
            var ownedDog = _context.Dogs.Find(idOwnedDog);
            if (ownedDog.PotentialMatches == idRemoveDog)
            {
                ownedDog.PotentialMatches = null;
            }
            else if (ownedDog.PotentialMatches2 == idRemoveDog)
            {
                ownedDog.PotentialMatches2 = null;
            }
            else if (ownedDog.PotentialMatches3 == idRemoveDog)
            {
                ownedDog.PotentialMatches3 = null;
            }
            else if (ownedDog.PotentialMatches4 == idRemoveDog)
            {
                ownedDog.PotentialMatches4 = null;
            }
            else if (ownedDog.PotentialMatches5 == idRemoveDog)
            {
                ownedDog.PotentialMatches5 = null;
            }

        }
        public ActionResult EditFilters(int? id)
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
        public ActionResult EditFilters(int id, Owner owner)
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
                    ownerToEdit.FilterAge = owner.FilterAge;
                    ownerToEdit.FilterBreed = owner.FilterBreed;
                    if (owner.FilterGender != "None")
                    {
                        ownerToEdit.FilterGender = owner.FilterGender;
                    }
                    if(owner.FilterTemperment == "None")
                    {
                        ownerToEdit.FilterTemperment = null;
                    }
                    if(owner.FilterTemperment != "None")
                    {
                        ownerToEdit.FilterTemperment = owner.FilterTemperment;
                    }
                    ownerToEdit.FilterWeight = owner.FilterWeight;
                    ownerToEdit.FilterDistance = owner.FilterDistance;
                    _context.Update(ownerToEdit);
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

                return RedirectToAction("PotentialDogMatches");
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", owner.IdentityUserId);
            return View(owner);
        }
        public ActionResult FilterDetails()
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
        public ActionResult PotentialDogDetails(int? id)
        {
            var dog = _context.Dogs.Find(id);
            if (dog == null)
            {
                return NotFound();
            }
            return View(dog);
        }
        public ActionResult PotentialDogMatchOwner(int? id)
        {
            var owner = _context.Owners.Find(id);
            if (owner == null)
            {
                return NotFound();
            }
            return View(owner);
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
        public async Task<ActionResult> Create(Owner owner)
        {
            if (ModelState.IsValid)
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                owner.IdentityUserId = userId;

                var ownerWithLatitudeLongitude = await _geocodingService.GetGeocoding(owner);
                ownerWithLatitudeLongitude.FilterDistance = 10;
                _context.Add(ownerWithLatitudeLongitude);
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
        public async Task<ActionResult> Edit(int id, Owner owner)
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
                    ownerToEdit.FirstName = owner.FirstName;
                    ownerToEdit.LastName = owner.LastName;
                    ownerToEdit.State = owner.State;
                    ownerToEdit.ZipCode = owner.ZipCode;
                    ownerToEdit.PictureURL = owner.PictureURL;
                    ownerToEdit.OwnerLatitude = owner.OwnerLatitude;
                    ownerToEdit.OwnerLongitude = owner.OwnerLongitude;
                    await _geocodingService.GetGeocoding(ownerToEdit);
                    _context.Update(ownerToEdit);
                    var ownersDogs = _context.Dogs.Where(d => d.Id == ownerToEdit.Id).ToList();
                    foreach (var dog in ownersDogs)
                    {
                        dog.OwnerLat = ownerToEdit.OwnerLatitude;
                        dog.OwnerLng = ownerToEdit.OwnerLatitude;
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
                    var dogToEdit = _context.Dogs.Find(id);
                    dogToEdit.Age = dog.Age;
                    dogToEdit.Bio = dog.Bio;
                    dogToEdit.Breed = dog.Breed;
                    dogToEdit.Gender = dog.Gender;
                    dogToEdit.Weight = dog.Weight;
                    dogToEdit.Name = dog.Name;
                    dogToEdit.Temperment = dog.Temperment;
                    _context.Update(dogToEdit);
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
