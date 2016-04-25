using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace Demo.Business.Managers.Monitoring
{
    [AttributeUsage(AttributeTargets.Class)]
    public class OperationReportServiceBehaviorAttribute : Attribute, IServiceBehavior
    {
        #region Fields

        private bool _isEnabled = false;

        #endregion

        #region Event

        public event EventHandler<OperationInfoEventArgs> ServiceOperationCalled;

        #endregion

        #region C-Tor

        public OperationReportServiceBehaviorAttribute(bool isEnabled)
        {
            this._isEnabled = isEnabled;
        }

        #endregion

        #region IServiceBehavior implementation

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            if (!this._isEnabled) return;

            foreach (var endpoint in serviceDescription.Endpoints)
            {
                foreach (var operation in endpoint.Contract.Operations)
                {
                    var operationBehavior = new OperationReportOperationBehaviorAttribute(this._isEnabled);

                    operationBehavior.ServiceOperationCalled += (sender, args) =>
                    {
                        if (ServiceOperationCalled == null) return;

                        // bubbling up to the host
                        ServiceOperationCalled(this, args);
                    };

                    operation.OperationBehaviors.Add(operationBehavior);
                }
            }
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }

        #endregion
    }
}
