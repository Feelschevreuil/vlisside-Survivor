using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace vlissides_bibliotheque.Validation
{
    /// <summary>
    /// Classe <c>CodePostalAttribute</c> hérite de <c>ValidationAttribute</c> et génère un data annotation.
    /// </summary>
    public class CodePostalAttribute : ValidationAttribute
	{
        /// <summary>
        /// Retourne un résultat de validation (une erreur ou un succès) par rapport à la composition
        /// du code postal reçu en string.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns>Un <c>ValidationResult</c></returns>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			string CodePostal = (string)value;

            // doit être d'une taille de 6 caractères
            if (CodePostal == null || CodePostal.Length != 6) {
                return new ValidationResult(ErrorMessage = $"Le code postal doit contenir 6 caractères.");
            }

            // nombre de lettres de A à Z dans la string
            int NbLettres = Regex.Matches(CodePostal, @"[a-zA-Z]").Count;
            // doit contenir 3 lettres
            if (NbLettres != 3) {
                return new ValidationResult(ErrorMessage = $"Le code postal doit contenir 3 lettres bien positionnées selon l'exemple suivant (où * est un chiffre) : A*A*A*.");
            }

            // valider que la position 1, 3 et 5 sont des int (index départ == 0)
            bool position1IsInt = Int32.TryParse(CodePostal[1].ToString(), out int valueInt);
            bool position3IsInt = Int32.TryParse(CodePostal[3].ToString(), out int valueInt2);
            bool position5IsInt = Int32.TryParse(CodePostal[5].ToString(), out int valueInt3);

            if (!(position1IsInt && position3IsInt && position5IsInt))
			{
                return new ValidationResult(ErrorMessage = $"Le code postal doit contenir 3 chiffres bien positionnés selon l'exemple suivant (où * est une lettre) : *0*0*0.");
            }

            return ValidationResult.Success;
        }
	}
}
