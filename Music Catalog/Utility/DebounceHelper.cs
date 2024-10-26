using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCatalog.Utility;

public class DebounceHelper
{
    private CancellationTokenSource _debounceToken;

    public void Debounce(Action action, int delay)
    {
        _debounceToken?.Cancel();
        _debounceToken = new CancellationTokenSource();
        var token = _debounceToken.Token;
        Task.Delay(delay, token).ContinueWith(t =>
        {
            if (!t.IsCanceled)
            {
                action();
            }
        }, token);
    }
}
