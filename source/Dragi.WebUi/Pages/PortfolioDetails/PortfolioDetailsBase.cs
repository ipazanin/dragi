using Dragi.Domain.PortfolioManagement.Models;
using Dragi.Domain.PortfolioManagement.Services;
using Dragi.WebUi.Extensions;
using Dragi.WebUi.Shared;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dragi.WebUi.Pages.PortfolioDetails;

public class PortfolioDetailsBase : CancellableComponentBase
{
    [Parameter]
    public required string Name { get; init; }

    protected Portfolio? Portfolio { get; set; }

    [Inject]
    private PortfolioReaderService PortfolioReaderService { get; init; } = null!;

    [Inject]
    private ISnackbar Snackbar { get; init; } = null!;

    protected override async Task OnInitializedAsync()
    {
        var getPortfolioResult = await PortfolioReaderService.GetPortfolio(Name, _cancellationTokenSource.Token);

        if (getPortfolioResult.IsFail)
        {
            Snackbar.ShowResult(getPortfolioResult);
        }
        else
        {
            Portfolio = getPortfolioResult.Value;
        }

        await base.OnInitializedAsync();
    }

}
