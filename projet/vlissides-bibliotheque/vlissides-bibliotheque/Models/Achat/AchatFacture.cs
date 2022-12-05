using System.ComponentModel.DataAnnotations;
using vlissides_bibliotheque.Models.Achat;
using vlissides_bibliotheque.Enums;

namespace vlissides_bibliotheque.Models.Achat
{

    /// <summary>
    /// Classe <c>AchatFacture</c> contient les informtions pour créer une facture et
    /// générer le modèle de vue.
    /// </summary>
    public class AchatFacture
    {

        public List<CommandeEtudiant> commandesPartielles { get; set; }
    }
}
