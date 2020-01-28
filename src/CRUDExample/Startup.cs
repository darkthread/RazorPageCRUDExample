using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUDExample.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CRUDExample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //註冊 DB Context，指定使用 SQL 資料庫
            services.AddDbContextPool<JournalDbContext>(options =>
            {
                //TODO: 實際應用時，連線字串不該寫死在程式碼裡，應應移入設定檔並加密儲存
                options.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=X:\Temp\Journal.mdf;Integrated Security=True;Connect Timeout=30");
            });

            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
                        //透過建構式參數取得 DBContext (依賴注入架構的標準做法)
                        JournalDbContext dbContext)
        {
            //檢查資料表是否已經存在，若不存在自動建立；
            //若資料表存在但版本太舊符則自動更新。
            //在正式環境自動更新資料庫有點可怕，我加了限定LocalDB執行的安全鎖
            if (dbContext.Database.GetDbConnection().ConnectionString.Contains("MSSQLLocalDB"))
            {
                dbContext.Database.Migrate();
            }


            if (env.IsDevelopment())
            {

                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
