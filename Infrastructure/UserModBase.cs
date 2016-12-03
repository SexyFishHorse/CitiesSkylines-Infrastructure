namespace SexyFishHorse.CitiesSkylines.Infrastructure
{
    using System.Diagnostics.CodeAnalysis;
    using ICities;
    using UI;

    public abstract class UserModBase : IUserMod
    {
        public abstract string Description { get; }

        public abstract string Name { get; }

        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public void OnSettingsUI(UIHelperBase uiHelperBase)
        {
            var uiHelper = uiHelperBase.AsStronglyTyped();

            ConfigureOptionsUi(uiHelper);
        }

        protected virtual void ConfigureOptionsUi(IStronglyTypedUiHelper uiHelper)
        {
        }
    }
}
