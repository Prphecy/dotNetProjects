﻿using AspNetCoreHero.ToastNotification.Abstractions;
using BlogApp.Data;
using BlogApp.Models;
using BlogApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace BlogApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PageController : Controller
    {
        private readonly ApplicationDbContext _context;
        public INotyfService _notification { get; }
        private readonly IWebHostEnvironment _webHostEnvironment;
        public PageController(ApplicationDbContext context, INotyfService notification, 
                                IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _notification = notification;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> About()
        {
            var about = await _context.Pages!.FirstOrDefaultAsync(x => x.Slug == "about");
            var vm = new PageVM()
            {
                Id = about!.Id,
                Title = about.Title,
                ShortDescription = about.ShortDescription,
                Description = about.Description,
                ThumbnailUrl = about.ThumbnailUrl,
            };
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> About(PageVM vm)
        {
            if(!ModelState.IsValid) { return View(vm); }
            var about = await _context.Pages!.FirstOrDefaultAsync(x => x.Slug == "about");
            if (about == null)
            {
                _notification.Error("Page not found!");
                return View();
            }
            about.Title = vm.Title;
            about.ShortDescription = vm.ShortDescription;
            about.Description = vm.Description;
            if (vm.Thumbnail != null)
            {
                about.ThumbnailUrl = UploadImage(vm.Thumbnail);
            }
            await _context.SaveChangesAsync();
            _notification.Success("About Page Updated!");
            return RedirectToAction("About", "Page", new { area = "Admin" });

        }

        [HttpGet]
        public async Task<IActionResult> Contact()
        {
            var page = await _context.Pages!.FirstOrDefaultAsync(x => x.Slug == "contact");
            var vm = new PageVM()
            {
                Id = page!.Id,
                Title = page.Title,
                ShortDescription = page.ShortDescription,
                Description = page.Description,
                ThumbnailUrl = page.ThumbnailUrl,
            };
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Contact(PageVM vm)
        {
            if (!ModelState.IsValid) { return View(vm); }
            var page = await _context.Pages!.FirstOrDefaultAsync(x => x.Slug == "contact");
            if (page == null)
            {
                _notification.Error("Page not found!");
                return View();
            }
            page.Title = vm.Title;
            page.ShortDescription = vm.ShortDescription;
            page.Description = vm.Description;
            if (vm.Thumbnail != null)
            {
                page.ThumbnailUrl = UploadImage(vm.Thumbnail);
            }
            await _context.SaveChangesAsync();
            _notification.Success("Contact Page Updated!");
            return RedirectToAction("Contact", "Page", new { area = "Admin" });

        }

        [HttpGet]
        public async Task<IActionResult> Privacy()
        {
            var page = await _context.Pages!.FirstOrDefaultAsync(x => x.Slug == "privacy");
            var vm = new PageVM()
            {
                Id = page!.Id,
                Title = page.Title,
                ShortDescription = page.ShortDescription,
                Description = page.Description,
                ThumbnailUrl = page.ThumbnailUrl,
            };
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Privacy(PageVM vm)
        {
            if (!ModelState.IsValid) { return View(vm); }
            var page = await _context.Pages!.FirstOrDefaultAsync(x => x.Slug == "privacy");
            if (page == null)
            {
                _notification.Error("Page not found!");
                return View();
            }
            page.Title = vm.Title;
            page.ShortDescription = vm.ShortDescription;
            page.Description = vm.Description;
            if (vm.Thumbnail != null)
            {
                page.ThumbnailUrl = UploadImage(vm.Thumbnail);
            }
            await _context.SaveChangesAsync();
            _notification.Success("Privacy Page Updated!");
            return RedirectToAction("Privacy", "Page", new { area = "Admin" });

        }

        private string UploadImage(IFormFile file)
        {
            string uniqueFileName = "";
            var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "thumbnails");
            uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            var filePath = Path.Combine(folderPath, uniqueFileName);
            using (FileStream fileStream = System.IO.File.Create(filePath))
            {
                file.CopyTo(fileStream);
            }
            return uniqueFileName;
        }
    }
}
