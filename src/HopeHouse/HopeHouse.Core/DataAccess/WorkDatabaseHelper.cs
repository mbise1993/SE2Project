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
    public static class WorkDatabaseHelper
    {
        private static SQLiteConnection _dbConnection = Database.GetConnection();

        public static void GetClientWork(Client client)
        {
            if (_dbConnection != null)
            {
                string queryString = "SELECT * FROM Work WHERE ClientID = " + client.ClientId;
                using (SQLiteCommand sqlQuery = new SQLiteCommand(queryString, _dbConnection))
                {
                    SQLiteDataReader reader = sqlQuery.ExecuteReader();
                    while (reader.Read())
                    {
                        client.Work = new Work()
                        {
                            WorkId = Database.ConvertValue<int>(reader["WorkID"]),
                            PartnerId = Database.ConvertValue<int>(reader["PartnerID"]),
                            Employer = Database.ConvertValue<string>(reader["Employer"]),
                            PhoneNumber = Database.ConvertValue<string>(reader["PhoneNumber"]),
                            WeeklyHours = Database.ConvertValue<int>(reader["WeeklyHours"]),
                            LivableWage = Database.ConvertValue<bool>(reader["LiveableWage"]),
                            ReasonNotWorking = Database.ConvertValue<string>(reader["ReasonNotWorking"]),
                            WillPursueWork = Database.ConvertValue<bool>(reader["WillPursueWork"])
                        };
                    }
                }
            }
        }

        public static void GetPartnerWork(Partner partner)
        {
            if (_dbConnection != null)
            {
                string queryString = "SELECT * FROM Work WHERE PartnerID = " + partner.PartnerID;
                using (SQLiteCommand sqlQuery = new SQLiteCommand(queryString, _dbConnection))
                {
                    SQLiteDataReader reader = sqlQuery.ExecuteReader();
                    while (reader.Read())
                    {
                        partner.Work = new Work()
                        {
                            WorkId = Database.ConvertValue<int>(reader["WorkID"]),
                            PartnerId = Database.ConvertValue<int>(reader["PartnerID"]),
                            Employer = Database.ConvertValue<string>(reader["Employer"]),
                            PhoneNumber = Database.ConvertValue<string>(reader["PhoneNumber"]),
                            WeeklyHours = Database.ConvertValue<int>(reader["WeeklyHours"]),
                            LivableWage = Database.ConvertValue<bool>(reader["LiveableWage"]),
                            ReasonNotWorking = Database.ConvertValue<string>(reader["ReasonNotWorking"]),
                            WillPursueWork = Database.ConvertValue<bool>(reader["WillPursueWork"])
                        };
                    }
                }
            }
        }

        public static int AddClientWork(int clientId, Work clientWorkInfo)
        {
            if (_dbConnection != null)
            {
                int workId;
                string statement =
                    "INSERT INTO Work (WorkID, ClientID, PartnerID, Employer, PhoneNumber, WeeklyHours, LiveableWage, " +
                    "ReasonNotWorking, WillPursueWork) " +
                    "VALUES (@WorkID, @ClientID, @PartnerID, @Employer, @PhoneNumber, @WeeklyHours, @LiveableWage, " +
                    "@ReasonNotWorking, @WillPursueWork);";

                using (SQLiteCommand cmd = new SQLiteCommand(statement, _dbConnection))
                {

                    cmd.Parameters.Add(new SQLiteParameter(@"WorkID", DbType.Int32) { Value = DBNull.Value });
                    cmd.Parameters.Add(new SQLiteParameter(@"ClientID", DbType.Int32) { Value = clientId });
                    cmd.Parameters.Add(new SQLiteParameter(@"PartnerID", DbType.Int32) { Value = DBNull.Value });
                    cmd.Parameters.Add(new SQLiteParameter(@"Employer", DbType.String) { Value = clientWorkInfo.Employer });
                    cmd.Parameters.Add(new SQLiteParameter(@"PhoneNumber", DbType.String) { Value = clientWorkInfo.PhoneNumber });
                    cmd.Parameters.Add(new SQLiteParameter(@"WeeklyHours", DbType.Int32) { Value = clientWorkInfo.WeeklyHours });
                    cmd.Parameters.Add(new SQLiteParameter(@"LiveableWage", DbType.Boolean) { Value = clientWorkInfo.LivableWage });
                    cmd.Parameters.Add(new SQLiteParameter(@"ReasonNotWorking", DbType.String) { Value = clientWorkInfo.ReasonNotWorking });
                    cmd.Parameters.Add(new SQLiteParameter(@"WillPursueWork", DbType.Boolean) { Value = clientWorkInfo.WillPursueWork });


                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "SELECT last_insert_rowID()";
                    workId = (int)(long)cmd.ExecuteScalar();
                }
                return workId;
            }
            return -1;
        }

        public static int AddPartnerWork(int partnerId, Work partnerWorkInfo)
        {
            if (_dbConnection != null)
            {
                int workId;
                string statement =
                    "INSERT INTO Work (WorkID, ClientID, PartnerID, Employer, PhoneNumber, WeeklyHours, LiveableWage, " +
                    "ReasonNotWorking, WillPursueWork) " +
                    "VALUES (@WorkID, @ClientID, @PartnerID, @Employer, @PhoneNumber, @WeeklyHours, @LiveableWage, " +
                    "@ReasonNotWorking, @WillPursueWork);";

                using (SQLiteCommand cmd = new SQLiteCommand(statement, _dbConnection))
                {

                    cmd.Parameters.Add(new SQLiteParameter(@"WorkID", DbType.Int32) { Value = DBNull.Value });
                    cmd.Parameters.Add(new SQLiteParameter(@"ClientID", DbType.Int32) { Value = DBNull.Value });
                    cmd.Parameters.Add(new SQLiteParameter(@"PartnerID", DbType.Int32) { Value = partnerId });
                    cmd.Parameters.Add(new SQLiteParameter(@"Employer", DbType.String) { Value = partnerWorkInfo.Employer });
                    cmd.Parameters.Add(new SQLiteParameter(@"PhoneNumber", DbType.String) { Value = partnerWorkInfo.PhoneNumber });
                    cmd.Parameters.Add(new SQLiteParameter(@"WeeklyHours", DbType.Int32) { Value = partnerWorkInfo.WeeklyHours });
                    cmd.Parameters.Add(new SQLiteParameter(@"LiveableWage", DbType.Boolean) { Value = partnerWorkInfo.LivableWage });
                    cmd.Parameters.Add(new SQLiteParameter(@"ReasonNotWorking", DbType.String) { Value = partnerWorkInfo.ReasonNotWorking });
                    cmd.Parameters.Add ( new SQLiteParameter ( @"WillPursueWork", DbType.Boolean ) { Value = partnerWorkInfo.WillPursueWork } );

                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "SELECT last_insert_rowID()";
                    workId = (int)(long)cmd.ExecuteScalar();
                }
                return workId;
            }
            return -1;
        }

        public static void UpdateWork(Work work)
        {
            if (_dbConnection != null)
            {
                string stmtToUpdateWork = string.Format("UPDATE Work SET Employer = \'" + work.Employer + "\', " +
                                                                                    "PhoneNumber = \'" + work.PhoneNumber + "\', " +
                                                                                    "WeeklyHours = \'" + work.WeeklyHours + "\', " +
                                                                                    "LiveableWage = \'" + Database.BoolToSqLite(work.LivableWage) + "\', " +
                                                                                    "ReasonNotWorking = \'" + work.ReasonNotWorking + "\', " +
                                                                                    "WillPursueWork = \'" + Database.BoolToSqLite(work.WillPursueWork) + "\' WHERE WorkID = " + work.WorkId);
                using (SQLiteCommand sqlQuery = new SQLiteCommand(stmtToUpdateWork, _dbConnection))
                {
                    sqlQuery.ExecuteNonQuery();
                }
            }
        }
    }
}
