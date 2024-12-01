using MongoDB.Driver;
using ParseCsv.Entities;
using ParseCsv.Parsers;
using ParseCsv.Validators.Services;
using Serilog;
using static ParseCsv.Validators.Services.CsvValidationService;

// Setup log and logfolder
var projectDirectory = Directory.GetParent(AppContext.BaseDirectory)?.Parent?.Parent?.Parent?.FullName;
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(Path.Combine(projectDirectory!, "logs/logs-.log"), rollingInterval: RollingInterval.Day)
    .CreateLogger();

// MongoDB
// var client = new MongoClient(@"mongodb://adminUser:securePassword@localhost:27017/MyTestDB?authSource=admin");
var client = new MongoClient(@"mongodb://myTestUser:securePassword@localhost:27017/MyTestDB?authSource=MyTestDB");

// Parse V1
Console.WriteLine("Parse v1:");
string filePathV1 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "person.csv");
var parsedPersons = ParseCsvPerson.ParsePerson(filePathV1, true); // Default skipHeader is 'false'. Set 'true' to skip header. 

// Validate V1
Console.WriteLine($"\nValidate v1\n");
CsvValidator(parsedPersons);

// Insert MongoDB
var databaseV1 = client.GetDatabase("MyTestDB");
var collectionV1 = databaseV1.GetCollection<Person>("Persons");
collectionV1.InsertMany(parsedPersons);


// Parse V2
Console.WriteLine("\nParse v2:\n");
string filePathV2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "color_srgb.csv");
var parsedSrgb = ParseCsvRgbColor.ParseRgbColor(filePathV2, true); // Default skipHeader is 'false'. Set 'true' to skip header. 

// Validate V2
Console.WriteLine($"\nValidate v2\n");
CsvValidator(parsedSrgb);

// Insert MongoDB
var databaseV2 = client.GetDatabase("MyTestDB");
var collectionV2 = databaseV2.GetCollection<RgbColor>("RgbColors");
collectionV2.InsertMany(parsedSrgb);

// Parse V3
Console.WriteLine("\nParse v3:\n");
string filePathV3 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "province_ca.csv");
var parsedProvinces = ParseCsvProvince.ParseProvince(filePathV3, true).ToList();

// Validate V3
Console.WriteLine($"\nValidate v3\n");
CsvValidationService.CsvValidator(parsedProvinces);

// Insert MongoDB
var databaseV3 = client.GetDatabase("MyTestDB");
var collectionV3 = databaseV3.GetCollection<Province>("Provinces");
collectionV3.InsertMany(parsedProvinces);

// Ensure logs are flushed
Log.CloseAndFlush();