using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using vlissides_bibliotheque.Models;

namespace vlissides_bibliotheque.ViewModels
{
    public class FactureCommandeVM
    {
        public List<CommandeEtudiant> CommandesEtudiant { get; set; }
    }
}
