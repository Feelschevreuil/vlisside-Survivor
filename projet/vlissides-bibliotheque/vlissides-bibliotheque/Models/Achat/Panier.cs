using System.ComponentModel.DataAnnotations;
using vlissides_bibliotheque.Utils;

namespace vlissides_bibliotheque.Models.Achat
{

    /// <summary>
    /// Classe <c>Panier</c> définit ce que l'on reçoit du panier de l'utilisateur.
    /// </summary>
    public class Panier
    {

        public List<LivreDesire> Neufs { get; set; }

        public List<LivreDesire> Numeriques { get; set; }

        public List<LivreDesire> Usages { get; set; }

        /// <summary>
        /// Confirme si le panier est vide.
        /// </summary>
        private bool IsEmpty()
        {

            bool panierVide;

            panierVide = CollectionUtils.CollectionNulleOuVide(Neufs) ||
                CollectionUtils.CollectionNulleOuVide(Neufs) ||
                CollectionUtils.CollectionNulleOuVide(Usages);

            return panierVide;
        }

        /// <summary>
        /// Confirme que les quantiés desirés des livres sont positives.
        /// Une vérification que le panier n'est pas vide est également
        /// effectuée.
        /// </summary>
        public bool QuantitesLivresPositives()
        {

            if(!IsEmpty())
            {

                if(!CollectionUtils.CollectionNulleOuVide(Neufs))
                {

                    if(!LivresDesiresValides(Neufs))
                    {
                        
                        return false;
                    }
                }
                
                if(!CollectionUtils.CollectionNulleOuVide(Numeriques))
                {

                    if(!LivresDesiresValides(Numeriques))
                    {
                        
                        return false;
                    }
                }

                if(!CollectionUtils.CollectionNulleOuVide(Usages))
                {

                    if(!LivresDesiresValides(Usages))
                    {
                        
                        return false;
                    }
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Valide qu'une lise de <c>LivreDesire</c> contient des
        /// données aptes à traiter.
        /// </summary>
        public bool LivresDesiresValides(List<LivreDesire> livresDesires)
        {

            foreach(LivreDesire livreDesire in livresDesires)
            {

                if(!livreDesire.EstValide())
                {

                    return false;
                }
            }

            return true;
        }
    }
}
