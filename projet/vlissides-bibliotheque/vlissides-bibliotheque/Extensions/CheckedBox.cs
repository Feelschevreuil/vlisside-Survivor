using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using vlissides_bibliotheque.DAO;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.DTO.Ajax;
using vlissides_bibliotheque.Extensions.Interface;
using vlissides_bibliotheque.Models;

namespace vlissides_bibliotheque
{
    public class CheckedBox : ICheckedBox
    {
        private readonly ApplicationDbContext _context;


        public CheckedBox(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<checkBoxCours> GetCoursCheckedBox(string etudiantId)
        {
            List<checkBoxCours> listCheckedBox = new();
            List<Cours> coursBD = _context.Cours
                .Include(x => x.ProgrammeEtude)
                .ToList();

            List<CoursEtudiant> coursAssocierEtudiant = _context.CoursEtudiants
                .Include(x => x.Cours)
                .Include(x => x.Etudiant)
                .Where(x => x.EtudiantId == etudiantId)
                .ToList();

            foreach (Cours cours in coursBD)
            {
                checkBoxCours checkBox = new();
                checkBox.Cours = cours;
                checkBox.Cocher = false;
                listCheckedBox.Add(checkBox);

            }

            listCheckedBox.Where(c => coursAssocierEtudiant.Any(e => e.CoursId == c.Cours.CoursId)).ToList().ForEach(b => b.Cocher = true);

            return listCheckedBox;
        }

        public List<checkBoxCours> GetCoursLivre(LivreBibliotheque livreBibliotheque)
        {
            List<checkBoxCours> listCheckedBox = new();
            List<Cours> coursBD = _context.Cours
                .Include(x => x.ProgrammeEtude)
                .ToList();

            List<CoursLivre> coursAssocierLivre = _context.CoursLivres
                .Include(x => x.LivreBibliotheque)
                .Where(x => x.LivreBibliothequeId == livreBibliotheque.LivreId)
                .ToList();

            foreach (Cours cours in coursBD)
            {
                checkBoxCours boxCours = new()
                {
                    Cours = cours,
                    Cocher = false
                };
                listCheckedBox.Add(boxCours);
            }

            listCheckedBox.Where(c => coursAssocierLivre.Any(e => e.CoursId == c.Cours.CoursId)).ToList().ForEach(b => b.Cocher = true);

            return listCheckedBox;
        }

        public List<checkBoxAuteurs> GetAuteursLivre(LivreBibliotheque livreBibliotheque)
        {
            List<checkBoxAuteurs> listCheckedBox = new();
            List<Auteur> auteursBD = _context.Auteurs
                .ToList();

            List<AuteurLivre> auteursAssocierLivre = _context.AuteursLivres
                .Include(x => x.LivreBibliotheque)
                .Where(x => x.LivreBibliothequeId == livreBibliotheque.LivreId)
                .ToList();

            foreach (Auteur auteur in auteursBD)
            {
                checkBoxAuteurs boxAuteur = new()
                {
                    Auteur = auteur,
                    Cocher = false
                };

                listCheckedBox.Add(boxAuteur);
            }

            listCheckedBox.Where(c => auteursAssocierLivre.Any(e => e.AuteurId == c.Auteur.AuteurId)).ToList().ForEach(b => b.Cocher = true);
            return listCheckedBox;
        }

        public List<checkBoxCours> GetCours()
        {
            List<checkBoxCours> listCheckedBox = new();
            List<Cours> coursBD = _context.Cours
                .Include(x => x.ProgrammeEtude)
                .ToList();

            foreach (Cours cours in coursBD)
            {
                checkBoxCours boxCours = new()
                {
                    Cours = cours,
                    Cocher = false
                };

                listCheckedBox.Add(boxCours);
            }

            return listCheckedBox;
        }

        public List<checkBoxAuteurs> GetAuteurs()
        {
            List<checkBoxAuteurs> listCheckedBox = new();
            List<Auteur> auteursBD = _context.Auteurs
                .ToList();

            foreach (Auteur auteur in auteursBD)
            {
                checkBoxAuteurs boxAuteurs = new()
                {
                    Auteur = auteur,
                    Cocher = false
                };

                listCheckedBox.Add(boxAuteurs);
            }
            return listCheckedBox;
        }

    }
}
