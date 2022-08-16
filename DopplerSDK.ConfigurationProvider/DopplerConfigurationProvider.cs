using Microsoft.Extensions.Configuration;

namespace DopplerSDK.ConfigurationProvider;


internal class DopplerConfigurationSource : IConfigurationSource
{
    private readonly DopplerClientConfiguration _configuration;

    public DopplerConfigurationSource(DopplerClientConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return new DopplerConfigurationProvider(_configuration);
    }

}

internal class DopplerConfigurationProvider : Microsoft.Extensions.Configuration.ConfigurationProvider
{
    private readonly DopplerClientConfiguration _configuration;

    public DopplerConfigurationProvider(DopplerClientConfiguration configuration)
    {
        _configuration = configuration;
    }

    public override void Load()
    {
        using var dopplerClient = new DopplerClient(_configuration);

        var response = dopplerClient.FetchSecretsAsync().GetAwaiter().GetResult();

        if (response.IsSuccess)
        {
            Data = response.Secrets;
        }
    }
}