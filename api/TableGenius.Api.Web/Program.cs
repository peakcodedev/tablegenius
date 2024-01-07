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
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        Log.Logger = new LoggerConfiguration()
            .Enrich.WithProperty("Application", "TableGenius.API")
            .Enrich.WithProperty("Environment", "")
            .Enrich.WithMachineName()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();

        CreateWebHostBuilder(args, env).Build().Run();
    }


    public static IHostBuilder CreateWebHostBuilder(string[] args, string envString)
    {
        var builder = new HostBuilder();
        builder
            .UseContentRoot(Directory.GetCurrentDirectory())
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .UseSerilog()
            .ConfigureAppConfiguration((hostContext, config) =>
            {
                var env = hostContext.HostingEnvironment;
                config.SetBasePath(env.ContentRootPath)
                    .AddJsonFile("appsettings.json", true, true)
                    .AddJsonFile($"appsettings.{envString}.json", true, true)
                    .AddEnvironmentVariables();
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseSentry(o =>
                {
                    o.Dsn =
                        "https://5fd0c1e8b9601f7e20843705beb4f154@o4506015135694848.ingest.sentry.io/4506015136546816";
                    o.Debug = true;
                    o.TracesSampleRate = 1.0;
                    o.EnableTracing = true;
                    o.AttachStacktrace = true;
                    o.MaxBreadcrumbs = 50;
                });
                webBuilder
                    .ConfigureKestrel(serverOptions => { serverOptions.AddServerHeader = false; })
                    .UseStartup<Startup>().CaptureStartupErrors(true);
            });
        return builder;
    }
}