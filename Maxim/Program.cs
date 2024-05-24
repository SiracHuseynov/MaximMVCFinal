using Maxim.Business.Services.Abstracts;
using Maxim.Business.Services.Concretes;
using Maxim.Core.Models;
using Maxim.Core.RepositoryAbstracts;
using Maxim.Data.DAL;
using Maxim.Data.RepositoryConcretes;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Maxim
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("default"));
            });

            builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = true;
                opt.Password.RequireDigit = true;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireUppercase = true;
                opt.Password.RequiredLength = 8;

                opt.User.RequireUniqueEmail = false;


            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders(); ;

            builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
            builder.Services.AddScoped<IServiceService, ServiceService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
			name: "areas",
			pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}" 
		  );

			app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
