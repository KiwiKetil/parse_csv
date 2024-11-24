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

            HashSet<string> errorMessages = [];

            if (split is not [var name, var hex, var rgb])
            {
                Log.Warning($"Failed parse on line {lineCounter}: {line} | Invalid field count. Field contains {split.Length} fields.");
                continue;
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                errorMessages.Add($"Empty or whitespace field: {nameof(name)}");
            }

            if (string.IsNullOrWhiteSpace(hex))
            {
                errorMessages.Add($"Empty or whitespace field: {nameof(hex)}");
            }

            if (string.IsNullOrWhiteSpace(rgb))
            {
                errorMessages.Add($"Empty or whitespace field: {nameof(rgb)}");
            }

            if (RegexHelper.ContainsSpecialCharacters(name))
            {
                errorMessages.Add($"Invalid Name: {name}");
            }

            if (errorMessages.Count > 0)
            {
                Log.Warning($"Failed parse on line {lineCounter}: {line} | Found {errorMessages.Count} Error(s): | {string.Join(" | ", errorMessages)}");
                continue;
            }

            result.Add($"{name}, {hex}, {rgb}");
        }

        return result;
    }
}
