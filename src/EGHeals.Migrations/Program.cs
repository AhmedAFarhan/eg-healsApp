// See https://aka.ms/new-console-template for more information

using EGHeals.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        config.AddEnvironmentVariables();
    })
    .ConfigureServices((context, services) =>
    {
        // Add Infrastructure Services with Configuration
        services.AddInfrastructureServices(context.Configuration);
    })
    .Build();

Console.WriteLine("Migrations console app is ready.");

