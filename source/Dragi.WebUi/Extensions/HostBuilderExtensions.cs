using Dragi.Application.MarketData.Configurations;
using Dragi.Application.MarketData.Services;
using Dragi.Domain.MarketData.Services;
using Dragi.Domain.PortfolioManagement.Repositories;
using Dragi.Domain.PortfolioManagement.Services;
using Dragi.Persistence.PortfolioManagement;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;

namespace Dragi.WebUi.Extensions;

public static class HostBuilderExtensions
{
    public static void AddServices(this WebAssemblyHostBuilder builder)
    {
        AddUtilityServices(builder);
        AddUiServices(builder);
        AddConfigurationServices(builder);
        AddDomainServices(builder);
        AddPersistentServices(builder);
    }

    private static void AddUtilityServices(WebAssemblyHostBuilder builder)
    {
        builder.Services.AddScoped(serviceProvider => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
    }

    private static void AddUiServices(WebAssemblyHostBuilder builder)
    {
        builder.Services.AddMudServices();
    }

    private static void AddConfigurationServices(WebAssemblyHostBuilder builder)
    {
        builder.Services.AddSingleton<PolygonConfiguration>();
        builder.Services.AddSingleton<AlphaVantageConfiguration>();
    }

    private static void AddDomainServices(WebAssemblyHostBuilder builder)
    {
        builder.Services.AddScoped<IMarketDataFetcherService, PolygonMarketDataFetcherService>();

        builder.Services.AddScoped<AssetCreatorService>();
        builder.Services.AddScoped<AssetReaderService>();

        builder.Services.AddScoped<PortfolioCreatorService>();
        builder.Services.AddScoped<PortfolioDeleterService>();
        builder.Services.AddScoped<PortfolioReaderService>();
        builder.Services.AddScoped<PortfolioUpdaterService>();

        builder.Services.AddScoped<TransactionCreatorService>();
    }

    private static void AddPersistentServices(WebAssemblyHostBuilder builder)
    {
        builder.Services.AddScoped<IAssetCreatorRepository, InMemoryAssetRepository>();
        builder.Services.AddScoped<IAssetReaderRepository, InMemoryAssetRepository>();

        builder.Services.AddScoped<ITransactionCreatorRepository, InMemoryTransactionRepository>();
        builder.Services.AddScoped<ITransactionReaderRepository, InMemoryTransactionRepository>();

        builder.Services.AddScoped<IPortfolioCreatorRepository, InMemoryPortfolioRepository>();
        builder.Services.AddScoped<IPortfolioDeleterRepository, InMemoryPortfolioRepository>();
        builder.Services.AddScoped<IPortfolioUpdaterRepository, InMemoryPortfolioRepository>();
        builder.Services.AddScoped<IPortfolioReaderRepository, InMemoryPortfolioRepository>();
    }
}
