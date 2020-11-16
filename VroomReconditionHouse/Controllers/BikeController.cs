using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VroomReconditionHouse.AppDbContext;
using VroomReconditionHouse.Models.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using cloudscribe.Pagination.Models;
using VroomReconditionHouse.Models;
using Microsoft.AspNetCore.Authorization;

namespace VroomReconditionHouse.Controllers
{
    public class BikeController : Controller
    {
        private readonly VroomDbContext _db;
        private readonly IWebHostEnvironment _HostingEnvironment;

        [BindProperty]
        public BikeViewModel BikeVM { get; set; }
        public BikeController(VroomDbContext db, IWebHostEnvironment  HostingEnvironment)
        {
            _db = db;
            _HostingEnvironment = HostingEnvironment;
            BikeVM = new BikeViewModel()
            {
                Makes = _db.Makes.ToList(),
                Models = _db.Models.ToList(),
                Bike = new Models.Bike()
            };
        }

        public IActionResult Index(string searchString, string sortOrder, int pageNumber=1, int pageSize=1)
        {
            if ((_db.Bikes.Count() - pageNumber * pageSize ) <0)
            {
                return NotFound();
            }
            ViewBag.CurrentSortOrder = sortOrder;
            ViewBag.CurrentFilter = searchString;
            ViewBag.PriceSortParam = string.IsNullOrEmpty(sortOrder) ? "price_desc" : "";
            var ExcludePages = pageNumber * pageSize - pageSize;

            var bikes = from b in _db.Bikes.Include(m => m.Make).Include(m => m.Model) select b;
            var bikeCount = _db.Bikes.Count();

            if (!string.IsNullOrEmpty(searchString))
            {
                bikes = bikes.Where(b => b.Make.Name.Contains(searchString));
                bikeCount = bikes.Count();
            }

            switch (sortOrder)
            {
                case "price_desc" :
                    bikes = bikes.OrderByDescending(b => b.Price);
                    break;
                default:
                    bikes = bikes.OrderBy(b => b.Price);
                    break;
            }
            bikes = bikes.Skip(ExcludePages).Take(pageSize);

            var results = new PagedResult<Bike>(){
                Data = bikes.AsNoTracking().ToList(),
                TotalItems = bikeCount,
                PageNumber = pageNumber,
                PageSize = pageSize                
            };
            return View(results);
        }

        public IActionResult Create()
        {
            return View(BikeVM);
        }

        [HttpPost, ActionName("Create")]
        public IActionResult CreatePost()
        {
            if (!ModelState.IsValid)
            {
                BikeVM.Makes = _db.Makes.ToList();
                BikeVM.Models = _db.Models.ToList();
                return View(BikeVM);
            }
            _db.Add(BikeVM.Bike);
            _db.SaveChanges();
            UploadImageIfAvailable();
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int Id)
        {
            BikeVM.Bike = _db.Bikes.Include(m => m.Model).Include(m => m.Make).SingleOrDefault(m => m.Id == Id);
            if (BikeVM.Bike == null)
            {
                return NotFound();
            }
            return View(BikeVM);
        }
        [HttpPost, ActionName("Edit")]
        public IActionResult Edit()
        {
            if (!ModelState.IsValid)
            {
                BikeVM.Makes = _db.Makes.ToList();
                BikeVM.Models = _db.Models.ToList();
                return View(BikeVM);
            }
            _db.Update(BikeVM.Bike);
            _db.SaveChanges();
            UploadImageIfAvailable();
            _db.SaveChanges();


             return RedirectToAction(nameof(Index));
        }
        [AllowAnonymous]
        public IActionResult View(int Id)
        {
            BikeVM.Bike = _db.Bikes.Include(m => m.Model).Include(m => m.Make).SingleOrDefault(m => m.Id == Id);
            if (BikeVM.Bike == null)
            {
                return NotFound();
            }
            return View(BikeVM);
        }

        private void UploadImageIfAvailable()
        {
            var wwwRootPath = _HostingEnvironment.WebRootPath;
            var BikeId = BikeVM.Bike.Id;
            var SavedBike = _db.Bikes.Find(BikeId);
            var files = HttpContext.Request.Form.Files;

            if (files.Count != 0)
            {
                var ImagePath = @"images/bike/";
                var FileExtension = Path.GetExtension(files[0].FileName);
                string RelPath = ImagePath + BikeId + FileExtension;
                var AbsPath = Path.Combine(wwwRootPath, RelPath);
                using (FileStream fs = new FileStream(AbsPath, FileMode.Create))
                {
                    files[0].CopyTo(fs);
                }
                SavedBike.ImagePath = RelPath;
            }
        }

        [HttpPost]
        public IActionResult Delete(int Id)
        {
            var model = _db.Bikes.Find(Id);
            if (model == null)
            {
                return NotFound();
            }
            _db.Bikes.Remove(model);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }

}
