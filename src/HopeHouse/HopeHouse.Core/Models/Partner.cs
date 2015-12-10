using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using HopeHouse.Common.Interfaces;
using HopeHouse.Core.DataAccess;

namespace HopeHouse.Core.Models
{
    public class Partner : IDataProvider
    {
        private int _partnerid;
        private int _clientid;
        private string _firstName;
        private string _middleInit;
        private string _lastName;
        private DateTime _birthday;
        private string _address;
        private string _city;
        private string _state;
        private string _zip;
        private string _phonenumber;
        private bool _livetogether;
        private bool _supportive;
        private int _workid;
        private Work _work;

        public int PartnerID
        {
            get { return _partnerid; }
            set { _partnerid = value; }
        }

        public int ClientID
        {
            get { return _clientid; }
            set { _clientid = value; }
        }

        [Filter]
        [Description("First Name")]
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        [Filter]
        [Description("Middle Initial")]
        public string MiddleInit
        {
            get { return _middleInit; }
            set { _middleInit = value; }
        }

        [Filter]
        [Description("Last Name")]
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        [Filter]
        [Description("Birth Date")]
        public DateTime Birthday
        {
            get { return _birthday; }
            set { _birthday = value; }
        }

        [Filter]
        [Description("Address")]
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        [Filter]
        [Description("City")]
        public string City
        {
            get { return _city; }
            set { _city = value; }
        }

        [Filter]
        [Description("State")]
        public string State
        {
            get { return _state; }
            set { _state = value; }
        }

        [Filter]
        [Description("Zip Code")]
        public string Zip
        {
            get { return _zip; }
            set { _zip = value; }
        }

        [Filter]
        [Description("Phone Number")]
        public string PhoneNumber
        {
            get { return _phonenumber; }
            set { _phonenumber = value; }
        }

        [Filter]
        [Description("Living Together?")]
        public bool LiveTogether
        {
            get { return _livetogether; }
            set { _livetogether = value; }
        }

        [Filter]
        [Description("Supportive?")]
        public bool Supportive
        {
            get { return _supportive; }
            set { _supportive = value; }
        }

        public int WorkID {
            get{ return _workid; }
            set { _workid = value; }
            }

        [Description("Work")]
        public Work Work
        {
            get
            {
                return _work;
            }
            set
            {
                _work = value;
            }
        }

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
            //            {"Partner ID", _partnerid},
            //            {"Client ID", _clientid},
            //            {"First Name", _firstName},
            //            {"Middle Initial", _middleInit},
            //            {"Last Name", _lastName},
            //            {"Birthday", _birthday},
            //            {"Address", _address},
            //            {"City", _city},
            //            {"State", _state},
            //            {"Zip", _zip},
            //            {"Phone Number", _phonenumber},
            //            {"Live Together", _livetogether},
            //            {"Supportive", _supportive},
            //            {"Work ID", _workid}
            //};
        }

        public string GetIdentifier()
        {
            return _lastName + ", " + _firstName + " " + _middleInit;
        }

        public void Update()
        {
            PartnerDatabaseHelper.UpdatePartner(this);
        }

        #endregion
    }
}
