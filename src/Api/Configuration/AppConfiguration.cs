#nullable enable
using System;
using System.IO;
using System.Reflection;
using Api.Configuration.GitVersion;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Path = System.IO.Path;

namespace Api.Configuration;

#pragma warning disable CS8618
public class AppConfiguration : IAppConfiguration
{
    private const string DefaultEnvironment = "local";

    public AppConfiguration(IConfigurationRoot? config = null, string? configurationName = null)
    {
        CurrentEnvironment = (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? DefaultEnvironment).ToLower();

        ApplicationPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        if (!string.IsNullOrEmpty(configurationName))
            CurrentEnvironment = configurationName!;

        SetParameterStorePath();
        
        if (Configuration != null) return;

        if (config == null)
        {
            InitializeConfiguration();
        }
        else
        {
            Configuration = config;
        }

    }

    public void InitializeConfiguration()
    {
        

        var config = new ConfigurationBuilder()
            .SetBasePath(ApplicationPath ?? string.Empty)
            .AddJsonFile("appsettings.json", true, true)
            .AddJsonFile($"appsettings.{CurrentEnvironment}.json", true, true)
            .AddEnvironmentVariables();

        var logPath = Path.Combine(ApplicationPath ?? string.Empty, "HpLogs.txt");

        if (File.Exists(logPath))
        {
            Console.WriteLine("Purging Log File");
            File.Delete(logPath);
        }


        Configuration = config.Build();

    }

    public IConfigurationRoot Configuration { get; private set; }

    private void SetParameterStorePath()
    {
        if (CurrentEnvironment != DefaultEnvironment)
        {
            ParameterStoreRootPath = Environment.GetEnvironmentVariable("ParameterStoreRootPath") ?? string.Empty;
        }
    }

    private static GitVersionInfo? LoadVersion()
    {
        var rootDirectory = AppDomain.CurrentDomain.BaseDirectory;

        // Specify the path to your JSON file (assuming it's in the root directory)
        var jsonFilePath = Path.Combine(rootDirectory, "version.json");

        // Check if the file exists
        if (File.Exists(jsonFilePath))
        {
            // Read the JSON file contents
            var jsonContent = File.ReadAllText(jsonFilePath);

            // Deserialize the JSON content into a VersionInfo object
            var version = JsonConvert.DeserializeObject<GitVersionInfo>(jsonContent);

            if (version != null)
            {
                Console.WriteLine($"Major: {version.FullSemVer}");
                return version;
            }

        }
        else
        {
            Console.WriteLine("Version JSON file not found.");
        }

        return null;
    }

    public string CurrentEnvironment { get; set; }
    public bool UseParameterStore { get; set; }
    public string SeqUrl { get; set; }
    public string ParameterStoreRootPath { get; set; }
    public bool IsDefaultConfig => CurrentEnvironment.ToLower().Contains(DefaultEnvironment.ToLower());
    public GitVersionInfo? Version => LoadVersion();
    public string? ApplicationPath { get; }
    public Settings Settings => (IsDefaultConfig || !UseParameterStore ?  Configuration.GetSection("ConfigurationSettings").Get<Settings>() :  Configuration.GetSection($"{CurrentEnvironment}:backend").Get<Settings>())!;
}