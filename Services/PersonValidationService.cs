using ParseCsv.Entities;
using ParseCsv.Validators;
using FluentValidation.Results;
using Serilog;

namespace ParseCsv.Services;

public static class PersonValidationService
{
    public static void ValidatePersons(List<Person> persons)
    {
        var validator = new PersonValidator();
        var count = 0;

        foreach (var person in persons) 
        {
            count++;
            ValidationResult validationResult = validator.Validate(person);

            if (validationResult.IsValid)
            {
                Log.Information($"Validation success on line {count} for person '{person.FirstName} {person.LastName}'");
            }
            else
            {
                var aggregatedErrors = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
                Log.Warning($"Validation failed on line {count} for person '{person.FirstName} {person.LastName}': {aggregatedErrors}");
            }
        }
    }
}
