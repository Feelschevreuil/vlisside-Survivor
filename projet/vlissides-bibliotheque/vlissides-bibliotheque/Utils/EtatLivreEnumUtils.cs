using vlissides_bibliotheque.Enums;

namespace vlissides_bibliotheque.Utils
{

    /// <summary>
    /// </summary>
    public static class EtatLivreEnumUtils
    {

        /// <summary>
        /// Cherche le nom complet d'un état de livre.
        /// </summary>
        /// <param name="etatLivreEnum">État livre à chercher le nom complet.</param>
        /// <returns>Le nom complet de l'état livre désiré.</returns>
        public static string GetNomCompletEtatLivre(EtatLivreEnum etatLivreEnum)
        {

            string nomCompletEtatLivre;

            if(etatLivreEnum == EtatLivreEnum.NEUF)
            {

                nomCompletEtatLivre = "Neuf";
            }
            else if(etatLivreEnum == EtatLivreEnum.NUMERIQUE)
            {

                nomCompletEtatLivre = "Numérique";
            }
            else
            {

                nomCompletEtatLivre = "Usagé";
            }

            return nomCompletEtatLivre;
        }
    }
}
