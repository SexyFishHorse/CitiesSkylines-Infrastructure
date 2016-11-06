namespace SexyFishHorse.CitiesSkylines.Infrastructure.Configuration
{
    using System.Collections.Generic;

    public class ModConfiguration
    {
        public ModConfiguration()
        {
            Settings = new List<KeyValuePair<string, object>>();
        }

        public List<KeyValuePair<string, object>> Settings { get; set; }
    }
}
