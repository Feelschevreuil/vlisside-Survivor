using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.Validation;

namespace vlissides_bibliotheque.ViewModels
{
	public class CreationLivreVM
	{
		[Required(ErrorMessage = "Le champ Titre ne peut être vide")]
		public string Titre { get; set; }

		[Required(ErrorMessage = "Le champ Résumé ne peut être vide")]
		public string Resume { get; set; }

		[Required(ErrorMessage = "Une photo doit être fournie")]
		public string Photo { get; set; }

		[Required(ErrorMessage = "La date de publication est nécéssaire")]
		[DisplayName("Date de publication")]
		public DateTime DatePublication { get; set; }

		[Isbn]
		public string ISBN { get; set; }

		public string Auteur { get; set; }
		public List<SelectListItem> Auteurs { get; set; }

		public string Cours { get; set; }
		public List<SelectListItem> ListeCours { get; set; }

		public string Etat { get; set; }
		public List<SelectListItem> Etats { get; set; }

		public string MaisonDedition { get; set; }
		public List<SelectListItem> MaisonsDeditions { get; set; }
	}

}

