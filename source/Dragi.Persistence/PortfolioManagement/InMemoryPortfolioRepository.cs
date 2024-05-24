using Dragi.Common.Results;
using Dragi.Domain.PortfolioManagement.Models;
using Dragi.Domain.PortfolioManagement.Repositories;

namespace Dragi.Persistence.PortfolioManagement;

public class InMemoryPortfolioRepository : IPortfolioCreatorRepository,
    IPortfolioReaderRepository,
    IPortfolioUpdaterRepository,
    IPortfolioDeleterRepository
{
    private readonly Dictionary<string, Portfolio> _portfoliosDictionary = [];

    public Task<Result<Portfolio>> CreatePortfolio(Portfolio portfolio, CancellationToken cancellationToken)
    {
        if (_portfoliosDictionary.ContainsKey(portfolio.Name))
        {
            return Task.FromResult(new Result<Portfolio>($"Portfolio with name {portfolio.Name} already exists!"));
        }

        _portfoliosDictionary[portfolio.Name] = portfolio;

        return Task.FromResult(new Result<Portfolio>(portfolio));
    }

    public Task<Result<Portfolio>> GetPortfolio(string name, CancellationToken cancellationToken)
    {
        var portfolioSearchResult = _portfoliosDictionary.TryGetValue(name, out var portfolio) switch
        {
            true => new Result<Portfolio>(portfolio),
            false => new Result<Portfolio>($"Portfolio {name} was not found"),
        };

        return Task.FromResult(portfolioSearchResult);
    }

    public Task<Result<IReadOnlyList<Portfolio>>> GetPortfolios(CancellationToken cancellationToken)
    {
        var portfolios = _portfoliosDictionary.Values.ToArray();

        return Task.FromResult(new Result<IReadOnlyList<Portfolio>>(portfolios));
    }

    public Task<Result<Portfolio>> UpdatePortfolio(Portfolio portfolio, CancellationToken cancellationToken)
    {
        _portfoliosDictionary[portfolio.Name] = portfolio;

        return Task.FromResult(new Result<Portfolio>(portfolio));
    }

    public Task<Result> DeletePortfolio(Portfolio portfolio, CancellationToken cancellationToken)
    {
        var isRemoved = _portfoliosDictionary.Remove(portfolio.Name);

        return isRemoved switch
        {
            true => Task.FromResult(Result.Success),
            false => Task.FromResult(new Result($"Portfolio with name {portfolio.Name} cannot be found!")),
        };
    }
}
