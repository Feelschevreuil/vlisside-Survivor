namespace vlissides_bibliotheque.Services
{

    /// <summary>
    /// Classe <c>ConfigurationService</c> qui ajoute les Servics pour <c>IConfigurationRoot</c>.
    /// </summary>
    public class ConfigurationService
    {

        private IConfigurationRoot _configuration;

        /// <summary>
        /// Constructeur qui crée un configuration root selon le nom du fichier de configuration.
        /// </summary>
        public ConfigurationService(string nomFichierConfiguration)
        {

            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(Directory.GetCurrentDirectory() + "/" + nomFichierConfiguration)
                .Build();
        }

        /// <summary>
        /// Cherche la valeur d'une propriété dans une section désirée.
        /// </summary>
        public string GetProprieteDeSection(string section, string propriete)
        {

            string valeur;

            valeur = _configuration.GetSection(section)[propriete] ?? 
                throw new InvalidOperationException("propriété " + propriete + " de la section " + section + " non trouvée dans le fichier de configuation!");

            return valeur;
        }
    }
}
