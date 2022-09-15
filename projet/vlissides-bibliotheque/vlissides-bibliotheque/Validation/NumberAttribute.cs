using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.Validation
{
    public class NumberAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string nbCurrent = (string)value;

            int nb;

            try {
                nb = Convert.ToInt32(nbCurrent);
            } catch { 
                return new ValidationResult(ErrorMessage = "Veuillez entrer un nombre.");
            }

            if(nb < 0) {
                return new ValidationResult(ErrorMessage = "Veuillez entrer un nombre positif.");
            }

            return ValidationResult.Success;
        }
    }
}
