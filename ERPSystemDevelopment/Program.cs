using EFApp;
using EFApp.EntityFrameworkCore;
using ERPSystemDevelopment.Controllers;
using ManagementApplication;
using ManagementApplication.BaseEntity;
using ManagementApplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Configuration;

namespace ERPSystemDevelopment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.AddDbContext<ApplicationContext>(x =>
                 //x.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=testdb;Trusted_Connection=True;")); //Это у меня на компе
                 x.UseSqlServer(@"Server=ms-sql-10.in-solve.ru;Database=1gb_granddb;User ID=1gb_grand-smeta-kostoma;Password=dfs$t55FD;Encrypt=True;TrustServerCertificate=False;"));//это на серваке

            
            builder.Services.AddScoped<BaseEntityService<Customer>>();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
