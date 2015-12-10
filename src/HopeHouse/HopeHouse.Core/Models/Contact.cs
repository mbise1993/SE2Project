using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using HopeHouse.Common.Interfaces;
using HopeHouse.Core.DataAccess;
using System;

namespace HopeHouse.Core.Models
{
    public class Contact : IDataProvider
    {
        #region Fields
        private int _contactId;
        private int _clientId;
        private string _firstName;
        private string _middleInit;
        private string _lastName;
        private string _reason;
        private string _contactType;
        private int _staffId;
        #endregion

        #region Properties
   
        public int ContactId
        {
            get
            {
                return _contactId;
            }
            set
            {
                _contactId = value;
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

        [Description("First Name")]
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

        [Description("Middle Initial")]
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

        [Description("Last Name")]
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

        [Description("Reason for Contact")]
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

        [Description("Contact Type")]
        public string ContactType
        {
            get
            {
                return _contactType;
            }
            set
            {
                _contactType = value;
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

            //        return new Dictionary<string, object>
            //        {
            //            {"Contact ID", _contactId},
            //            {"Client ID", _clientId},
            //            {"Client", _client},
            //            {"First Name", _firstName},
            //            {"Middle Initial", _middleInit},
            //            {"Last Name", _lastName},
            //            {"Reason", _reason},
            //            {"In Office", _inOffice},
            //            {"Staff ID", _staffId}
            //};
        }

        public string GetIdentifier()
        {
            return _lastName + ", " + _firstName + " " + _middleInit;
        }

        public void Update()
        {
            ContactDatabaseHelper.UpdateContact(this);
        }

        #endregion

    }
}