using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.Utils
{

    /// <summary>
    /// Classe <c>CollectionUtils</c> contient les utilitées reliées au temps.
    /// </summary>
    public class TimeUtils
    {

        /// <summary>
        /// Transforme les minutes en milisecondes.
        /// </summary>
        /// <param name="minutes">Les minutes à convertir.</param>
        /// <returns>Les minutes désirées en milisecondes.</returns>
        public static int MinutesEnMilisecondes(int minutes)
        {

            int milisecondes;

            milisecondes = (minutes * 60) * 1000;

            return milisecondes;
        }
    }
}
