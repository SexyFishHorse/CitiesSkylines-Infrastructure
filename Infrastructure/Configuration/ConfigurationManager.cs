namespace SexyFishHorse.CitiesSkylines.Infrastructure.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using JetBrains.Annotations;
    using SexyFishHorse.CitiesSkylines.Logger;

    public class ConfigurationManager
    {
        private readonly IConfigStore configStore;

        private ModConfiguration configuration;

        public ConfigurationManager(string modName)
        {
            configStore = new ConfigStore(modName, modName + ".xml");
        }

        public ILogger Logger { get; set; }

        public T GetSetting<T>(string settingKey)
        {
            EnsureConfigLoaded();

            if (configuration.Settings.Any(x => x.Key == settingKey))
            {
                var value = configuration.Settings.Single(x => x.Key == settingKey).Value;
                try
                {
                    return (T)value;
                }
                catch (InvalidCastException ex)
                {
                    var message = string.Format(
                        "Tried to cast value '{0}' of type '{1}' to '{2}'.",
                        value,
                        value.GetType().Name,
                        typeof(T).Name);

                    TryLogError(message);

                    throw new InvalidCastException(message, ex);
                }
            }

            TryLog("No setting found for {0}", settingKey);

            return default(T);
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

            TryLog("Migrated {0} to {1}", oldSettingKey, newSettingKey);
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

            TryLog("Saved setting " + settingKey + " with value " + value);
        }

        private void EnsureConfigLoaded()
        {
            if (configuration == null)
            {
                configuration = configStore.LoadConfigFromFile();

                TryLog("Loaded config from file");
            }
        }

        private void RemoveSetting(string settingKey)
        {
            var setting = configuration.Settings.FirstOrDefault(x => x.Key == settingKey);
            configuration.Settings.Remove(setting);

            configStore.SaveConfigToFile(configuration);

            TryLog("Removed setting for {0}", settingKey);
        }

        [StringFormatMethod("format")]
        private void TryLog(string format, params object[] args)
        {
            if (Logger != null)
            {
                Logger.Info(format, args);
            }
        }

        private void TryLogError(string message)
        {
            if (Logger != null)
            {
                Logger.Error(message);
            }
        }
    }
}
