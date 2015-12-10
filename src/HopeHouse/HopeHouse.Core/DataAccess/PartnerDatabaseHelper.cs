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
    public static class PartnerDatabaseHelper
    {
        private static SQLiteConnection _dbConnection = Database.GetConnection();

        public static void GetClientPartner(Client client)
        {
            if (_dbConnection != null)
            {
                string queryString = "SELECT * FROM Partner WHERE ClientID = " + client.ClientId;
                using (SQLiteCommand sqlQuery = new SQLiteCommand(queryString, _dbConnection))
                {
                    SQLiteDataReader reader = sqlQuery.ExecuteReader();
                    while (reader.Read())
                    {
                        client.Partner = new Partner()
                        {
                            Address = Database.ConvertValue<string>(reader["Address"]),
                            Birthday = Database.ConvertValue<DateTime>(reader["BirthDate"]),
                            City = Database.ConvertValue<string>(reader["City"]),
                            ClientID = Database.ConvertValue<int>(reader["ClientID"]),
                            FirstName = Database.ConvertValue<string>(reader["FirstName"]),
                            LastName = Database.ConvertValue<string>(reader["LastName"]),
                            LiveTogether = Database.ConvertValue<bool>(reader["LiveTogether"]),
                            MiddleInit = Database.ConvertValue<string>(reader["MiddleInit"]),
                            PartnerID = Database.ConvertValue<int>(reader["PartnerID"]),
                            PhoneNumber = Database.ConvertValue<string>(reader["PhoneNumber"]),
                            State = Database.ConvertValue<string>(reader["State"]),
                            Supportive = Database.ConvertValue<bool>(reader["Supportive"]),
                            WorkID = Database.ConvertValue<int>(reader["WorkID"]),
                            Zip = Database.ConvertValue<string>(reader["Zip"]),
                        };

                        WorkDatabaseHelper.GetPartnerWork(client.Partner);
                    }
                }
            }
        }

        public static int AddClientPartner(int clientId, Partner partner)
        {
            if (_dbConnection != null)
            {
                int partnerID;
                string statement =
                    "INSERT INTO Partner (PartnerID, ClientID, FirstName, MiddleInit, LastName, " +
                    "BirthDate, Address, City, State, Zip, PhoneNumber, LiveTogether, Supportive, WorkID) " +
                    "VALUES (@PartnerID, @ClientID, @FirstName, @MiddleInit, @LastName, @BirthDate, @Address, @City, " +
                    "@State, @Zip, @PhoneNumber, @LiveTogether, @Supportive, @WorkID);";

                using (SQLiteCommand cmd = new SQLiteCommand(statement, _dbConnection))
                {

                    cmd.Parameters.Add(new SQLiteParameter(@"PartnerID", DbType.Int32) { Value = DBNull.Value });
                    cmd.Parameters.Add(new SQLiteParameter(@"ClientID", DbType.Int32) { Value = clientId });
                    cmd.Parameters.Add(new SQLiteParameter(@"FirstName", DbType.String) { Value = partner.FirstName });
                    cmd.Parameters.Add(new SQLiteParameter(@"MiddleInit", DbType.String) { Value = partner.MiddleInit });
                    cmd.Parameters.Add(new SQLiteParameter(@"LastName", DbType.String) { Value = partner.LastName });
                    cmd.Parameters.Add(new SQLiteParameter(@"BirthDate", DbType.DateTime) { Value = partner.Birthday });
                    cmd.Parameters.Add(new SQLiteParameter(@"Address", DbType.String) { Value = partner.Address });
                    cmd.Parameters.Add(new SQLiteParameter(@"City", DbType.String) { Value = partner.City });
                    cmd.Parameters.Add(new SQLiteParameter(@"State", DbType.String) { Value = partner.State });
                    cmd.Parameters.Add(new SQLiteParameter(@"Zip", DbType.String) { Value = partner.Zip });
                    cmd.Parameters.Add(new SQLiteParameter(@"PhoneNumber", DbType.String) { Value = partner.PhoneNumber });
                    cmd.Parameters.Add(new SQLiteParameter(@"LiveTogether", DbType.Boolean) { Value = partner.LiveTogether });
                    cmd.Parameters.Add(new SQLiteParameter(@"Supportive", DbType.Boolean) { Value = partner.Supportive });
                    cmd.Parameters.Add(new SQLiteParameter(@"WorkID", DbType.Int32) { Value = partner.WorkID });


                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "SELECT last_insert_rowID()";
                    partnerID = (int)(long)cmd.ExecuteScalar();
                }
                return partnerID;
            }
            return -1;
        }

        public static void UpdatePartner(Partner partner)
        {
            if (_dbConnection != null)
            {
                string stmtToUpdateWork = string.Format("UPDATE Partner SET FirstName = \'" + partner.FirstName + "\', " +
                                                                                    "MiddleInit = \'" + partner.MiddleInit + "\', " +
                                                                                    "LastName = \'" + partner.LastName + "\', " +
                                                                                    "BirthDate = \'" + Database.DateTimeToSqLite(partner.Birthday) + "\', " +
                                                                                    "Address = \'" + partner.Address + "\', " +
                                                                                    "City = \'" + partner.City + "\', " +
                                                                                    "State = \'" + partner.State + "\', " +
                                                                                    "Zip = \'" + partner.Zip + "\', " +
                                                                                    "PhoneNumber = \'" + partner.PhoneNumber + "\', " +
                                                                                    "LiveTogether = \'" + Database.BoolToSqLite(partner.LiveTogether) + "\', " +
                                                                                    "Supportive = \'" + Database.BoolToSqLite(partner.Supportive) + "\', " +
                                                                                    "WorkID = \'" + partner.WorkID + "\' WHERE PartnerID = " + partner.PartnerID + ";");
                using (SQLiteCommand sqlQuery = new SQLiteCommand(stmtToUpdateWork, _dbConnection))
                {
                    sqlQuery.ExecuteNonQuery();
                }

                if (partner.Work != null)
                {
                    WorkDatabaseHelper.UpdateWork(partner.Work);
                }
                
            }
        }
    }
}
