using Blazorise.Snackbar;
using Prodex.Client.DiHelpers;

namespace Prodex.Client.Services.Snackbar;


[RegisterSingleton]
public class SnackbarService
{
    private Func<string, SnackbarColor, Task> _handler;

    public void Init(Func<string, SnackbarColor, Task> action)
    {
        _handler = action;
    }

    public async Task PushAsync(string message, SnackbarColor color)
    {
        await _handler(message, color);
    }
}