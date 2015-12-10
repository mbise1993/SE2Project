using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using HopeHouse.Common.Interfaces;
using HopeHouse.Core.DataAccess;

namespace HopeHouse.Core.Models
{
    public class Points : IDataProvider
    {
        #region Fields
        private int _pointId;
        private int _clientId;
        private int _amount;
        private DateTime _date;
        private string _reason;
        private int _staffId;
        #endregion

        #region Properties

        public int PointId
        {
            get
            {
                return _pointId;
            }
            set
            {
                _pointId = value;
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

        [Description("Amount")]
        public int Amount
        {
            get
            {
                return _amount;
            }
            set
            {
                _amount = value;
            }
        }

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

        [Description("Reason")]
        public string Reason
        {
            get
            {
                return _reason;
            }
            set
            {
                _reason = value;
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
        #endregion        
		
		#region Overridden Methods

        public override string ToString()
        {
            return _reason;
        }

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

            //        return new Dictionary<string, object>
            //        {
            //            {"Point ID", _pointId},
            //            {"Client ID", _clientId},
            //            {"Amount", _amount},
            //            {"Date", _date},
            //            {"Reason", _reason},
            //            {"Staff ID", _staffId}
            //};
        }

        public string GetIdentifier()
        {
            return _reason;
        }

        public void Update()
        {
            PointsDatabaseHelper.UpdatePointsEntry(this);
        }

        #endregion
    }
}