using Dragi.Common.Results;
using Dragi.Domain.PortfolioManagement.Models;

namespace Dragi.Domain.PortfolioManagement.Repositories;

public interface IAssetCreatorRepository
{
    public Task<Result<Asset>> CreateAsset(Asset asset, CancellationToken cancellationToken);
}
