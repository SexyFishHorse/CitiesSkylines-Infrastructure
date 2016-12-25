namespace SexyFishHorse.CitiesSkylines.Infrastructure
{
    using System.Diagnostics.CodeAnalysis;
    using ICities;
    using JetBrains.Annotations;
    using UI;
    using UI.Configuration;
    using UI.Extensions;

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

        protected void ConfigureOptionsPanel(IStronglyTypedUiHelper uiHelper)
        {
            if (OptionsPanelManager != null)
            {
                OptionsPanelManager.ConfigureOptionsPanel(uiHelper);
            }
        }
    }
}
