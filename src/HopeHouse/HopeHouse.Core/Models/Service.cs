using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using HopeHouse.Common.Interfaces;
using HopeHouse.Core.DataAccess;

namespace HopeHouse.Core.Models
{
    public class Service : IDataProvider
    {
        #region Private Fields

        private int _requestId;
        private int _receivedId;
        private int _clientId;
        private int _staffId;
        private DateTime _date;
        private string _description;

        #endregion

        #region Properties

        public int RequestId
        {
            get
            {
                return _requestId;
            }
            set
            {
                _requestId = value;
            }
        }

        public int ReceivedId
        {
            get
            {
                return _receivedId;
            }
            set
            {
                _receivedId = value;
            }
        }

        public int ClientId
        {
            get
            {
                return _clientId;
            }
            set
            {
                _clientId = value;
            }
        }

        public int StaffId
        {
            get
            {
                return _staffId;
            }
            set
            {
                _staffId = value;
            }
        }

        [Filter]
        [Description("Date")]
        public DateTime Date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
            }
        }

        [Filter]
        [Description("Description")]
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }

        #endregion

        #region IDataProvider Implementation

        public void SetDataProperty(string propertyName, object value)
        {
            PropertyInfo[] properties = GetType().GetProperties().Where(
                x => x.GetCustomAttribute<DescriptionAttribute>() != null).ToArray();

            foreach (PropertyInfo property in properties)
            {
                DescriptionAttribute attr = property.GetCustomAttribute<DescriptionAttribute>();

                if (attr != null)
                {
                    if (attr.Description == propertyName)
                    {
                        if (property.PropertyType.Name.Equals("Int32"))
                            property.SetValue(this, Int32.Parse(value.ToString()));
                        else if (property.PropertyType.Name.Equals("DateTime"))
                            property.SetValue(this, DateTime.Parse(value.ToString()));
                        else
                            property.SetValue(this, value);
                    }
                }
            }
        }

        public Dictionary<string, object> GetData()
        {

            Dictionary<string, object> data = new Dictionary<string, object>();

            PropertyInfo[] properties = GetType().GetProperties().Where(
                x => x.GetCustomAttribute<DescriptionAttribute>() != null).ToArray();

            foreach (PropertyInfo property in properties)
            {
                DescriptionAttribute descriptionAttr = property.GetCustomAttribute<DescriptionAttribute>();

                if (descriptionAttr != null)
                {
                    object propertyValue = property.GetValue(this, null);
                    data[descriptionAttr.Description] = propertyValue;
                }
            }

            return data;

            //return new Dictionary<string, object>
            //{
            //    {"Description", _description },
            //    {"Date", _date}
            // };
        }

        public string GetIdentifier()
        {
            return _description;
        }

        #endregion

        public override string ToString()
        {
            return _description;
        }

        public void Update()
        {
            if(_receivedId > 0)
            {
                ServicesReceivedDatabaseHelper.UpdateServiceReceived(this);
            }

            if(_requestId > 0)
            {
                ServicesRequestedDatabaseHelper.UpdateServiceRequested(this);
            }
        }
    }
}
