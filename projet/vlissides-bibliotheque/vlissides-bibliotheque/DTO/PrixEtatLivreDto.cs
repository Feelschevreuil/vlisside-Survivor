using vlissides_bibliotheque.Enums;

namespace vlissides_bibliotheque.DTO
{
    public class PrixEtatLivreDto
    {
        public int PrixEtatLivreId { get; set; }

        public EtatLivreEnum EtatLivre { get; set; }

        public int LivreBibliothequeId { get; set; }

        public double Prix { get; set; }

        public int QuantiteUsage { get; set; } = 0;
    }
}
