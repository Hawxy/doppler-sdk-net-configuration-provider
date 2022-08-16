using Microsoft.Extensions.Configuration;

namespace DopplerSDK.ConfigurationProvider;

public static class ConfigurationBuilderExtensions
{
    public static IConfigurationBuilder AddDoppler(this IConfigurationBuilder configurationBuilder, Action<DopplerClientConfiguration> configuration)
    {
        var config = new DopplerClientConfiguration();
        configuration(config);
        return configurationBuilder.Add(new DopplerConfigurationSource(config));
    }

}