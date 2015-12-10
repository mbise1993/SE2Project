using HopeHouse.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopeHouse.Core.DataAccess
{
    public static class HousingDatabaseHelper
    {
        private static SQLiteConnection _dbConnection = Database.GetConnection();

        public static void GetClientHousing(Client client)
        {
            if (_dbConnection != null)
            {
                string queryString = "SELECT * FROM Housing WHERE ClientID = " + client.ClientId;
                using (SQLiteCommand sqlQuery = new SQLiteCommand(queryString, _dbConnection))
                {
                    SQLiteDataReader reader = sqlQuery.ExecuteReader();
                    while (reader.Read())
                    {
                        client.Housing = new Housing()
                        {
                            HousingId = Database.ConvertValue<int>(reader["HousingID"]),
                            ClientId = Database.ConvertValue<int>(reader["ClientID"]),
                            PartnerId = Database.ConvertValue<int>(reader["PartnerID"]),
                            HousingStatus = Database.ConvertValue<string>(reader["HousingStatus"]),
                            ResidentialProgram = Database.ConvertValue<bool>(reader["ResidentialProgram"])
                        };
                    }
                }
            }
        }

        public static void AddClientHousing(int clientId, Housing clientHousingInfo)
        {
            if (_dbConnection != null)
            {
                string statement =
                    "INSERT INTO Housing (HousingID, ClientID, PartnerID, HousingStatus, ResidentialProgram) " +
                    "VALUES (@HousingID, @ClientID, @PartnerID, @HousingStatus, @ResidentialProgram);";

                using (SQLiteCommand cmd = new SQLiteCommand(statement, _dbConnection))
                {

                    cmd.Parameters.Add(new SQLiteParameter(@"HousingID", DbType.Int32) { Value = DBNull.Value });
                    cmd.Parameters.Add(new SQLiteParameter(@"ClientID", DbType.Int32) { Value = clientId });
                    cmd.Parameters.Add(new SQLiteParameter(@"PartnerID", DbType.Int32) { Value = clientHousingInfo.PartnerId });
                    cmd.Parameters.Add(new SQLiteParameter(@"HousingStatus", DbType.String) { Value = clientHousingInfo.HousingStatus });
                    cmd.Parameters.Add(new SQLiteParameter(@"ResidentialProgram", DbType.Boolean) { Value = clientHousingInfo.ResidentialProgram });


                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateHousing(Housing housing)
        {
            if (_dbConnection != null)
            {
                string stmtToUpdateHousing = string.Format("UPDATE Housing SET PartnerID = \'" + housing.PartnerId + "\', " +
                                                                                    "HousingStatus = \'" + housing.HousingStatus + "\', " +
                                                                                    "ResidentialProgram = \'" + Database.BoolToSqLite(housing.ResidentialProgram) + "\' WHERE HousingID = " + housing.HousingId);
                using (SQLiteCommand sqlQuery = new SQLiteCommand(stmtToUpdateHousing, _dbConnection))
                {
                    sqlQuery.ExecuteNonQuery();
                }
            }
        }
    }
}
