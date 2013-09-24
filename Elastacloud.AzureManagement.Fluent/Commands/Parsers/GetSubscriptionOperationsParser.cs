/************************************************************************************************************
 * This software is distributed under a GNU Lesser License by Elastacloud Limited and it is free to         *
 * modify and distribute providing the terms of the license are followed. From the root of the source the   *
 * license can be found in /Resources/license.txt                                                           * 
 *                                                                                                          *
 * Web at: www.elastacloud.com                                                                              *
 * Email: info@elastacloud.com                                                                              *
 ************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Elastacloud.AzureManagement.Fluent.Types;

namespace Elastacloud.AzureManagement.Fluent.Commands.Parsers
{
    internal class GetSubscriptionOperationsParser : BaseParser
    {
        public GetSubscriptionOperationsParser(XDocument response)
            : base(response)
        {
            CommandResponse = new List<SubscriptionOperation>();
        }

        internal override void Parse()
        {

            IEnumerable<XElement> rootElements = Document.Element(GetSchema() + RootElement)
                .Elements(GetSchema() + "SubscriptionOperations");
            if (rootElements == null)
                return;

            rootElements = rootElements.Elements(GetSchema() + "SubscriptionOperation");
            foreach (XElement subscriptionOperation in rootElements)
            {
                var operation = new SubscriptionOperation()
                    {
                        OperationId = subscriptionOperation.Element(GetSchema() + "OperationId").Value,
                        OperationName = subscriptionOperation.Element(GetSchema() + "OperationName").Value,
                        OperationStartedTime = DateTime.Parse(subscriptionOperation.Element(GetSchema() + "OperationStartedTime").Value),
                        OperationCompletedTime = DateTime.Parse(subscriptionOperation.Element(GetSchema() + "OperationCompletedTime").Value)
                    };

                if (subscriptionOperation.Elements(GetSchema() + "OperationParameters") != null)
                {
                    var parameterList = subscriptionOperation.Elements(GetSchema() + "OperationParameters")
                        .Elements(GetSchema() + "OperationParameter")
                        .Select(xElement => new OperationParameter()
                        {
                            Name = (string)xElement.Element(XNamespace.Get(DataContractServiceManagementSchema) + "Name").Value,
                            Value = (string)xElement.Element(XNamespace.Get(DataContractServiceManagementSchema) + "Value").Value
                        }).ToList();
                    operation.OperationParameters = parameterList;
                }

                var operationCaller = subscriptionOperation.Elements(GetSchema() + "OperationCaller")
                        .Select(xElement => new OperationCaller()
                        {
                            UsedServiceManagementApi = Boolean.Parse(xElement.Element(GetSchema() + "UsedServiceManagementApi").Value),
                            UserEmailAddress = (string)xElement.Element(GetSchema() + "UserEmailAddress").Value,
                            ClientIP = (string)xElement.Element(GetSchema() + "ClientIP").Value
                        }).First();

                operation.OperationCaller = operationCaller;


                var operationStatus = subscriptionOperation.Elements(GetSchema() + "OperationStatus")
                        .Select(xElement => new OperationStatus()
                        {
                            ID = (string)xElement.Element(GetSchema() + "ID").Value,
                            Status = (string)xElement.Element(GetSchema() + "Status").Value,
                            HttpStatusCode = int.Parse(xElement.Element(GetSchema() + "HttpStatusCode").Value),
                        }).First();

                operation.OperationStatus = operationStatus;


                CommandResponse.Add(operation);
            }
        }

        #region Overrides of BaseParser

        internal override string RootElement
        {
            get { return GetSubscriptionOperationsParser; }
        }

        protected override XNamespace GetSchema()
        {
            return XNamespace.Get(WindowsAzureSchema);
        }

        #endregion
    }
}