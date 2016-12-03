namespace SexyFishHorse.CitiesSkylines.Infrastructure.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;
    using IO;
    using Validation.Arguments;

    public class ConfigStore : IConfigStore
    {
        public const string DefaultConfigFileName = "ModConfiguration.xml";

        private XmlSerializer serializer;

        public ConfigStore(string modFolderName, string configFileName = DefaultConfigFileName)
        {
            var modFolderPath = GamePaths.GetModFolderPath(GameFolder.Configs);
            modFolderPath = Path.Combine(modFolderPath, modFolderName);

            Directory.CreateDirectory(modFolderPath);

            ConfigFileInfo = new FileInfo(Path.Combine(modFolderPath, configFileName));

            serializer = new XmlSerializer(typeof(ModConfiguration));

            if (!ConfigFileInfo.Exists)
            {
                SaveConfigToFile(new ModConfiguration());
            }
        }

        public FileInfo ConfigFileInfo { get; private set; }

        public T GetSetting<T>(string key)
        {
            key.ShouldNotBeNull("key");

            var modConfiguration = LoadConfigFromFile();

            if (modConfiguration.Settings.Any(x => x.Key == key))
            {
                return (T) modConfiguration.Settings.Single(x => x.Key == key).Value;
            }

            return default(T);
        }

        public bool HasSetting(string key)
        {
            var config = LoadConfigFromFile();

            return config.Settings.Any(x => x.Key == key);
        }

        public void RemoveSetting(string key)
        {
            key.ShouldNotBeNull("key");

            var config = LoadConfigFromFile();

            config.Settings.Remove(config.Settings.Find(x => x.Key == key));

            SaveConfigToFile(config);
        }

        public void SaveSetting<T>(string key, T value)
        {
            key.ShouldNotBeNull("key");

            if (value == null)
            {
                var message =
                    string.Format(
                        "The configuration value for the key {0} is null. Use RemoveSetting to remove a value",
                        key);

                throw new ArgumentNullException("value", message);
            }

            var modConfiguration = LoadConfigFromFile();

            if (modConfiguration.Settings.Any(x => x.Key == key))
            {
                modConfiguration.Settings.Remove(modConfiguration.Settings.Single(x => x.Key == key));
            }

            modConfiguration.Settings.Add(new KeyValuePair<string, object>(key, value));

            SaveConfigToFile(modConfiguration);
        }

        public ModConfiguration LoadConfigFromFile()
        {
            if (!ConfigFileInfo.Exists)
            {
                return new ModConfiguration();
            }

            using (var fileStream = ConfigFileInfo.OpenRead())
            {
                var config = serializer.Deserialize(fileStream) as ModConfiguration;

                return config ?? new ModConfiguration();
            }
        }

        public void SaveConfigToFile(ModConfiguration modConfiguration)
        {
            using (var fileStream = ConfigFileInfo.Open(FileMode.Create, FileAccess.Write))
            {
                serializer.Serialize(fileStream, modConfiguration);
            }
        }
    }
}
