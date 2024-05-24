using Dragi.Common.Results;
using Dragi.Domain.PortfolioManagement.DataObject;
using Dragi.Domain.PortfolioManagement.Models;
using Dragi.Domain.PortfolioManagement.Repositories;
using Dragi.Domain.PortfolioManagement.Validators;

namespace Dragi.Domain.PortfolioManagement.Services;

public class PortfolioUpdaterService
{
    public PortfolioUpdaterService(
        IPortfolioUpdaterRepository portfolioUpdaterRepository,
        IPortfolioReaderRepository portfolioReaderRepository)
    {
    }

    public Task<Result<Portfolio>> UpdatePortfolio(UpdatePortfolioData updatePortfolioData, CancellationToken cancellationToken)
    {
        var invalidReasons = PortfolioValidator.IsValid(updatePortfolioData);

        if (invalidReasons.Count != 0)
        {
            return Task.FromResult(new Result<Portfolio>(invalidReasons));
        }

        throw new NotImplementedException();
    }
}
