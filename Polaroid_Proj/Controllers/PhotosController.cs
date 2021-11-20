using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Photos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Photos.Include(p => p.Album);
            return View(await applicationDbContext.ToListAsync());
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
        public async Task<IActionResult> Create([Bind("GalleryItemId,Title,Description")] Photo photo)
        {

            if (ModelState.IsValid)
            {
                DateTime currentDate = DateTime.Now;
                photo.CapturedDate = currentDate;

                photo.ImageUrl = "this is an image url placeholder";

                /*if (photo.GalleryPhoto != null)
                {
                    //image storage path
                    string localFolder = "imageTest";
                    localFolder += photo.GalleryPhoto.FileName + currentDate.ToString("yymmssfff");
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, localFolder);
                }
*/



                _context.Add(photo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlbumId"] = new SelectList(_context.Albums, "GalleryItemId", "Discriminator", photo.AlbumId);
            return View(photo);
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
            ViewData["AlbumId"] = new SelectList(_context.Albums, "GalleryItemId", "Discriminator", photo.AlbumId);
            return View(photo);
        }

        // POST: Photos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ImageUrl,AlbumId,FileName,GalleryItemId,CapturedDate,UserId,Title,Description")] Photo photo)
        {
            if (id != photo.GalleryItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(photo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhotoExists(photo.GalleryItemId))
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
            ViewData["AlbumId"] = new SelectList(_context.Albums, "GalleryItemId", "Discriminator", photo.AlbumId);
            return View(photo);
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
            var photo = await _context.Photos.FindAsync(id);
            _context.Photos.Remove(photo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhotoExists(int id)
        {
            return _context.Photos.Any(e => e.GalleryItemId == id);
        }
    }
}
