using System;
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

            //檢查同日期是否已有資料，若是顯示日期重複
            if (_context.Records.Any(o => o.Date == DailyRecord.Date))
            {
                ModelState.AddModelError("DailyRecord.Date", "該日期記錄已存在");
                return Page();
            }

            _context.Records.Add(DailyRecord);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
