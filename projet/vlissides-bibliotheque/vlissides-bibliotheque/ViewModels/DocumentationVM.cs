using System.Collections.Generic;

namespace vlissides_bibliotheque.ViewModels
{
    /// <summary>
    /// Classe <c>DocumentationVM</c> offre de la documentation sur 
    /// les fonctionnalités accessible du site web.
    /// </summary>
    public class DocumentationVM
    {
        public bool IsAdminReserved { get; set; } = false;
        public string Titre { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public List<SectionDocumentationVM> Sections { get; set; }
    }
}
