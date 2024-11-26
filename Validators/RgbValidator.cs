using FluentValidation;
using ParseCsv.Entities;

namespace ParseCsv.Validators;
public class RgbValidator : AbstractValidator<RgbColor>
{
    public RgbValidator()
    {
        RuleFor(rgb => rgb.Name)
            .NotEmpty().WithMessage("Name can not be empty")
            .Length(2, 10).WithMessage("Name must be between 2 and 10 characters")
            .Matches(@"^[a-zA-Z ]+$").WithMessage("Name can not contain special characters");

        RuleFor(rgb => rgb.Hex)
            .NotEmpty().WithMessage("Hex can not be empty")
            .Length(2, 10).WithMessage("Hex must be between 2 and 10 characters.")
            .Matches(@"^#[a-zA-Z0-9]+$").WithMessage("Hex must start with # and can not contain any other special characters");

        RuleFor(rgb => rgb.Rgb)
            .NotEmpty().WithMessage("Rgb is required")
            .MaximumLength(25);
    }
}
