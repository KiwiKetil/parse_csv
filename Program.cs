using ParseCsv.ParseV1;

var (res, ) = CsvFileParserV1.ParseCsvFile(@"C:\Users\Ketil\OneDrive\Dokumenter\test_parsing_fail.csv");

foreach (var line in res)
{
    Console.WriteLine($"SUCCESS: {line}");
}

//Console.WriteLine($"\nCsv original lines total: {lineCounter}");
//Console.WriteLine($"Successful lines total: {result.Count}");
//Console.WriteLine($"Failed lines total: {linesThatFailed.Count}\n");
