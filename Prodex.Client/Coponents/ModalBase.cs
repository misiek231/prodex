using Blazorise;
using Microsoft.AspNetCore.Components;

namespace Prodex.Client.Coponents;

public abstract class ModalBase : ComponentBase
{
    protected abstract Modal ModalRef { get; } 
    protected long? Param { get; private set; }

    public void Show()
    {
        Param = null;
        ModalRef?.Show();
        OnShow();
    }

    public async Task Show(long param)
    {
        Param = param;
        ModalRef?.Show();
        await OnShowWithParam();
    }

    public void Hide()
    {
        ModalRef?.Hide();
    }

    protected virtual Task OnShowWithParam()
    {
        return Task.CompletedTask;
    }

    protected virtual void OnShow()
    { }
}
