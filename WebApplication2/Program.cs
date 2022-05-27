using WebApplication2.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication2.WeatherRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//
builder.Services.AddCors();
builder.Services.AddDbContextPool<WeatherContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("WeatherContextConnectionString")));

builder.Services.AddScoped<IWeatherRepository, SqlWeatherData>(); 

var app = builder.Build();

// Configure the HTTP request pipeline.

//
app.UseCors(options => 
options.WithOrigins("http://localhost:4200")
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
