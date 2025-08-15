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

            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection"); //��� ���� "DefaultConnectionHome"
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


            //��������� ����������� ����� �� ����� ���� �������.
            //� ���� ���� �� ���������� �� ��� ������ ������
            //� ����� � ���� ��������� � ���� ���� ���������� � � �������� � � �������� �
            //�� ����� ������� �� �����. �� ������� ���������. ��� ��� ��-�� �����
            // �� ���������� ����.

            #region
            if (!app.Environment.IsDevelopment())
            {
                // ���������� ���������� ������ - ��� ��������� �����
                app.UseExceptionHandler(errorApp =>
                {
                    errorApp.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "text/html";

                        var exceptionHandlerPathFeature = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerPathFeature>();

                        if (exceptionHandlerPathFeature?.Error != null)
                        {
                            // �������� ������
                            logger.LogError(exceptionHandlerPathFeature.Error, "Unhandled exception occurred");

                            // ����� ������� ������� �������� � ���������� �� ������
                            await context.Response.WriteAsync("<html><body>\r\n");
                            await context.Response.WriteAsync("��������� ���������� ������ �������.<br><br>\r\n");

                            // � ���������� �� ������� ����������� ������, �� � dev ����� ��������
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
                // � ������ ���������� �������� ��������� �������� ������
                app.UseDeveloperExceptionPage();
            }
            #endregion





            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapRazorPages().WithStaticAssets();

            //������������ ������ ��������
            app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

            await app.RunAsync();
        }
    }
}
