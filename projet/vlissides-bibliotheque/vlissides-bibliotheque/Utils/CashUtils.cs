namespace vlissides_bibliotheque.Utils
{

    /// <summary>
    /// </summary>
    public static class CashUtils
    {

        /// <summary>
        /// Transforme un montant d'argent en montant affichable.
        /// </summary>
        /// <param name="montant">Montent d'argent à formater.</param>
        /// <returns>Le montant d'argent affichable.</returns>
        public static string FormatToCurrency(double montant)
        {

            string montantFormate;
            int montantTronque;

            montantTronque = (int)(montant * 100);
            montantFormate = montantTronque.ToString();
            montantFormate = montantTronque != 0 ? montantFormate.Insert(montantFormate.Count() - 2, ".") : "0.00";

            return montantFormate + " $";
        }

        /// <summary>
        /// Calcule les taxes sur le produit
        /// </summary>
        /// <param name="montant">Montant à convertir.</param>
        /// <param name="tps">TPS à appliquer.</param>
        /// <param name="tvq">TVQ à appliquer.</param>
        /// <returns>Le montant avec les taxes calculés</returns>
        public static double CalculerTaxes(double montant, decimal tps, decimal tvq)
        {

            if(tvq > 0)
            {

                montant = montant * decimal.ToDouble(1 + tvq);
            }

            if(tps > 0)
            {

                montant = montant * decimal.ToDouble(1 + tps);
            }

            return montant;
        }
    }
}
