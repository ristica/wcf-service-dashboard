using System;
using System.Configuration;
using System.ServiceModel.Configuration;

namespace Demo.Business.Managers.Monitoring
{
    public class OperationReportServiceBehaviorExtension : BehaviorExtensionElement
    {
        public override Type BehaviorType
        {
            get
            {
                return typeof(OperationReportServiceBehaviorAttribute);
            }
        }

        protected override object CreateBehavior()
        {
            return new OperationReportServiceBehaviorAttribute(this.Enabled);
        }

        [ConfigurationProperty("enabled")]
        public bool Enabled
        {
            get { return (bool)base["enabled"]; }
            set { base["enabled"] = value; }
        }
    }
}
