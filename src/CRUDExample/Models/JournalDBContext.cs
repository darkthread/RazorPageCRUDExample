using Microsoft.EntityFrameworkCore;

namespace CRUDExample.Models
{
    /// <summary>
    /// 日誌資料
    /// </summary>
    public class JournalDbContext : DbContext
    {
        public DbSet<DailyRecord> Records { get; set; }

        public JournalDbContext(DbContextOptions options) : base(options)
        {

        }

        //REF: https://blog.darkthread.net/blog/ef-core-notes-2/
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //建立日期 Unique Index
            modelBuilder.Entity<DailyRecord>()
                .HasIndex(o => o.Date).IsUnique();
        }
    }
}
