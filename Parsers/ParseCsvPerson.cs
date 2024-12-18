﻿using ParseCsv.Entities;
using Serilog;

namespace ParseCsv.Parsers;

public static class ParseCsvPerson
{
    public static List<Person> ParsePerson(string filePath, bool? skipHeader = false)
    {
        if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
        {
            throw new ArgumentException($"Filepath: \"{filePath}\" is invalid or file does not exist.");
        }

        using var reader = new StreamReader(filePath);

        int lineCounter = 0;
        int validCount = 0;
        List<Person> result = [];        

        if (skipHeader == true)
        {
            reader.ReadLine();
            lineCounter++; 
        }

        string? line;

        while ((line = reader.ReadLine()) != null)
        {
            lineCounter++;

            if (string.IsNullOrWhiteSpace(line))
            {
                Log.Warning($"Failed to parse line {lineCounter}: Empty row encountered.");
                continue;
            }

            var split = line.Split(',').Select(p => p.Trim().Trim('"')).ToArray();

            if (split is not [var firstName, var lastName, var email, var ageStr, var country])
            {
                Log.Warning($"Failed to parse line {lineCounter}: Invalid field count. Expected 5 fields but found {split.Length}.");
                continue;
            }

            if (!int.TryParse(ageStr, out int age))
            {
                Log.Warning($"Failed to parse line {lineCounter}: {firstName}, {lastName}, {email}, {age}, {country} Invalid 'int' data type for 'age' field. Value: '{ageStr}'.");
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
            Log.Information($"Successfully parsed line {lineCounter}: {firstName}, {lastName}, {email}, {age}, {country}");
            validCount++;
            result.Add(person);
            
        }
        Console.WriteLine();
        int totalProcessedLines = lineCounter - (skipHeader == true ? 1 : 0);
        Log.Information($"Parse completed. Total valid parsed: {validCount} out of {totalProcessedLines}");
        return result;
    }
}
