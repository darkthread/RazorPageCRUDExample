using CRUDExample.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestProject
{
    [TestClass]
    public class UnitTest2
    {
        JournalDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<JournalDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
            return new JournalDbContext(options);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var ctx = GetDbContext();
            var data = new DailyRecord()
            {
                Date = DateTime.Now,
                EventSummary = "No Events",
                Remark = "ABC",
                Status = StatusFlags.Warn,
                User = "Jeffrey"
            };
            ctx.Records.Add(data);
            ctx.SaveChanges();
            var rec = ctx.Records.FirstOrDefault();
            Assert.IsNotNull(rec);
        }
    }
}
