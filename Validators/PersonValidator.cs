using FluentValidation;
using ParseCsv.Entities;

namespace ParseCsv.Validators;

public class PersonValidator : AbstractValidator<Person>
{
    public PersonValidator()
    {
        RuleFor(person => person.FirstName)
            .NotEmpty().WithMessage("FirstName can not be empty")
            .Length(2, 25).WithMessage("Firstname must be between 2 and 25 characters")
            .Matches(@"^[a-zA-Z0-9 ]+$").WithMessage("Firstname can not contain special characters");

        RuleFor(person => person.LastName)
            .NotEmpty().WithMessage("LastName can not be empty")
            .Length(2, 25).WithMessage("LastName must be between 2 and 25 characters.")
            .Matches(@"^[a-zA-Z0-9 ]+$").WithMessage("Lastname can not contain special characters");

        RuleFor(person => person.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format");

        RuleFor(person => person.Age)
            .InclusiveBetween(0, 115).WithMessage("Age must be between 0 and 115");

        RuleFor(person => person.Country)
            .Length(3, 30).WithMessage("Country must be between 3 and 30 characters")
            .NotEmpty().WithMessage("Country can not be empty")
            .Matches(@"^[a-zA-Z0-9 ]+$").WithMessage("Country can not contain special characters");
    }
}
