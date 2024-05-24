using Microsoft.AspNetCore.Components;

namespace Dragi.WebUi.Shared;

public abstract class CancellableComponentBase : ComponentBase, IDisposable
{
    private bool _disposedValue;

    protected CancellationTokenSource _cancellationTokenSource = new();

    protected virtual void Dispose(bool disposing)
    {
        if (_disposedValue)
        {
            return;
        }

        if (disposing)
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
        }

        _disposedValue = true;
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
