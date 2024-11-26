using FluentValidation;
using ParseCsv.Entities;
using ParseCsv.Validation.Validators;

namespace ParseCsv.Validation.Factory;
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
        if (typeof(T) == typeof(Province))
        {
            return (IValidator<T>)new ProvinceValidator();
        }
        throw new NotSupportedException($"No validator found for type {typeof(T).Name}");
    }
}
