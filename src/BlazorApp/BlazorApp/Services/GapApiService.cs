namespace BlazorApp.Services;

public class GapApiService
{
    private readonly HttpClient _http;

    public GapApiService(HttpClient http)
    {
        _http = http;
    }

    public async Task<string> ExecuteAsync(string code)
    {
        var response = await _http.PostAsJsonAsync("/gap/execute", new { Code = code });
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<GapResult>();
        return result?.Output ?? "";
    }
}

public record GapResult(string Output, string Error);
