using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.Validation
{
    public class NumberAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string nbCurrent = (string)value;

            if(nbCurrent == null) 
            {
                return new ValidationResult(ErrorMessage = "Le champ {0} est requis.");
            }

            return ValidationResult.Success;
        }
    }
}
