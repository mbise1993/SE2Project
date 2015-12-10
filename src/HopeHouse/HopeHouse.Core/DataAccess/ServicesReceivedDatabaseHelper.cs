using HopeHouse.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopeHouse.Core.DataAccess
{
    public static class ServicesReceivedDatabaseHelper
    {
        private static SQLiteConnection _dbConnection = Database.GetConnection();

        public static void GetClientServicesReceived(Client client)
        {
            client.ServicesReceived = new ServicesReceivedAggregation();

            if (_dbConnection != null)
            {
                string queryString = "SELECT * FROM ServicesReceived WHERE ClientID = " + client.ClientId;
                using (SQLiteCommand sqlQuery = new SQLiteCommand(queryString, _dbConnection))
                {
                    SQLiteDataReader reader = sqlQuery.ExecuteReader();
                    while (reader.Read())
                    {
                        Service service = new Service()
                        {
                            ClientId = Database.ConvertValue<int>(reader["ClientID"]),
                            Date = Database.ConvertValue<DateTime>(reader["Date"]),
                            Description = Database.ConvertValue<string>(reader["Description"]),
                            ReceivedId = Database.ConvertValue<int>(reader["ReceivedID"]),
                            StaffId = Database.ConvertValue<int>(reader["StaffID"]),
                        };

                        client.ServicesReceived.Add(service);
                    }
                }
            }
        }

        public static void AddClientServiceReceived(int clientId, Service serviceReceived)
        {
            if (_dbConnection != null)
            {
                string stmtToAddClientServiceReceived = string.Format("INSERT INTO ServicesReceived Values(NULL, '" + clientId +
                "','" + Database.DateTimeToSqLite(serviceReceived.Date) + "','" + serviceReceived.Description + "','" + serviceReceived.StaffId + "')");

                using (SQLiteCommand sqlQuery = new SQLiteCommand(stmtToAddClientServiceReceived, _dbConnection))
                {
                    sqlQuery.ExecuteNonQuery();
                }

            }
        }

        public static void AddClientServicesReceived(int clientId, List<Service> servicesReceived)
        {
            foreach(Service serviceReceived in servicesReceived)
            {
                AddClientServiceReceived(clientId, serviceReceived);
            }
        }

        public static void UpdateServiceReceived(Service serviceReceived)
        {
            if (_dbConnection != null)
            {
                string stmtToUpdateServiceReceived = string.Format("UPDATE ServicesReceived SET Date = \'" + Database.DateTimeToSqLite(serviceReceived.Date) + "\', " +
                                                                                    "Description = \'" + serviceReceived.Description + "\', " +
                                                                                    "StaffID = \'" + serviceReceived.StaffId + "\' WHERE ReceivedID = " + serviceReceived.ReceivedId);
                using (SQLiteCommand sqlQuery = new SQLiteCommand(stmtToUpdateServiceReceived, _dbConnection))
                {
                    sqlQuery.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateServicesReceived(List<Service> servicesReceived)
        {
            foreach(Service serviceReceived in servicesReceived)
            {
                UpdateServiceReceived(serviceReceived);
            }
        }

        public static void DeleteServiceReceived(Service serviceReceived)
        {
            if (_dbConnection != null)
            {
                string commandString = $"DELETE FROM ServicesReceived WHERE ReceivedID={serviceReceived.ReceivedId}";

                using (SQLiteCommand command = new SQLiteCommand(commandString, _dbConnection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
