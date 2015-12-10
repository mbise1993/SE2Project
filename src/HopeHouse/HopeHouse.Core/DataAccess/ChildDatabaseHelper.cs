using HopeHouse.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopeHouse.Core.DataAccess
{
    public static class ChildDatabaseHelper
    {
        private static SQLiteConnection _dbConnection = Database.GetConnection();

        public static void GetClientChildren(Client client)
        {
            client.Children = new ChildAggregation();

            if (_dbConnection != null)
            {
                string queryString = "SELECT * FROM Child WHERE ClientID = " + client.ClientId;
                using (SQLiteCommand sqlQuery = new SQLiteCommand(queryString, _dbConnection))
                {
                    SQLiteDataReader reader = sqlQuery.ExecuteReader();
                    while (reader.Read())
                    {
                        Child child = new Child()
                        {
                            ClientId = Database.ConvertValue<int>(reader["ClientID"]),
                            ChildId = Database.ConvertValue<int>(reader["ChildID"]),
                            PartnerId = Database.ConvertValue<int>(reader["PartnerID"]),
                            FirstName = Database.ConvertValue<string>(reader["FirstName"]),
                            MiddleInit = Database.ConvertValue<string>(reader["MiddleInit"]),
                            LastName = Database.ConvertValue<string>(reader["LastName"]),
                            BirthDate = Database.ConvertValue<DateTime>(reader["BirthDate"]),
                            Pediatrician = Database.ConvertValue<string>(reader["Pediatrician"]),
                            ReceivingHealthcare = Database.ConvertValue<bool>(reader["ReceivingHealthcare"]),
                            InDaycare = Database.ConvertValue<bool>(reader["InDaycare"]),
                            Disabled = Database.ConvertValue<bool>(reader["Disabled"])
                        };

                        client.Children.Add(child);
                    }
                }
            }
        }

        public static void AddClientChild(int clientId, Child clientChildInfo)
        {
            if (_dbConnection != null)
            {
                string stmtToAddClientHousingInfo = string.Format("INSERT INTO Child Values(NULL, '" + clientId +
                "','" + clientChildInfo.PartnerId + "','" + clientChildInfo.FirstName + "','" + clientChildInfo.MiddleInit + "','" +
                clientChildInfo.LastName + "','" + Database.DateTimeToSqLite(clientChildInfo.BirthDate) + "','" + clientChildInfo.Pediatrician
                + "','" + clientChildInfo.ReceivingHealthcare + "','" + clientChildInfo.InDaycare + "','" + clientChildInfo.Disabled + "')");

                using (SQLiteCommand sqlQuery = new SQLiteCommand(stmtToAddClientHousingInfo, _dbConnection))
                {
                    sqlQuery.ExecuteNonQuery();
                }

            }
        }

        public static void AddClientChildren(int clientId, List<Child> children)
        {
            foreach(Child child in children)
            {
                AddClientChild(clientId, child);
            }
        }

        public static void UpdateChild(Child child)
        {
            if (_dbConnection != null)
            {
                string stmtToUpdateChild = string.Format("UPDATE Child SET FirstName = \'" + child.FirstName + "\', " +
                                                                                    "MiddleInit = \'" + child.MiddleInit + "\', " +
                                                                                    "LastName = \'" + child.LastName + "\', " +
                                                                                    "BirthDate = \'" + Database.DateTimeToSqLite(child.BirthDate) + "\', " +
                                                                                    "Pediatrician = \'" + child.Pediatrician + "\', " +
                                                                                    "ReceivingHealthCare = \'" + Database.BoolToSqLite(child.ReceivingHealthcare) + "\', " +
                                                                                    "InDaycare = \'" + Database.BoolToSqLite(child.InDaycare) + "\', " +
                                                                                    "Disabled = \'" + Database.BoolToSqLite(child.Disabled) + "\' WHERE ChildID = " + child.ChildId);
                using (SQLiteCommand sqlQuery = new SQLiteCommand(stmtToUpdateChild, _dbConnection))
                {
                    sqlQuery.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateChildren(List<Child> children)
        {
            foreach(Child child in children)
            {
                UpdateChild(child);
            }
        }

        public static void DeleteChild(Child child)
        {
            if(_dbConnection != null)
            {
                string commandString = $"DELETE FROM Child WHERE ChildID={child.ChildId}";

                using (SQLiteCommand command = new SQLiteCommand(commandString, _dbConnection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
