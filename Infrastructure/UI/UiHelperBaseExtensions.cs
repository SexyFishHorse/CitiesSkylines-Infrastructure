namespace SexyFishHorse.CitiesSkylines.Infrastructure.UI
{
    using ICities;
    using JetBrains.Annotations;

    [UsedImplicitly(ImplicitUseTargetFlags.Members)]
    public static class UiHelperBaseExtensions
    {
        [NotNull]
        public static IStronglyTypedUiHelper AsStronglyTyped([NotNull] this UIHelperBase uiHelper)
        {
            return new StronglyTypedUiHelper(uiHelper);
        }
    }
}
