namespace Dragi.Common.Utilities;

public class GaussianRandomGenerator
{
    private readonly Random _random;

    public GaussianRandomGenerator()
    {
        _random = new Random();
    }

    /// <summary>
    ///
    /// </summary>
    /// <remarks>
    /// https://stackoverflow.com/questions/218060/random-gaussian-variables
    /// </remarks>
    /// <param name="seed"></param>
    /// <returns></returns>
    public double GetRandomNormalDistribution(double mean, double standardDeviation)
    {
        // uniform(0,1] random doubles
        var u1 = 1.0 - _random.NextDouble();
        var u2 = 1.0 - _random.NextDouble();

        // random normal(0,1)
        var randStandardNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);

        //random normal(mean,stdDev^2)
        var randomNormal = mean + standardDeviation * randStandardNormal;

        return randomNormal;
    }
}
