using Microsoft.EntityFrameworkCore;

using APIWeather.Controllers;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddSwaggerGen(c => {
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    c.IgnoreObsoleteActions();
    c.IgnoreObsoleteProperties();
    c.CustomSchemaIds(type => type.FullName);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//
builder.Services.AddCors();


//builder.Services.AddScoped<WeatherAPIController>();
//builder.Services.Add(new WeatherAPIController(new WebApplication2.Controllers.WeatherController(new )))

var app = builder.Build();

// Configure the HTTP request pipeline.

//
app.UseCors(options =>
options.WithOrigins("http://localhost:4201")
.AllowAnyMethod()
.AllowAnyHeader()
);

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
