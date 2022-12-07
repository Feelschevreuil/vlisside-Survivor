using Microsoft.AspNetCore.Identity;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.ViewModels;
/// Source:  https://exceptionnotfound.net/using-conditional-csharp-linq-clauses-to-make-a-multiple-input-search-engine/
namespace vlissides_bibliotheque.Extentions
{
    public static class CsvExtention
    {
        public static IEnumerable<CsvEtudiantVM> CsvEnVm(this IEnumerable<string> source)
        {
            foreach (var line in source)
            {
                var columns = line.Split(';');
                yield return new CsvEtudiantVM
                {
                    Matricule = columns[0],
                    Courriel = columns[1],
                    MotDePasse = columns[2],
                    Prenom = columns[3],
                    Nom = columns[4],
                    Adresse = columns[5],
                    ProgrammeEtude = columns[6],
                };
            }
        }

        
    }
}
