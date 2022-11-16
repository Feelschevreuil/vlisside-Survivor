using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.Models
{
    /// <summary>
    /// Classe <c>Auteur</c> définit la table auteur dans la base de données.
    /// </summary>
    public class Auteur
    {
        [Required]
        public int AuteurId { get; set; }

        [Required]
        [StringLength(40)]
        public string Nom { get; set; }

        [Required]
        [StringLength(40)]
        public string Prenom { get; set; }

	/// <summary>
	/// Concatenate le prénom de l'auteur et son nom.
	/// </summary>
	public string GetNomComplet()
	{

	    string nomComplet;

	    nomComplet = Prenom + " " + Nom;

	    return nomComplet;
	}
    }
}
