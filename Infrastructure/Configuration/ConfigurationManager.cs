namespace SexyFishHorse.CitiesSkylines.Infrastructure.Configuration
{
    using System.Collections.Generic;
    using System.Linq;

    public class ConfigurationManager
    {
        private static ConfigurationManager instance;

        public static ConfigurationManager Instance
        {
            get
            {
                return instance ?? (instance = new ConfigurationManager());
            }
            set
            {
                instance = value;
            }
        }

        public void Init(string modName)
        {
            configStore = new ConfigStore(modName);
        }

        private IConfigStore configStore;

        private ModConfiguration configuration;

        private ConfigurationManager()
        {
        }

        public T GetSetting<T>(string settingKey)
        {
            if (configuration == null)
            {
                configuration = configStore.LoadConfigFromFile();
            }

            if (configuration.Settings.Any(x => x.Key == settingKey))
            {
                return (T)configuration.Settings.Single(x => x.Key == settingKey).Value;
            }

            return default(T);
        }

        public void SaveSetting(string settingKey, object value)
        {
            if (configuration.Settings.Any(x => x.Key == settingKey))
            {
                configuration.Settings.Remove(configuration.Settings.Single(x => x.Key == settingKey));
            }

            var newKeyValuePair = new KeyValuePair<string, object>(settingKey, value);
            configuration.Settings.Add(newKeyValuePair);

            configStore.SaveConfigToFile(configuration);
        }
    }
}