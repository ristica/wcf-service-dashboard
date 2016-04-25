using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Demo.Business.Managers.Monitoring
{
    [AttributeUsage(AttributeTargets.Method)]
    public class OperationReportOperationBehaviorAttribute : Attribute, IOperationBehavior
    {
        #region Fields

        private bool _isEnabled = false;

        #endregion

        #region Event

        public event EventHandler<OperationInfoEventArgs> ServiceOperationCalled;

        #endregion

        #region C-Tor

        public OperationReportOperationBehaviorAttribute(bool isEnabled)
        {
            this._isEnabled = isEnabled;
        }

        #endregion

        #region IOperationBehavior implementation

        public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
        {
        }

        public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
        {
            if (!this._isEnabled) return;

            var serviceName = dispatchOperation.Parent.Type.Name;
            var parameterInspector = new OperationReportParameterInspector(serviceName);

            parameterInspector.ServiceOperationCalled += (sender, args) =>
            {
                if (ServiceOperationCalled == null) return;

                // bubbling up to OperationReportServiceBehaviorAttribute
                ServiceOperationCalled(this, args);
            };

            dispatchOperation.ParameterInspectors.Add(parameterInspector);
        }

        public void Validate(OperationDescription operationDescription)
        {
        }

        #endregion
    }
}
