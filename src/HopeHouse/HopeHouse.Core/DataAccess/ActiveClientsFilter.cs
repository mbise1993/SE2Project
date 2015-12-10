namespace HopeHouse.Core.DataAccess
{
    public class ActiveClientsFilter : StringFilter
    {
        #region Constructor

        public ActiveClientsFilter() : base(FilterType.Client, "Is Active?", "True")
        {
            
        }

        #endregion

        #region Overridden Members

        public override string DisplayValue
        {
            get
            {
                return "Active";
            }
        }

        #endregion
    }
}
