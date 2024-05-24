using Dragi.Domain.PortfolioManagement.DataObject;

namespace Dragi.Domain.PortfolioManagement.Validators;

public static class PortfolioValidator
{
    public static IReadOnlyList<string> IsValid(CreatePortfolioData createPortfolioData)
    {
        var invalidReasons = new List<string>();
        ValidateName(invalidReasons, createPortfolioData.Name);

        return invalidReasons;
    }

    public static IReadOnlyList<string> IsValid(UpdatePortfolioData updatePortfolioData)
    {
        var invalidReasons = new List<string>();
        ValidateName(invalidReasons, updatePortfolioData.Name);

        return invalidReasons;
    }

    private static void ValidateName(List<string> invalidReasons, string portfolioName)
    {
        if (string.IsNullOrWhiteSpace(portfolioName))
        {
            invalidReasons.Add("Portfolio name must not be empty!");
            return;
        }

        var isMatch = Regex.IsMatch(portfolioName, "^[a-zA-Z0-9-_]+$");

        if (!isMatch)
        {
            invalidReasons.Add("Portfolio name must only consist of characters: a-z, A-Z, 0-9, - or _");
            return;
        }
    }
}
