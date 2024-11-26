using FluentValidation;
using ParseCsv.Entities;

namespace ParseCsv.Validation.Validators;
public class ProvinceValidator : AbstractValidator<Province>
{
    public ProvinceValidator()
    {
        RuleFor(province => province.ProvinceName)
            .NotEmpty().WithMessage("Province name is required")
            .Length(4, 35).WithMessage("Province name must be between 4 and 35 characters")
            .Matches(@"^[a-zA-Z ]+$").WithMessage("Province name can not contain special characters or numbers");

        RuleFor(province => province.Abbreviation)
            .NotEmpty().WithMessage("Abbreviation is required")
            .Matches(@"^[A-Z]{2}$").WithMessage("Abbreviation must consist of exactly 2 uppercase letters.");
    }
}
