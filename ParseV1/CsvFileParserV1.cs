namespace ParseCsv.ParseV1;

using ParseCsv.RegexHelper;
using Serilog;

public static class CsvFileParserV1
{
    public static List<string> ParseCsvFile(string filePath, bool skipHeader = true)
    {
        if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
        {
            throw new FileNotFoundException("File path is invalid or file does not exist.");
        }

        using var reader = new StreamReader(filePath);

        int lineCounter = 0;
        List<string> result = [];

        string? line;

        if (skipHeader)
        {
            reader.ReadLine();
            lineCounter++;
        }

        while ((line = reader.ReadLine()) != null)
        {
            lineCounter++;
            var split = line.Split(',').Select(p => p.Trim().Trim('"')).ToArray();

            if (split is not [var firstName, var lastName, var email, var ageString, var country])
            {
                Log.Warning($"Failed parse on line {lineCounter}. Invalid field count. {line}");
                continue;
            }

            List<string> errorMessages = [];

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

            if (!int.TryParse(ageString, out int age))
            {
                errorMessages.Add($"Invalid age: {ageString}");
            }

            if (RegexHelper.ContainsSpecialCharacters(country))
            {
                errorMessages.Add($"Invalid country: {country}");
            }

            if (errorMessages.Count > 0)
            {
                Log.Warning($"Failed parse on line {lineCounter}. Found {errorMessages.Count} Error(s): {string.Join(" | ", errorMessages)}");
                continue;
            }

            result.Add(line);
        }

        return result;
    }
}

