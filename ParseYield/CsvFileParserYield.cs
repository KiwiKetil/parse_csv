using Serilog;

namespace ParseCsv.ParseYield;

using ParseCsv.RegexHelper;

public static class CsvFileParserYield
{
    public static IEnumerable<string> ParseCsvFileYield(string filePath, bool skipHeader = true)
    {
        if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
        {
            throw new FileNotFoundException("File path is invalid or file does not exist.");
        }

        using var reader = new StreamReader(filePath);

        int lineCounter = 0;

        if (skipHeader)
        {
            reader.ReadLine();
            lineCounter++;
        }

        string? line;

        while ((line = reader.ReadLine()) != null)
        {
            lineCounter++;
            List<string> errorMessages = [];

            var split = line.Split(',').Select(p => p.Trim().Trim('"')).ToArray();

            if (split is not [var province, var abbreviation])
            {
               Log.Warning($"Failed parse on line {lineCounter}. Invalid field count. {line}");
                continue;
            }

            if (string.IsNullOrWhiteSpace(province) || string.IsNullOrWhiteSpace(abbreviation))
            {
                errorMessages.Add($"Empty or whitespace field(s): {line}");
            }

            if (RegexHelper.ContainsSpecialCharacters(province)) 
            {
                errorMessages.Add($"Invalid Name: {province}");
            }

            if (RegexHelper.ContainsSpecialCharacters(abbreviation))
            {
                errorMessages.Add($"Invalid Name: {abbreviation}");
            }

            if (errorMessages.Count > 0)
            {
                Log.Warning($"Failed parse on line {lineCounter}. Found {errorMessages.Count} Error(s): {string.Join(" | ", errorMessages)}");
                continue;
            }

            yield return $"{province}, {abbreviation}";
        }
    }
}
