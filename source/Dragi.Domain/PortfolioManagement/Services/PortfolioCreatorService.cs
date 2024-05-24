using Dragi.Common.Results;
using Dragi.Domain.PortfolioManagement.DataObject;
using Dragi.Domain.PortfolioManagement.Models;
using Dragi.Domain.PortfolioManagement.Repositories;
using Dragi.Domain.PortfolioManagement.Validators;

namespace Dragi.Domain.PortfolioManagement.Services;

public class PortfolioCreatorService
{
    private readonly IPortfolioCreatorRepository _portfolioCreatorRepository;
    private readonly IPortfolioReaderRepository _portfolioReaderRepository;

    public PortfolioCreatorService(
        IPortfolioCreatorRepository portfolioCreatorRepository,
        IPortfolioReaderRepository portfolioReaderRepository)
    {
        _portfolioCreatorRepository = portfolioCreatorRepository;
        _portfolioReaderRepository = portfolioReaderRepository;
    }

    public async Task<Result<Portfolio>> CreatePortfolio(CreatePortfolioData createPortfolioData, CancellationToken cancellationToken)
    {
        var invalidReasons = PortfolioValidator.IsValid(createPortfolioData);

        if (invalidReasons.Count != 0)
        {
            return new Result<Portfolio>(invalidReasons);
        }

        var savedPortfolios = await _portfolioReaderRepository.GetPortfolios(cancellationToken);

        if (savedPortfolios.IsSuccess && savedPortfolios.Value.Any(savedPortfolio => savedPortfolio.Name == createPortfolioData.Name))
        {
            return "Portfolio Name must be unique";
        }

        var portfolioToCreate = new Portfolio(
            createPortfolioData.Name,
            initialCash: 0M,
            assets: []);

        var createdPortfolio = await _portfolioCreatorRepository.CreatePortfolio(portfolioToCreate, cancellationToken);

        return createdPortfolio;
    }
}
