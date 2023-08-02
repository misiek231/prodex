using System.Net;
using System.Text.Json;

namespace Prodex.Client.RestClients;

public class HttpResponseMessage<TResult> : HttpResponseMessage
{
    public TResult Result { get; set; }

    public HttpResponseMessage(HttpStatusCode code) : base(code) { }
    public HttpResponseMessage(HttpResponseMessage message) : base()
    {
        StatusCode = message.StatusCode;
        Content = message.Content;
    }

    public async Task InitResultAsync()
    {
        Result = JsonSerializer.Deserialize<TResult>(await Content.ReadAsStringAsync(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }
}