using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Enums;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.ViewModels;

namespace vlissides_bibliotheque
{
    public static class ListDropDown
    {
        public static List<SelectListItem> ListDropDownAuteurs(ApplicationDbContext _context)
        {

            List<SelectListItem> Liste = new List<SelectListItem>();

            Liste.Add(new SelectListItem { Value = "", Text = "Choisissez un auteur" });

            foreach (var e in _context.Auteurs)
                Liste.Add(new SelectListItem { Value = e.AuteurId.ToString(), Text = e.Nom + ", " + e.Prenom });

            return Liste;
        }

        public static List<SelectListItem> ListDropDownMaisonDedition(ApplicationDbContext _context)
        {

            List<SelectListItem> Liste = new List<SelectListItem>();

            Liste.Add(new SelectListItem { Value = "", Text = "Choisissez une maison d'édition" });

            foreach (var e in _context.MaisonsEdition)
                Liste.Add(new SelectListItem { Value = e.MaisonEditionId.ToString(), Text = e.Nom });

            return Liste;
        }
        public static List<SelectListItem> ListDropDownProgrammesEtude(ApplicationDbContext _context)
        {

            List<SelectListItem> Liste = new List<SelectListItem>();
            List<ProgrammeEtude> bdProgrammeEtude = _context.ProgrammesEtudes.ToList();
            Liste.Add(new SelectListItem { Value = "", Text = "Choisissez un programme d'étude" });

            foreach (var e in bdProgrammeEtude)
                Liste.Add(new SelectListItem { Value = e.ProgrammeEtudeId.ToString(), Text = e.Nom });

            return Liste;
        }

        public static List<SelectListItem> ListDropDownEtatsLivre()
        {
            List<SelectListItem> Liste = new List<SelectListItem>();
            List<EtatLivreEnum> listEnum = Enum.GetValues(typeof(EtatLivreEnum)).Cast<EtatLivreEnum>().ToList();

            for (int e = 0; e< Enum.GetNames(typeof(EtatLivreEnum)).Length; e++)
            {
                string valeurAffichage = listEnum[e].ToString().ToLower();
                valeurAffichage = char.ToUpper(valeurAffichage[0]) + valeurAffichage.Substring(1);
                Liste.Add(new SelectListItem { Value = e.ToString(), Text = valeurAffichage });
            }
            return Liste;
        }

        public static List<SelectListItem> ListDropDownStatutCommande()
        {
            List<SelectListItem> Liste = new List<SelectListItem>();
            List<StatusFacture> listEnum = Enum.GetValues(typeof(StatusFacture)).Cast<StatusFacture>().ToList();

            for (int e = 0; e < Enum.GetNames(typeof(StatusFacture)).Length; e++)
            {
                string valeurAffichage = listEnum[e].ToString().ToLower();
                valeurAffichage = char.ToUpper(valeurAffichage[0]) + valeurAffichage.Substring(1);
                Liste.Add(new SelectListItem { Value = e.ToString(), Text = valeurAffichage });
            }
            return Liste;
        }

        public static List<SelectListItem> ListDropDownEtudiant(ApplicationDbContext _context)
        {

            List<SelectListItem> Liste = new List<SelectListItem>();

            Liste.Add(new SelectListItem { Value = "", Text = "Choisissez un étudiant" });

            foreach (var e in _context.Etudiants)
                Liste.Add(new SelectListItem { Value = e.Id.ToString(), Text = e.Nom + ", " + e.Prenom });

            return Liste;
        }
    }
}
