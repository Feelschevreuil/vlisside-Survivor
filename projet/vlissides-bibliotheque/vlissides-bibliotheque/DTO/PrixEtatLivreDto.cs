
namespace vlissides_bibliotheque.DTO
{
    public class PrixEtatLivreDto
    {
        public decimal PrixNeuf { get; set; }
        
        public decimal PrixNumerique { get; set; }
        
        public decimal PrixUsager { get; set; }

        public int QuantiteUsage { get; set; } = 0;
    }
}
