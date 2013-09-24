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
    }
}