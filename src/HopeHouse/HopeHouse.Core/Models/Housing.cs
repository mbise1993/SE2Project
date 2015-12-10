using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using HopeHouse.Common.Interfaces;
using HopeHouse.Core.DataAccess;
using System;

namespace HopeHouse.Core.Models
{
    public class Housing : IDataProvider
    {
        #region Fields
        private int _housingId;
        private int _clientId;
        private int _partnerId;
        private string _housingStatus;
        private bool _residentialProgram;
        #endregion

        #region Properties
    
        public int HousingId
        {
            get
            {
                return _housingId;
            }
            set
            {
                _housingId = value;
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

        public int PartnerId
        {
            get
            {
                return _partnerId;
            }
            set
            {
                _partnerId = value;
            }
        }

        [Filter]
        [Description("Housing Status")]
        public string HousingStatus
        {
            get
            {
                return _housingStatus;
            }
            set
            {
                _housingStatus = value;
            }
        }

        [Filter]
        [Description("In Residential Program?")]
        public bool ResidentialProgram
        {
            get
            {
                return _residentialProgram;
            }
            set
            {
                _residentialProgram = value;
            }
        }
        #endregion

        #region Overridden Methods

        public override string ToString()
        {
            return _housingStatus;
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
            //            {"Housing ID", _housingId},
            //            {"Client ID", _clientId},
            //            {"Partner ID", _partnerId},
            //            {"Housing Status", _housingStatus},
            //            {"Resident Program", _residentialProgram}
            //};
        }

        public string GetIdentifier()
        {
            return _housingStatus;
        }

        public void Update()
        {
            HousingDatabaseHelper.UpdateHousing(this);
        }

        #endregion
    }
}