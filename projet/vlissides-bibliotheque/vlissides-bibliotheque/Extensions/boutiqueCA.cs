using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using vlissides_bibliotheque.Constantes;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.ViewModels;
using vlissides_bibliotheque.Enums;
using System.Globalization;

namespace vlissides_bibliotheque
{
    public static class boutiqueCA
    {
        public static List<Professeur> CreerProfesseurs()
        {
            List<Professeur> listProfesseur = new();

            Professeur professeur = new()
            {
                ProfesseurId = 0,
                NumeroProfesseur = 1001,
                Prenom = "Marie-Paule",
                Nom = "Demers"
            };
            listProfesseur.Add(professeur);

            Professeur professeur1 = new()
            {
                ProfesseurId = 0,
                NumeroProfesseur = 1200,
                Prenom = "Cynthia",
                Nom = "Langevin"
            };
            listProfesseur.Add(professeur1);

            Professeur professeur2 = new()
            {
                ProfesseurId = 0,
                NumeroProfesseur = 1003,
                Prenom = "Chantale",
                Nom = "Sylvestre"
            };
            listProfesseur.Add(professeur2);

            Professeur professeur3 = new()
            {
                ProfesseurId = 0,
                NumeroProfesseur = 1250,
                Prenom = "Dominique",
                Nom = "Demers"
            };
            listProfesseur.Add(professeur3);

            Professeur professeur4 = new()
            {
                ProfesseurId = 0,
                NumeroProfesseur = 1006,
                Prenom = "Carole",
                Nom = "Lemarin"
            };
            listProfesseur.Add(professeur4);

            Professeur professeur5 = new()
            {
                ProfesseurId = 0,
                NumeroProfesseur = 1452,
                Prenom = "Doris",
                Nom = "Vermette"
            };
            listProfesseur.Add(professeur5);

            Professeur professeur6 = new()
            {
                ProfesseurId = 0,
                NumeroProfesseur = 1234,
                Prenom = "Maxime",
                Nom = "Beaudry"
            };
            listProfesseur.Add(professeur6);

            Professeur professeur7 = new()
            {
                ProfesseurId = 0,
                NumeroProfesseur = 1860,
                Prenom = "Jean Jacques",
                Nom = "Lesage"
            };
            listProfesseur.Add(professeur7);

            Professeur professeur8 = new()
            {
                ProfesseurId = 0,
                NumeroProfesseur = 1744,
                Prenom = "Maurice",
                Nom = "Tremblay"
            };
            listProfesseur.Add(professeur8);

            Professeur professeur9 = new()
            {
                ProfesseurId = 0,
                NumeroProfesseur = 1666,
                Prenom = "Christophe",
                Nom = "Lemay"
            };
            listProfesseur.Add(professeur9);

            Professeur professeur10 = new()
            {
                ProfesseurId = 0,
                NumeroProfesseur = 1555,
                Prenom = "Sylvie",
                Nom = "Hébert"
            };

            Professeur professeur11 = new()
            {
                ProfesseurId = 0,
                NumeroProfesseur = 1688,
                Prenom = "Marie-Eve",
                Nom = "Leclerc"
            };
            listProfesseur.Add(professeur11);

            Professeur professeur12 = new()
            {
                ProfesseurId = 0,
                NumeroProfesseur = 1988,
                Prenom = "Johanne",
                Nom = "Morin"
            };
            listProfesseur.Add(professeur12);

            Professeur professeur13 = new()
            {
                ProfesseurId = 0,
                NumeroProfesseur = 1222,
                Prenom = "Paul",
                Nom = "Sarrazin"
            };
            listProfesseur.Add(professeur13);

            Professeur professeur14 = new()
            {
                ProfesseurId = 0,
                NumeroProfesseur = 1333,
                Prenom = "Yvan",
                Nom = "Côté"
            };
            listProfesseur.Add(professeur14);

            Professeur professeur15 = new()
            {
                ProfesseurId = 0,
                NumeroProfesseur = 1444,
                Prenom = "Ernest",
                Nom = "Stallone"
            };
            listProfesseur.Add(professeur15);

            return listProfesseur;

        }

        public static List<ProgrammeEtude> CreerProgrammeEtude()
        {
            List<ProgrammeEtude> listProgrammeEtude = new();

            ProgrammeEtude programmeEtude = new()
            {
                ProgrammeEtudeId = 0,
                Code = "414",
                Nom = "Techniques de tourisme"
            };
            listProgrammeEtude.Add(programmeEtude);

            ProgrammeEtude programmeEtude1 = new()
            {
                ProgrammeEtudeId = 0,
                Code = "201",
                Nom = "Sciences de la Nature"
            };
            listProgrammeEtude.Add(programmeEtude1);

            ProgrammeEtude programmeEtude2 = new()
            {
                ProgrammeEtudeId= 0,
                Code = "351",
                Nom = "Techniques d’éducation spécialisée"

            };
            listProgrammeEtude.Add(programmeEtude2);

            ProgrammeEtude programmeEtude3 = new() 
            {
                ProgrammeEtudeId = 0,
                Code = "241",
                Nom= "Techniques de génie mécanique"
            };
            listProgrammeEtude.Add(programmeEtude3);

            return listProgrammeEtude;
        }
    }
}

