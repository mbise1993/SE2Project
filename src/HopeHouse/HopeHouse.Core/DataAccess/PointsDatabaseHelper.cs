using HopeHouse.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopeHouse.Core.DataAccess
{
    public static class PointsDatabaseHelper
    {
        private static SQLiteConnection _dbConnection = Database.GetConnection();

        public static void GetClientPoints(Client client)
        {
            client.Points = new PointsAggregation();

            if (_dbConnection != null)
            {
                string queryString = "SELECT * FROM Points WHERE ClientID = " + client.ClientId;
                using (SQLiteCommand sqlQuery = new SQLiteCommand(queryString, _dbConnection))
                {
                    SQLiteDataReader reader = sqlQuery.ExecuteReader();
                    while (reader.Read())
                    {
                        Points points = new Points()
                        {
                            PointId = Database.ConvertValue<int>(reader["PointID"]),
                            ClientId = Database.ConvertValue<int>(reader["ClientID"]),
                            Amount = Database.ConvertValue<int>(reader["Amount"]),
                            Date = Database.ConvertValue<DateTime>(reader["Date"]),
                            Reason = Database.ConvertValue<string>(reader["Reason"]),
                            StaffId = Database.ConvertValue<int>(reader["StaffID"])
                        };

                        client.Points.Add(points);
                    }
                }
            }
        }

        public static void AddClientPoints(int clientId, Points clientPointsInfo)
        {
            if (_dbConnection != null)
            {
                string stmtToAddClientPointsInfo = string.Format("INSERT INTO Points Values(NULL, '" + clientId +
                "','" + clientPointsInfo.Amount + "','" + clientPointsInfo.Date + "','" + clientPointsInfo.Reason + "','" +
                clientPointsInfo.StaffId + "')");

                using (SQLiteCommand sqlQuery = new SQLiteCommand(stmtToAddClientPointsInfo, _dbConnection))
                {
                    sqlQuery.ExecuteNonQuery();
                }

            }
        }

        public static void UpdatePointsEntry(Points points)
        {
            if (_dbConnection != null)
            {
                string stmtToUpdatePoints = string.Format("UPDATE Points SET Amount = \'" + points.Amount + "\', " +
                                                                                    "Date = \'" + Database.DateTimeToSqLite(points.Date) + "\', " +
                                                                                    "Reason = \'" + points.Reason + "\', " +
                                                                                    "StaffID = \'" + points.StaffId + "\' WHERE PointID = " + points.PointId);
                using (SQLiteCommand sqlQuery = new SQLiteCommand(stmtToUpdatePoints, _dbConnection))
                {
                    sqlQuery.ExecuteNonQuery();
                }
            }
        }

        public static void UpdatePoints(List<Points> points)
        {
            foreach(Points pointsEntry in points)
            {
                UpdatePointsEntry(pointsEntry);
            }
        }

        public static void DeletePoints(Points points)
        {
            if(_dbConnection != null)
            {
                string commandString = $"DELETE FROM Points WHERE PointsID={points.PointId}";

                using (SQLiteCommand command = new SQLiteCommand(commandString, _dbConnection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
