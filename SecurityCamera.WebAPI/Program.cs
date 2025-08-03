using SecurityCamera.Infrastructure.IoC;
using SecurityCamera.WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddCors(policy => policy.AddPolicy("MyPolicy", builder =>
{
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));

var csvPath = builder.Configuration.GetValue<string>("CsvSettings:CsvPath");
builder.Services.AddInfrastructureServices(csvPath);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseCors("MyPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
