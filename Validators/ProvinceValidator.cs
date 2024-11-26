using FluentValidation;
using ParseCsv.Entities;

namespace ParseCsv.Validators;
public class ProvinceValidator : AbstractValidator<Province>
{
    public ProvinceValidator()
    {
        RuleFor(province => province.ProvinceName)
            .NotEmpty().WithMessage("Province name can not be empty")
            .Length(4, 20).WithMessage("Province name must be between 4 and 20 characters")
            .Matches(@"^[a-zA-Z ]+$").WithMessage("Province name can not contain special characters or numbers");

        RuleFor(province => province.Abbreviation)
            .NotEmpty().WithMessage("Abbreviation can not be empty")
            .Length(2).WithMessage("Abbreviation must be 2 characters.")
            .Matches(@"^[A-Z]$").WithMessage("Abbreviation must consist of only uppercase letters.");       
    }
}
