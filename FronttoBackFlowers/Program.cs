using FronttoBackFlowers.DAL;
using FronttoBackFlowers.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FronttoBackFlowers
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            object addNewtonsoftJson = builder.Services.AddControllersWithViews().AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Error);
            builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), 
                builder =>
                {
                    builder.MigrationsAssembly("FronttoBackFlowers");
                }));
            builder.Services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
                options.SignIn.RequireConfirmedEmail = true; 

                options.User.RequireUniqueEmail = true;

                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            builder.Services.AddSession(opt => opt.IdleTimeout= TimeSpan.FromMinutes(5));
            var app = builder.Build(); 

            if(app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else

            {
                app.UseExceptionHandler("/ErrorPage");
            }

            app.UseStatusCodePagesWithReExecute("/ErrorPage/ErrorAction","?code={0}");


            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

                app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.Run();
        }
    }
}