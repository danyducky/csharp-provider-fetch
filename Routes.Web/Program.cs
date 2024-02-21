using Microsoft.Extensions.Options;
using Routes.Infrastructure.Abstractions.Interfaces;
using Routes.Infrastructure.Implementations.Services;
using Routes.UseCases.Provider.GetProviderAvailability;
using Routes.Web.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("Application"));
builder.Services.AddControllers();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(typeof(GetProviderAvailabilityQuery).Assembly);
    cfg.Lifetime = ServiceLifetime.Scoped;
});
builder.Services.AddMemoryCache();
builder.Services.AddHttpClient();

builder.Services.AddScoped<ISearchService, SearchService>(provider =>
{
    var settings = provider.GetRequiredService<IOptions<AppSettings>>();
    var factory = provider.GetRequiredService<IHttpClientFactory>();

    return new SearchService(settings.Value.ProviderUrl, factory);
});

builder.Services.AddApiVersioning(options =>
{
    // indicating whether a default version is assumed when a client does
    // does not provide an API version.
    options.AssumeDefaultVersionWhenUnspecified = true;
}).AddApiExplorer(options =>
{
    // Add the versioned API explorer, which also adds IApiVersionDescriptionProvider service
    // note: the specified format code will format the version as "'v'major[.minor][-status]"
    options.GroupNameFormat = "'v'VVV";

    // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
    // can also be used to control the format of the API version in route templates
    options.SubstituteApiVersionInUrl = true;
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.UseHttpsRedirection();

app.Run();