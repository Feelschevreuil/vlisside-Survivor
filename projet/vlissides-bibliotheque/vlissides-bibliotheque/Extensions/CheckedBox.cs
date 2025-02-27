using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using vlissides_bibliotheque.DAO.Interface;
using vlissides_bibliotheque.DTO.Ajax;
using vlissides_bibliotheque.Extensions.Interface;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.ViewModels;

namespace vlissides_bibliotheque
{
    public class CheckedBox : ICheckedBox
    {
        private readonly IDAO<LivreBibliotheque> _livreDAO;
        private readonly IDAO<Auteur> _auteurDAO;
        private readonly IDAO<Cours> _coursDAO;
        private readonly IDAOEtudiant<Etudiant> _etudiantDAO;
        private readonly IMapper _mapper;


        public CheckedBox(IDAO<LivreBibliotheque> livreDAO, IDAO<Auteur> auteurDAO, IDAO<Cours> coursDAO, IDAOEtudiant<Etudiant> etudiantDAO, IMapper mapper)
        {
            _livreDAO = livreDAO;
            _auteurDAO = auteurDAO;
            _coursDAO = coursDAO;
            _etudiantDAO = etudiantDAO;
            _mapper = mapper;
        }
        public List<checkBoxCours> GetCoursCheckedBox(string etudiantId)
        {
            List<checkBoxCours> listCheckedBox = new();
            var etudiant = _etudiantDAO.GetById(etudiantId);

            foreach (Cours cours in _coursDAO.GetAll())
                listCheckedBox.Add(new()
                {
                    Cours = cours,
                    Cocher = etudiant.Cours.Any(c=> c.CoursId == cours.CoursId) ? true : false,
                });

            
            return listCheckedBox;
        }

        public List<checkBoxCours> GetCoursLivre(int livreId)
        {
            List<checkBoxCours> listCheckedBox = new();
            LivreBibliotheque livre = _livreDAO.GetById(livreId);

            foreach (Cours cours in _coursDAO.GetAll())
                listCheckedBox.Add(new()
                {
                    Cours = cours,
                    Cocher = livre.Cours.Any(c => c.CoursId == cours.CoursId) ? true : false
                });


            return listCheckedBox;
        }

        public List<checkBoxAuteurs> GetAuteursLivre(int livreId)
        {
            List<checkBoxAuteurs> listCheckedBox = new();
            LivreBibliotheque livre = _livreDAO.GetById(livreId);

            foreach (var auteur in _auteurDAO.GetAll())
                listCheckedBox.Add(
                    new()
                    {
                        Auteur = _mapper.Map<AuteurVM>(auteur),
                        Cocher = livre.Auteurs.Any(a => a.AuteurId == auteur.AuteurId) ? true : false
                    }
                    );

            return listCheckedBox;
        }

        public List<checkBoxCours> GetCours()
        {
            List<checkBoxCours> listCheckedBox = new();

            foreach (Cours cours in _coursDAO.GetAll())
                listCheckedBox.Add(new()
                {
                    Cours = cours,
                    Cocher = false
                });
            

            return listCheckedBox;
        }

        public List<checkBoxAuteurs> GetAuteurs()
        {
            List<checkBoxAuteurs> listCheckedBox = new();

            foreach (Auteur auteur in _auteurDAO.GetAll())
                listCheckedBox.Add(new()
                {
                    Auteur = _mapper.Map<AuteurVM>(auteur),
                    Cocher = false
                });
            
            return listCheckedBox;
        }

    }
}
