namespace vlissides_bibliotheque.ViewModels
{
    /// <summary>
    /// Classe <c>SectionDocumentationVM</c> donne de l'information précise
    /// sur une section de la documentation.
    /// </summary>
    public class SectionDocumentationVM
    {
        public bool IsAdminReserved { get; set; } = false;
        public string Titre { get; set; }
        public string Description { get; set; }
        public string NB { get; set; }
        public string LienImage { get; set; }
    }
}
