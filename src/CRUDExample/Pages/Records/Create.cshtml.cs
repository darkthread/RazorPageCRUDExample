﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CRUDExample.Models;

namespace CRUDExample
{
    public class CreateModel : PageModel
    {
        private readonly CRUDExample.Models.JournalDbContext _context;

        public CreateModel(CRUDExample.Models.JournalDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public DailyRecord DailyRecord { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Records.Add(DailyRecord);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
