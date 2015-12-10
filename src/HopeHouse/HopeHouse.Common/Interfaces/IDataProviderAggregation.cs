using System.Collections.Generic;

namespace HopeHouse.Common.Interfaces
{
    public interface IDataProviderAggregation
    {
        string GetDataProviderType();

        Dictionary<string, IDataProvider> GetDataProviders();

        void AddDataProvider(int clientId, IDataProvider dataProvider);

        void DeleteDataProvider(IDataProvider dataProvider);

        string GetIdentifier();
    }
}
