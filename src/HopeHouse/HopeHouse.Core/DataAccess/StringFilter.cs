using System;
using System.Collections;
using System.Collections.Generic;
using HopeHouse.Common.Interfaces;
using HopeHouse.Core.Models;

namespace HopeHouse.Core.DataAccess
{
     public class StringFilter : Filter
    {
        #region Private Fields

         private string _filterString;

        #endregion

        #region Properties

         public string FilterString
         {
             get
             {
                 return _filterString;
             }
         }

        #endregion

        #region Constructor

         public StringFilter(FilterType filterType, string fieldName, string filterString) :
             base(filterType, fieldName)
         {
             _filterString = filterString;
         }

        #endregion

        #region Filter Implementations

         public override string DisplayValue
         {
             get
             {
                if (_filterString == "True")
                {
                    return "Yes";
                }

                if (_filterString == "False")
                {
                    return "No";
                }

                return _filterString;
             }
         }

        public override bool CheckFilter(Client client)
        {
            switch (FilterType)
            {
                case FilterType.Client:
                    return CheckProperties(client);

                case FilterType.Child:
                    return CheckPropertiesEnumerable(client.Children);

                case FilterType.Contact:
                    return CheckProperties(client.Contact);

                case FilterType.EducationHistory:
                    return CheckProperties(client.EducationHistory);

                case FilterType.Housing:
                    return CheckProperties(client.Housing);

                case FilterType.Points:
                    return CheckPropertiesEnumerable(client.Points);

                case FilterType.Pregnancy:
                    return CheckProperties(client.Pregnancy);

                case FilterType.School:
                    return CheckProperties(client.School);

                case FilterType.ServicesReceived:
                    return CheckPropertiesEnumerable(client.ServicesReceived);

                case FilterType.ServicesRequested:
                    return CheckPropertiesEnumerable(client.ServicesRequested);

                case FilterType.Work:
                    return CheckProperties(client.Work);
            }

            return false;
        }

        #endregion

        #region Private Fields

        private bool CheckProperties(IDataProvider checkObject)
        {
            if (checkObject != null)
            {
                Dictionary<string, object> objectData = checkObject.GetData();

                if (objectData.ContainsKey(_fieldName))
                {
                    try
                    {
                        return objectData[_fieldName].ToString().Contains(_filterString);
                    }
                    catch (NullReferenceException)
                    {
                        return false;
                    }
                }
            }

            return false;
        }

        private bool CheckPropertiesEnumerable(IEnumerable checkObject)
        {
            if (checkObject != null)
            {
                foreach (object obj in checkObject)
                {
                    if (obj is IDataProvider)
                    {
                        return CheckProperties((IDataProvider)obj);
                    }
                }
            }

            return false;
        }

        #endregion
    }
}
