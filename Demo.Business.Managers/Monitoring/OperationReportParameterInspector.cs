using System;
using System.ServiceModel.Dispatcher;

namespace Demo.Business.Managers.Monitoring
{
    public class OperationReportParameterInspector : IParameterInspector
    {
        #region Fields

        private readonly string _serviceName;

        #endregion

        #region Event

        public event EventHandler<OperationInfoEventArgs> ServiceOperationCalled;

        #endregion

        #region C-Tor

        public OperationReportParameterInspector(string serviceName)
        {
            this._serviceName = serviceName;
        }

        #endregion

        #region IParameterInspector implementation

        public void AfterCall(string operationName, object[] outputs, object returnValue, object correlationState)
        {
            if (ServiceOperationCalled == null) return;

            // bubbling up to OperationReportOperationBehaviorAttribute
            ServiceOperationCalled(this, new OperationInfoEventArgs(this._serviceName, operationName, "UP", outputs));
        }

        public object BeforeCall(string operationName, object[] inputs)
        {
            if (ServiceOperationCalled == null) return null;

            // bubbling up to OperationReportOperationBehaviorAttribute
            ServiceOperationCalled(this, new OperationInfoEventArgs(this._serviceName, operationName, "DOWN", inputs));

            return null;
        }

        #endregion
    }
}
