using vlissides_bibliotheque.Models;

namespace vlissides_bibliotheque.Services
{

    /// <summary>
    /// Clsase <c>AdresseService</c> qui ajoute des Services pour les <c>Adresse</c>s.
    /// </summary>
    public class AdresseService
    {

        /// <summary>
        /// Compare deux <c>Adresse</c>s et regarde si les propriétés diffèrent.
        /// </summary>
        /// <param name="adresseDeReference"><c>Adresse</c> de référence.</param>
        /// <param name="adresseModifiee"><c>Adresse</c> modifiée à confirmer.</param>
        public static bool EstDifferentDe
        (
            Adresse adresseDeReference,
            Adresse adresseModifiee
        )
        {

            if
            (
                !string
                    .Equals
                    (
                        adresseDeReference.Ville, 
                        adresseModifiee.Ville, 
                        StringComparison.CurrentCultureIgnoreCase
                    )
            )
            {

                return true;
            }
            else if(adresseDeReference.NumeroCivique != adresseModifiee.NumeroCivique)
            {

                return true;
            }
            else if
            (
                !string
                    .Equals
                    (
                        adresseDeReference.Rue, 
                        adresseModifiee.Rue, 
                        StringComparison.CurrentCultureIgnoreCase
                    )
            )
            {

                return true;
            }
            else if
            (
                !string
                    .Equals
                    (
                        adresseDeReference.CodePostal, 
                        adresseModifiee.CodePostal, 
                        StringComparison.CurrentCultureIgnoreCase
                    )
            )
            {

                return true;
            }
            else if(adresseModifiee.ProvinceId != 0 && (adresseDeReference.ProvinceId != adresseModifiee.ProvinceId))
            {

                return true;
            }

            return false;
        }

        /// <summary>
        /// Applique la ou les différences d'un adresse à un autre.
        /// </summary>
        /// <param name="adresseAMettreAJour">Adresse à mettre à jour.</param>
        /// <param name="adresseModifiee">Adresse avec les modifications.</param>
        public static Adresse MettreAJourProprietes
        (
            Adresse adresseAMettreAJour,
            Adresse adresseModifiee
        )
        {

            if
            (
                !string
                    .Equals
                    (
                        adresseAMettreAJour.Ville, 
                        adresseModifiee.Ville, 
                        StringComparison.CurrentCultureIgnoreCase
                    )
            )
            {

                adresseAMettreAJour.Ville = adresseModifiee.Ville;
            }
            else if(adresseAMettreAJour.NumeroCivique != adresseModifiee.NumeroCivique)
            {

                adresseAMettreAJour.NumeroCivique = adresseModifiee.NumeroCivique;
            }
            else if
            (
                !string
                    .Equals
                    (
                        adresseAMettreAJour.Rue, 
                        adresseModifiee.Rue, 
                        StringComparison.CurrentCultureIgnoreCase
                    )
            )
            {

                adresseAMettreAJour.Rue = adresseModifiee.Rue;
            }
            else if
            (
                !string
                    .Equals
                    (
                        adresseAMettreAJour.CodePostal, 
                        adresseModifiee.CodePostal, 
                        StringComparison.CurrentCultureIgnoreCase
                    )
            )
            {

                adresseAMettreAJour.CodePostal = adresseModifiee.CodePostal;
            }
            else if(adresseAMettreAJour.ProvinceId != adresseModifiee.ProvinceId)
            {

                adresseAMettreAJour.ProvinceId = adresseModifiee.ProvinceId;
            }

            return adresseAMettreAJour;
        }
    }
}
