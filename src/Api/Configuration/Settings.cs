using System;
using System.Collections.Generic;

#pragma warning disable CS8618

namespace Api.Configuration;

/// <summary>
///     Global Configuration Class for easier retrieving appsettings
/// </summary>
public class Settings
{
    /// <summary>
    /// AWS Access Key
    /// </summary>
    /// <remarks>
    /// If not set, the default credentials are used.
    /// THIS SHOULD NOT BE SET INSIDE THE AWS ENVIRONMENT
    /// </remarks>
    public string AwsAccessKeyId { get; set; }

    /// <summary>
    /// AWS Secret Access Key
    /// </summary>
    /// <remarks>
    /// If not set, the default credentials are used.
    /// THIS SHOULD NOT BE SET INSIDE THE AWS ENVIRONMENT
    /// </remarks>
    public string AwsSecretAccessKey { get; set; }
    
    public Marten Marten { get; set; }

    public string DefaultTenantId { get; set; }

    public string SeqUrl { get; set; }

    /// <summary>
    /// Collection of AWS Services and their endpoints.
    /// </summary>
    public Dictionary<string, string> AwsServiceEndpoints { get; set; }

    public AwsAppSyncGraphQlConfig AwsAppSyncGraphQlConfig { get; set; }
    public AwsCognitoAuthorization AwsCognitoAuthorization { get; set; }

    //Collection Of AWS Sqs Que
    public List<AwsSqsQueueConfig> AwsSqsQueueConfigs { get; set; }

    public AwsPinpointConfig AwsPinpointConfig { get; set; }
    public TwilioConfig TwilioConfig { get; set; }

    public string InviteUrl { get; set; } 
    
    public SentryOptions SentryOptions { get; set; }

    /// <summary>
    /// Must use the https://learn.microsoft.com/en-us/aspnet/core/test/dev-tunnels?view=aspnetcore-7.0
    /// </summary>
    public string DevTunnelUrl =>  Environment.GetEnvironmentVariable("VS_TUNNEL_URL");
}

public class SentryOptions
{
    public string Dsn { get; set; }
}

public class Marten
{
    public string ConnectionString { get; set; }
}


#region AWS

public class AwsAppSyncGraphQlConfig
{
    public string Url { get; set; }
    public string ApiKey { get; set; }
}

public class AwsCognitoAuthorization
{
    public string ValidIssuer { get; set; }
    public string ClientId { get; set; }
    public string UserPoolId { get; set; }
}

public class AwsSqsQueueConfig
{
    public bool IsFiFoQueue { get; set; }
    public string Name { get; set; }
    public bool IsForSending { get; set; }
    public bool IsForReceiving { get; set; }
}

public class AwsPinpointConfig
{
    public string ApplicationId { get; set; }
   
}

public class TwilioConfig
{ 
    public string AccountSID { get; set; }
    public string AuthToken { get; set; }
    public string FromPhoneNumber { get; set; }
}

#endregion
