using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using vlissides_bibliotheque.Constantes;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.ViewModels;

namespace vlissides_bibliotheque
{
    public static class AffichagePrix
    {
        public static string GetPrixNeufEnDecimal( TuileLivreBibliotequeVM Model)
        {
            double prixInitial = 0;
            string Prixdecimal = "";
            PrixEtatLivre getEntrerPrix = Model.prixEtatLivre.Find(x=>x.EtatLivre.Nom == NomEtatLivre.NEUF);

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
            PrixEtatLivre getEntrerPrix = Model.prixEtatLivre.Find(x => x.EtatLivre.Nom == NomEtatLivre.DIGITAL);

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
            PrixEtatLivre getEntrerPrix = Model.prixEtatLivre.Find(x => x.EtatLivre.Nom == NomEtatLivre.USAGE);

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

    }
}

