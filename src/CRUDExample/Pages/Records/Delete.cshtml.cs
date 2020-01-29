using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CRUDExample.Models;

namespace CRUDExample
{
    public class DeleteModel : PageModel
    {
        private readonly CRUDExample.Models.JournalDbContext _context;

        public DeleteModel(CRUDExample.Models.JournalDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DailyRecord DailyRecord { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DailyRecord = await _context.Records.FirstOrDefaultAsync(m => m.Id == id);

            if (DailyRecord == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DailyRecord = await _context.Records.FindAsync(id);

            if (DailyRecord != null)
            {
                _context.Records.Remove(DailyRecord);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
