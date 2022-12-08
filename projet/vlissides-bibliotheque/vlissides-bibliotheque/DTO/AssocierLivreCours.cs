using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.Validation;
using vlissides_bibliotheque.ViewModels;

namespace vlissides_bibliotheque.DTO
{
    public class AssocierLivreCours : CreationLivreVM
    {
        public List<int> Cours { get; set; }
        public List<int> Auteurs { get; set; }
        public int Id { get; set; }
        public string DateFormater { get; set; }
    }
}
