using HopeHouse.Core.Models;

namespace HopeHouse.Core.DataAccess
{
    public class AllClientsFilter : Filter
    {
        #region Constructor

        public AllClientsFilter() : base(FilterType.Client, "First Name")
        {

        }

        #endregion

        public override string DisplayValue
        {
            get
            {
                return "All Clients";
            }
        }

        public override bool CheckFilter(Client client)
        {
            return true;
        }
    }
}
