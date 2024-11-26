using FluentValidation;
using ParseCsv.Entities;

namespace ParseCsv.Validation.Validators;
public class RgbValidator : AbstractValidator<RgbColor>
{
    public RgbValidator()
    {
        RuleFor(rgb => rgb.Name)
            .NotEmpty().WithMessage("Name is required")
            .Length(2, 10).WithMessage("Name must be between 2 and 10 characters")
            .Matches(@"^[a-zA-Z ]+$").WithMessage("Name can not contain special characters or numbers");

        RuleFor(rgb => rgb.Hex)
            .NotEmpty().WithMessage("Hex is required")
            .Length(2, 10).WithMessage("Hex must be between 2 and 10 characters.")
            .Matches(@"^#[a-zA-Z0-9]+$").WithMessage("Hex must start with # and can not contain any other special characters");

        RuleFor(rgb => rgb.Rgb)
            .NotEmpty().WithMessage("Rgb is required")
            .MaximumLength(25);
    }
}
