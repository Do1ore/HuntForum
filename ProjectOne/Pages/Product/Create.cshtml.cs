﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectFuse.Models;

namespace ProjectFuse.Pages.Product
{
    [Authorize(Roles = "admin")]
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CreateModel(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty] public InputModel Input { get; set; }

        public class InputModel
        {
            public Models.Product Product { get; set; } = default!;
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || Input?.Product == null)
            {
                return Page();
            }

            var fileName = ("productImage" + Input!.Product.ItemImage!.FileName).Trim().Replace(' ', '_');

            var relativePath = $@"\img\products\{fileName}";

            var fullPath = _webHostEnvironment.WebRootPath + relativePath;

            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }

            Input.Product.ImagePath = relativePath;
            await using (FileStream sw = new FileStream(fullPath, FileMode.Create))
            {
                await Input.Product.ItemImage.CopyToAsync(sw);
            }

            _context.Products.Add(Input.Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}