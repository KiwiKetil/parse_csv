using ParseCsv.Entities;
using ParseCsv.Splitters;
using Serilog;

namespace ParseCsv.Parsers;

public static class ParseCsvRgbColor
{
    public static List<RgbColor> ParseRgbColor(string filePath, bool? skipHeader = false)
    {
        if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
        {
            throw new ArgumentException($"Filepath: \"{filePath}\" is invalid or file does not exist.");
        }

        int lineCounter = 0;
        int validCount = 0;
        List<RgbColor> result = [];

        var lines = File.ReadLines(filePath);

        if (skipHeader == true)
        {
            lines = lines.Skip(1);
            lineCounter++;
        }

        foreach (string line in lines)
        {
            lineCounter++;

            if (string.IsNullOrWhiteSpace(line))
            {
                Log.Warning($"Failed to parse line {lineCounter}: Empty row encountered.");
                continue;
            }

            var split = ColorCsvSplitter.ManualSplitCsvLine(line);

            if (split is not [var name, var hex, var rgb])
            {
                Log.Warning($"Failed to parse line {lineCounter}: Invalid field count. Expected 3 fields but found {split.Length}.");
                continue;
            }

            name = name.Trim().Trim('"');
            hex = hex.Trim().Trim('"');
            rgb = rgb.Trim().Trim('"');

            RgbColor sRgb = new()
            {
                Name = name,
                Hex = hex,
                Rgb = rgb,
            };

            result.Add(sRgb);
            validCount++;
            Log.Information($"Successfully parsed line {lineCounter}: {name}, {hex}, {rgb}");
        }

        Console.WriteLine();
        int totalProcessedLines = lineCounter - (skipHeader == true ? 1 : 0);
        Log.Information($"Parse completed. Total valid parsed: {validCount} out of {totalProcessedLines}");
        return result;
    }
}
