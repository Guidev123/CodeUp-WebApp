using System.ComponentModel.DataAnnotations;

namespace CodeUp.WebApp.ViewModels.Validations;

public class AgeValidator : ValidationAttribute
{
    private readonly int _minAge;

    public AgeValidator(int minAge)
    {
        _minAge = minAge;
        ErrorMessage = $"You need to be over {minAge} years old.";
    }

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value is DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;

            if (birthDate.Date > today.AddYears(-age))
                age--;

            if (age >= _minAge)
                return ValidationResult.Success ?? default!;
        }

        return new ValidationResult(ErrorMessage);
    }
}
