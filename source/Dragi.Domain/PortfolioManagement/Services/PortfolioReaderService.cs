using Dragi.Common.Results;
using Dragi.Domain.PortfolioManagement.Models;
using Dragi.Domain.PortfolioManagement.Repositories;

namespace Dragi.Domain.PortfolioManagement.Services;

public class PortfolioReaderService
{
    private readonly IPortfolioReaderRepository _portfolioReaderRepository;

    public PortfolioReaderService(IPortfolioReaderRepository portfolioReaderRepository)
    {
        _portfolioReaderRepository = portfolioReaderRepository;
    }

    public async Task<Result<Portfolio>> GetPortfolio(string name, CancellationToken cancellationToken)
    {
        var getPortfolioResult = await _portfolioReaderRepository.GetPortfolio(name, cancellationToken);

        return getPortfolioResult;
    }
}
