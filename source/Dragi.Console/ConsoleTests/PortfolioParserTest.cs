using Dragi.Application.MarketData.Configurations;
using Dragi.Application.MarketData.Services;
using Dragi.Application.PortfolioManagement.Services;
using Dragi.Domain.PortfolioManagement.Services;
using Dragi.Domain.PortfolioManagement.Validators;
using Dragi.Persistence.PortfolioManagement;

namespace Dragi.Console.ConsoleTests;

public static class PortfolioParserTest
{
    public static async Task TestPortfolioParsing()
    {
        var inMemoryPortfolioRepository = new InMemoryPortfolioRepository();
        var inMemoryAssetRepository = new InMemoryAssetRepository();
        var inMemoryTransactionRepository = new InMemoryTransactionRepository();
        var portfolioCreatorService = new PortfolioCreatorService(
            portfolioCreatorRepository: inMemoryPortfolioRepository,
            portfolioReaderRepository: inMemoryPortfolioRepository);
        var polygonMarketDataFetcherService = new PolygonMarketDataFetcherService(
            polygonConfiguration: new PolygonConfiguration
            {
                ApiKey = Environment.GetEnvironmentVariable("POLYGONCONFIGURATION__APIKEY")!,
            },
            httpClientFactory: new ConsoleHttpClientFactory());
        var assetValidator = new AssetValidator(polygonMarketDataFetcherService);
        var assetCreatorService = new AssetCreatorService(
            assetCreatorRepository: inMemoryAssetRepository,
            marketDataFetcherService: polygonMarketDataFetcherService,
            assetValidator: assetValidator);
        var assetReaderService = new AssetReaderService(assetReaderRepository: inMemoryAssetRepository);
        var transactionCreatorService = new TransactionCreatorService(
            transactionCreatorRepository: inMemoryTransactionRepository,
            assetReaderService: assetReaderService);

        var yahooFinancePortfolioParserService = new YahooFinancePortfolioParser(
            columnDelimiter: ',',
            assetCreatorService: assetCreatorService,
            assetReaderService: assetReaderService,
            transactionCreatorService: transactionCreatorService,
            portfolioCreatorService: portfolioCreatorService);

        var parsePortfolioResult = await yahooFinancePortfolioParserService.ParsePortfolio("stocks", "/Users/ivan.pazanin/Downloads/portfolio.csv", default);

        if (parsePortfolioResult.IsFail)
        {
            System.Console.WriteLine(parsePortfolioResult.FailReason);
        }
    }
}
