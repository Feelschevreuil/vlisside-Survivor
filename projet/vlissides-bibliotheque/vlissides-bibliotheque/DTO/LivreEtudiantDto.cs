using System;

namespace vlissides_bibliotheque.DTO
{
    public class LivreEtudiantDto
    {
        public int LivreId { get; set; }
        public string EtudiantEmail { get; set; }
        public string EtudiantId { get; set; }
        public string Titre { get; set; }
        public string Isbn { get; set; }
        public string Resume { get; set; }
        public string Photo { get; set; }
        public DateTime DatePublication { get; set; }
        public string MaisonEdition { get; set; }
        public string Auteur { get; set; }
        public decimal? Prix { get; set; }
    }
}
