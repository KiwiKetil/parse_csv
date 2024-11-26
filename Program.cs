using Serilog;
using static ParseCsv.Parsers.ParseCsvPerson;
using static ParseCsv.Parsers.ParseCsvRgbColor;
using static ParseCsv.Services.ValidationService;

// Setup log and logfolder
var projectDirectory = Directory.GetParent(AppContext.BaseDirectory)?.Parent?.Parent?.Parent?.FullName;
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(Path.Combine(projectDirectory!, "logs/logs-.log"), rollingInterval: RollingInterval.Day)
    .CreateLogger();

/*
// Parse V1
Console.WriteLine("Parse v1:");
string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "person.csv");
var parsedPersons = ParsePerson(filePath, true); // Default skipHeader is 'false'. Set 'true' to skip header. 

// Validate V1
Console.WriteLine($"\nValidate v1");
CsvValidator(parsedPersons);  


// Parse V2
Console.WriteLine("\nParse v2:\n");
string filePathV2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "color_srgb.csv");
var parsedSrgb = ParseRgbColor(filePathV2, true); // Default skipHeader is 'false'. Set 'true' to skip header. 

// Validate V2
Console.WriteLine($"\nValidate v2");
CsvValidator(parsedSrgb);

*/

// Parse V3

// Validate V3


// Ensure logs are flushed
Log.CloseAndFlush();