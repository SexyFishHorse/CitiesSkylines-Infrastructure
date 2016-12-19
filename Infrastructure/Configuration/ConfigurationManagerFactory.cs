namespace SexyFishHorse.CitiesSkylines.Infrastructure.Configuration
{
    using System.Collections.Generic;

    public static class ConfigurationManagerFactory
    {
        private static readonly IDictionary<string, ConfigurationManager> Managers = new Dictionary<string, ConfigurationManager>();

        public static ConfigurationManager GetOrCreateConfigurationManager(string modName)
        {
            if (!Managers.ContainsKey(modName))
            {
                Managers.Add(new KeyValuePair<string, ConfigurationManager>(modName, ConfigurationManager.Create(modName)));
            }

            return Managers[modName];
        }
    }
}
