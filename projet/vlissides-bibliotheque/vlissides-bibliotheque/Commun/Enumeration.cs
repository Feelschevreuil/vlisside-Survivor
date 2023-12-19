namespace vlissides_bibliotheque.Commun
{
    public static class Enumeration
    {
        public enum EtatLivreEnum
        {
            NEUF = 1,
            NUMERIQUE = 2,
            USAGE = 3
        }

        public enum StatutCommandeEnum
        {
            CORRECT = 1,
            MANQUE_INVENTAIRE = 2,
            QUANTITEE_CORRIGE_SELON_DISPONIBILITE = 3,
            INEXISTANT = 4
        }

        public enum StatutFactureEnum
        {
            ATTENTE_PAIEMENT = 1,
            TRANSIT = 2,
            LIVREE = 3,
            ANNULEE = 4,
            ANNULEE_NON_PAYE_DELAIS = 5
        }
    }
}
