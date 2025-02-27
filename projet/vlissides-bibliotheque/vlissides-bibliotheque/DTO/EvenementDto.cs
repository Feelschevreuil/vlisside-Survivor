using System;

namespace vlissides_bibliotheque.DTO
{
    public class EvenementDto
    {
        public string Nom { get; set; }

        public DateTime Debut { get; set; }

        public DateTime Fin { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public string CommanditaireNom { get; set; }
        
        public string CommanditaireCourriel { get; set; }
        
        public string CommanditaireMessage { get; set; }
    }
}
