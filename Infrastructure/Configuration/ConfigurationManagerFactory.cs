namespace SexyFishHorse.CitiesSkylines.Infrastructure.Configuration
{
    using System.Collections.Generic;
    using JetBrains.Annotations;

    [PublicAPI]
    public static class ConfigurationManagerFactory
    {
        private static readonly IDictionary<string, IConfigurationManager> Managers =
            new Dictionary<string, IConfigurationManager>();

        [PublicAPI]
        public static IConfigurationManager GetOrCreateConfigurationManager(string modName)
        {
            if (!Managers.ContainsKey(modName))
            {
                Managers.Add(new KeyValuePair<string, IConfigurationManager>(modName, ConfigurationManager.Create(modName)));
            }

            return Managers[modName];
        }
    }
}
