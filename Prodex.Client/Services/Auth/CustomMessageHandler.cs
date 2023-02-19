using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components;
using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace Prodex.Client.Services.Auth;

public class CustomMessageHandler : DelegatingHandler
{
    private readonly NavigationManager _navigationManager;
    private readonly ILocalStorageService _localStorage;

    public CustomMessageHandler(NavigationManager navigationManager, ILocalStorageService localStorage)
    {
        _navigationManager = navigationManager;
        _localStorage = localStorage;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {

        var token = await _localStorage.GetItemAsStringAsync("token", cancellationToken);
        if (!string.IsNullOrEmpty(token)) 
            request.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);

        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            // TODO: refreshing token
            _navigationManager.NavigateTo("login");
        }

        return response;
    }
}
