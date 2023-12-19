//using vlissides_bibliotheque.ViewModels;
//using vlissides_bibliotheque.Models;
//using vlissides_bibliotheque.DTO.Ajax;

//namespace vlissides_bibliotheque.Services
//{

//    /// <summary>
//    /// Clsase <c>AdresseService</c> qui ajoute des Services pour les <c>Adresse</c>s.
//    /// </summary>
//    public class AdresseService
//    {

//        /// <summary>
//        /// Compare deux <c>Adresse</c>s et regarde si les propriétés diffèrent.
//        /// </summary>
//        /// <param name="adresseDeReference"><c>Adresse</c> de référence.</param>
//        /// <param name="adresseModifiee"><c>Adresse</c> modifiée à confirmer.</param>
//        public static bool EstDifferentDe
//        (
//            Adresse adresseDeReference,
//            Adresse adresseModifiee
//        )
//        {

//            if
//            (
//                !string
//                    .Equals
//                    (
//                        adresseDeReference.Ville, 
//                        adresseModifiee.Ville, 
//                        StringComparison.CurrentCultureIgnoreCase
//                    )
//            )
//            {

//                return true;
//            }
//            else if(adresseDeReference.NumeroCivique != adresseModifiee.NumeroCivique)
//            {

//                return true;
//            }
//            else if(adresseDeReference.App != adresseModifiee.App)
//            {

//                return true;
//            }
//            else if
//            (
//                !string
//                    .Equals
//                    (
//                        adresseDeReference.Rue, 
//                        adresseModifiee.Rue, 
//                        StringComparison.CurrentCultureIgnoreCase
//                    )
//            )
//            {

//                return true;
//            }
//            else if
//            (
//                !string
//                    .Equals
//                    (
//                        adresseDeReference.CodePostal, 
//                        adresseModifiee.CodePostal, 
//                        StringComparison.CurrentCultureIgnoreCase
//                    )
//            )
//            {

//                return true;
//            }
//            else if(adresseModifiee.ProvinceId != 0 && (adresseDeReference.ProvinceId != adresseModifiee.ProvinceId))
//            {

//                return true;
//            }

//            return false;
//        }

//        /// <summary>
//        /// Applique la ou les différences d'un adresse à un autre.
//        /// </summary>
//        /// <param name="adresseAMettreAJour">Adresse à mettre à jour.</param>
//        /// <param name="adresseModifiee">Adresse avec les modifications.</param>
//        public static Adresse MettreAJourProprietes
//        (
//            Adresse adresseAMettreAJour,
//            Adresse adresseModifiee
//        )
//        {

//            if
//            (
//                !string
//                    .Equals
//                    (
//                        adresseAMettreAJour.Ville, 
//                        adresseModifiee.Ville, 
//                        StringComparison.CurrentCultureIgnoreCase
//                    )
//            )
//            {

//                adresseAMettreAJour.Ville = adresseModifiee.Ville;
//            }
//            else if(adresseAMettreAJour.NumeroCivique != adresseModifiee.NumeroCivique)
//            {

//                adresseAMettreAJour.NumeroCivique = adresseModifiee.NumeroCivique;
//            }
//            else if(adresseAMettreAJour.App != adresseModifiee.App)
//            {

//                adresseAMettreAJour.App = adresseModifiee.App;
//            }
//            else if
//            (
//                !string
//                    .Equals
//                    (
//                        adresseAMettreAJour.Rue, 
//                        adresseModifiee.Rue, 
//                        StringComparison.CurrentCultureIgnoreCase
//                    )
//            )
//            {

//                adresseAMettreAJour.Rue = adresseModifiee.Rue;
//            }
//            else if
//            (
//                !string
//                    .Equals
//                    (
//                        adresseAMettreAJour.CodePostal, 
//                        adresseModifiee.CodePostal, 
//                        StringComparison.CurrentCultureIgnoreCase
//                    )
//            )
//            {

//                adresseAMettreAJour.CodePostal = adresseModifiee.CodePostal;
//            }
//            else if(adresseAMettreAJour.ProvinceId != adresseModifiee.ProvinceId)
//            {

//                adresseAMettreAJour.ProvinceId = adresseModifiee.ProvinceId;
//            }

//            return adresseAMettreAJour;
//        }

//        /// <summary>
//        /// Mappe les données d'un modèle de vue contenant des informations d'adresse 
//        /// à un objet adresse.
//        /// </summary>
//        /// <param name="achatInformationsLivraisonVM">
//        /// Modèle de vue contenant les informations de l'adresse.
//        /// </param>
//        /// <returns>L'adresse avec les informatoins de la vue partielle.</returns>
//        public static Adresse GetFromInformationsLivraison
//        (
//            AchatInformationsLivraisonDTO achatInformationsLivraisonDTO)
//        {

//            Adresse adresse;

//            adresse = new()
//            {
//                Ville = achatInformationsLivraisonDTO.Ville,
//                NumeroCivique = achatInformationsLivraisonDTO.NumeroCivique,
//                App = achatInformationsLivraisonDTO.App,
//                Rue = achatInformationsLivraisonDTO.Rue,
//                CodePostal = achatInformationsLivraisonDTO.CodePostal
//            };

//            return adresse;
//        }
//    }
//}
