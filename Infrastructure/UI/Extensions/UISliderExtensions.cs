namespace SexyFishHorse.CitiesSkylines.Infrastructure.UI.Extensions
{
    using ColossalFramework.UI;
    using Validation.Arguments;

    public static class UISliderExtensions
    {
        public static string GetLabelText(this UISlider slider)
        {
            var panel = slider.GetComponentInParent<UIPanel>();
            var uiLabel = panel.Find<UILabel>("Label");

            return uiLabel.text;
        }

        public static void SetLabelText(this UISlider slider, string label)
        {
            label.ShouldNotBeNullOrEmpty("label");

            var panel = slider.GetComponentInParent<UIPanel>();
            var uiLabel = panel.Find<UILabel>("Label");

            uiLabel.text = label;
        }
    }
}
