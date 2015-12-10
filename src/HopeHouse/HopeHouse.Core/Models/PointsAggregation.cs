using HopeHouse.Common.Interfaces;
using HopeHouse.Core.DataAccess;
using System;
using System.Collections.Generic;

namespace HopeHouse.Core.Models
{
    public class PointsAggregation : List<Points>, IDataProviderAggregation
    {
        #region IDataProviderAggregation Implementations

        public string GetDataProviderType()
        {
            return "Points";
        }

        public Dictionary<string, IDataProvider> GetDataProviders()
        {
            Dictionary<string, IDataProvider> dataProviders = new Dictionary<string, IDataProvider>();

            int pointsNum = 1;
            foreach (Points points in this)
            {
                dataProviders[$"Points Entry {pointsNum}"] = points;
                pointsNum++;
            }

            return dataProviders;
        }

        public string GetIdentifier()
        {
            return ToString();
        }

        public void AddDataProvider(int clientId, IDataProvider dataProvider)
        {
            if(dataProvider is Points)
            {
                this.Add((Points)dataProvider);

                PointsDatabaseHelper.AddClientPoints(clientId, (Points)dataProvider);
            }
        }

        public void DeleteDataProvider(IDataProvider dataProvider)
        {
            if(dataProvider is Points)
            {
                this.Remove((Points)dataProvider);

                PointsDatabaseHelper.DeletePoints((Points)dataProvider);
            }
        }

        #endregion

        public override string ToString()
        {
            int numPoints = 0;
            foreach(Points points in this)
            {
                numPoints += points.Amount;
            }

            return $"Total Points: {numPoints}";
        }
    }
}
