using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.Validation;

namespace vlissides_bibliotheque.ViewModels
{
	public class CreationLivreVM
	{
        [Required(ErrorMessage = "Le champ {0} est requis.")]
        public string Titre { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        public string Resume { get; set; }

        
        public string Photo { get; set; }

		[NotMapped]
        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [DisplayName("Photo")]
		public IFormFile fichierImage { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [DisplayName("Date de publication")]
		[DataType(DataType.Date)]
		public DateTime DatePublication { get; set; } = DateTime.Now;

		[Required(ErrorMessage = "Le champ {0} est requis.")]
		[DisplayName("Usagé")]
		public double PrixUsage { get; set; } = 0;

		[Required(ErrorMessage = "Le champ {0} est requis.")]
		[DisplayName("Numérique")]
		public double PrixNumerique { get; set; } = 0;

        [Required(ErrorMessage = "Le champ {0} est requis.")]
		[DisplayName("Neuf")]
		public double PrixNeuf { get; set; } 

		[DisplayName("Quantité")]
		public int? QuantiteUsagee { get; set; }

		[DisplayName("Vendable")]
		public bool PossedeNumerique { get; set; } = false;

		[DisplayName("Vendable")]
		public bool PossedeNeuf { get; set; } = false;

		public bool Obligatoire { get; set; } = false;

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [Isbn]
        [Range (1000000000,9999999999999,ErrorMessage = "Veuillez entrer un nombre.")]
        public string ISBN { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [DisplayName("Auteur")]
        public int? AuteurId { get; set; }

		
		public List<SelectListItem> Auteurs { get; set; }
        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [DisplayName("Cours")]
        public int? CoursId { get; set; }
		
		public List<SelectListItem> ListeCours { get; set; }

		[DisplayName("Maison d'édition")]
        [Required(ErrorMessage = "Le champ {0} est requis.")]
        public int? MaisonDeditionId { get; set; }
		
		public List<SelectListItem> MaisonsDeditions { get; set; }
	}

}

