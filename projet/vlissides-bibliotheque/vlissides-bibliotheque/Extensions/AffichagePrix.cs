using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using vlissides_bibliotheque.Constantes;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.ViewModels;
using vlissides_bibliotheque.Enums;

namespace vlissides_bibliotheque
{
    public static class AffichagePrix
    {
        public static string GetPrixNeufEnDecimal( TuileLivreBibliotequeVM Model)
        {
            double prixInitial = 0;
            string Prixdecimal = "";
            PrixEtatLivre getEntrerPrix = Model.prixEtatLivre.Find(x=>x.EtatLivre == EtatLivreEnum.NEUF);

            if (getEntrerPrix != null)
            {
                prixInitial = getEntrerPrix.Prix;
                prixInitial = Math.Floor(100 * prixInitial) / 100;
                if (prixInitial == (int)prixInitial)
                {
                    Prixdecimal = ",00";
                }
                return prixInitial + Prixdecimal;
            }
            return "";
        }

        public static string GetPrixNumeriqueEnDecimal(TuileLivreBibliotequeVM Model)
        {
            double prixInitial = 0;
            string Prixdecimal = "";
            PrixEtatLivre getEntrerPrix = Model.prixEtatLivre.Find(x => x.EtatLivre == EtatLivreEnum.NUMERIQUE);

            if (getEntrerPrix != null)
            {
                prixInitial = getEntrerPrix.Prix;
                prixInitial = Math.Floor(100 * prixInitial) / 100;
                if (prixInitial == (int)prixInitial)
                {
                    Prixdecimal = ",00";
                }
                return prixInitial + Prixdecimal;
            }
            return "";
        }

        public static string GetPrixUsageEnDecimal(TuileLivreBibliotequeVM Model)
        {
            double prixInitial = 0;
            string Prixdecimal = "";
            PrixEtatLivre getEntrerPrix = Model.prixEtatLivre.Find(x => x.EtatLivre == EtatLivreEnum.USAGE);

            if (getEntrerPrix != null)
            {
                prixInitial = getEntrerPrix.Prix;
                prixInitial = Math.Floor(100 * prixInitial) / 100;
                if (prixInitial == (int)prixInitial)
                {
                    Prixdecimal = ",00";
                }
                return prixInitial + Prixdecimal;
            }
            return "";
        }

        public static string GetPremierPrix (TuileLivreBibliotequeVM Model)
        {
            double prixInitial = 0;
            string Prixdecimal = "";
            List<PrixEtatLivre> prixEtatLivres = new();

            if (Model.prixEtatLivre != null && Model.prixEtatLivre.Count() > 0)
            {
                foreach(PrixEtatLivre etatLivre in Model.prixEtatLivre)
                {
                    if(etatLivre != null)
                    {

                        prixEtatLivres.Add(etatLivre);
                    }
                }

                prixInitial = prixEtatLivres.FirstOrDefault().Prix;
                prixInitial = Math.Floor(100 * prixInitial) / 100;
                if (prixInitial == (int)prixInitial)
                {
                    Prixdecimal = ",00";
                    return prixInitial + Prixdecimal;
                }
                return prixInitial.ToString();
            }
            return "";
        }
    }
}

