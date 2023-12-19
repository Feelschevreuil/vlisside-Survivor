using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using vlissides_bibliotheque.Commun;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Extensions.Interface;

namespace vlissides_bibliotheque
{
    public class ListDropDown : IDropDownList
    {
        private readonly ApplicationDbContext _context;
        public ListDropDown(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<SelectListItem> ListDropDownAuteurs()
        {
            List<SelectListItem> Liste = new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "Choisissez un auteur" }
            };

            foreach (var e in _context.Auteurs)
                Liste.Add(new SelectListItem { Value = e.AuteurId.ToString(), Text = e.Nom + ", " + e.Prenom });

            return Liste;
        }

        public List<SelectListItem> ListDropDownMaisonDedition()
        {
            List<SelectListItem> Liste = new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "Choisissez une maison d'édition" }
            };

            foreach (var e in _context.MaisonsEdition)
                Liste.Add(new SelectListItem { Value = e.MaisonEditionId.ToString(), Text = e.Nom });

            return Liste;
        }
        public List<SelectListItem> ListDropDownProgrammesEtude()
        {
            List<SelectListItem> Liste = new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "Choisissez un programme d'étude" }
            };

            foreach (var e in _context.ProgrammesEtudes.ToList())
                Liste.Add(new SelectListItem { Value = e.ProgrammeEtudeId.ToString(), Text = e.Nom });

            return Liste;
        }

        public List<SelectListItem> ListDropDownEtatsLivre()
        {
            List<SelectListItem> Liste = new List<SelectListItem>();
            List<Enumeration.EtatLivreEnum> listEnum = Enum.GetValues(typeof(Enumeration.EtatLivreEnum)).Cast<Enumeration.EtatLivreEnum>().ToList();

            for (int e = 0; e < Enum.GetNames(typeof(Enumeration.EtatLivreEnum)).Length; e++)
            {
                string valeurAffichage = listEnum[e].ToString().ToLower();
                valeurAffichage = char.ToUpper(valeurAffichage[0]) + valeurAffichage.Substring(1);
                Liste.Add(new SelectListItem { Value = e.ToString(), Text = valeurAffichage });
            }
            return Liste;
        }

        public List<SelectListItem> ListDropDownStatutCommande()
        {
            List<SelectListItem> Liste = new List<SelectListItem>();
            List<Enumeration.StatutFactureEnum> listEnum = Enum.GetValues(typeof(Enumeration.StatutFactureEnum)).Cast<Enumeration.StatutFactureEnum>().ToList();

            for (int e = 0; e < Enum.GetNames(typeof(Enumeration.StatutFactureEnum)).Length; e++)
            {
                string valeurAffichage = listEnum[e].ToString().ToLower();
                valeurAffichage = char.ToUpper(valeurAffichage[0]) + valeurAffichage.Substring(1);
                Liste.Add(new SelectListItem { Value = e.ToString(), Text = valeurAffichage });
            }
            return Liste;
        }

        public List<SelectListItem> ListDropDownEtudiant()
        {
            List<SelectListItem> Liste = new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "Choisissez un étudiant" }
            };

            foreach (var e in _context.Etudiants.ToList())
                Liste.Add(new SelectListItem { Value = e.Id.ToString(), Text = e.Nom + ", " + e.Prenom });

            return Liste;
        }
    }
}
