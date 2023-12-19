namespace vlissides_bibliotheque.Commun
{
    public static class Constante
    {
        //Configuration
        public const string FICHIER_CONFIGURATION_PRINCIPAL = "appsettings.json";
        public const string PROPRIETE_STRIPE = "Stripe";
        public const string PROPRIETE_STRIPE_CLE_API_PUBLIQUE = "PublicApiKey";
        public const string PROPRIETE_STRIPE_CLE_API_PRIVEE = "PrivateApiKey";

        //ConstanteDao
        public const int QUANTITE_PAR_PAGE = 20;
        public const int QUANTITE_SUGGESTIONS = 10;
        public const int PAGE_PAR_DEFAULT = 0;

        //Email
        public const string EMAIL_ADMIN = "AdminAleatoire@CollegeConnaissanceAleatoire.qc.ca";

        //Roles
        public const string Admin = "Admin";
        public const string Utilisateur = "Utilisateur";
        public const string Etudiant = "Etudiant";

        //Taxe
        public const decimal TPS = 0.05m;
        public const decimal TVQ = 0.09975m;

    }
}
