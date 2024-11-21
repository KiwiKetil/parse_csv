namespace ParseCsv.ParseV1;

using ParseCsv.RegexHelper;
public class CsvFileParserV1
{
    public static List<string> ParseCsvFile(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
        {
            throw new FileNotFoundException("File path is invalid or file does not exist.");
        }

        int lineCounter = 0;
        List<string> result = [];

        using var reader = new StreamReader(filePath);
        string? line;

        //reader.ReadLine(); //// Optional: Skip header etc if activated

        while ((line = reader.ReadLine()) != null)
        {
            lineCounter++;
            var split = line.Split(',').Select(p => p.Trim()).ToArray();

            if (split is not [var firstName, var lastName, var email, var ageString, var country])
            {
                Console.WriteLine($"Failed Parse on line {lineCounter}. Missing field(s). Current Data: {line}");
                continue; 
            }

            List<string> errorMessages = [];
            
            if (!RegexHelper.EmailRegex().IsMatch(email))
            {
                errorMessages.Add($"Invalid email: {email}");
            }

            if (!int.TryParse(ageString, out int age))
            {
                errorMessages.Add($"Invalid age: {ageString}");
            }

            if (errorMessages.Count > 0)
            {
                Console.WriteLine($"Failed parse on line {lineCounter}. Errors: {string.Join("; ", errorMessages)}");
                continue;
            }

            result.Add(line);
        }

        return result;
    }
}

