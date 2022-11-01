using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.ViewModels;

namespace vlissides_bibliotheque
{
    public static class UtilisateurExtension
    {
        public static Adresse GetAdresse(this Etudiant etudiant, ApplicationDbContext context)
        {
            int adresseId = etudiant.AdresseId;

            Adresse adresseFacturation = context.Adresses
                .Include(a => a.Province)
                .FirstOrDefault(a => a.AdresseId == adresseId);

            return adresseFacturation;
        }

        public static Etudiant ModelBinding(this Etudiant etudiant, Adresse adresse, GestionProfilVM vm)
        {
            adresse.App = vm.App;
            adresse.CodePostal = vm.CodePostal;
            adresse.NumeroCivique = Convert.ToInt32(vm.NoCivique);
            adresse.Rue = vm.Rue;
            adresse.Ville = vm.Ville;
            adresse.ProvinceId = vm.ProvinceId;

            etudiant.Email = vm.Courriel;
            etudiant.UserName = vm.Courriel;
            etudiant.Nom = vm.Nom;
            etudiant.Prenom = vm.Prenom;
            etudiant.PhoneNumber = vm.NoTelephone;
            etudiant.ProgrammeEtudeId = vm.ProgrammeEtudeId;

            return etudiant;
        }

        public static IdentityUser ModelBinding(this IdentityUser admin, GestionProfilVM vm)
        {
            admin.Email = vm.Courriel;
            admin.UserName = vm.Courriel;
            admin.PhoneNumber = vm.NoTelephone;

            return admin;
        }

        public static GestionProfilVM GetEtudiantProfilVM(this Etudiant etudiant, ApplicationDbContext context)
        {
            Adresse adresse = etudiant.GetAdresse(context);

            GestionProfilVM vm = new() {
                Courriel = etudiant.Email,
                Nom = etudiant.Nom,
                Prenom = etudiant.Prenom,
                NoTelephone = etudiant.PhoneNumber,
                ProgrammeEtudeId = etudiant.ProgrammeEtudeId,
                ProgrammeEtudes = new SelectList(context.ProgrammesEtudes.ToList(), nameof(ProgrammeEtude.ProgrammeEtudeId), nameof(ProgrammeEtude.Nom)),

                NoCivique = adresse.NumeroCivique.ToString(),
                Rue = adresse.Rue,
                Ville = adresse.Ville,
                App = adresse.App,
                CodePostal = adresse.CodePostal,
                ProvinceId = adresse.Province.ProvinceId,

                Provinces = new SelectList(context.Provinces.ToList(), nameof(Province.ProvinceId), nameof(Province.Nom)),
            };

            return vm;
        }

        public static GestionProfilVM GetAdminProfilVM(this IdentityUser admin)
        {
            GestionProfilVM vm = new() {
                Courriel = admin.Email,
                NoTelephone = admin.PhoneNumber
            };

            return vm;
        }
    }
}
