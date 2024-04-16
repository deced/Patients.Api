using Microsoft.OpenApi.Models;
using Patients.Api.Data.Repository;
using Patients.Api.Middlewares;
using Patients.Api.Services;
using Patients.Api.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(MongoRepository<>));
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo()
        {
            Title = "Patients Api",
            Version = "v1"
        }
    );

    var filePath = Path.Combine(System.AppContext.BaseDirectory, "Patients.Api.xml");
    c.IncludeXmlComments(filePath);
});

var app = builder.Build();

app.MapGet("/health-check", () => "ok");
app.UseSwagger();
app.UseSwaggerUI();

app.UseCustomExceptionHandler();
app.MapControllers();
app.Run();