//https://json2csharp.com/
namespace vlissides_bibliotheque.DTO
{
    public class GetLivres
    {
        public List<int> Neuf { get; set; }
        public List<int> Usage { get; set; }
        public List<int> Numerique { get; set; }
    }
}