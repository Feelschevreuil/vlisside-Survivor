using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.Validation
{
    public class ImageAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || !value.ToString().StartsWith("data"))
            {
                return new ValidationResult(ErrorMessage = "Veuillez choisir une image.");
            }

            string imageBase64 =  value.ToString();
            if(imageBase64.StartsWith("data:image/png;") || imageBase64.StartsWith("data:image/jpeg;") || imageBase64.StartsWith("data:image/jpg;"))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage = "Veuillez choisir une image d'un format valide");

        }
    }
}
