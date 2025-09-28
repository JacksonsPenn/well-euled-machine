namespace Api.Services.Abstractions;

public interface IGapKernelClient
{
    Task<string> ExecuteAsync(string code);
}
