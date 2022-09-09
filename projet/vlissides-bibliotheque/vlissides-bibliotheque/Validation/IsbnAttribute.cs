using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.Validation
{
    public class IsbnAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string ISBN = (string)value;

            if(ISBN.Length < 10 || ISBN.Length > 13) 
            {
                return new ValidationResult(ErrorMessage = $"Le nombre de charactères de l'ISBN doit être entre 10 et 13.");
            }

            return ValidationResult.Success;
        }
    }
}
