

using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.Models
{
    /// <summary>
    /// Classe <c>Province</c> instancie une nouvelle province.
    /// </summary>
    public class Province
    {
        [Required]
        public int ProvinceId { get; set; }

        [Required]
        public string Nom { get; set; }
    }
}
