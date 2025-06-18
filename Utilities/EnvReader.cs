using DotNetEnv;
using System;

public static class EnvReader
{
    static EnvReader()
    {
        try
        {
            // Load .env file from the current directory (project root)
            Env.Load();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error loading .env file: " + ex.Message);
            throw;
        }
    }

    public static string BaseUrl => GetEnvValue("BASE_URL");
    public static string Url => GetEnvValue("URL");
    public static string Username => GetEnvValue("USERNAME");
    public static string Password => GetEnvValue("PASSWORD");
    public static string ReportingMail => GetEnvValue("REPORTINGMAIL");
    public static string ReportingPassword => GetEnvValue("REPORTINGPASSWORD");


    private static string GetEnvValue(string key)
    {
        var value = Env.GetString(key);
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new Exception($"Environment variable '{key}' is missing or empty in .env file.");
        }
        return value;
    }
}
