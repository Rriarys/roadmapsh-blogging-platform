using System.ComponentModel.DataAnnotations;

namespace BloggingPlatform.API.DTOs.DtoHelpers;

public class TagLengthValidatorAttribute(int maxLength) : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is List<string> tags)
        {
            foreach (string tag in tags)
            {
                if (tag.Length > maxLength)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }
        }
        return ValidationResult.Success;
    }
}
