namespace SexyFishHorse.CitiesSkylines.Infrastructure.Configuration
{
    using ICities;
    using JetBrains.Annotations;

    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public interface IModWithOptionsPanel
    {
        // ReSharper disable once InconsistentNaming
        void OnSettingsUI(UIHelperBase uiHelper);
    }
}
