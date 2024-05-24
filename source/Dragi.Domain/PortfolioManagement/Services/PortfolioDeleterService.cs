using Dragi.Common.Results;
using Dragi.Domain.PortfolioManagement.Repositories;

namespace Dragi.Domain.PortfolioManagement.Services;

public class PortfolioDeleterService
{
    private readonly IPortfolioReaderRepository _portfolioReaderRepository;
    private readonly IPortfolioDeleterRepository _portfolioDeleterRepository;

    public PortfolioDeleterService(
        IPortfolioReaderRepository portfolioReaderRepository,
        IPortfolioDeleterRepository portfolioDeleterRepository)
    {
        _portfolioReaderRepository = portfolioReaderRepository;
        _portfolioDeleterRepository = portfolioDeleterRepository;
    }

    public async Task<Result> DeletePortfolio(string name, CancellationToken cancellationToken)
    {
        var getPortfolioResult = await _portfolioReaderRepository.GetPortfolio(name, cancellationToken);

        if (getPortfolioResult.IsFail)
        {
            return new Result(getPortfolioResult.FailReason);
        }

        var deletePortfolioResult = await _portfolioDeleterRepository.DeletePortfolio(getPortfolioResult.Value, cancellationToken);

        return deletePortfolioResult;
    }
}
