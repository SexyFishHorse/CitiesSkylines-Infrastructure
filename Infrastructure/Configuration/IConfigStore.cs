namespace SexyFishHorse.CitiesSkylines.Infrastructure.Configuration
{
    using System.IO;
    using JetBrains.Annotations;

    [UsedImplicitly(ImplicitUseTargetFlags.Members)]
    public interface IConfigStore
    {
        [NotNull]
        FileInfo ConfigFileInfo { get; }

        [CanBeNull]
        T GetSetting<T>(string key);

        void RemoveSetting(string key);

        void SaveSetting<T>(string key, T value);

        bool HasSetting(string key);

        ModConfiguration LoadConfigFromFile();

        void SaveConfigToFile([NotNull] ModConfiguration modConfiguration);
    }
}
