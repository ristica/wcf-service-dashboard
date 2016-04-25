using System;

namespace Demo.Business.Managers.Monitoring
{
    public class OperationInfoEventArgs : EventArgs
    {
        public string ServiceName { get; set; }
        public string OperationName { get; set; }
        public DateTime Timestamp { get; private set; }
        public string Direction { get; set; }

        public object[] Parameter { get; set; }

        public OperationInfoEventArgs(string serviceName, string operationName, string direction, object[] parameter)
        {
            this.ServiceName = serviceName;
            this.OperationName = operationName;
            this.Direction = direction;
            this.Parameter = parameter;
            this.Timestamp = DateTime.Now;
        }
    }
}
