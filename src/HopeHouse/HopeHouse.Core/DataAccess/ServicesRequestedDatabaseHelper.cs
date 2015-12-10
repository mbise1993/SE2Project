using HopeHouse.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopeHouse.Core.DataAccess
{
    public static class ServicesRequestedDatabaseHelper
    {
        private static SQLiteConnection _dbConnection = Database.GetConnection();

        public static void GetClientServicesRequested(Client client)
        {
            client.ServicesRequested = new ServicesRequestedAggregation();

            if (_dbConnection != null)
            {
                string queryString = "SELECT * FROM ServicesRequested WHERE ClientID = " + client.ClientId;
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
                            RequestId = Database.ConvertValue<int>(reader["RequestID"]),
                            StaffId = Database.ConvertValue<int>(reader["StaffID"]),
                        };

                        client.ServicesRequested.Add(service);
                    }
                }
            }
        }

        public static void AddClientServiceRequested(int clientId, Service serviceRequested)
        {
            if (_dbConnection != null)
            {
                string stmtToAddClientServiceRequested = string.Format("INSERT INTO ServicesRequested Values(NULL, '" + clientId +
                "','" + Database.DateTimeToSqLite(serviceRequested.Date) + "','" + serviceRequested.Description + "','" + serviceRequested.StaffId + "')");

                using (SQLiteCommand sqlQuery = new SQLiteCommand(stmtToAddClientServiceRequested, _dbConnection))
                {
                    sqlQuery.ExecuteNonQuery();
                }

            }
        }

        public static void AddClientServicesRequested(int clientId, List<Service> servicesRequested)
        {
            foreach (Service serviceRequested in servicesRequested)
            {
                AddClientServiceRequested(clientId, serviceRequested);
            }
        }

        public static void UpdateServiceRequested(Service serviceRequested)
        {
            if (_dbConnection != null)
            {
                string stmtToUpdateServiceRequested = string.Format("UPDATE ServicesRequested SET Date = \'" + Database.DateTimeToSqLite(serviceRequested.Date) + "\', " +
                                                                                    "Description = \'" + serviceRequested.Description + "\', " +
                                                                                    "StaffID = \'" + serviceRequested.StaffId + "\' WHERE RequestID = " + serviceRequested.RequestId);
                using (SQLiteCommand sqlQuery = new SQLiteCommand(stmtToUpdateServiceRequested, _dbConnection))
                {
                    sqlQuery.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateServicesRequested(List<Service> servicesRequested)
        {
            foreach (Service serviceRequested in servicesRequested)
            {
                UpdateServiceRequested(serviceRequested);
            }
        }

        public static void DeleteServiceRequested(Service serviceRequested)
        {
            if (_dbConnection != null)
            {
                string commandString = $"DELETE FROM ServicesRequested WHERE RequestID={serviceRequested.RequestId}";

                using (SQLiteCommand command = new SQLiteCommand(commandString, _dbConnection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
