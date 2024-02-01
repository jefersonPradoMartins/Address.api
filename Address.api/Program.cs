using Address.api.MappingProfiles;
using Address.api.Swagger;
using Address.Infrastructure.Data.Repositories;
using Address.Infrastructure.Data.Repositories.Interface;
using Address.Infrastructure.ExternalServices.Clients.ViaCep;
using Address.Service.External;
using Address.Service.External.Interface;
using Address.Service.Repositories;
using Address.Service.Repositories.Interface;
using Asp.Versioning;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Timeout;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddControllers().AddJsonOptions(x =>
//   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve ) ;

#region Swagger
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen(options =>
{
    // Add a custom operation filter which sets default values
    options.OperationFilter<SwaggerDefaultValues>();

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});
#endregion

#region AutoMapper
builder.Services.AddAutoMapper(typeof(ResponseToDomain));
#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#region Context
builder.Services.AddDbContext<Address.Infrastructure.Data.Context.Context>();
#endregion

#region ApiVersion

builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = ApiVersion.Default; //new ApiVersion(1, 0);
    options.ReportApiVersions = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new QueryStringApiVersionReader("api-version"),
        new HeaderApiVersionReader("api-version"),
        new UrlSegmentApiVersionReader());
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'V";
    options.SubstituteApiVersionInUrl = true;
});
#endregion


// Define a generic resilience pipeline
// First parameter is the type of key, second one is the type of the results the generic pipeline works with
builder.Services.AddResiliencePipeline<string, HttpResponseMessage>("my-pipeline", builder =>
{
    builder.AddRetry(new()
    {
        MaxRetryAttempts = 2,
        BackoffType = DelayBackoffType.Exponential,
        ShouldHandle = new PredicateBuilder<HttpResponseMessage>()
            .Handle<HttpRequestException>()
            .Handle<TimeoutRejectedException>()
            // .HandleResult(response => response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            .HandleResult(response => !response.IsSuccessStatusCode)
    }).AddTimeout(TimeSpan.FromSeconds(50));
});




#region Circuit Breaker Polly

#endregion

builder.Services.AddTransient<IAddressRepository, AddressRepository>();
builder.Services.AddTransient<IAddressService, AddressService>();

builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
builder.Services.AddTransient<ICustomerService, CustomerService>();

builder.Services.AddHttpClient<IViaCepRepository, ViaCepRepository>();
builder.Services.AddTransient<IViaCepExternalService, ViaCepExternalService>();

builder.Services.AddStackExchangeRedisOutputCache(

    options =>
    {
        options.Configuration = builder.Configuration.GetConnectionString("Redis");
        options.InstanceName = "intelligent_nightingale";
    });

builder.Services.AddOutputCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        var descriptions = app.DescribeApiVersions();

        // Build a swagger endpoint for each discovered API version
        foreach (var description in descriptions)
        {
            var url = $"/swagger/{description.GroupName}/swagger.json";
            var name = description.GroupName.ToUpperInvariant();
            options.SwaggerEndpoint(url, name);
        }
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseOutputCache();

app.Run();
