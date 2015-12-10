using System;
using System.Collections.Generic;
using System.Data.SQLite;
using HopeHouse.Core.Models;
using HopeHouse.Common.Helpers;
using System.Data;

namespace HopeHouse.Core.DataAccess
{
    public static class StaffDatabaseHelper
    {
        private static SQLiteConnection _dbConnection = Database.GetConnection();

        public static Staff AuthenticateStaff(string userName, string password)
        {
            String queryString = String.Format("SELECT * FROM Staff WHERE Username='{0}'", userName);
            using (SQLiteCommand sqlQuery = new SQLiteCommand(queryString, _dbConnection))
            {
                SQLiteDataReader reader = sqlQuery.ExecuteReader();

                while (reader.Read())
                {
                    if (reader["Password"].Equals((password)))
                    {
                        return new Staff
                        {
                            StaffId = Database.ConvertValue<int>(reader["StaffID"]),
                            FirstName = Database.ConvertValue<string>(reader["FirstName"]),
                            MiddleInit = Database.ConvertValue<string>(reader["MiddleInit"]),
                            LastName = Database.ConvertValue<string>(reader["LastName"]),
                            Username = Database.ConvertValue<string>(reader["Username"]),
                            PhoneNumber = Database.ConvertValue<string>(reader["PhoneNumber"]),
                            IsAdministrator = Database.ConvertValue<bool>(reader["IsAdministrator"]),
                            IsActive = Database.ConvertValue<bool>(reader["IsActive"])
                        };
                    }
                }
            }

            return null;

        }

        public static List<Staff> GetAllStaff()
        {
            Staff staff;
            List<Staff> staffList = new List<Staff>();

            if (_dbConnection != null)
            {
                string queryString = "SELECT * FROM Staff";
                using (SQLiteCommand sqlQuery = new SQLiteCommand(queryString, _dbConnection))
                {
                    SQLiteDataReader reader = sqlQuery.ExecuteReader();

                    while (reader.Read())
                    {
                        staff = new Staff
                        {
                            StaffId = Database.ConvertValue<int>(reader["StaffID"]),
                            FirstName = Database.ConvertValue<string>(reader["FirstName"]),
                            MiddleInit = Database.ConvertValue<string>(reader["MiddleInit"]),
                            LastName = Database.ConvertValue<string>(reader["LastName"]),
                            Username = Database.ConvertValue<string>(reader["Username"]),
                            Password = Database.ConvertValue<string>(reader["Password"]),
                            PhoneNumber = Database.ConvertValue<string>(reader["PhoneNumber"]),
                            IsAdministrator = Database.SqLiteToBool(Database.ConvertValue<int>(reader["IsAdministrator"]))//,
                            //IsActive = Database.SqLiteToBool((int)((Int64)reader["IsActive"]))

                        };

                        staffList.Add(staff);
                    }
                }
                return staffList;
            }
            return null;
        }

        public static void AddStaff(Staff staff)
        {
            if (_dbConnection != null)
            {
                string statement = "INSERT INTO Staff (StaffID,FirstName, MiddleInit, LastName, Username, Password, PhoneNumber, IsAdministrator, IsActive)" +
                                   "VALUES(null,@FirstName, @MiddleInit, @LastName, @Username, @Password, @PhoneNumber, @IsAdministrator, 1)";

                using (SQLiteCommand cmd = new SQLiteCommand(statement, _dbConnection))
                {
                    cmd.Parameters.Add(new SQLiteParameter(@"FirstName", DbType.String) { Value = staff.FirstName });
                    cmd.Parameters.Add(new SQLiteParameter(@"MiddleInit", DbType.String) { Value = staff.MiddleInit });
                    cmd.Parameters.Add(new SQLiteParameter(@"LastName", DbType.String) { Value = staff.LastName });
                    cmd.Parameters.Add(new SQLiteParameter(@"Username", DbType.String) { Value = staff.Username });
                    cmd.Parameters.Add(new SQLiteParameter(@"Password", DbType.String) { Value = staff.Password });
                    cmd.Parameters.Add(new SQLiteParameter(@"PhoneNumber", DbType.String) { Value = staff.PhoneNumber });
                    cmd.Parameters.Add(new SQLiteParameter(@"IsAdministrator", DbType.Boolean) { Value = staff.IsAdministrator });

                    cmd.ExecuteNonQuery();
                }
            }

        }

        public static void UpdateStaff(Staff staff)
        {
            if (_dbConnection != null)
            {
                string stmtToUpdateStaff = string.Format("UPDATE Staff SET FirstName = \'" + staff.FirstName + "\', " +
                                                                                  "MiddleInit = \'" + staff.MiddleInit + "\', " +
                                                                                  "LastName = \'" + staff.LastName + "\', " +
                                                                                  "UserName = \'" + staff.Username + "\', " +
                                                                                  "Password = \'" + PasswordEncrypt.EncryptPassword(staff.Password) + "\', " +
                                                                                  "PhoneNumber = \'" + staff.PhoneNumber + "\', " +
                                                                                  "IsAdministrator = \'" + Database.BoolToSqLite(staff.IsAdministrator) + "\' WHERE StaffID = " + staff.StaffId);
                using (SQLiteCommand sqlQuery = new SQLiteCommand(stmtToUpdateStaff, _dbConnection))
                {
                    sqlQuery.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteStaff(Staff staff)
        {
            int staffId = staff.StaffId;
            string queryToDeleteStaff = "DELETE FROM Staff WHERE StaffID = " + staffId;

            using (SQLiteCommand sqlQuery = new SQLiteCommand(queryToDeleteStaff, _dbConnection))
            {
                sqlQuery.ExecuteNonQuery();
            }

        }
    }
}
