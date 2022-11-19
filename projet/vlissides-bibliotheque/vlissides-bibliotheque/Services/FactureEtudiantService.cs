using vlissides_bibliotheque.Models;

namespace vlissides_bibliotheque.Services
{

    /// <summary>
    /// Clsase <c>FactureEtudiantService</c> qui ajoute des Services pour <c>FactureEtudiant</c>.
    /// </summary>
    public class FactureEtudiantService
    {


        /// <summary>
        /// Compare deux factures etudiants et regarde si les propriétés diffèrent.
        /// Tenir en compte que ni l'id ni l'étudiant sont comparés.
        /// </summary>
        /// <param name="idFactureEtudiantReference">Objet de référence.</param>
        /// <param name="factureEtudiantModifie">Objet modifié à comparer.</param>
        public static bool EstDifferentDe
        (
            FactureEtudiant factureEtudiantReference, 
            FactureEtudiant factureEtudiantModifie
        )
        {
            
            if
            (
                !string
                    .Equals
                    (
                        factureEtudiantReference.AdresseLivraison, 
                        factureEtudiantModifie.AdresseLivraison, 
                        StringComparison.OrdinalIgnoreCase
                    )
            )
            {

                return true;
            }
            else if(!factureEtudiantReference.DateFacturation.Equals(factureEtudiantModifie.DateFacturation))
            {

                return true;
            }
            else if(factureEtudiantReference.Tps != factureEtudiantModifie.Tps)
            {

                return true;
            }
            else if(factureEtudiantReference.Tvq != factureEtudiantModifie.Tvq)
            {

                return true;
            }

            return false;
        }

        /// <summary>
        /// Applique la ou les différences d'un objet à un autre.
        /// </summary>
        /// <param name="factureEtudiantAMettreAJour">Objet à mettre à jour.</param>
        /// <param name="factureEtudiantModifie">Objet avec les modifications.</param>
        public static FactureEtudiant MettreAJourProprietes
        (
            FactureEtudiant factureEtudiantAMettreAJour, 
            FactureEtudiant factureEtudiantModifie
        )
        {
            
            if
            (
                !string
                    .Equals
                    (
                        factureEtudiantAMettreAJour.AdresseLivraison, 
                        factureEtudiantModifie.AdresseLivraison, 
                        StringComparison.OrdinalIgnoreCase
                    )
            )
            {

                factureEtudiantAMettreAJour.AdresseLivraison = factureEtudiantModifie.AdresseLivraison;
            }
            if(!factureEtudiantAMettreAJour.DateFacturation.Equals(factureEtudiantModifie.DateFacturation))
            {

                factureEtudiantAMettreAJour.DateFacturation = factureEtudiantModifie.DateFacturation;
            }
            if(factureEtudiantAMettreAJour.Tps != factureEtudiantModifie.Tps)
            {

                factureEtudiantAMettreAJour.Tps = factureEtudiantModifie.Tps;
            }
            if(factureEtudiantAMettreAJour.Tvq != factureEtudiantModifie.Tvq)
            {

                factureEtudiantAMettreAJour.Tvq = factureEtudiantModifie.Tvq;
            }

            return factureEtudiantAMettreAJour;
        }
    }
}
