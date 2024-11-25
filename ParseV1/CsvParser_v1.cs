namespace ParseCsv.ParseV1;

using ParseCsv.RegexHelper;
using Serilog;

public static class CsvParser_v1
{
    public static List<Person> ParseCsvFile(string filePath, bool skipHeader = true)
    {
        if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
        {
            throw new FileNotFoundException("File path is invalid or file does not exist.");
        }

        using var reader = new StreamReader(filePath);

        int lineCounter = 0;
        List<Person> result = [];

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
                Log.Warning($"Failed parse on line {lineCounter}: {line} | Invalid field count. Expected 5 fields, found {split.Length} fields.");
                continue;
            }

            HashSet<string> errorMessages = [];

            if (RegexHelper.ContainsSpecialCharacters(firstName))
            {
                errorMessages.Add($"Invalid firstName: {firstName}");
            }

            if (RegexHelper.ContainsSpecialCharacters(lastName))
            {
                errorMessages.Add($"Invalid lastName: {lastName}");
            }

            if (!RegexHelper.IsValidEmail(email))
            {
                errorMessages.Add($"Invalid email: {email}");
            }

            if (!int.TryParse(ageStr, out int age))
            {
                errorMessages.Add($"Invalid age: {ageStr}");
            }

            if (RegexHelper.ContainsSpecialCharacters(country))
            {
                errorMessages.Add($"Invalid country: {country}");
            }

            if (errorMessages.Count > 0)
            {
                Log.Warning($"Failed parse on line {lineCounter}: {line} | Found {errorMessages.Count} Error(s): | {string.Join(" | ", errorMessages)}");
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

