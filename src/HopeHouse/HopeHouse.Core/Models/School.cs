using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using HopeHouse.Common.Interfaces;
using HopeHouse.Core.DataAccess;
using System;

namespace HopeHouse.Core.Models
{
    public class School : IDataProvider
    {
        #region Fields
        private int _schoolId;
        private int _clientId;
        private string _schoolName;
        private bool _currentlyEnroll;
        private int _hoursEnrolled;
        private string _term;
        #endregion

        #region Properties

        [Filter]
        [Description("Term")]
        public string Term
        {
            get
            {
                return _term;
            }
            set
            {
                _term = value;
            }
        }

        [Filter]
        [Description("Hours Enrolled")]
        public int HoursEnrolled
        {
            get
            {
                return _hoursEnrolled;
            }
            set
            {
                _hoursEnrolled = value;
            }
        }

        [Filter]
        [Description("Currently Enrolled?")]
        public bool CurrentlyEnrolled
        {
            get
            {
                return _currentlyEnroll;
            }
            set
            {
                _currentlyEnroll = value;
            }
        }

        [Filter]
        [Description("School Name")]
        public string SchoolName
        {
            get
            {
                return _schoolName;
            }
            set
            {
                _schoolName = value;
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

        public int SchoolId
        {
            get
            {
                return _schoolId;
            }
            set
            {
                _schoolId = value;
            }
        }
        #endregion   

		#region Constructors
		
		#endregion
		
		#region Overridden Methods

        public override string ToString()
        {
            string identifier = "None";

            if (!string.IsNullOrEmpty(_schoolName))
            {
                identifier = _schoolName + " (Term: " + _term + ")";
            }

            return identifier;
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
            //            {"School ID", _schoolId},
            //            {"Client ID", _clientId},
            //            {"School Name", _schoolName},
            //            {"Currently Enroll",  _currentlyEnroll},
            //            {"Hours Enrolled", _hoursEnrolled},
            //            {"Term", _term}
            //};
        }

        public string GetIdentifier()
        {
            return ToString();
        }

        public void Update()
        {
            SchoolDatabaseHelper.UpdateSchool(this);
        }

        #endregion
    }
}