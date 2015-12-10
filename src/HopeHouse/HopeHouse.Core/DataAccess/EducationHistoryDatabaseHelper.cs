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
    public static class EducationHistoryDatabaseHelper
    {
        private static SQLiteConnection _dbConnection = Database.GetConnection();

        public static void GetClientEducationHistory(Client client)
        {
            if (_dbConnection != null)
            {
                string queryString = "SELECT * FROM EducationHistory WHERE ClientID = " + client.ClientId;
                using (SQLiteCommand sqlQuery = new SQLiteCommand(queryString, _dbConnection))
                {
                    SQLiteDataReader reader = sqlQuery.ExecuteReader();
                    while (reader.Read())
                    {
                        client.EducationHistory = new EducationHistory()
                        {
                            EducationId = Database.ConvertValue<int>(reader["EducationID"]),
                            ClientId = Database.ConvertValue<int>(reader["ClientID"]),
                            HighSchoolGrad = Database.ConvertValue<bool>(reader["HighSchoolGrad"]),
                            DropOutGrade = Database.ConvertValue<double>(reader["DropOutGrade"]),
                            Ged = Database.ConvertValue<bool>(reader["GED"]),
                            ObtainingGed = Database.ConvertValue<bool>(reader["ObtainingGED"]),
                            CollegeGrad = Database.ConvertValue<bool>(reader["CollegeGrad"]),
                            InCollege = Database.ConvertValue<bool>(reader["InCollege"])
                        };
                    }
                }
            }
        }

        public static void AddClientEducationHistory(int clientId, EducationHistory clientEducationHistory)
        {
            if (_dbConnection != null)
            {
                string statement =
                    "INSERT INTO EducationHistory (EducationID, ClientID, HighSchoolGrad, DropOutGrade, GED, " +
                    "ObtainingGED, CollegeGrad, InCollege) " +
                    "VALUES (@EducationID, @ClientID, @HighSchoolGrad, @DropOutGrade, @GED, @ObtainingGED, @CollegeGrad, @InCollege);";

                using (SQLiteCommand cmd = new SQLiteCommand(statement, _dbConnection))
                {

                    cmd.Parameters.Add(new SQLiteParameter(@"EducationID", DbType.Int32) { Value = DBNull.Value });
                    cmd.Parameters.Add(new SQLiteParameter(@"ClientID", DbType.Int32) { Value = clientId });
                    cmd.Parameters.Add(new SQLiteParameter(@"highSchoolGrad", DbType.Boolean) { Value = clientEducationHistory.HighSchoolGrad });
                    cmd.Parameters.Add(new SQLiteParameter(@"DropOutGrade", DbType.Double) { Value = clientEducationHistory.DropOutGrade });
                    cmd.Parameters.Add(new SQLiteParameter(@"GED", DbType.Boolean) { Value = clientEducationHistory.Ged });
                    cmd.Parameters.Add(new SQLiteParameter(@"ObtainingGED", DbType.Boolean) { Value = clientEducationHistory.ObtainingGed });
                    cmd.Parameters.Add(new SQLiteParameter(@"CollegeGrad", DbType.Boolean) { Value = clientEducationHistory.CollegeGrad });
                    cmd.Parameters.Add(new SQLiteParameter(@"InCollege", DbType.Boolean) { Value = clientEducationHistory.InCollege });


                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateEducationHistory(EducationHistory educationHistory)
        {
            if (_dbConnection != null)
            {
                string stmtToUpdateEducationHistory = string.Format("UPDATE EducationHistory SET HighSchoolGrad = \'" + Database.BoolToSqLite(educationHistory.HighSchoolGrad) + "\', " +
                                                                                    "DropOutGrade = \'" + educationHistory.DropOutGrade + "\', " +
                                                                                    "GED = \'" + Database.BoolToSqLite(educationHistory.Ged) + "\', " +
                                                                                    "ObtainingGED = \'" + Database.BoolToSqLite(educationHistory.ObtainingGed) + "\', " +
                                                                                    "CollegeGrad = \'" + Database.BoolToSqLite(educationHistory.CollegeGrad) + "\', " +
                                                                                    "InCollege = \'" + Database.BoolToSqLite(educationHistory.InCollege) + "\' WHERE EducationID = " + educationHistory.EducationId);
                using (SQLiteCommand sqlQuery = new SQLiteCommand(stmtToUpdateEducationHistory, _dbConnection))
                {
                    sqlQuery.ExecuteNonQuery();
                }
            }
        }
    }
}
