using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using vlissides_bibliotheque.DAO;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.DTO;
using vlissides_bibliotheque.Models;

namespace vlissides_bibliotheque
{
    public static class CheckedBox
    {
        public static List<checkBoxCours> GetCoursCheckedBox(ApplicationDbContext _context, string etudiantId)
        {
            List<checkBoxCours> listCheckedBox = new();
            List<Cours> coursBD = _context.Cours
                .Include(x => x.ProgrammeEtude)
                .ToList();
            List<CoursEtudiant> listCoursEtudiant = _context.CoursEtudiants
                .Include(x => x.Cours)
                .Include(x => x.Etudiant)
                .ToList();
            List<CoursEtudiant> coursAssocierEtudiant = listCoursEtudiant.FindAll(x => x.EtudiantId == etudiantId);

            foreach (Cours cours in coursBD)
            {
                checkBoxCours checkBox = new();
                checkBox.Cours = cours;
                checkBox.Cocher = false;
                listCheckedBox.Add(checkBox);

            }

            foreach (CoursEtudiant coursEtudiant in coursAssocierEtudiant)
            {
                listCheckedBox.Find(x => x.Cours.CoursId == coursEtudiant.CoursId).Cocher = true;

            }
            return listCheckedBox;
        }

        public static List<checkBoxCours> GetCoursLivre(ApplicationDbContext _context, LivreBibliotheque livreBibliotheque)
        {
            List<checkBoxCours> listCheckedBox = new();
            List<Cours> coursBD = _context.Cours
                .Include(x => x.ProgrammeEtude)
                .ToList();

            List<CoursLivre> coursAssocierLivre = _context.CoursLivres
                .Include(x=>x.LivreBibliotheque)
                .ToList()
                .FindAll(x => x.LivreBibliothequeId == livreBibliotheque.LivreId);

            foreach (Cours cours in coursBD)
            {
                checkBoxCours boxCours = new()
                {
                    Cours = cours,
                    Cocher = false
                };

                listCheckedBox.Add(boxCours);
            }

            foreach (CoursLivre coursLivre in coursAssocierLivre)
            {
                listCheckedBox.Find(x => x.Cours.CoursId == coursLivre.CoursId).Cocher = true;

            }


            return listCheckedBox;
        }

        public static List<checkBoxAuteurs> GetAuteursLivre(ApplicationDbContext _context, LivreBibliotheque livreBibliotheque)
        {
            List<checkBoxAuteurs> listCheckedBox = new();
            List<Auteur> auteursBD = _context.Auteurs
                .ToList();

            List<AuteurLivre> auteursAssocierLivre = _context.AuteursLivres
                .Include(x => x.LivreBibliotheque)
                .ToList()
                .FindAll(x => x.LivreBibliothequeId == livreBibliotheque.LivreId);

            foreach (Auteur auteur in auteursBD)
            {
                checkBoxAuteurs boxAuteur = new()
                {
                    Auteur = auteur,
                    Cocher = false
                };

                listCheckedBox.Add(boxAuteur);
            }

            foreach (AuteurLivre auteurLivre in auteursAssocierLivre)
            {
                listCheckedBox.Find(x => x.Auteur.AuteurId == auteurLivre.AuteurId).Cocher = true;

            }
            return listCheckedBox;
        }

        public static List<checkBoxCours> GetCours(ApplicationDbContext _context)
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

        public static List<checkBoxAuteurs> GetAuteurs (ApplicationDbContext _context)
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

        public static List<checkBoxLivre> GetLivres(LivresBibliothequeDAO _livreDAO)
        {
            List<checkBoxLivre> listCheckedBox = new();
            List<LivreBibliotheque> livreBD = _livreDAO.GetAll()
                .OrderBy(x => x.Titre)
                .ToList();
                

            foreach (LivreBibliotheque livre in livreBD)
            {
                checkBoxLivre checkBox = new()
                {
                    livre = livre,
                    Cocher = false
                };

                listCheckedBox.Add(checkBox);
            }

            return listCheckedBox;
        }
    }
}
