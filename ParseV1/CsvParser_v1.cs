﻿using Serilog;
using static ParseCsv.ParseV1.ValidateFieldsV1;

namespace ParseCsv.ParseV1;

public static class CsvParser_v1
{
    public static List<Person> ParseCsvFile(string filePath, bool skipHeader = true)
    {
        if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
        {
            throw new ArgumentException($"Filepath: \"{filePath}\" is invalid or file does not exist.");
        }

        using var reader = new StreamReader(filePath);

        int lineCounter = 0;
        List<Person> result = new();

        string? line;

        if (skipHeader)
        {
            reader.ReadLine();
            lineCounter++;
        }

        while ((line = reader.ReadLine()) != null)
        {
            lineCounter++;

            if (string.IsNullOrWhiteSpace(line))
            {
                Log.Warning($"Line {lineCounter}: Empty row encountered.");
                continue;
            }

            var split = line.Split(',').Select(p => p.Trim().Trim('"')).ToArray();

            if (split is not [var firstName, var lastName, var email, var ageStr, var country])
            {
                Log.Warning($"Failed on line {lineCounter}: {line} | Invalid field count. Expected 5 fields, found {split.Length} fields.");
                continue;
            }

            HashSet<string> errors = new();

            if (!int.TryParse(ageStr, out int age))
            {
                errors.Add($"Could not parse age: {ageStr}");
            }

            errors.UnionWith(ValidateCsvFields(firstName, lastName, email, country));

            if (errors.Count > 0)
            {
                Log.Warning($"Failed on line {lineCounter}: {line} | Found {errors.Count} Error(s): | {string.Join(" | ", errors)}");
                continue; 
            }

            Person person = new()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Age = age,
                Country = country
            };

            result.Add(person);
        }

        return result;
    }
}
