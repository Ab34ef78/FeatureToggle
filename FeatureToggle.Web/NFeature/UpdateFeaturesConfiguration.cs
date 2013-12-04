using NFeature.Configuration;
using System.Configuration;
using System.Web.Configuration;

namespace FeatureToggle.Web.NFeature
{

    public class UpdateFeaturesConfiguration
    {
        private static Configuration configFile = WebConfigurationManager.OpenWebConfiguration("/");

        public static void SetFeatureState(string featureName, FeatureState state)
        {
            FeatureConfigurationSection<Feature> config = configFile.GetSection("features") as FeatureConfigurationSection<Feature>;

            foreach (FeatureConfigurationElement<Feature, DefaultTenantEnum> f in config.FeatureSettings)
            {
                if (f.Name == featureName)
                {
                    f.State = state;
                }
            }

            configFile.Save();
        }
    }

}
