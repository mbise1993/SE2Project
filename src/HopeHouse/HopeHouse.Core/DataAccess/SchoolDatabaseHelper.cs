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
    public static class SchoolDatabaseHelper
    {
        private static SQLiteConnection _dbConnection = Database.GetConnection();

        public static void GetClientSchool(Client client)
        {
            if (_dbConnection != null)
            {
                string queryString = "SELECT * FROM School WHERE ClientID = " + client.ClientId;
                using (SQLiteCommand sqlQuery = new SQLiteCommand(queryString, _dbConnection))
                {
                    SQLiteDataReader reader = sqlQuery.ExecuteReader();
                    while (reader.Read())
                    {
                        client.School = new School()
                        {
                            ClientId = Database.ConvertValue<int>(reader["ClientID"]),
                            SchoolId = Database.ConvertValue<int>(reader["SchoolID"]),
                            SchoolName = Database.ConvertValue<string>(reader["SchoolName"]),
                            CurrentlyEnrolled = Database.ConvertValue<bool>(reader["CurrentlyEnroll"]),
                            HoursEnrolled = Database.ConvertValue<int>(reader["HoursEnrolled"]),
                            Term = Database.ConvertValue<string>(reader["Term"])
                        };
                    }
                }
            }
        }

        public static int AddClientSchool(int clientId, School clientSchoolInfo)
        {
            if (_dbConnection != null)
            {
                int schoolId;
                string statement =
                    "INSERT INTO School (SchoolID, ClientID, SchoolName, CurrentlyEnroll, HoursEnrolled, Term) " +
                    "VALUES (@SchoolID, @ClientID, @SchoolName, @CurrentlyEnroll, @HoursEnrolled, @Term);";

                using (SQLiteCommand cmd = new SQLiteCommand(statement, _dbConnection))
                {

                    cmd.Parameters.Add(new SQLiteParameter(@"SchoolID", DbType.Int32) { Value = DBNull.Value });
                    cmd.Parameters.Add(new SQLiteParameter(@"ClientID", DbType.Int32) { Value = clientId });
                    cmd.Parameters.Add(new SQLiteParameter(@"SchoolName", DbType.String) { Value = clientSchoolInfo.SchoolName });
                    cmd.Parameters.Add(new SQLiteParameter(@"CurrentlyEnroll", DbType.Boolean) { Value = clientSchoolInfo.CurrentlyEnrolled });
                    cmd.Parameters.Add(new SQLiteParameter(@"HoursEnrolled", DbType.Int32) { Value = clientSchoolInfo.HoursEnrolled });
                    cmd.Parameters.Add(new SQLiteParameter(@"Term", DbType.String) { Value = clientSchoolInfo.Term });


                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "SELECT last_insert_rowID()";
                    schoolId = (int)(long)cmd.ExecuteScalar();
                }
                return schoolId;
            }
            return -1;
        }

        public static void UpdateSchool(School school)
        {
            if (_dbConnection != null)
            {
                string stmtToUpdateSchool = string.Format("UPDATE School SET SchoolName = \'" + school.SchoolName + "\', " +
                                                                                    "CurrentlyEnroll = \'" + Database.BoolToSqLite(school.CurrentlyEnrolled) + "\', " +
                                                                                    "HoursEnrolled = \'" + school.HoursEnrolled + "\', " +
                                                                                    "Term = \'" + school.Term + "\' WHERE SchoolID = " + school.SchoolId);
                using (SQLiteCommand sqlQuery = new SQLiteCommand(stmtToUpdateSchool, _dbConnection))
                {
                    sqlQuery.ExecuteNonQuery();
                }
            }
        }
    }
}
