using Microsoft.AspNetCore.Identity;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Authentication.Cookies;
using Stripe;
using vlissides_bibliotheque.DAO;
using vlissides_bibliotheque.ViewModels;
using vlissides_bibliotheque.Services;
using AutoMapper;
using vlissides_bibliotheque.Mapper;
using vlissides_bibliotheque.Extensions.Interface;
using vlissides_bibliotheque.Services.Interface;
using vlissides_bibliotheque.DAO.Interface;

namespace vlissides_bibliotheque
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string connectionString;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                connectionString = builder.Configuration.GetConnectionString("mssql") ?? throw new InvalidOperationException("Connection string 'mssql' not found.");

                builder.Services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(connectionString)
                );
            }
            else
            {
                connectionString = builder.Configuration.GetConnectionString("sqlite") ?? throw new InvalidOperationException("Connection string 'sqlite' not found.");
                builder.Services.AddDbContext<ApplicationDbContext>(
                    options => options.UseSqlite(
                    connectionString
                    )
                );
            }
            
            #region "addScopeService"

            builder.Services.AddScoped<LivresBibliothequeDAO>();
            builder.Services.AddScoped<ILivreBibliotheque, LivreBibliothequeService>();
            builder.Services.AddScoped<IEvenementVM, EvenementService>();
            builder.Services.AddScoped<ICheckedBox,CheckedBox>();
            builder.Services.AddScoped<IDropDownList,ListDropDown>();
            builder.Services.AddScoped<IPrix,PrixService>();
            builder.Services.AddScoped<IUtilisateur,UtilisateurService>();

            #endregion

            #region addScopeDAO
            
            builder.Services.AddScoped<IDAO<LivreBibliotheque>, LivresBibliothequeDAO>();
            builder.Services.AddScoped<IDAO<Evenement>, EvenementDAO>();
            builder.Services.AddScoped<IDAO<PrixEtatLivre>, PrixEtatLivreDAO>();
            builder.Services.AddScoped<IDAO<ProgrammeEtude>, ProgrammeEtudeDAO>();
            builder.Services.AddScoped<IDAO<Cours>, CoursDAO>();
            builder.Services.AddScoped<IDAOEtudiant<Etudiant>, EtudiantDAO>();
            builder.Services.AddScoped<IDAO<Auteur>, AuteurDAO>();
            
            #endregion

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<Utilisateur>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddSignInManager()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddIdentityCore<Etudiant>()
                .AddRoles<IdentityRole>()
                .AddSignInManager()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.PostConfigure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme,
                opt =>
                {
                    opt.LoginPath = "/Connexion";
                });

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
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
