namespace Elastacloud.AzureManagement.Fluent.Types
{
    /// <summary>
    /// The operation
    /// </summary>
    public class OperationStatus
    {
        public string ID { get; set; }
        public string Status { get; set; }
        public int HttpStatusCode { get; set; }
    }
}