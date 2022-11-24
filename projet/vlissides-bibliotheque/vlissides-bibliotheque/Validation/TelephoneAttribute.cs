using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.Validation
{
    /// <summary>
    /// Classe <c>TelephoneAttribute</c> hérite de <c>ValidationAttribute</c> et génère un data annotation.
    /// </summary>
    public class TelephoneAttribute : ValidationAttribute
	{
        /// <summary>
        /// Retourne un résultat de validation (une erreur ou un succès) par rapport à la taille 
        /// du numéro de téléphone et au type voulu (int).
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns>Un <c>ValidationResult</c></returns>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			string Telephone = (string)value;

            bool isInt = Int32.TryParse(Telephone, out int valueInt);

            if (Telephone != null && Telephone.Length == 10 && isInt) 
			{
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage = $"Le téléphone doit contenir 10 chiffres.");
		}
	}
}
