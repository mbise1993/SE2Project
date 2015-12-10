using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using HopeHouse.Common.Interfaces;
using HopeHouse.Core.DataAccess;
using System;

namespace HopeHouse.Core.Models
{
    public class EducationHistory : IDataProvider
    {
        #region Fields
        private int _educationId;
        private int _clientId;
        private bool _highSchoolGrad;
        private double _dropOutGrade;
        private bool _gEd;
        private bool _obtainingGed;
        private bool _collegeGrad;
        private bool _inCollege;
        #endregion

        #region Properties

        public int EducationId
        {
            get
            {
                return _educationId;
            }
            set
            {
                _educationId = value;
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
        [Description("High School Graduate?")]
        public bool HighSchoolGrad
        {
            get
            {
                return _highSchoolGrad;
            }
            set
            {
                _highSchoolGrad = value;
            }
        }

        [Filter]
        [Description("Drop Out Grade")]
        public double DropOutGrade
        {
            get
            {
                return _dropOutGrade;
            }
            set
            {
                _dropOutGrade = value;
            }
        }

        [Filter]
        [Description("Has GED?")]
        public bool Ged
        {
            get
            {
                return _gEd;
            }
            set
            {
                _gEd = value;
            }
        }

        [Filter]
        [Description("Obtaining GED?")]
        public bool ObtainingGed
        {
            get
            {
                return _obtainingGed;
            }
            set
            {
                _obtainingGed = value;
            }
        }

        [Filter]
        [Description("College Graduate?")]
        public bool CollegeGrad
        {
            get
            {
                return _collegeGrad;
            }
            set
            {
                _collegeGrad = value;
            }
        }

        [Filter]
        [Description("In College?")]
        public bool InCollege
        {
            get
            {
                return _inCollege;
            }
            set
            {
                _inCollege = value;
            }
        }
        #endregion

        #region Overridden Methods

        public override string ToString()
        {
            string identifier = "None";

            if (_collegeGrad)
            {
                identifier = "Graduated college";
            }
            else if (_inCollege)
            {
                identifier = "Enrolled in college";
            }
            else if (_highSchoolGrad)
            {
                identifier = "Graduated high school";
            }
            else if (_gEd)
            {
                identifier = "Has GED";
            }
            else if (_obtainingGed)
            {
                identifier = "Obtaining GED";
            }
            else if (_dropOutGrade > 0)
            {
                identifier = "Dropped out of high school in " + _dropOutGrade + "th grade";
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
            //            {"Education ID", _educationId},
            //            {"Client ID", _clientId},
            //            {"Highschool Gaduate", _highSchoolGrad},
            //            {"Dropout Graduate", _dropOutGrade},
            //            {"GED", _gEd},
            //            {"Obtaining GED", _obtainingGed},
            //            {"College Graduate", _collegeGrad},
            //            {"In College", _inCollege}
            //};
        }

        public string GetIdentifier()
        {
            return ToString();
        }

        public void Update()
        {
            EducationHistoryDatabaseHelper.UpdateEducationHistory(this);
        }

        #endregion
    }
}