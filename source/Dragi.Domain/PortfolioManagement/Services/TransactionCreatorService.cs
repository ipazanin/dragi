using Dragi.Common.Results;
using Dragi.Domain.PortfolioManagement.DataObject;
using Dragi.Domain.PortfolioManagement.Models;
using Dragi.Domain.PortfolioManagement.Repositories;

namespace Dragi.Domain.PortfolioManagement.Services;

public class TransactionCreatorService
{
    private readonly ITransactionCreatorRepository _transactionCreatorRepository;
    private readonly AssetReaderService _assetReaderService;

    public TransactionCreatorService(
        ITransactionCreatorRepository transactionCreatorRepository,
        AssetReaderService assetReaderService)
    {
        _transactionCreatorRepository = transactionCreatorRepository;
        _assetReaderService = assetReaderService;
    }

    public async Task<Result<Transaction>> CreateTransaction(CreateTransactionData createTransactionData, CancellationToken cancellationToken)
    {
        var getAssetResult = await _assetReaderService.GetAsset(createTransactionData.AssetTicker, cancellationToken);

        if (getAssetResult.IsFail)
        {
            return getAssetResult.FailReason;
        }

        var transaction = new Transaction
        {
            Id = default,
            Price = createTransactionData.Price,
            NumberOfUnits = createTransactionData.NumberOfUnits,
            AssetTicker = createTransactionData.AssetTicker,
            Asset = getAssetResult.Value,
        };

        var createTransactionResult = await _transactionCreatorRepository.CreateTransaction(transaction, cancellationToken);

        return createTransactionResult;
    }
}
