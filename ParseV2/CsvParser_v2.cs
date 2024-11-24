﻿using Serilog;
using static ParseCsv.ParseV2.ValidateFieldsV2;

namespace ParseCsv.ParseV2;

public static class CsvParser_v2
{
    public static List<SRgbColor> ParseCsvFile2(string filePath, bool skipHeader = true)
    {
        if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
        {
            throw new ArgumentException($"Filepath: \"{filePath}\" is invalid or file does not exist.");
        }

        int lineCounter = 0;
        List<SRgbColor> result = [];

        var lines = File.ReadLines(filePath);

        if (skipHeader)
        {
            lines = lines.Skip(1);
            lineCounter++;
        }

        foreach (string line in lines)
        {
            lineCounter++;

            if (string.IsNullOrWhiteSpace(line))
            {
                Log.Warning($"Line {lineCounter}: Empty row encountered.");
                continue;
            }

            var split = line.Split(',', 3).Select(p => p.Trim().Trim('"')).ToArray(); // 3 only works if there are exactly 3 fields and field with extra ',' is last field (split[2])

            if (split is not [var name, var hex, var rgb])
            {
                Log.Warning($"Failed parse on line {lineCounter}: {line} | Invalid field count. Expected 3 fields, but found {split.Length} fields.");
                continue;
            }

            var validationErrors = ValidateCsvFields(name, hex, rgb);

            if (validationErrors.Count > 0)
            {
                Log.Warning($"Failed parse on line {lineCounter}: {line} | Found {validationErrors.Count} Error(s): | {string.Join(" | ", validationErrors)}");
                continue;
            }

            SRgbColor sRgb = new()
            {
                Name = name,
                Hex = hex,
                Rgb = rgb,
            };

            result.Add(sRgb);
        }

        return result;
    }
}
