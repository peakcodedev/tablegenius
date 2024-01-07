using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using TableGenius.Api.Infrastructure;
using TableGenius.Api.Presentation;
using TableGenius.Api.Presentation.Mapper;
using TableGenius.Api.Repo.BlobStorage;
using TableGenius.Api.Repo.Database;
using TableGenius.Api.Services;
using TableGenius.Api.Settings;
using TableGenius.Api.Web.Config;

namespace TableGenius.Api.Web;

public class Startup
{
    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
        Configuration = configuration;
        Env = env;
    }

    private IConfiguration Configuration { get; }
    private IWebHostEnvironment Env { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddAutoMapper(c => c.AddProfile<Mappers>(), typeof(Startup));
        services.Configure<GeneralSettings>(Configuration.GetSection("GeneralSettings"));
        services.Configure<BlobStorageSettings>(Configuration.GetSection("BlobStorageSettings"));
        services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
        services.Configure<Auth0Options>(Configuration.GetSection("Auth0Options"));
        services.Configure<DatabaseOptions>(option =>
        {
            option.Database = Configuration.GetConnectionString("Database");
        });
        services.AddSwaggerConfigServices();
        services.AddSingleton(Configuration);
        services.AddOptions();
        services.AddCorsConfigServices();
        services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
        services.AddAutofac();
        services.AddMemoryCache();
        services.AddControllers().AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        });
        services.AddDatabase(Configuration.GetConnectionString("Database"));
        services.Configure<MvcOptions>(options => { options.EnableEndpointRouting = false; });
        services.AddAuthZero(Configuration.GetSection("Auth0Options").Get<Auth0Options>());
        services.AddPresenters();
        services.AddServices();
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
        MapperInjector.RegisterModule(builder);
        InfrastructureInjector.RegisterModule(builder);
        BlobRepositoryInjector.RegisterModule(builder);
    }
}