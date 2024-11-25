using ParseCsv.ParseV1;
using ParseCsv.ParseV2;
using ParseCsv.ParseYield;
using Serilog;

// Setup log and logfilefolder
var projectDirectory = Directory.GetParent(AppContext.BaseDirectory)?.Parent?.Parent?.Parent?.FullName;
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(Path.Combine(projectDirectory!, "logs/logs-.log"), rollingInterval: RollingInterval.Day)
    .CreateLogger();

// Parse V1
Console.WriteLine("Parse v1:\n");
string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ParseV1", "test_parsing_fail.csv");
var res = CsvParser_v1.ParseCsvFile(filePath, false);

foreach (var line in res)
{
    Console.WriteLine($"{line}");
}

Console.WriteLine();

// Parse V2
Console.WriteLine("Parse v2:\n");
string filePath2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ParseV2", "color_srgb.csv");

var res2 = CsvParser_v2.ParseCsvFile2(filePath2);
foreach (var line in res2)
{
    Console.WriteLine(line);
}

Console.WriteLine();

// ParseYield
Console.WriteLine("Parse yield:\n");
string filePathYield = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ParseYield", "province_ca.csv");
var resYield = CsvParser_v3_Yield.ParseCsvFileYield(filePathYield);

foreach (var line in resYield)
    Console.WriteLine(line);

// Ensure logs are flushed
Log.CloseAndFlush();