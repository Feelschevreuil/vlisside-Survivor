using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using vlissides_bibliotheque.Constantes;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.ViewModels;
using vlissides_bibliotheque.Enums;
using System.Globalization;

namespace vlissides_bibliotheque
{
    public static class AffichagePrix
    {
        public static double GetPrixEnDouble (string prix)
        {
            prix = prix.Replace(".", ",");
            double prixEnDouble = Convert.ToDouble(prix);
            return prixEnDouble;
        }

        public static string GetPrixNeufEnDecimal( TuileLivreBibliotequeVM Model)
        {
            double prixInitial = 0;
            string Prixdecimal = "";
            PrixEtatLivre? getEntrerPrix = Model.prixEtatLivre.FirstOrDefault(x=>x.EtatLivre == EtatLivreEnum.NEUF);

            if (getEntrerPrix != null)
            {
                prixInitial = getEntrerPrix.Prix;
                prixInitial = Math.Floor(100 * prixInitial) / 100;
                if (prixInitial == (int)prixInitial)
                {
                    Prixdecimal = ".00";
                }
                return prixInitial + Prixdecimal;
            }
            return "";
        }

        public static string GetPrixNumeriqueEnDecimal(TuileLivreBibliotequeVM Model)
        {
            double prixInitial = 0;
            string Prixdecimal = "";
            PrixEtatLivre? getEntrerPrix = Model.prixEtatLivre.FirstOrDefault(x => x.EtatLivre == EtatLivreEnum.NUMERIQUE);

            if (getEntrerPrix != null)
            {
                prixInitial = getEntrerPrix.Prix;
                prixInitial = Math.Floor(100 * prixInitial) / 100;
                if (prixInitial == (int)prixInitial)
                {
                    Prixdecimal = ".00";
                }
                return prixInitial + Prixdecimal;
            }
            return "";
        }

        public static string GetPrixUsageEnDecimal(TuileLivreBibliotequeVM Model)
        {
            double prixInitial = 0;
            string Prixdecimal = "";
            PrixEtatLivre? getEntrerPrix = Model.prixEtatLivre.FirstOrDefault(x => x.EtatLivre == EtatLivreEnum.USAGE);

            if (getEntrerPrix != null)
            {
                prixInitial = getEntrerPrix.Prix;
                prixInitial = Math.Floor(100 * prixInitial) / 100;
                if (prixInitial == (int)prixInitial)
                {
                    Prixdecimal = ".00";
                }
                return prixInitial + Prixdecimal;
            }
            return "";
        }

        public static PrixEtatLivre? GetPremierPrix (TuileLivreBibliotequeVM Model)
        {
            double prixInitial = 0;
            string prixAvecDecimal = "";
            PrixEtatLivre prixEtat = new();
            List<PrixEtatLivre> prixEtatLivres = new();

            if (Model.prixEtatLivre != null && Model.prixEtatLivre.Count() > 0)
            {
                foreach (PrixEtatLivre etatLivre in Model.prixEtatLivre)
                {
                    if (etatLivre != null)
                    {

                        prixEtatLivres.Add(etatLivre);
                    }
                }

                 if(prixEtatLivres.Find(x=>x.EtatLivre == EtatLivreEnum.NEUF) != null)
                {
                   prixInitial = prixEtatLivres.Single(x => x.EtatLivre == EtatLivreEnum.NEUF).Prix;
                    prixEtat.EtatLivre = EtatLivreEnum.NEUF;
                }
                else if(prixEtatLivres.Find(x => x.EtatLivre == EtatLivreEnum.NUMERIQUE) != null)
                {
                    prixInitial = prixEtatLivres.Single(x => x.EtatLivre == EtatLivreEnum.NUMERIQUE).Prix;
                    prixEtat.EtatLivre = EtatLivreEnum.NUMERIQUE;

                }
                else if(prixEtatLivres.Find(x => x.EtatLivre == EtatLivreEnum.USAGE) != null)
                {
                    prixInitial = prixEtatLivres.Single(x => x.EtatLivre == EtatLivreEnum.USAGE).Prix;
                    prixEtat.EtatLivre = EtatLivreEnum.USAGE;

                }

                prixInitial = Math.Floor(100 * prixInitial) / 100;
                if (prixInitial == (int)prixInitial)
                {
                    prixAvecDecimal = prixInitial.ToString() + ".00";
                    prixEtat.Prix = float.Parse(prixAvecDecimal, CultureInfo.InvariantCulture);

                    return prixEtat;
                }
                prixEtat.Prix = prixInitial;
                prixEtat.EtatLivre = prixEtatLivres.First().EtatLivre;
                return prixEtat;
            }
            return null;
        }
     
        public static string AjouteDecimal(double prixInitial)
        {
            string prixAvecDecimal = "";
            prixInitial = Math.Floor(100 * prixInitial) / 100;
            if (prixInitial == (int)prixInitial)
            {
                prixAvecDecimal = prixInitial.ToString() + ".00";
                return prixAvecDecimal;
            }
            return prixInitial.ToString()+".00"; 
        }
    }
}

