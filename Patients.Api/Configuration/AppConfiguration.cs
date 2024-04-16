namespace Patients.Api.Configuration;

public static class AppConfiguration
{
    public static readonly string ConnectionString =
        Environment.GetEnvironmentVariable("CONNECTION_STRING") ?? "mongodb://localhost:27017";
    
    public static readonly string DatabaseName =
        Environment.GetEnvironmentVariable("DATABASE_NAME") ?? "patients";
}