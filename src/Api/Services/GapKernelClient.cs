using Api.Services.Abstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.WebSockets;
using System.Text;

namespace Api.Services;
public class GapKernelClient : IGapKernelClient, IAsyncDisposable
{
    private readonly HttpClient _httpClient;
    private string? _kernelId;
    private ClientWebSocket? _ws;

    public GapKernelClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> ExecuteAsync(string code)
    {
        try
        {
            if (_kernelId == null || _ws == null || _ws.State != WebSocketState.Open)
            {
                await RestartKernel();
            }

            return await SendAndReceive(_ws!, code);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[GapKernelClient] Error: {ex.Message}. Restarting kernel...");
            await RestartKernel();
            return await SendAndReceive(_ws!, code);
        }
    }

    private async Task RestartKernel()
    {
        _kernelId = await StartKernel();
        _ws = await ConnectToKernel(_kernelId);
    }

    private async Task<string> StartKernel()
    {
        var response = await _httpClient.PostAsync("/api/kernels", new StringContent("{}", Encoding.UTF8, "application/json"));
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var obj = JObject.Parse(json);
        return obj["id"]!.ToString();
    }

    private async Task<ClientWebSocket> ConnectToKernel(string kernelId)
    {
        var ws = new ClientWebSocket();
        var wsUrl = _httpClient.BaseAddress!.ToString().Replace("http", "ws") + $"api/kernels/{kernelId}/channels";
        await ws.ConnectAsync(new Uri(wsUrl), CancellationToken.None);
        return ws;
    }

    private async Task<string> SendAndReceive(ClientWebSocket ws, string code)
    {
        var msg = new
        {
            header = new
            {
                msg_id = Guid.NewGuid().ToString(),
                username = "user",
                session = Guid.NewGuid().ToString(),
                msg_type = "execute_request",
                version = "5.0"
            },
            parent_header = new { },
            metadata = new { },
            content = new { code, silent = false }
        };

        var jsonMsg = JsonConvert.SerializeObject(msg);
        await ws.SendAsync(Encoding.UTF8.GetBytes(jsonMsg), WebSocketMessageType.Text, true, CancellationToken.None);

        var buffer = new byte[8192];
        while (true)
        {
            var result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            if (result.MessageType == WebSocketMessageType.Close)
            {
                throw new Exception("WebSocket closed by Jupyter");
            }

            var responseJson = Encoding.UTF8.GetString(buffer, 0, result.Count);
            var obj = JObject.Parse(responseJson);

            if (obj["msg_type"]?.ToString() == "execute_result")
            {
                return obj["content"]?["data"]?["text/plain"]?.ToString() ?? "";
            }
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_ws != null)
        {
            try { await _ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Shutdown", CancellationToken.None); } catch { }
            _ws.Dispose();
        }
    }
}
