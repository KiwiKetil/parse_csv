using FluentValidation;
using ParseCsv.Entities;
using ParseCsv.Validators;

namespace ParseCsv.Services;
public static class ValidatorFactory
{
    public static IValidator<T> GetValidator<T>()
    {
        if (typeof(T) == typeof(Person))
        {
            return (IValidator<T>)new PersonValidator();
        }
        if (typeof(T) == typeof(RgbColor))
        {
            return (IValidator<T>)new RgbValidator();
        }
        throw new NotSupportedException($"No validator found for type {typeof(T).Name}");
    }
}
