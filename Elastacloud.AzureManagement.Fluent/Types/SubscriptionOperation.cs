using System;
using System.Collections.Generic;
namespace Elastacloud.AzureManagement.Fluent.Types
{
    /// <summary>
    /// The operation
    /// </summary>
    public class SubscriptionOperation
    {
        public string OperationId { get; set; }
        public string OperationName { get; set; }
        public List<OperationParameter> OperationParameters { get; set; }
        public OperationCaller OperationCaller { get; set; }
        public OperationStatus OperationStatus { get; set; }
        public DateTime OperationStartedTime { get; set; }
        public DateTime OperationCompletedTime { get; set; }
    }
}