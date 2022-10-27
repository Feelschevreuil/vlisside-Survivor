using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.Validation
{
    public class NumberAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value == null)
            {
                return new ValidationResult(ErrorMessage = "Veuillez entrer un nombre.");
            }
            var nbCurrent = value.ToString();
            if(string.IsNullOrEmpty(nbCurrent)) 
            {
                return new ValidationResult(ErrorMessage = "Veuillez entrer un nombre.");
            }
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
