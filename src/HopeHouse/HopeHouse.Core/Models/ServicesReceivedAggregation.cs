using HopeHouse.Common.Interfaces;
using HopeHouse.Core.DataAccess;
using System;
using System.Collections.Generic;

namespace HopeHouse.Core.Models
{
    public class ServicesReceivedAggregation : List<Service>, IDataProviderAggregation
    {
        #region IDataProviderAggregation Implementations

        public string GetDataProviderType()
        {
            return "Service Received";
        }

        public Dictionary<string, IDataProvider> GetDataProviders()
        {
            Dictionary<string, IDataProvider> dataProviders = new Dictionary<string, IDataProvider>();

            int serviceNum = 1;
            foreach (Service service in this)
            {
                dataProviders[$"Service {serviceNum}"] = service;
                serviceNum++;
            }

            return dataProviders;
        }

        public string GetIdentifier()
        {
            return Count == 1 ? "1 Service Received" : $"{Count} Services Received";
        }

        public void AddDataProvider(int clientId, IDataProvider dataProvider)
        {
            if(dataProvider is Service)
            {
                this.Add((Service)dataProvider);

                ServicesReceivedDatabaseHelper.AddClientServiceReceived(clientId, (Service)dataProvider);
            }
        }

        public void DeleteDataProvider(IDataProvider dataProvider)
        {
            if(dataProvider is Service)
            {
                this.Remove((Service)dataProvider);

                ServicesReceivedDatabaseHelper.DeleteServiceReceived((Service)dataProvider);
            }
        }

        #endregion

        public override string ToString()
        {
            return $"{Count} Services Received";
        }
    }
}
