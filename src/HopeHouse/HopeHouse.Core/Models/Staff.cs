using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using HopeHouse.Common.Interfaces;
using HopeHouse.Core.DataAccess;
using System;

namespace HopeHouse.Core.Models
{
    public class Staff : IDataProvider
    {
        #region Fields
        private int _staffId;
        private string _firstName;
        private string _middleInit;
        private string _lastName;
        private string _username;
        private string _password;
        private string _phoneNumber;
        private bool _isAdministrator;
        private bool _isActive;
        #endregion

        #region Properties
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

        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
            }
        }

        public string MiddleInit
        {
            get
            {
                return _middleInit;
            }
            set
            {
                _middleInit = value;
            }
        }

        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
            }
        }

        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }

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

        public bool IsAdministrator
        {
            get
            {
                return _isAdministrator;
            }
            set
            {
                _isAdministrator = value;
            }
        }

        public bool IsActive
        {
            get
            {
                return _isActive;
            }
            set
            {
                _isActive = value;
            }
        }
        #endregion

        #region Constructor(s)
        #endregion        
		
		#region Overridden Methods

        public override string ToString()
        {
            return _lastName + ", " + _firstName + " " + _middleInit + ".";
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

            //return new Dictionary<string, object>
            //{
            //    {"Staff ID", _staffId},
            //    {"First Name", _firstName},
            //    {"Middle Initial", _middleInit},
            //    {"Last Name", _lastName},
            //    {"Username", _username},
            //    {"Password", _password},
            //    {"Phone Number", _phoneNumber},
            //    {"Is Administrator", _isAdministrator}
            //};
        }

        public string GetIdentifier()
        {
            return _lastName + ", " + _firstName + ", " + _middleInit;
        }

        public void Update()
        {
            StaffDatabaseHelper.UpdateStaff(this);
        }

        #endregion

    }
}
