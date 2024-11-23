using ParseCsv.ParseV1;

string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ParseV1", "test_parsing_fail.csv");
var res = CsvFileParserV1.ParseCsvFile(filePath);

foreach (var line in res)
{
    Console.WriteLine($"{line}");
}
