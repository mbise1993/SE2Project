using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using HopeHouse.Common.Interfaces;
using HopeHouse.Core.DataAccess;

namespace HopeHouse.Core.Models
{
    public class Pregnancy: IDataProvider
    {
        private int _pregnancyid;
        private int _clientid;
        private int _partnerid;
        private DateTime _duedate;
        private bool _carriedtoterm;
        private string _intentions;
        private bool _birthcontrol;
        private bool _verifiedpregnancy;
        private bool _medinsurance;
        private string _ob;
        private bool _prenatalvitamin;
        private bool _wic;
        private bool _hugs;
        private bool _hugsNurse;
        private bool _foodstamps;
        private bool _familiesfirst;
        private bool _lifebridgeFss;
        private string _resultofpregnancy;
        private int _staffid;
        private bool _returnPregnancy;
        private bool _needPregnancyTest;
        private bool _signedReleaseForTest;

        public int PregnancyId
        {
            get
            {
                return _pregnancyid;
            }
            set
            {
                _pregnancyid = value;
            }
        }

        public int ClientId
        {
            get
            {
                return _clientid;
            }
            set
            {
                _clientid = value;
            }
        }

        public int PartnerId
        {
            get
            {
                return _partnerid;
            }
            set
            {
                _partnerid = value;
            }
        }

        [Filter]
        [Description("Due Date")]
        public DateTime DueDate
        {
            get
            {
                return _duedate;
            }
            set
            {
                _duedate = value;
            }
        }

        [Filter]
        [Description("Carried To Term?")]
        public bool CarriedToTerm
        {
            get
            {
                return _carriedtoterm;
            }
            set
            {
                _carriedtoterm = value;
            }
        }

        [Filter]
        [Description("Intentions")]
        public string Intentions
        {
            get
            {
                return _intentions;
            }
            set
            {
                _intentions = value;
            }
        }

        [Filter]
        [Description("On Birth Control?")]
        public bool BirthControl
        {
            get
            {
                return _birthcontrol;
            }
            set
            {
                _birthcontrol = value;
            }
        }

        [Filter]
        [Description("Pregnancy Verified?")]
        public bool VerifiedPregnancy
        {
            get
            {
                return _verifiedpregnancy;
            }
            set
            {
                _verifiedpregnancy = value;
            }
        }

        [Filter]
        [Description("Has Medical Insurance?")]
        public bool MedInsurance
        {
            get
            {
                return _medinsurance;
            }
            set
            {
                _medinsurance = value;
            }
        }

        [Filter]
        [Description("OBGYN")]
        public string Ob
        {
            get
            {
                return _ob;
            }
            set
            {
                _ob = value;
            }
        }

        [Filter]
        [Description("Taking Prenatal Vitamins?")]
        public bool PrenatalVitamin
        {
            get
            {
                return _prenatalvitamin;
            }
            set
            {
                _prenatalvitamin = value;
            }
        }

        [Filter]
        [Description("WIC?")]
        public bool Wic
        {
            get
            {
                return _wic;
            }
            set
            {
                _wic = value;
            }
        }

        [Filter]
        [Description("Hugs?")]
        public bool Hugs
        {
            get
            {
                return _hugs;
            }
            set
            {
                _hugs = value;
            }
        }

        [Filter]
        [Description("Hugs Nurse?")]
        public bool HugsNurse
        {
            get
            {
                return _hugsNurse;
            }
            set
            {
                _hugsNurse = value;
            }
        }

        [Filter]
        [Description("Food Stamps?")]
        public bool FoodStamps
        {
            get
            {
                return _foodstamps;
            }
            set
            {
                _foodstamps = value;
            }
        }

        [Filter]
        [Description("Families First?")]
        public bool FamiliesFirst
        {
            get
            {
                return _familiesfirst;
            }
            set
            {
                _familiesfirst = value;
            }
        }

        [Filter]
        [Description("Life Bridge?")]
        public bool LifeBridgeFss
        {
            get
            {
                return _lifebridgeFss;
            }
            set
            {
                _lifebridgeFss = value;
            }
        }

        [Filter]
        [Description("Result Of Pregnancy")]
        public string ResultOfPregnancy
        {
            get
            {
                return _resultofpregnancy;
            }
            set
            {
                _resultofpregnancy = value;
            }
        }

        public int StaffId
        {
            get
            {
                return _staffid;
            }
            set
            {
                _staffid = value;
            }
        }

        [Filter]
        [Description("Return Pregnancy?")]
        public bool ReturnPregnancy
        {
            get
            {
                return _returnPregnancy;
            }
            set
            {
                _returnPregnancy = value;
            }
        }

        [Filter]
        [Description("Needs Pregnancy Test?")]
        public bool NeedPregnancyTest
        {
            get
            {
                return _needPregnancyTest;
            }
            set
            {
                _needPregnancyTest = value;
            }
        }

        [Filter]
        [Description("Signed Release For Test?")]
        public bool SignedReleaseForTest
        {
            get
            {
                return _signedReleaseForTest;
            }
            set
            {
                _signedReleaseForTest = value;
            }
        }

        #region Overridden Methods

        public override string ToString()
        {
            string identifier = "Verified: ";

            if (_verifiedpregnancy)
            {
                identifier += "Yes ";
            }
            else
            {
                identifier += "No ";
            }

            identifier += "Intentions: " + _intentions;

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
            //            {"Pregnancy ID", _pregnancyid},
            //            {"Client ID", _clientid},
            //            {"Partner ID", _partnerid},
            //            {"Due Date", _duedate},
            //            {"Carried To Term", _carriedtoterm},
            //            {"Intentions", _intentions},
            //            {"Birth Control", _birthcontrol},
            //            {"Verified Pregnancy", _verifiedpregnancy},
            //            {"Medical Issurance", _medinsurance},
            //            {"OB", _ob},
            //            {"Prenatal Vitamin", _prenatalvitamin},
            //            {"WIC", _wic},
            //            {"HUGS", _hugs},
            //            {"HUGS Nurse", _hugsNurse},
            //            {"Food Stamps", _foodstamps},
            //            {"Families First", _familiesfirst},
            //            {"Life Bridge FSS", _lifebridgeFss},
            //            {"Result of Pregnancy", _resultofpregnancy},
            //            {"Staff ID", _staffid},
            //            {"Return Pregnancy", _returnPregnancy},
            //            {"Need Pregnancy Test", _needPregnancyTest},
            //            {"Signed Release For Test", _signedReleaseForTest}
            //};
        }

        public string GetIdentifier()
        {
            return ToString();
        }

        public void Update()
        {
            PregnancyDatabaseHelper.UpdatePregnancy(this);
        }

        #endregion
    }
}
