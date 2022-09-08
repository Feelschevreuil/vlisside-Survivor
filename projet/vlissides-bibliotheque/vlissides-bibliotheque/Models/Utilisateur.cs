using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vlissides_bibliotheque.Models
{
    public class Utilisateur : IdentityUser
    {
        [Required]
        [StringLength(40)]
        public string Nom { get; set; }

        [Required]
        [StringLength(40)]
        public string Prenom { get; set; }
    }
}
