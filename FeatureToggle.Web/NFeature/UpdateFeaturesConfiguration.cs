using FeatureToggle.Web.Models;
using NFeature.Configuration;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace FeatureToggle.Web.NFeature
{

    public class FeaturesConfiguration
    {
        private static string _FileName = ConfigurationManager.AppSettings["NFeatureXMLConfig"];

        public static void SetFeatureState(string featureName, FeatureState state)
        {
            var featureSettingRepo = new AppConfigFeatureSettingRepository<Feature>();

            Configuration configFile = ConfigurationManager<FeatureConfigurationSection<Feature, DefaultTenantEnum>>.GetXMLConfig(_FileName);

            FeatureConfigurationSection<Feature> config = configFile.Sections["features"] as FeatureConfigurationSection<Feature>;

            foreach (FeatureConfigurationElement<Feature, DefaultTenantEnum> f in config.FeatureSettings)
            {
                if (f.Name == featureName)
                {
                    f.State = state;
                    break;
                }
            }

            configFile.Save();
        }


        public static IEnumerable<FeatureModel> GetAllFeatures()
        {
            var featureSettingRepo = new AppConfigFeatureSettingRepository<Feature>();

            List<FeatureSetting<Feature, DefaultTenantEnum>> featuresConfig = featureSettingRepo.GetFeatureSettingsFromXML(_FileName).ToList();

            List<FeatureModel> features = featuresConfig.Select(f =>
                 new FeatureModel
                 {
                     Name = f.Feature.ToString(),
                     State = f.FeatureState,
                     EndDtg = f.EndDtg,
                     StartDtg = f.StartDtg,
                     Dependencies = f.Dependencies
                 }).ToList();

            return features;
        }
    }
}
