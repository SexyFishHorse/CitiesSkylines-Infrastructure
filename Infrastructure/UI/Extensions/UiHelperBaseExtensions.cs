namespace SexyFishHorse.CitiesSkylines.Infrastructure.UI.Extensions
{
    using System.Reflection;
    using ColossalFramework.UI;
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

        [NotNull]
        public static UILabel AddLabel(this IStronglyTypedUiHelper uiHelper, string text)
        {
            var field = uiHelper.UiHelper.GetType().GetField("m_Root", BindingFlags.NonPublic | BindingFlags.Instance);
            var root = (UIComponent)field.GetValue(uiHelper.UiHelper);

            var label = root.AddUIComponent<UILabel>();
            label.text = text;

            return label;
        }
    }
}
