using HopeHouse.Common.Interfaces;
using HopeHouse.Core.DataAccess;
using System;
using System.Collections.Generic;

namespace HopeHouse.Core.Models
{
    public class ServicesRequestedAggregation : List<Service>, IDataProviderAggregation
    {
        #region IDataProviderAggregation Implementations

        public string GetDataProviderType()
        {
            return "Service Requested";
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
            return Count == 1 ? "1 Service Requested" : $"{Count} Services Requested";
        }

        public void AddDataProvider(int clientId, IDataProvider dataProvider)
        {
            if (dataProvider is Service)
            {
                this.Add((Service)dataProvider);

                ServicesRequestedDatabaseHelper.AddClientServiceRequested(clientId, (Service)dataProvider);
            }
        }

        public void DeleteDataProvider(IDataProvider dataProvider)
        {
            if (dataProvider is Service)
            {
                this.Remove((Service)dataProvider);

                ServicesRequestedDatabaseHelper.DeleteServiceRequested((Service)dataProvider);
            }
        }

        #endregion

        public override string ToString()
        {
            return $"{Count} Services Requested";
        }
    }
}
