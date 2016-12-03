namespace SexyFishHorse.CitiesSkylines.Infrastructure
{
    using System.Diagnostics.CodeAnalysis;
    using Configuration;
    using ICities;
    using JetBrains.Annotations;
    using UI;

    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public abstract class UserModBase : IUserMod
    {
        public abstract string Description { get; }

        public abstract string Name { get; }

        protected IOptionsPanelManager OptionsPanelManager { get; set; }

        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public void OnSettingsUI(UIHelperBase uiHelperBase)
        {
            var uiHelper = uiHelperBase.AsStronglyTyped();

            ConfigureOptionsPanel(uiHelper);
        }

        protected virtual void ConfigureOptionsPanel(IStronglyTypedUiHelper uiHelper)
        {
            if (OptionsPanelManager != null)
            {
                OptionsPanelManager.ConfigureOptionsPanel(uiHelper);
            }
        }
    }
}
