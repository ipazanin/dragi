using Dragi.Common.Results;
using Dragi.Domain.PortfolioManagement.Models;
using Dragi.Domain.PortfolioManagement.Repositories;

namespace Dragi.Persistence.PortfolioManagement;

public class InMemoryTransactionRepository : ITransactionCreatorRepository, ITransactionReaderRepository
{
    private readonly Dictionary<int, Transaction> _transactionsDictionary = new();

    public Task<Result<Transaction>> CreateTransaction(Transaction transaction, CancellationToken cancellationToken)
    {
        if (transaction.Id == default)
        {
            var uniqueId = _transactionsDictionary.Values.Any() switch
            {
                true => _transactionsDictionary.Values.Max(transaction => transaction.Id) + 1,
                false => 1,
            };

            var updatedTransaction = transaction with { Id = uniqueId };
            _transactionsDictionary.Add(updatedTransaction.Id, updatedTransaction);
            return Task.FromResult(new Result<Transaction>(updatedTransaction));
        }

        if (_transactionsDictionary.ContainsKey(transaction.Id))
        {
            return Task.FromResult(new Result<Transaction>($"Transaction with ID {transaction.Id} already exists!"));
        }

        _transactionsDictionary[transaction.Id] = transaction;

        return Task.FromResult(new Result<Transaction>(transaction));
    }

    public Task<Result<Transaction>> GetTransaction(int transactionId, CancellationToken cancellationToken)
    {
        var getTransactionResult = _transactionsDictionary.TryGetValue(transactionId, out var transaction) switch
        {
            true => new Result<Transaction>(transaction),
            false => new Result<Transaction>($"Transaction with ID {transactionId} was not found!"),
        };

        return Task.FromResult(getTransactionResult);
    }
}
