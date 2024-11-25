using Serilog;
using static ParseCsv.Parsers.ParseV1;
using static ParseCsv.Services.PersonValidationService;

// Setup log and logfolder
var projectDirectory = Directory.GetParent(AppContext.BaseDirectory)?.Parent?.Parent?.Parent?.FullName;
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(Path.Combine(projectDirectory!, "logs/logs-.log"), rollingInterval: RollingInterval.Day)
    .CreateLogger();

// Parse V1
Console.WriteLine("Parse v1:\n");

string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "person.csv");

var parsedPersons = ParseCsvFileV1(filePath, false);
ValidatePersons(parsedPersons);

Console.WriteLine($"Parsed {parsedPersons.Count} persons.");



//foreach (var person in res)
//{
//    Console.WriteLine($"{person}");
//}

//Console.WriteLine();

//// Parse V2
//Console.WriteLine("Parse v2:\n");
//string filePath2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ParseV2", "color_srgb.csv");

//var res2 = ParseCsvFileV2(filePath2);
//foreach (var line in res2)
//{
//    Console.WriteLine(line);
//}

//Console.WriteLine();

//// ParseYield
//Console.WriteLine("Parse yield:\n");
//string filePathYield = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ParseYield", "province_ca.csv");
//var resYield = ParseCsvFileYield(filePathYield);

//foreach (var line in resYield)
//    Console.WriteLine(line);



// Ensure logs are flushed
Log.CloseAndFlush();