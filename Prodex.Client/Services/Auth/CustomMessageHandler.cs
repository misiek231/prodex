using Blazored.LocalStorage;
using Blazorise.LoadingIndicator;
using Microsoft.AspNetCore.Components;
using Prodex.Client.Services.Snackbar;
using System.Net.Http.Headers;

namespace Prodex.Client.Services.Auth;

public class CustomMessageHandler(NavigationManager navigationManager, ILocalStorageService localStorage, ILoadingIndicatorService loadingIndicator, SnackbarService snackbarService) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        await loadingIndicator.Show();
        var token = await localStorage.GetItemAsStringAsync("token", cancellationToken);
        if (!string.IsNullOrEmpty(token))
            request.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);

        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            // TODO: refreshing token
            await localStorage.RemoveItemAsync("token", cancellationToken);
            navigationManager.NavigateTo("login");
        }
        await loadingIndicator.Hide();

        if(request.Method == HttpMethod.Post || request.Method == HttpMethod.Put || request.Method == HttpMethod.Delete) 
        {
            if(response.IsSuccessStatusCode)
                await snackbarService.PushAsync("Akcja zakończona pomyślnie", Blazorise.Snackbar.SnackbarColor.Success);
            else
                await snackbarService.PushAsync("Wystąpił błąd. Spróbuj ponownie lub skontaktuj się z administratorem systemu", Blazorise.Snackbar.SnackbarColor.Danger);
        }

        return response;
    }
}
