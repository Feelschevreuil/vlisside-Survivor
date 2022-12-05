using System.ComponentModel.DataAnnotations;
using vlissides_bibliotheque.Utils;

namespace vlissides_bibliotheque.Models.Achat
{

    /// <summary>
    /// Classe <c>LivreDesire</c> représente que que le client veut 
    /// pour chauqe livre dans son panier.
    /// </summary>
    public class LivreDesire
    {

        public int LivreId { get; set; }
        public int Quantite { get; set; }

        /// <summary>
        /// Valide que le <c>LivreDesire</c> possède des données aptes à traiter.
        /// </summary>
        public bool EstValide()
        {

            if(LivreId > 0 && Quantite > 0)
            {

                return true;
            }

            return false;
        }
    }
}
