using Dragi.Common.Results;
using MudBlazor;

namespace Dragi.WebUi.Extensions;

public static class SnackBarExtensions
{
    public static void ShowResult(this ISnackbar snackbar, Result result)
    {
        if (result.IsSuccess)
        {
            return;
        }

        snackbar.Add(message: result.FailReason, severity: Severity.Error);
    }
}
