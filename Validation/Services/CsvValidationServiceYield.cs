﻿using FluentValidation.Results;
using Newtonsoft.Json;
using ParseCsv.Validation.Factory;
using Serilog;

namespace ParseCsv.Validation.Services;
public class CsvValidationServiceYield
{
    public static IEnumerable<T> CsvValidatorYield<T>(List<T> list)
       where T : class
    {
        if (list == null || list.Count == 0)
        {
            Log.Warning("CSV validation skipped: The provided list is empty.");
            yield break; 
        }
        var validator = ValidatorFactory.GetValidator<T>();
        int validCount = 0;
        int invalidCount = 0;

        foreach (var item in list)
        {
            ValidationResult validationResult = validator.Validate(item);

            if (validationResult.IsValid)
            {
                validCount++;
                Log.Information($"Validation success for {item!.GetType().Name}: {JsonConvert.SerializeObject(item)}");
                yield return item;
            }
            else
            {
                invalidCount++;
                var aggregatedErrors = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
                Log.Warning($"Validation failed for {item!.GetType().Name}: {JsonConvert.SerializeObject(item)} Errors({validationResult.Errors.Count}): {aggregatedErrors}");
            }
        }
        Console.WriteLine();
        Log.Information($"Validation completed. Total valid items: {validCount} out of {list.Count}");
    }
}
