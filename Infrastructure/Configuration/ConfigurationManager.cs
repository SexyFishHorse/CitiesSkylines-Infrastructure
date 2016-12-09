namespace SexyFishHorse.CitiesSkylines.Infrastructure.Configuration
{
    using System.Collections.Generic;
    using System.Linq;

    public class ConfigurationManager
    {
        private readonly IConfigStore configStore;

        private ModConfiguration configuration;

        public ConfigurationManager(string modName)
        {
            configStore = new ConfigStore(modName, modName + ".xml");
        }

        public T GetSetting<T>(string settingKey)
        {
            EnsureConfigLoaded();

            if (configuration.Settings.Any(x => x.Key == settingKey))
            {
                return (T)configuration.Settings.Single(x => x.Key == settingKey).Value;
            }

            return default(T);
        }

        public void SaveSetting(string settingKey, object value)
        {
            EnsureConfigLoaded();

            if (configuration.Settings.Any(x => x.Key == settingKey))
            {
                configuration.Settings.Remove(configuration.Settings.Single(x => x.Key == settingKey));
            }

            var newKeyValuePair = new KeyValuePair<string, object>(settingKey, value);
            configuration.Settings.Add(newKeyValuePair);

            configStore.SaveConfigToFile(configuration);
        }

        private void EnsureConfigLoaded()
        {
            if (configuration == null)
            {
                configuration = configStore.LoadConfigFromFile();
            }
        }

        public void Migrate<T>(string oldSettingKey, string newSettingKey)
        {
            EnsureConfigLoaded();

            if (configuration.Settings.All(x => x.Key != oldSettingKey))
            {
                return;
            }

            var settingValue = configuration.Settings.Single(x => x.Key == oldSettingKey);

            RemoveSetting(oldSettingKey);
            SaveSetting(newSettingKey, (T)settingValue.Value);

            configStore.SaveConfigToFile(configuration);
        }

        private void RemoveSetting(string settingKey)
        {
            var setting = configuration.Settings.FirstOrDefault(x => x.Key == settingKey);
            configuration.Settings.Remove(setting);

            configStore.SaveConfigToFile(configuration);
        }
    }
}