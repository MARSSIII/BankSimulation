using Microsoft.Extensions.Configuration;

namespace Presentation.Extensions;

public static class ConfigurationInitializer
{
    public static IConfigurationRoot InitializeConfiguration()
    {
        return new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile(
                "/home/marssiii/RiderProjects/MARSSIII/Presentation/AppSetting.json",
                optional: false,
                reloadOnChange: true)
            .Build();
    }
}