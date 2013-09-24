using System;
using System.Collections.Generic;
using System.Net;
using Elastacloud.AzureManagement.Fluent.Commands.Parsers;
using Elastacloud.AzureManagement.Fluent.Commands.Services;
using Elastacloud.AzureManagement.Fluent.Types;

namespace Elastacloud.AzureManagement.Fluent.Commands.Subscriptions
{
    /// <summary>
    /// Gets the operations for this subscription 
    /// </summary>
    internal class GetSubscriptionOperationsCommand : ServiceCommand
    {
        // https://management.core.windows.net/<subscription-id>/operations
        internal GetSubscriptionOperationsCommand(DateTime startDate, DateTime endDate)
        {
            ServiceType = "";
            HttpVerb = HttpVerbGet;
            HttpCommand = string.Format("operations?StartTime={0}&EndTime={1}", startDate.ToUniversalTime().ToString("o"), endDate.ToUniversalTime().ToString("o"));
        }

        internal List<SubscriptionOperation> Operations { get; set; }

        protected override void ResponseCallback(HttpWebResponse webResponse)
        {
            Operations = Parse(webResponse, BaseParser.GetSubscriptionOperationsParser);
            SitAndWait.Set();
        }
    }
}