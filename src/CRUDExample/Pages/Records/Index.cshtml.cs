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
    public class IndexModel : PageModel
    {
        private readonly CRUDExample.Models.JournalDbContext _context;

        public IndexModel(CRUDExample.Models.JournalDbContext context)
        {
            _context = context;
        }

        public IList<DailyRecord> DailyRecord { get;set; }

        public async Task OnGetAsync()
        {
            DailyRecord = await _context.Records.ToListAsync();
        }
    }
}
