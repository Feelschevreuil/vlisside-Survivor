using Microsoft.EntityFrameworkCore;
using vlissides_bibliotheque.Models;

namespace vlissides_bibliotheque.Data
{

    /// <summary>
    /// Interface <c>IDbContext</c> qui définit les propriétés que les DbContext
    /// doivent avoir.
    /// </summary>
    public interface IDbContext
    {

	DbSet<Adresse> Adresses { get; set; }
	DbSet<Auteur> Auteurs { get; set; }
	DbSet<AuteurLivre> AuteursLivres { get; set; }
	DbSet<CommandeEtudiant> CommandesEtudiants { get; set; }
	DbSet<Commanditaire> Commanditaires { get; set; }
	DbSet<EtatLivre> EtatsLivres { get; set; }
	DbSet<Etudiant> Etudiants { get; set; }
	DbSet<Evaluation> Evaluations { get; set; }
	DbSet<EvaluationLivre> EvaluationsLivres { get; set; }
	DbSet<Evenement> Evenements { get; set; }
	DbSet<LivreBibliotheque> LivresBibliotheque { get; set; }
	DbSet<LivreEtudiant> LivresEtudiants { get; set; }
	DbSet<ProgrammeEtude> ProgrammesEtudes { get; set; }
	DbSet<Utilisateur> Utilisateurs { get; set; }
	DbSet<TypePaiement> TypesPaiement { get; set; }
	DbSet<MaisonEdition> MaisonsEditions { get; set; }
	DbSet<PrixEtatLivre> PrixEtatsLivres { get; set; }
	DbSet<CoursLivre> CoursLivres { get; set; }
	DbSet<Cours> Cours { get; set; }
	DbSet<CoursProfesseur> CoursProfesseurs { get; set; }
	DbSet<Professeur> Professeurs { get; set; }
	DbSet<Province> Provinces { get; set; }
	DbSet<CoursEtudiant> CoursEtudiants { get; set; }
	DbSet<FactureEtudiant> FacturesEtudiants { get; set; }
    }
}
