using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using HopeHouse.Common.Interfaces;
using HopeHouse.Core.DataAccess;

namespace HopeHouse.Core.Models
{
    public class Item : IDataProvider
    {
        private int _itemid;
        private string _itemname;
        private string _itemdescription;
        private int _costinpoints;
        private int _itemquantity;// posibility of removal

        public int ItemId
        {
            get { return _itemid; }
            set { _itemid = value; }
        }

        [Description("Name")]
        public string ItemName
        {
            get { return _itemname; }
            set { _itemname = value; }
        }

        [Description("Description")]
        public string ItemDescription
        {
            get { return _itemdescription; }
            set { _itemdescription = value; }
        }

        [Description("Cost In Points")]
        public int CostInPoints
        {
            get { return _costinpoints; }
            set { _costinpoints = value; }
        }

        [Description("Quantity")]
        public int ItemQuantity
        {
            get { return _itemquantity; }
            set { _itemquantity = value; }
        }


        public override string ToString()
        {
            return _itemname;
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

            //        return  new Dictionary<string, object>
            //        {
            //            {"Item ID", _itemid},
            //            {"Item Name", _itemname},
            //            {"Item Description", _itemdescription},
            //            {"Cost in Points", _costinpoints},
            //            {"Item Quantity", _itemquantity}
            //};
        }

        public string GetIdentifier()
        {
            return _itemname;
        }

        public void Update()
        {
            //ItemDatabaseHelper.UpdateItem(this);
        }
    }
}