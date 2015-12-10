using System;

namespace HopeHouse.Core.DataAccess
{
    public class AddedThisYearFilter : DateFilter
    {
        #region Constructor

        public AddedThisYearFilter() : base(FilterType.Client, "Date Entered", "Any", "Any", DateTime.Now.Year.ToString())
        {

        }

        #endregion

        #region Overridden Members

        public override string DisplayValue
        {
            get
            {
                return "Added This Year";
            }
        }

        #endregion
    }
}
