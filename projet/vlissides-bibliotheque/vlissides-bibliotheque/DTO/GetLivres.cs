//https://json2csharp.com/
namespace vlissides_bibliotheque.DTO
{
    public class Neufs
    {
        public int LivreId { get; set; }
        public int Quantite { get; set; }
    }

    public class Numeriques
    {
        public int LivreId { get; set; }
        public int Quantite { get; set; }
    }

    public class GetLivres
    {
        public List<Neufs> Neufs { get; set; }
        public List<Numeriques> Numeriques { get; set; }
        public List<Usages> Usages { get; set; }
    }

    public class Usages
    {
        public int LivreId { get; set; }
        public int Quantite { get; set; }
    }
}