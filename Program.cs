﻿using ParseCsv.ParseV1;
using ParseCsv.ParseV2;
using Serilog;

// Setup log and logfilefolder
var projectDirectory = Directory.GetParent(AppContext.BaseDirectory)?.Parent?.Parent?.Parent?.FullName;
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(Path.Combine(projectDirectory!, "logs/logs-.log"), rollingInterval: RollingInterval.Day)
    .CreateLogger();

// Parse
string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ParseV1", "test_parsing_fail.csv");
var res = CsvFileParserV1.ParseCsvFile(filePath);

foreach (var line in res)
{
    Console.WriteLine($"{line}");
}

Console.WriteLine();

string filePath2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ParseV2", "color_srgb.csv");

var res2 = CsvFileParserV2.ParseCsvFile2(filePath2);
foreach (var line in res2)
{
    Console.WriteLine(line);
}

// Ensure logs are flushed
Log.CloseAndFlush();