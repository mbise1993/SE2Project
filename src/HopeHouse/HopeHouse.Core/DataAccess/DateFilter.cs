using System;
using System.Collections;
using System.Collections.Generic;
using HopeHouse.Common.Interfaces;
using HopeHouse.Core.Models;

namespace HopeHouse.Core.DataAccess
{
    public class DateFilter : Filter
    {
        #region Private Fields

        private string _filterDay;
        private string _filterMonth;
        private string _filterYear;

        #endregion

        #region Properties

        public string FilterDay
        {
            get
            {
                return _filterDay;
            }
        }

        public string FilterMonth
        {
            get
            {
                return _filterMonth;
            }
        }

        public string FilterYear
        {
            get
            {
                return _filterYear;
            }
        }

        #endregion

        #region Constructor

        public DateFilter(FilterType filterType, string fieldName, string filterDay, string filterMonth, string filterYear) :
            base(filterType, fieldName)
        {
            _filterDay = filterDay;
            _filterMonth = filterMonth;
            _filterYear = filterYear;
        }

        #endregion

        #region Filter Implementations

        public override string DisplayValue
        {
            get
            {
                return $"{_filterMonth}/{_filterDay}/{_filterYear}";
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
                    if (objectData[_fieldName] is DateTime)
                    {
                        return CheckDate((DateTime)objectData[_fieldName]);
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

        private bool CheckDate(DateTime date)
        {
            bool dayPassed;
            bool monthPassed;
            bool yearPassed;

            if (_filterDay == "Any")
            {
                dayPassed = true;
            }
            else
            {
                dayPassed = date.Day.ToString() == _filterDay;
            }

            if (_filterMonth == "Any")
            {
                monthPassed = true;
            }
            else
            {
                monthPassed = date.Month.ToString() == _filterMonth;
            }

            if (_filterYear == "Any")
            {
                yearPassed = true;
            }
            else
            {
                yearPassed = date.Year.ToString() == _filterYear;
            }

            return dayPassed && monthPassed && yearPassed;
        }

        #endregion
    }
}
