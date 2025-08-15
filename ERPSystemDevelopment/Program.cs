using EFApp;
using EFApp.EntityFrameworkCore;
using ManagementApplication.BaseEntity;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace ERPSystemDevelopment
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            //builder.Services.AddDbContext<ApplicationContext>();

            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection"); //или дома "DefaultConnectionHome"
            builder.Services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(connectionString));

 
            //builder.Services.AddDbContext<ApplicationContext>();

            builder.Services.AddScoped<BaseEntityService<Customer>>();
            builder.Services.AddScoped<BaseEntityService<Resource>>();
            builder.Services.AddScoped<BaseEntityService<UnitMeasurement>>();
            var app = builder.Build();


            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                db.Database.EnsureCreated();
                db.Dispose();
                SqlConnection.ClearAllPools();
            }


            var logger = app.Services.GetRequiredService<ILogger<Program>>();


            //Настройку логирование ранее не делал взял готовую.
            //У меня сайт не запускался не мог понять почему
            //В итоге у меня обращенеи к базе было определено и в программ и в контесте и
            //всё время работал со своей. Ща пытаюсь настроить. так как из-за этого
            // не запускался сайт.

            #region
            if (!app.Environment.IsDevelopment())
            {
                // Глобальный обработчик ошибок - для продакшен среды
                app.UseExceptionHandler(errorApp =>
                {
                    errorApp.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "text/html";

                        var exceptionHandlerPathFeature = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerPathFeature>();

                        if (exceptionHandlerPathFeature?.Error != null)
                        {
                            // Логируем ошибку
                            logger.LogError(exceptionHandlerPathFeature.Error, "Unhandled exception occurred");

                            // Можно вывести простую страницу с сообщением об ошибке
                            await context.Response.WriteAsync("<html><body>\r\n");
                            await context.Response.WriteAsync("Произошла внутренняя ошибка сервера.<br><br>\r\n");

                            // В продакшене не выводим подробности ошибки, но в dev можно добавить
                            if (app.Environment.IsDevelopment())
                            {
                                await context.Response.WriteAsync($"<div>{exceptionHandlerPathFeature.Error.Message}</div>\r\n");
                            }

                            await context.Response.WriteAsync("</body></html>\r\n");
                        }
                    });
                });

                app.UseHsts();
            }
            else
            {
                // В режиме разработки включаем подробные страницы ошибок
                app.UseDeveloperExceptionPage();
            }
            #endregion





            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapRazorPages().WithStaticAssets();

            //регистрируем нужные маршруты
            app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

            await app.RunAsync();
        }
    }
}
