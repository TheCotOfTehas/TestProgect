using EFApp;
using EFApp.EntityFrameworkCore;
using ERPSystemDevelopment.Controllers;
using ManagementApplication;
using ManagementApplication.BaseEntity;
using ManagementApplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace ERPSystemDevelopment
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            string connectionString =  builder.Configuration.GetConnectionString("DefaultConnection"); //или дома "DefaultConnectionHome"
            builder.Services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddScoped<BaseEntityService<Customer>>();
            builder.Services.AddScoped<BaseEntityService<Resource>>();
            builder.Services.AddScoped<BaseEntityService<UnitMeasurement>>();



            var app = builder.Build();


            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            using (ApplicationContext db = new ApplicationContext())
            {
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapRazorPages().WithStaticAssets();

            //регистрируем нужные маршруты
            app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
