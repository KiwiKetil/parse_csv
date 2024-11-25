using Serilog;
using static ParseCsv.ParseYield.ValidateFieldsV3;

namespace ParseCsv.ParseYield;
public static class CsvParser_v3_Yield
{
    public static IEnumerable<ProvinceInfo> ParseCsvFileYield(string filePath, bool skipHeader = true)
    {
        if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
        {
            throw new ArgumentException($"Filepath: \"{filePath}\" is invalid or file does not exist.");
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

            if (string.IsNullOrWhiteSpace(line))
            {
                Log.Warning($"Line {lineCounter}: Empty row encountered.");
                continue;
            }

            var split = line.Split(',').Select(p => p.Trim().Trim('"')).ToArray();

            if (split is not [var province, var abbreviation])
            {
                Log.Warning($"Failed parse on line {lineCounter}: {line} | Invalid field count. Expected 2 fields, but found {split.Length} fields.");
                continue;
            }

            var validationErrors = ValidateCsvFields(province, abbreviation);

            if (validationErrors.Count > 0)
            {
                Log.Warning($"Failed parse on line {lineCounter}: {line} | Found {validationErrors.Count} Error(s): | {string.Join(" | ", validationErrors)}");
                continue;
            }

            ProvinceInfo provinceInfo = new()
            {
                Province = province,
                Abbreviation = abbreviation,
            };

            yield return provinceInfo;
        }
    }
}
