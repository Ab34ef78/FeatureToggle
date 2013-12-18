using FeatureToggle.Web.Models;
using NFeature;
using NFeature.Configuration;
using NFeature.DefaultImplementations;
using System;
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


        public static IEnumerable<FeatureModel> GetFeatureModel()
        {
            List<FeatureSetting<Feature, DefaultTenantEnum>> featuresConfig = GetFeatureConfig();

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

        public static List<FeatureSetting<Feature, DefaultTenantEnum>> GetFeatureConfig()
        {
            var featureSettingRepo = new AppConfigFeatureSettingRepository<Feature>();

            List<FeatureSetting<Feature, DefaultTenantEnum>> featuresConfig = featureSettingRepo.GetFeatureSettingsFromXML(_FileName).ToList();

            return featuresConfig;
        }

        private static IFeatureManifest<Feature> GetFeatureManifest()
        {
            IFeatureManifest<Feature> featureManifest;

            //Define the availability-checking function
            Func<FeatureSetting<Feature, DefaultTenantEnum>, EmptyArgs, bool> fn =
                                (f, args) =>
                                DefaultFunctions.AvailabilityCheckFunction(f,
                                 Tuple.Create(FeatureVisibilityMode.Normal,
                                 DateTime.Now));

            //Take care of feature manifest initialization       
            var featureSettingRepo = new AppConfigFeatureSettingRepository<Feature>();
            var availabilityChecker = new FeatureSettingAvailabilityChecker<Feature>(fn);

            var featureSettingService =
                    new FeatureSettingService<Feature>(availabilityChecker, featureSettingRepo);
            var manifestCreationStrategy =
                    new ManifestCreationStrategyDefault<Feature>(featureSettingRepo, featureSettingService);

            //use the default
            var featureManifestService = new FeatureManifestService<Feature>(manifestCreationStrategy);

            featureManifest = featureManifestService.GetManifest(_FileName);


            return featureManifest;
        }

        public static bool CheckFeatureAvailability(FeatureSetting<Feature, DefaultTenantEnum> s)
        {
            return s.Feature.IsAvailable(GetFeatureManifest());
        }

        public static bool CheckFeatureAvailability(Feature a)
        {
            return a.IsAvailable(GetFeatureManifest());
        }
        
    }
}
