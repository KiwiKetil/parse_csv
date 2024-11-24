namespace ParseCsv.ParseV2;

using ParseCsv.RegexHelper;
using Serilog;

public static class CsvFileParserV2
{
    public static List<string> ParseCsvFile2(string filePath, bool skipHeader = true)
    {
        if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
        {
            throw new FileNotFoundException("File path is invalid or file does not exist.");
        }

        int lineCounter = 0;
        List<string> result = [];

        IEnumerable<string> lines = File.ReadLines(filePath);

        if (skipHeader)
        {
            lines = lines.Skip(1);
            lineCounter++;
        }

        foreach (string line in lines)
        {
            lineCounter++;

            var split = line.Split(',', 3).Select(p => p.Trim().Trim('"')).ToArray(); // 3 only works if there are exactly 3 fields and field with extra ',' is last field (split[2])

            List<string> errorMessages = [];

            if (split is not [var Name, var Hex, var Rgb])
            {
                Log.Warning($"Failed parse on line {lineCounter}. Invalid field count. {line}");
                continue;
            }

            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Hex) || string.IsNullOrWhiteSpace(Rgb))
            {
                errorMessages.Add($"Empty or whitespace field(s): {line}");
            }

            if (RegexHelper.ContainsSpecialCharacters(Name))
            {
                errorMessages.Add($"Invalid Name: {Name}");
            }

            if (errorMessages.Count > 0)
            {
                Log.Warning($"Failed parse on line {lineCounter}. Found {errorMessages.Count} Error(s): {string.Join(" | ", errorMessages)}");
                continue;
            }

            result.Add($"{Name}, {Hex}, {Rgb}");
        }

        return result;
    }
}
