namespace ParseCsv.ParseV1;

using ParseCsv.RegexHelper;
public class CsvFileParserV1
{
    public static (List<string> result, int totalLines, int failedLines) ParseCsvFile(string filePath)
    { 
        if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
        {
            throw new ArgumentException("File path is invalid or file does not exist.", nameof(filePath));
        }

        int lineCounter = 0;
        List<string> linesThatFailed = [];
        List<string> result = [];

        using (var reader = new StreamReader(filePath))
        {
            string? line;

            //reader.ReadLine(); //// Optional: Skip header if activated

            while ((line = reader.ReadLine()) != null)
            {
                lineCounter++;
                var split = line.Split(',').Select(p => p.Trim()).ToArray();

                List<string> errorMessages = [];

                if (split.Length != 5)
                {
                    errorMessages.Add($"Failed! Line {lineCounter}. Incorrect number of fields ({split.Length}). Actual data: {line}");
                }
                else
                {
                    var (firstName, lastName, email, ageString, country) = (split[0], split[1], split[2], split[3], split[4]);

                    if (!RegexHelper.EmailRegex().IsMatch(email))
                    {
                        errorMessages.Add($"Failed! Line {lineCounter}. Invalid email. Actual data: {email}");
                    }

                    if (!int.TryParse(ageString, out int age))
                    {
                        errorMessages.Add($"Failed! Line {lineCounter}. Invalid age. Actual Data: {ageString}");
                    }

                    if (errorMessages.Count == 0)
                    {
                        result.Add($"{lastName}, {firstName} ({age}) - {email} from {country}");
                    }
                }

                if (errorMessages.Count > 0)
                {
                    linesThatFailed.Add(string.Join(" | ", errorMessages));
                }
            }
        }
      
        return (result, lineCounter, linesThatFailed);
    }
}
