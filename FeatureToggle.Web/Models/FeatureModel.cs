using NFeature.Configuration;
using System;

namespace FeatureToggle.Web.Models
{
    public class FeatureModel
    {
        public string Name  { get; set; }
        public string Description  { get; set; }
        public FeatureState State  { get; set; }
        public FeatureToggle.Web.NFeature.Feature[] Dependencies  { get; set; }
        public DateTime EndDtg { get; set; }
        public DateTime StartDtg { get; set; }
    }
}