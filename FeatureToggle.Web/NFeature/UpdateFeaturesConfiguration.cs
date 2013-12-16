using FeatureToggle.Web.Models;
using NFeature.Configuration;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Configuration;

namespace FeatureToggle.Web.NFeature
{

    public class FeaturesConfiguration
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


        public static IEnumerable<FeatureModel> GetAllFeatures()
        {
            List<FeatureModel> features = new List<FeatureModel>();

            FeatureConfigurationSection<Feature> config = configFile.GetSection("features") as FeatureConfigurationSection<Feature>;

            foreach (FeatureConfigurationElement<Feature, DefaultTenantEnum> f in config.FeatureSettings)
            {
                features.Add(new FeatureModel
                {
                    Name = f.Name,
                    State = f.State,
                    Description = f.Description,
                    EndDtg = f.EndDtg,
                    StartDtg = f.StartDtg,
                    Dependencies = f.Dependencies
                });
            }

            return features;
        }
    }

}
