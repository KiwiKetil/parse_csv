namespace ParseCsv.ParseV2;

public static class CsvFileParserV2
{
    public static List<string> ParseCsvFile2(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
        {
            throw new FileNotFoundException("File path is invalid or file does not exist.");
        }

        int lineCounter = 0;
        List<string> result = [];

        var lines = File.ReadLines(filePath).Skip(0); // adjust param if needed

        foreach (string line in lines)
        {
            lineCounter++;

            var split = line.Split(',', 3).Select(p => p.Trim().Trim('"')).ToArray(); // 3 only works in this specific case

            if (split is not [var Name, var Hex, var Rgb])
            {
                Console.WriteLine($"Failed parse on line {lineCounter}. Invalid field count. {line}");
                continue;
            }

            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Hex) || string.IsNullOrWhiteSpace(Rgb))
            {
                Console.WriteLine($"Failed parse on line {lineCounter}. Line has empty or whitespace field(s). Actual data: {line}");
                continue;
            }
            result.Add($"{Name}, {Hex}, {Rgb}");

        }
        return result;
    }
}
