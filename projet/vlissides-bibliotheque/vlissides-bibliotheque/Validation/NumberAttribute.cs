using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.Validation
{
    public class NumberAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult(ErrorMessage = "Veuillez entrer un nombre.");
            }


            decimal nbCurrent;

            if (decimal.TryParse(value.ToString(), out nbCurrent))
            {
                if (nbCurrent < 0)
                {
                    return new ValidationResult(ErrorMessage = "Veuillez entrer un nombre positif.");
                }
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage = "Veuillez entrer un nombre.");

        }
    }
}
