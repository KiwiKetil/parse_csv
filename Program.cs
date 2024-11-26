using ParseCsv.Parsers;
using Serilog;

using static ParseCsv.Parsers.ParseCsvPerson;
using static ParseCsv.Parsers.ParseCsvRgbColor;
using static ParseCsv.Parsers.ParseCsvProvince;
using static ParseCsv.Validators.Services.CsvValidationService;

// Setup log and logfolder
var projectDirectory = Directory.GetParent(AppContext.BaseDirectory)?.Parent?.Parent?.Parent?.FullName;
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(Path.Combine(projectDirectory!, "logs/logs-.log"), rollingInterval: RollingInterval.Day)
    .CreateLogger();

/*
// Parse V1
Console.WriteLine("Parse v1:");
string filePathV1 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "person.csv");
var parsedPersons = ParsePerson(filePathV1, true); // Default skipHeader is 'false'. Set 'true' to skip header. 

// Validate V1
Console.WriteLine($"\nValidate v1\n");
CsvValidator(parsedPersons);  

*/
// Parse V2
Console.WriteLine("\nParse v2:\n");
string filePathV2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "color_srgb.csv");
var parsedSrgb = ParseRgbColor(filePathV2, true); // Default skipHeader is 'false'. Set 'true' to skip header. 

// Validate V2
Console.WriteLine($"\nValidate v2\n");
CsvValidator(parsedSrgb);



// Parse V3
Console.WriteLine("\nParse v3:\n");
string filePathV3 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "province_ca.csv");
var parsedProvinces = ParseProvince(filePathV3).ToList();

// Validate and Parse V3
Console.WriteLine($"\nValidate v3\n");
CsvValidator(parsedProvinces);

// Ensure logs are flushed
Log.CloseAndFlush();