using HopeHouse.Common.Interfaces;
using HopeHouse.Core.DataAccess;
using System;
using System.Collections.Generic;

namespace HopeHouse.Core.Models
{
    public class ChildAggregation : List<Child>, IDataProviderAggregation
    {
        #region IDataProviderAggregation Implementations

        public string GetDataProviderType()
        {
            return "Child";
        }

        public Dictionary<string, IDataProvider> GetDataProviders()
        {
            Dictionary<string, IDataProvider> dataProviders = new Dictionary<string, IDataProvider>();

            int childNum = 1;
            foreach(Child child in this)
            {
                dataProviders[$"Child {childNum}"] = child;
                childNum++;
            }

            return dataProviders;
        }

        public string GetIdentifier()
        {
            return Count == 1 ? "1 Child" : $"{Count} Children";
        }

        public void AddDataProvider(int clientId, IDataProvider dataProvider)
        {
            if(dataProvider is Child)
            {
                this.Add((Child)dataProvider);

                ChildDatabaseHelper.AddClientChild(clientId, (Child)dataProvider);
            }
        }

        public void DeleteDataProvider(IDataProvider dataProvider)
        {
            if(dataProvider is Child)
            {
                this.Remove((Child)dataProvider);

                ChildDatabaseHelper.DeleteChild((Child)dataProvider);
            }
        }

        #endregion

        public override string ToString()
        {
            return $"{Count} Children";
        }
    }
}
