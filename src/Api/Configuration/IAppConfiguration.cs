#nullable enable
using Api.Configuration.GitVersion;
using Microsoft.Extensions.Configuration;

#pragma warning disable CS8618
namespace Api.Configuration;

public interface IAppConfiguration
{
    void InitializeConfiguration();
    IConfigurationRoot Configuration { get; }
    string CurrentEnvironment { get; set; }
    bool UseParameterStore { get; set; }
    string ParameterStoreRootPath { get; set; }
    bool IsDefaultConfig { get; }
    Settings Settings { get; }
    GitVersionInfo? Version { get; }
    public string? ApplicationPath { get; }

}

