using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using HopeHouse.Common.Interfaces;
using HopeHouse.Core.DataAccess;

namespace HopeHouse.Core.Models
{
    public class Class : IDataProvider
    {
        private int _classid;
        private string _classname;
        private string _classdescription;
        private DateTime _starttime;
        private DateTime _endtime;

        public int ClassId
        {
            get { return _classid; }
            set { _classid = value; }
        }

        [Filter]
        [Description("Name")]
        public string ClassName
        {
            get { return _classname;}
            set { _classname = value; }
        }

        [Filter]
        [Description("Description")]
        public string ClassDescription
        {
            get { return _classdescription;}
            set { _classdescription = value; }
        }

        [Filter]
        [Description("Start Time")]
        public DateTime StartTime
        {
            get { return _starttime;}
            set { _starttime = value; }
        }

        [Filter]
        [Description("End Time")]
        public DateTime EndTime
        {
            get { return _endtime;}
            set { _endtime = value; }
        }


        public override string ToString()
        {
            return _classname;
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
            //            {"Class ID", _classid},
            //            {"Class Name", _classname},
            //            {"Class Description", _classdescription},
            //            {"Start Time", _starttime},
            //            {"End Time", _endtime}
            //};
        }

        public string GetIdentifier()
        {
            return _classname;
        }

        public void Update()
        {
            //ClassDatabaseHelper.UpdateChild(this);
        }
    }
}