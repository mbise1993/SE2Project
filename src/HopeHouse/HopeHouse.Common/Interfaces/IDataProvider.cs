using System.Collections.Generic;

namespace HopeHouse.Common.Interfaces
{
    public interface IDataProvider
    {
        // Sets one of the object's properties
        void SetDataProperty(string propertyName, object value);

        // Returns collection of data provided by object
        Dictionary<string, object> GetData();

        // Returns identifier for this object
        string GetIdentifier();

        void Update();
    }
}
