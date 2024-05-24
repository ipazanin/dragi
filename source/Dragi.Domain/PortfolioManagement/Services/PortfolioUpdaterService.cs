using Dragi.Common.Results;
using Dragi.Domain.PortfolioManagement.DataObject;
using Dragi.Domain.PortfolioManagement.Models;
using Dragi.Domain.PortfolioManagement.Repositories;
using Dragi.Domain.PortfolioManagement.Validators;

namespace Dragi.Domain.PortfolioManagement.Services;

public class PortfolioUpdaterService
{
    private readonly IPortfolioUpdaterRepository _portfolioUpdaterRepository;

    public PortfolioUpdaterService(
        IPortfolioUpdaterRepository portfolioUpdaterRepository,
        IPortfolioReaderRepository portfolioReaderRepository)
    {
        _portfolioUpdaterRepository = portfolioUpdaterRepository;
    }

    public async Task<Result<Portfolio>> UpdatePortfolio(UpdatePortfolioData updatePortfolioData, CancellationToken cancellationToken)
    {
        var invalidReasons = PortfolioValidator.IsValid(updatePortfolioData);

        if (invalidReasons.Count != 0)
        {
            return new Result<Portfolio>(invalidReasons);
        }

        throw new NotImplementedException();
    }
}
