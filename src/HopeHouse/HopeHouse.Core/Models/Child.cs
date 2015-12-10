using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using HopeHouse.Common.Interfaces;
using HopeHouse.Core.DataAccess;

namespace HopeHouse.Core.Models
{
    public class Child : IDataProvider
    {
        #region Fields
        private int _childId;
        private int _clientId;
        private int _partnerId;
        private string _firstName;
        private string _middleInit;
        private string _lastName;
        private DateTime _birthDate;
        private string _pediatrician;
        private bool _receivingHealthCare;
        private bool _inDaycare;
        private bool _disabled;
        #endregion

        #region Properties
        
        public int ChildId
        {
            get
            {
                return _childId;
            }
            set
            {
                _childId = value;
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

        [Filter]
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

        [Filter]
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

        [Filter]
        [Description("Birth Date")]
        public DateTime BirthDate
        {
            get
            {
                return _birthDate;
            }
            set
            {
                _birthDate = value;
            }
        }

        [Filter]
        [Description("Pediatrician")]
        public string Pediatrician
        {
            get
            {
                return _pediatrician;
            }
            set
            {
                _pediatrician = value;
            }
        }

        [Filter]
        [Description("Receiving Health Care?")]
        public bool ReceivingHealthcare
        {
            get
            {
                return _receivingHealthCare;
            }
            set
            {
                _receivingHealthCare = value;
            }
        }

        [Filter]
        [Description("In Daycare?")]
        public bool InDaycare
        {
            get
            {
                return _inDaycare;
            }
            set
            {
                _inDaycare = value;
            }
        }

        [Filter]
        [Description("Disabled?")]
        public bool Disabled
        {
            get
            {
                return _disabled;
            }
            set
            {
                _disabled = value;
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

            foreach(PropertyInfo property in properties)
            {
                DescriptionAttribute attr = property.GetCustomAttribute<DescriptionAttribute>();

                if(attr != null)
                {
                    if(attr.Description == propertyName)
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
            //    {"Child ID", _childId },
            //    {"Client ID",_clientId},
            //    {"Partner ID", _partnerId},
            //    {"First Name", _firstName},
            //    {"Middle Initial", _middleInit},
            //    {"Last Name", _lastName},
            //    {"Birth Date", _birthDate},
            //    {"Pediatrician", _pediatrician},
            //    {"Recieving Healthcare", _receivingHealthCare},
            //    {"In Daycare", _inDaycare},
            //    {"Disabled", _disabled}

            // };
        }

        public string GetIdentifier()
        {
            return _lastName + ", " + _firstName + " "+ _middleInit;
        }

        public void Update()
        {
            ChildDatabaseHelper.UpdateChild(this);
        }

        #endregion
    }
}