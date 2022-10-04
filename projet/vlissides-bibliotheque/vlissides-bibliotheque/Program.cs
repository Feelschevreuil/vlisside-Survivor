using Microsoft.AspNetCore.Identity;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace vlissides_bibliotheque
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

	    string connectionString;
	    connectionString = builder.Configuration.GetConnectionString("mssql") ?? throw new InvalidOperationException("Connection string 'mssql' not found.");
    
	    builder.Services.AddDbContext<ApplicationDbContext>(options =>
		options.UseSqlServer(connectionString)
	    );

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<Utilisateur>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddSignInManager()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddIdentityCore<Etudiant>()
                .AddRoles<IdentityRole>()
                .AddSignInManager()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()) {
                app.UseMigrationsEndPoint();
            } else {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}