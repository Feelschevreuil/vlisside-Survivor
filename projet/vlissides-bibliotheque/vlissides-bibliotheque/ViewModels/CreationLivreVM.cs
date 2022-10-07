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
		[DataType(DataType.Date)]
		public DateTime DatePublication { get; set; } = DateTime.Now;

		[DisplayName("Usagé")]
		public double PrixUsage { get; set; }

		[DisplayName("Numérique")]
		public double PrixNumerqiue { get; set; }

		[DisplayName("Neuf")]
		public double PrixNeuf { get; set; }

		[DisplayName("Quantité")]
		public int? QuantiteUsagee { get; set; }

		[DisplayName("Vendable")]
		public bool PossedeNumerique { get; set; } = false;

		[DisplayName("Vendable")]
		public bool PossedeNeuf { get; set; } = false;

		public bool Obligatoire { get; set; } = false;

		[Isbn]
		public string ISBN { get; set; }

		public string Auteur { get; set; }

		[Required(ErrorMessage = "Le champ Auteur doit être rempli")]
		public List<SelectListItem> Auteurs { get; set; }

		public string Cours { get; set; }
		[Required(ErrorMessage = "Le champ Cours doit être rempli")]
		public List<SelectListItem> ListeCours { get; set; }

		[DisplayName("Maison d'édition")]
		public string MaisonDedition { get; set; }
		[Required(ErrorMessage = "Le champ Maison d'édition doit être rempli")]
		public List<SelectListItem> MaisonsDeditions { get; set; }
	}

}

