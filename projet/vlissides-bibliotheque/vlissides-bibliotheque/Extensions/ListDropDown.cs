using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using vlissides_bibliotheque.Data;
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
    }
}
