using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Extensions.Interface;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.Services.Interface;
using vlissides_bibliotheque.ViewModels;

namespace vlissides_bibliotheque.Services
{
    public class UtilisateurService : IUtilisateur
    {
        private readonly ApplicationDbContext _context;
        private readonly ICheckedBox _checkedBox;

        public UtilisateurService(ApplicationDbContext context, ICheckedBox checkedBox)
        {
            _context = context;
            _checkedBox = checkedBox;
        }

        public Adresse GetAdresse(Etudiant etudiant)
        {
            int adresseId = etudiant.AdresseId;

            Adresse adresseFacturation = _context.Adresses
                .Include(a => a.Province)
                .FirstOrDefault(a => a.AdresseId == adresseId);

            return adresseFacturation;
        }

        public Etudiant ModelBinding(Etudiant etudiant, Adresse adresse, GestionProfilVM vm)
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

        public Utilisateur ModelBinding(Utilisateur admin, GestionProfilVM vm)
        {
            admin.Email = vm.Courriel;
            admin.Nom = vm.Nom;
            admin.Prenom = vm.Prenom;
            admin.UserName = vm.Courriel;
            admin.PhoneNumber = vm.NoTelephone;

            return admin;
        }

        public GestionProfilVM GetEtudiantProfilVM(Etudiant etudiant)
        {
            Adresse adresse = GetAdresse(etudiant);

            GestionProfilVM vm = new()
            {
                Courriel = etudiant.Email,
                Nom = etudiant.Nom,
                Prenom = etudiant.Prenom,
                NoTelephone = etudiant.PhoneNumber,
                ProgrammeEtudeId = etudiant.ProgrammeEtudeId,
                ProgrammeEtudes = new SelectList(_context.ProgrammesEtudes.ToList(), nameof(ProgrammeEtude.ProgrammeEtudeId), nameof(ProgrammeEtude.Nom)),
                NoCivique = adresse.NumeroCivique.ToString(),
                Rue = adresse.Rue,
                Ville = adresse.Ville,
                App = adresse.App,
                CodePostal = adresse.CodePostal,
                ProvinceId = adresse.Province.ProvinceId,
                NomProvince = adresse.Province.Nom,
                checkBoxCours = _checkedBox.GetCoursCheckedBox(etudiant.Id),
                Provinces = new SelectList(_context.Provinces.ToList(), nameof(Province.ProvinceId), nameof(Province.Nom)),
            };

            return vm;
        }

        public GestionProfilVM GetAdminProfilVM(Utilisateur admin)
        {
            GestionProfilVM vm = new()
            {
                Courriel = admin.Email,
                Nom = admin.Nom,
                Prenom = admin.Prenom,
                NoTelephone = admin.PhoneNumber
            };

            return vm;
        }

        public GestionProfilVM NewGestionProfilVM()
        {
            GestionProfilVM vm = new()
            {
                ProgrammeEtudes = new SelectList(_context.ProgrammesEtudes.ToList(), nameof(ProgrammeEtude.ProgrammeEtudeId), nameof(ProgrammeEtude.Nom)),
                Provinces = new SelectList(_context.Provinces.ToList(), nameof(Province.ProvinceId), nameof(Province.Nom))
            };
            return vm;
        }

        public InscriptionVM NewInscriptionVM()
        {
            InscriptionVM vm = new()
            {
                ProgrammeEtudes = new SelectList(_context.ProgrammesEtudes.ToList(), nameof(ProgrammeEtude.ProgrammeEtudeId), nameof(ProgrammeEtude.Nom)),
                Provinces = new SelectList(_context.Provinces.ToList(), nameof(Province.ProvinceId), nameof(Province.Nom))
            };
            return vm;
        }

    }
}
