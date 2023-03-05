using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.ViewModels;

namespace vlissides_bibliotheque.Services.Interface
{
    public interface IUtilisateur
    {
        Adresse GetAdresse(Etudiant etudiant);
        Etudiant ModelBinding(Etudiant etudiant, Adresse adresse, GestionProfilVM vm);
        Utilisateur ModelBinding(Utilisateur admin, GestionProfilVM vm);
        GestionProfilVM GetEtudiantProfilVM(Etudiant etudiant);
        GestionProfilVM GetAdminProfilVM(Utilisateur admin);
        GestionProfilVM NewGestionProfilVM();
        InscriptionVM NewInscriptionVM();
    }
}
