using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria.Utils.Validation
{
    public class MinimumFiveWordAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            string fieldValue = (string)value;
            if (fieldValue.Trim().Split(" ").Length < 5)
            {
                return new ValidationResult("La descrizione deve contenere almeno 5 parole");
            }
            return ValidationResult.Success;
        }
    }
}