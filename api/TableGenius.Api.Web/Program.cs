using System;
using System.IO;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace TableGenius.Api.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true).AddJsonFile("appsettings.development.json", true, true)
            .AddEnvironmentVariables().Build();
        Console.WriteLine(configuration);
        Log.Logger = new LoggerConfiguration()
            .Enrich.WithProperty("Application", "TableGenius.API")
            .Enrich.WithProperty("Environment", "")
            .Enrich.WithMachineName()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();

        CreateHostBuilder(args).ConfigureWebHostDefaults(webBuilder =>
        {
            // Add the following line:
            webBuilder.UseSentry(o =>
            {
                o.Dsn = "https://5fd0c1e8b9601f7e20843705beb4f154@o4506015135694848.ingest.sentry.io/4506015136546816";
                // When configuring for the first time, to see what the SDK is doing:
                o.Debug = true;
                // Set TracesSampleRate to 1.0 to capture 100% of transactions for performance monitoring.
                // We recommend adjusting this value in production.
                o.TracesSampleRate = 1.0;
                o.EnableTracing = true;
                o.AttachStacktrace = true;
                o.MaxBreadcrumbs = 50;
            });
        }).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .UseSerilog()
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}