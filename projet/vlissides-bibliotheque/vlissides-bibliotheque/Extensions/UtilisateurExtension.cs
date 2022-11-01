using Microsoft.AspNetCore.Identity;
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
    }
}
