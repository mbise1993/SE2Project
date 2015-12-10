using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using HopeHouse.Common.Interfaces;
using HopeHouse.Core.DataAccess;
using System;

namespace HopeHouse.Core.Models
{
    public class Work : IDataProvider
    {
        #region Fields
        private int _workId;
        private int _clientId;
        private int _partnerId;
        private string _employer;
        private string _phoneNumber;
        private int _weeklyHours;
        private bool _livableWage;
        private int _personalId;
        private string _reasonNotWorking;
        private bool _willPursueWork;
        #endregion

        #region Properties
        public int WorkId
        {
            get
            {
                return _workId;
            }
            set
            {
                _workId = value;
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

        [Filter]
        [Description("Employer")]
        public string Employer
        {
            get
            {
                return _employer;
            }
            set
            {
                _employer = value;
            }
        }

        [Filter]
        [Description("Phone Number")]
        public string PhoneNumber
        {
            get
            {
                return _phoneNumber;
            }
            set
            {
                _phoneNumber = value;
            }
        }

        [Filter]
        [Description("Hours Per Week")]
        public int WeeklyHours
        {
            get
            {
                return _weeklyHours;
            }
            set
            {
                _weeklyHours = value;
            }
        }

        [Filter]
        [Description("Making Livable Wage?")]
        public bool LivableWage
        {
            get
            {
                return _livableWage;
            }
            set
            {
                _livableWage = value;
            }
        }

        public int PersonalId
        {
            get
            {
                return _personalId;
            }
            set
            {
                _personalId = value;
            }
        }

        [Filter]
        [Description("Reason Not Working")]
        public string ReasonNotWorking
        {
            get
            {
                return _reasonNotWorking;
            }
            set
            {
                _reasonNotWorking = value;
            }
        }

        [Filter]
        [Description("Will Pursue Work?")]
        public bool WillPursueWork
        {
            get
            {
                return _willPursueWork;
            }
            set
            {
                _willPursueWork = value;
            }
        }
        #endregion

        #region Constructor(s)
        #endregion        
		
		#region Overridden Methods

        public override string ToString()
        {
            string identifier = "N/A";

            if (!string.IsNullOrEmpty(_employer))
            {
                identifier = "Employer: " + _employer;
            }
            else if (!string.IsNullOrEmpty(_reasonNotWorking))
            {
                identifier = "Not Working: " + _reasonNotWorking;
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
            //            {"Work ID",_workId},
            //            {"Partner ID", _partnerId},
            //            {"Employer", _employer},
            //            {"Phone Number", _phoneNumber},
            //            {"Weekly Hours", _weeklyHours},
            //            {"Livable Wage", _livableWage},
            //            {"Personal ID", _personalId},
            //            {"Reason Not Working", _reasonNotWorking},
            //            {"Will Pursue Work", _willPursueWork}
            //};
        }

        public string GetIdentifier()
        {
            return ToString();
        }

        public void Update()
        {
            WorkDatabaseHelper.UpdateWork(this);
        }

        #endregion
    }
}