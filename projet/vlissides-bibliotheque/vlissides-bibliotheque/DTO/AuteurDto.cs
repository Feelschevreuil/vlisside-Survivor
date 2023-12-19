

namespace vlissides_bibliotheque.DTO
{
    public class AuteurDto
    {
        public int AuteurId { get; set; }

        public string Nom { get; set; }

        public string Prenom { get; set; }

        public virtual string NomComplet { get { return Prenom + " " + Nom; } }
    }
}
