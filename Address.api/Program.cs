
using Address.Infrastructure.Data.Context;
using Address.Infrastructure.Data.Repositories;
using Address.Infrastructure.Data.Repositories.Interface;
using Address.Infrastructure.ExternalServices.Clients.ViaCep;
using Address.Service.External;
using Address.Service.External.Interface;
using Address.Service.Repositories;
using Address.Service.Repositories.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Context
builder.Services.AddDbContext<Context>();
#endregion


builder.Services.AddTransient<IAddressRepository, AddressRepository>();
builder.Services.AddTransient<IAddressService, AddressService>();

builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
builder.Services.AddTransient<ICustomerService, CustomerService>();

builder.Services.AddHttpClient<IViaCepRepository, ViaCepRepository>();
builder.Services.AddTransient<IViaCepExternalService, ViaCepExternalService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
