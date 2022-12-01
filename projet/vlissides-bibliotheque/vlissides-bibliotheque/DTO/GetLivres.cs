//https://json2csharp.com/
namespace vlissides_bibliotheque.DTO
{
    public class Neuf
    {
        public int LivreId { get; set; }
        public int Quantite { get; set; }
    }

    public class Numerique
    {
        public int LivreId { get; set; }
        public int Quantite { get; set; }
    }

    public class GetLivres
    {
        public List<Neuf> Neuf { get; set; }
        public List<Numerique> Numerique { get; set; }
        public List<Usage> Usage { get; set; }
    }

    public class Usage
    {
        public int LivreId { get; set; }
        public int Quantite { get; set; }
    }
}