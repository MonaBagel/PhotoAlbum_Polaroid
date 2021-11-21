using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Polaroid_Proj.Data;
using Polaroid_Proj.Models.Gallery;

namespace Polaroid_Proj.Controllers
{
    public class PhotosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PhotosController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this._webHostEnvironment = webHostEnvironment;
        }

        // GET: Photos with search function Search Function
        public async Task<IActionResult> Index(string SearchValue)
        {
            var photos = from p in _context.Photos select p;
            //photos = photos.Where(p => p.AlbumId == null);


            if (!String.IsNullOrEmpty(SearchValue))
            {
                photos = photos.Where(x => x.Title.Contains(SearchValue) || x.Owner.Contains(SearchValue));

            }
            else
               photos = _context.Photos;

            photos = photos.OrderBy(p => p.Title);
            return View(await photos.ToListAsync());
        }


        // GET: Photos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photo = await _context.Photos
                .Include(p => p.Album)
                .FirstOrDefaultAsync(m => m.GalleryItemId == id);
            if (photo == null)
            {
                return NotFound();
            }

            return View(photo);
        }

        // GET: Photos/Create
        public IActionResult Create()
        {
            ViewData["AlbumId"] = new SelectList(_context.Albums, "GalleryItemId", "Discriminator");
            return View();
        }

        // POST: Photos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GalleryItemId,Title,Description,GalleryPhoto")] PhotoModel photoModel)
        {
            DateTime currentDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                //owner test variable
                photoModel.Owner = "Sam";
                //photoModel.Owner = "Jim";

                //album test
                photoModel.AlbumId = 28;

                //Change captured date to the current date
                photoModel.CapturedDate = currentDate;

                //photoModel.ImageUrl = "this is an image url placeholder";
                if (photoModel.GalleryPhoto != null)
                {                   
                    string rootPath = _webHostEnvironment.WebRootPath;
                    //give image unique name to avoid conflicts
                    string fileName = Path.GetFileNameWithoutExtension(photoModel.GalleryPhoto.FileName);
                    string fileExtension = Path.GetExtension(photoModel.GalleryPhoto.FileName);
                    photoModel.ImageName = fileName = fileName + currentDate.ToString("yymmssfff") + fileExtension;


                    //where image will be stored
                    string path = Path.Combine(rootPath + "/imageTest/", fileName);
                    //local test storage area for photos
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await photoModel.GalleryPhoto.CopyToAsync(fileStream);
                    }
                }

                _context.Add(photoModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlbumId"] = new SelectList(_context.Albums, "GalleryItemId", "Discriminator", photoModel.AlbumId);
            return View(photoModel);
        }

        // GET: Photos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photo = await _context.Photos.FindAsync(id);
            if (photo == null)
            {
                return NotFound();
            }
            //ViewData["AlbumId"] = new SelectList(_context.Albums, "GalleryItemId", "Discriminator", photo.AlbumId);
            return View(photo);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ImageName,GalleryItemId,Owner,Title,Description,CapturedDate")] PhotoModel photoModel)
        {
            if (id != photoModel.GalleryItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(photoModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhotoExists(photoModel.GalleryItemId))
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
            //ViewData["AlbumId"] = new SelectList(_context.Albums, "GalleryItemId", "Discriminator", photoModel.AlbumId);
            return View(photoModel);
        }

        // GET: Photos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photo = await _context.Photos
                .Include(p => p.Album)
                .FirstOrDefaultAsync(m => m.GalleryItemId == id);
            if (photo == null)
            {
                return NotFound();
            }

            return View(photo);
        }

        // POST: Photos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var photoModel = await _context.Photos.FindAsync(id);

            //delete image from folder
            var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "imageTest", photoModel.ImageName);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            _context.Photos.Remove(photoModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhotoExists(int id)
        {
            return _context.Photos.Any(e => e.GalleryItemId == id);
        }

        //download function
        [HttpGet]
        public async Task<IActionResult> Download(string fileName) 
        {
            if (fileName == null)
            {
                return Content("File name not found.");
            }


            //Specify path to fetch photo from
            string rootPath = _webHostEnvironment.WebRootPath;
            
            string path = Path.Combine(rootPath + "\\imageTest\\", fileName);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }

            memory.Position = 0;
            var extension = Path.GetExtension(path).ToLowerInvariant();
            return File(memory, GetMimeTypes()[extension], Path.GetFileName(path));
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".bmp", "image/bmp"},
                {".ico", "image/ico"},
                {".jpg", "image/jpg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".gif", "image/tiff"},
                {".png", "image/png"},
            };
        }
        }
}
