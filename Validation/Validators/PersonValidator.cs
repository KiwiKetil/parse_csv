using FluentValidation;
using ParseCsv.Entities;

namespace ParseCsv.Validation.Validators;

public class PersonValidator : AbstractValidator<Person>
{
    public PersonValidator()
    {
        RuleFor(person => person.FirstName)
            .NotEmpty().WithMessage("FirstName is required")
            .Length(2, 25).WithMessage("Firstname must be between 2 and 25 characters")
            .Matches(@"^[a-zA-Z ]+$").WithMessage("Firstname can not contain special characters or numbers");

        RuleFor(person => person.LastName)
            .NotEmpty().WithMessage("LastName is required")
            .Length(2, 25).WithMessage("LastName must be between 2 and 25 characters.")
            .Matches(@"^[a-zA-Z ]+$").WithMessage("Lastname can not contain special characters or numbers");

        RuleFor(person => person.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format");

        RuleFor(person => person.Age)
            .NotEmpty().WithMessage("Age is required")
            .InclusiveBetween(0, 115).WithMessage("Age must be between 0 and 115");

        RuleFor(person => person.Country)
            .NotEmpty().WithMessage("Country is required")
            .Length(3, 30).WithMessage("Country must be between 3 and 30 characters")
            .Matches(@"^[a-zA-Z ]+$").WithMessage("Country can not contain special characters or numbers");
    }
}
