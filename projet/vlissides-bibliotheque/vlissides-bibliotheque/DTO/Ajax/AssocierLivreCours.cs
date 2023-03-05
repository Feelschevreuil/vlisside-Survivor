using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.Validation;
using vlissides_bibliotheque.ViewModels;

namespace vlissides_bibliotheque.DTO.Ajax
{
    public class AssocierLivreCours : CreationLivreVM
    {
        public List<int> Cours { get => Cours; set => Cours = value; }
        public List<int> Auteurs { get => Auteurs; set => Auteurs = value; }
        public int Id { get; set; }
        public string DateFormater { get => DateFormater; set => DateFormater = value; }
    }
}
