using System.ComponentModel;
using HopeHouse.Core.Models;

namespace HopeHouse.Core.DataAccess
{
    #region Filter Types Enum

    public enum FilterType
    {
        [Description("Client")]
        Client,
        [Description("Pregnancy")]
        Pregnancy,
        [Description("Children")]
        Child,
        [Description("Work")]
        Work,
        [Description("School")]
        School,
        [Description("Points")]
        Points,
        [Description("Contact Information")]
        Contact,
        [Description("Housing")]
        Housing,
        [Description("Education History")]
        EducationHistory,
        [Description("Received Services")]
        ServicesReceived,
        [Description("Requested Services")]
        ServicesRequested
    }

    #endregion

    public abstract class Filter
    {
        #region Protected Fields

        //Currently have issues with protected name types not matching up
        //Properties have suggested names
        //TODO: Auto property?
        protected FilterType FilterType;
        protected string _fieldName;

        #endregion

        #region Properties

        public FilterType FilterTable
        {
            get
            {
                return FilterType;
            }
        }

        public string FieldName
        {
            get
            {
                return _fieldName;
            }
        }

        #endregion

        #region Constructor

        public Filter(FilterType filterType, string fieldName)
        {
            FilterType = filterType;
            _fieldName = fieldName;
        }

        #endregion

        #region Abstract Members

        public abstract string DisplayValue { get; }

        public abstract bool CheckFilter(Client client);

        #endregion
    }
}
