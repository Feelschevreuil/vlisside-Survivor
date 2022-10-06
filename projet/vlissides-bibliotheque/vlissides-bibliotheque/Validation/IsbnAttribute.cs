using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.Validation
{
    /// <summary>
    /// Classe <c>IsbnAttribute</c> hérite de <c>ValidationAttribute</c> et génère un data annotation.
    /// </summary>
    public class IsbnAttribute : ValidationAttribute
	{
        /// <summary>
        /// Retourne un résultat de validation (une erreur ou un succès) par rapport à la taille du isbn.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns>Un <c>ValidationResult</c></returns>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			string ISBN = (string)value;

			if(ISBN != null && (ISBN.Length == 10 || ISBN.Length == 13)) 
			{
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage = $"Le nombre de charactères de l'ISBN doit être entre 10 et 13.");
		}
	}
}
