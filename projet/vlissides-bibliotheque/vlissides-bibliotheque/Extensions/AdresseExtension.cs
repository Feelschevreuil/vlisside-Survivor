using vlissides_bibliotheque.Models;

namespace vlissides_bibliotheque
{

    /// <summary>
    /// Classe <c>StringExtensions</c> contenant les extensions nécessaires pour la manipulation des adresses.
    /// </summary>
    public static class AdresseExtension
    {

        /// <summary>
        /// Copie les informations d'une adresse en créant un nouvel objet.
        /// </summary>
        public static Adresse CopierDonnees(this Adresse adresseOriginale)
        {

            Adresse adresse;

            adresse = new()
            {
                App = adresseOriginale.App,
                CodePostal = adresseOriginale.CodePostal,
                NumeroCivique = adresseOriginale.NumeroCivique,
                ProvinceId = adresseOriginale.ProvinceId,
                Rue = adresseOriginale.Rue,
                Ville = adresseOriginale.Ville
            };

            return adresse;
        }
    }
}
