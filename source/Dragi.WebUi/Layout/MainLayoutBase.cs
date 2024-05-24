// MainLayoutBase.cs
//
// © 2022 Espresso News. All rights reserved.

using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dragi.WebUi.Layout;

public class MainLayoutBase : LayoutComponentBase
{
    protected bool DrawerOpen { get; set; } = true;

    protected MudTheme Theme { get; } = new();

    protected bool IsDarkMode { get; set; }

    protected void DrawerToggle()
    {
        DrawerOpen = !DrawerOpen;
        StateHasChanged();
    }

    protected void ThemeButtonClicked()
    {
        IsDarkMode = !IsDarkMode;
        StateHasChanged();
    }
}
