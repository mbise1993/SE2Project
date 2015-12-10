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
    public static class ContactDatabaseHelper
    {
        private static SQLiteConnection _dbConnection = Database.GetConnection();

        public static void GetClientContact(Client client)
        {
            if (_dbConnection != null)
            {
                string queryString = "SELECT * FROM Contact WHERE ClientID = " + client.ClientId;
                using (SQLiteCommand sqlQuery = new SQLiteCommand(queryString, _dbConnection))
                {
                    SQLiteDataReader reader = sqlQuery.ExecuteReader();
                    while (reader.Read())
                    {
                        client.Contact = new Contact()
                        {
                            ContactId = Database.ConvertValue<int>(reader["ContactID"]),
                            ContactType = Database.ConvertValue<string>(reader["ContactType"]),
                            ClientId = Database.ConvertValue<int>(reader["ClientID"]),
                            FirstName = Database.ConvertValue<string>(reader["FirstName"]),
                            MiddleInit = Database.ConvertValue<string>(reader["MiddleInit"]),
                            LastName = Database.ConvertValue<string>(reader["LastName"]),
                            Reason = Database.ConvertValue<string>(reader["Reason"]),
                            StaffId = Database.ConvertValue<int>(reader["StaffID"])
                        };
                    }
                }
            }
        }

        public static void AddClientContact(int clientId, Contact clientContactInfo)
        {
            if (_dbConnection != null)
            {
                string statement =
                    "INSERT INTO Contact (ContactID, ClientID, Client, FirstName, MiddleInit, LastName, Reason " +
                    "InOffice, StaffID) " +
                    "VALUES (@ContactID, @ClientID, @FirstName, @MiddleInit, @LastName, @Reason, @ContactType, @StaffID);";

                using (SQLiteCommand cmd = new SQLiteCommand(statement, _dbConnection))
                {

                    cmd.Parameters.Add(new SQLiteParameter(@"ContactID", DbType.Int32) { Value = DBNull.Value });
                    cmd.Parameters.Add(new SQLiteParameter(@"ClientID", DbType.Int32) { Value = clientId });
                    cmd.Parameters.Add(new SQLiteParameter(@"FirstName", DbType.String) { Value = clientContactInfo.FirstName });
                    cmd.Parameters.Add(new SQLiteParameter(@"MiddleInit", DbType.String) { Value = clientContactInfo.MiddleInit });
                    cmd.Parameters.Add(new SQLiteParameter(@"LastName", DbType.String) { Value = clientContactInfo.LastName });
                    cmd.Parameters.Add(new SQLiteParameter(@"Reason", DbType.String) { Value = clientContactInfo.Reason });
                    cmd.Parameters.Add(new SQLiteParameter(@"ContactType", DbType.Boolean) { Value = clientContactInfo.ContactType });
                    cmd.Parameters.Add(new SQLiteParameter(@"StaffID", DbType.Int32) { Value = clientContactInfo.StaffId });


                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateContact(Contact contact)
        {
            if (_dbConnection != null)
            {
                string stmtToUpdateContact = string.Format("UPDATE Contact SET FirstName = \'" + contact.FirstName + "\', " +
                                                                                    "MiddleInit = \'" + contact.MiddleInit + "\', " +
                                                                                    "LastName = \'" + contact.LastName + "\', " +
                                                                                    "Reason = \'" + contact.Reason + "\', " +
                                                                                    "ContactType = \'" + contact.ContactType + "\', " +
                                                                                    "StaffID = \'" + contact.StaffId + "\' WHERE ContactID = " + contact.ContactId);
                using (SQLiteCommand sqlQuery = new SQLiteCommand(stmtToUpdateContact, _dbConnection))
                {
                    sqlQuery.ExecuteNonQuery();
                }
            }
        }
    }
}
