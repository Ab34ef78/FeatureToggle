using NFeature.Configuration;
using System;

namespace FeatureToggle.Web.Models
{
    public class FeatureModel
    {
        public FeatureToggle.Web.MyFeature.Feature Feature { get; set; }
        public string Name  { get; set; }
        public FeatureState State  { get; set; }
        public FeatureToggle.Web.MyFeature.Feature[] Dependencies  { get; set; }
        public DateTime EndDtg { get; set; }
        public DateTime StartDtg { get; set; }
    }
}