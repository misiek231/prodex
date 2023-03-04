using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components;
using Blazored.LocalStorage;
using System.Net.Http.Headers;
using Blazorise.LoadingIndicator;

namespace Prodex.Client.Services.Auth;

public class CustomMessageHandler : DelegatingHandler
{
    private readonly NavigationManager _navigationManager;
    private readonly ILocalStorageService _localStorage;
    private readonly ILoadingIndicatorService _loadingIndicator;

    public CustomMessageHandler(NavigationManager navigationManager, ILocalStorageService localStorage, ILoadingIndicatorService loadingIndicator)
    {
        _navigationManager = navigationManager;
        _localStorage = localStorage;
        _loadingIndicator = loadingIndicator;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        await _loadingIndicator.Show();
        var token = await _localStorage.GetItemAsStringAsync("token", cancellationToken);
        if (!string.IsNullOrEmpty(token)) 
            request.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);

        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            // TODO: refreshing token
            await _localStorage.RemoveItemAsync("token");
            _navigationManager.NavigateTo("login");
        }
        await _loadingIndicator.Hide();

        return response;
    }
}
