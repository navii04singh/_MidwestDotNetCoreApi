using Microsoft.EntityFrameworkCore;
using Midwest.Data;
using Midwest.Repositories;
using Midwest.services;
using PayPal.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MidWestDBContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("MidWestConnectionString")));


builder.Services.AddScoped<IPaypalService, PayPalService>();
builder.Services.AddScoped<ILicenseService, LicenseService>();
builder.Services.AddScoped<LicenseValidationService>();

builder.Services.AddScoped<LicenseService>();


var app = builder.Build();
// In the Configure method

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();

app.UseCors(builder => builder
    .WithOrigins("http://localhost:4200") // Add your Angular app's URL here
    .AllowAnyHeader()
    .AllowAnyMethod()
        .AllowCredentials() // Allow credentials like cookies to be sent with the request

);



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();