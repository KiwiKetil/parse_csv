using ParseCsv.Entities;
using Serilog;

namespace ParseCsv.Parsers;
public static class ParseCsvProvince
{
    public static IEnumerable<Province> ParseProvince(string filePath, bool? skipHeader = false)
    {
        if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
        {
            throw new ArgumentException($"Filepath: \"{filePath}\" is invalid or file does not exist.");
        }

        using var reader = new StreamReader(filePath);

        int lineCounter = 0;
        int validCount = 0;

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

            if (split is not [var province, var abbreviation])
            {
                Log.Warning($"Failed to parse line {lineCounter}: Invalid field count. Expected 2 fields but found {split.Length}.");
                continue;
            }

            Province provinceInfo = new()
            {
                ProvinceName = province,
                Abbreviation = abbreviation,
            };

            yield return provinceInfo;
            validCount++; 
            Log.Information($"Successfully parsed line {lineCounter}: {province}, {abbreviation}"); 
        }
        Console.WriteLine();
        int totalProcessedLines = lineCounter - (skipHeader == true ? 1 : 0);
        Log.Information($"Parse completed. Total valid parsed: {validCount} out of {totalProcessedLines}"); 
    }
}
