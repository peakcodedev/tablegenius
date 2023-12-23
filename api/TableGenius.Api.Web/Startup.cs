using Autofac;
using Autofac.Extensions.DependencyInjection;
using TableGenius.Api.Infrastructure;
using TableGenius.Api.Presentation;
using TableGenius.Api.Presentation.Mapper;
using TableGenius.Api.Repo.BlobStorage;
using TableGenius.Api.Repo.Database;
using TableGenius.Api.Services;
using TableGenius.Api.Settings;
using TableGenius.Api.Web.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace TableGenius.Api.Web;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddAutoMapper(c => c.AddProfile<Mappers>(), typeof(Startup));
        services.Configure<GeneralSettings>(Configuration.GetSection("GeneralSettings"));
        services.Configure<BlobStorageSettings>(Configuration.GetSection("BlobStorageSettings"));
        services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
        services.Configure<DatabaseOptions>(option =>
        {
            option.Database = Configuration.GetConnectionString("Database");
        });
        services.AddSwaggerConfigServices();
        services.AddSingleton(Configuration);
        services.AddOptions();
        services.AddCorsConfigServices();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddAutofac();
        services.AddMemoryCache();
        services.AddControllers().AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        });
        services.AddDatabase();
        services.Configure<MvcOptions>(options => { options.EnableEndpointRouting = false; });
        //services.AddAuthZero();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        DataServiceCollectionExtensions.UpdateDatabase(app);
        if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

        app.AddSwaggerConfig();
        app.UseAuthentication();
        app.UseAuthorization();
        app.AddCorsConfig();
        app.UseHttpsRedirection();
        app.AddRouteConfig();

        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        });
    }

    public void ConfigureContainer(ContainerBuilder builder)
    {
        ServicesInjector.RegisterModule(builder);
        PresenterInjector.RegisterModule(builder);
        MapperInjector.RegisterModule(builder);
        InfrastructureInjector.RegisterModule(builder);
        BlobRepositoryInjector.RegisterModule(builder);
    }
}