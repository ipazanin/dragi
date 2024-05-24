
using Dragi.Common.Results;
using Dragi.Domain.MarketData.Models;
using Dragi.Domain.PortfolioManagement.DataObject;
using Dragi.Domain.PortfolioManagement.Models;
using Dragi.Domain.PortfolioManagement.Services;

namespace Dragi.Application.PortfolioManagement.Services;

public class YahooFinancePortfolioParser
{
    private const int NumberOfColumns = 16;

    private const string CashSymbol = "$$CASH";
    private const string TradeDateFormat = "yyyyMMdd";

    private const int SymbolIndex = 0;
    private const int TradeDateIndex = 9;
    private const int PriceIndex = 10;
    private const int QuantityIndex = 11;

    private readonly char _columnDelimiter;
    private readonly AssetCreatorService _assetCreatorService;
    private readonly AssetReaderService _assetReaderService;
    private readonly TransactionCreatorService _transactionCreatorService;
    private readonly PortfolioCreatorService _portfolioCreatorService;

    public YahooFinancePortfolioParser(
        char columnDelimiter,
        AssetCreatorService assetCreatorService,
        AssetReaderService assetReaderService,
        TransactionCreatorService transactionCreatorService,
        PortfolioCreatorService portfolioCreatorService)
    {
        _columnDelimiter = columnDelimiter;
        _assetCreatorService = assetCreatorService;
        _assetReaderService = assetReaderService;
        _transactionCreatorService = transactionCreatorService;
        _portfolioCreatorService = portfolioCreatorService;
    }

    public async Task<Result> ParsePortfolio(
        string portfolioName,
        string pathToExportedFile,
        CancellationToken cancellationToken)
    {
        var createPortfolioData = new CreatePortfolioData
        {
            Name = portfolioName
        };

        var createPortfolioResult = await _portfolioCreatorService.CreatePortfolio(createPortfolioData, cancellationToken);
        if (createPortfolioResult.IsFail)
        {
            return createPortfolioResult.FailReason;
        }

        var portfolio = createPortfolioResult.Value;

        using var exportedFile = File.OpenRead(pathToExportedFile);
        using var reader = new StreamReader(exportedFile);

        var errors = new List<string>();

        // skip header line
        var line = await reader.ReadLineAsync(cancellationToken);
        var lineCount = 1;

        while ((line = await reader.ReadLineAsync(cancellationToken)) != null)
        {
            lineCount++;

            var columns = line.Split(_columnDelimiter);

            if (columns.Length != NumberOfColumns)
            {
                errors.Add($"{lineCount}: Invalid number of columns");
                continue;
            }

            var symbol = columns[SymbolIndex];

            if (symbol is CashSymbol)
            {
                var parseCashResult = ParseCash(columns);

                if (parseCashResult.IsSuccess)
                {
                    portfolio.DepositCash(parseCashResult.Value);
                }
                else
                {
                    errors.Add($"{lineCount}: {parseCashResult.FailReason}");
                }

                continue;
            }

            var parseTransactionResult = await ParseTransaction(columns, cancellationToken);

            if (parseTransactionResult.IsFail)
            {
                errors.Add($"{lineCount}: {parseTransactionResult.FailReason}");
                continue;
            }

            var transaction = parseTransactionResult.Value;

            portfolio.AddTransaction(transaction);
        }

        return errors.Count switch
        {
            0 => Result.Success,
            _ => new Result(errors),
        };
    }

    private async Task<Result<Transaction>> ParseTransaction(IReadOnlyList<string> columns, CancellationToken cancellationToken)
    {
        var createAssetResult = await ParseAsset(columns, cancellationToken);

        if (createAssetResult.IsFail)
        {
            return createAssetResult.FailReason;
        }

        var asset = createAssetResult.Value;

        var getTradeDateResult = GetTradeDate(columns);
        if (getTradeDateResult.IsFail)
        {
            return getTradeDateResult.FailReason;
        }

        var getPurchasePriceResult = GetPurchasePrice(columns);
        if (getPurchasePriceResult.IsFail)
        {
            return getPurchasePriceResult.FailReason;
        }

        var getNumberOfUnitsResult = GetNumberOfUnits(columns);
        if (getNumberOfUnitsResult.IsFail)
        {
            return getNumberOfUnitsResult.FailReason;
        }

        var price = new Price
        {
            Timestamp = getTradeDateResult.Value,
            Value = getPurchasePriceResult.Value,
        };

        var createTransactionData = new CreateTransactionData
        {
            AssetTicker = asset.Ticker,
            Price = price,
            NumberOfUnits = getNumberOfUnitsResult.Value,
        };

        var createTransactionResult = await _transactionCreatorService.CreateTransaction(createTransactionData, cancellationToken);

        return createTransactionResult;
    }

    private async Task<Result<Asset>> ParseAsset(IReadOnlyList<string> columns, CancellationToken cancellationToken)
    {
        var ticker = columns[SymbolIndex];

        var createAssetData = new CreateAssetData
        {
            Ticker = ticker,
        };

        var getAssetResult = await _assetReaderService.GetAsset(ticker, cancellationToken);

        if (getAssetResult.IsSuccess)
        {
            return getAssetResult;
        }

        var createAssetResult = await _assetCreatorService.CreateAsset(createAssetData, cancellationToken);

        return createAssetResult;
    }

    private static Result<DateTimeOffset> GetTradeDate(IReadOnlyList<string> columns)
    {
        var tradeDateColumn = columns[TradeDateIndex];

        var isTradeDateParsed = DateTimeOffset.TryParseExact(
                    input: tradeDateColumn,
                    format: TradeDateFormat,
                    formatProvider: CultureInfo.InvariantCulture,
                    styles: DateTimeStyles.None,
                    result: out var tradeDate);

        if (!isTradeDateParsed)
        {
            return $"Failed to parse trade date {tradeDateColumn} with format {TradeDateFormat} to Date";
        }

        return tradeDate;
    }

    private static Result<decimal> GetPurchasePrice(IReadOnlyList<string> columns)
    {
        var purchasePriceColumn = columns[PriceIndex];

        var isPurchasePriceParsed = decimal.TryParse(
            s: purchasePriceColumn,
            provider: CultureInfo.InvariantCulture,
            result: out var purchasePrice);

        if (!isPurchasePriceParsed)
        {
            return $"Failed to parse purchase price {purchasePriceColumn} to Number";
        }

        return purchasePrice;
    }

    private static Result<decimal> GetNumberOfUnits(IReadOnlyList<string> columns)
    {
        var numberOfUnitsColumn = columns[QuantityIndex];

        var isNumberOfUnitsParsed = decimal.TryParse(numberOfUnitsColumn, CultureInfo.InvariantCulture, out var numberOfUnits);

        if (!isNumberOfUnitsParsed)
        {
            return $"Failed to parse quantity {numberOfUnitsColumn} to Number";
        }

        return numberOfUnits;
    }

    private static Result<decimal> ParseCash(IReadOnlyList<string> columns)
    {
        var cashColumn = columns[QuantityIndex];

        if (!decimal.TryParse(cashColumn, CultureInfo.InvariantCulture, out var cash))
        {
            return $"Failed to parse {cashColumn} to number";
        }

        return cash;
    }
}
