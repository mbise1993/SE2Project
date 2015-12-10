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
    public static class PregnancyDatabaseHelper
    {
        private static SQLiteConnection _dbConnection = Database.GetConnection();

        public static void GetClientPregnancy(Client client)
        {
            if (_dbConnection != null)
            {
                string queryString = "SELECT * FROM Pregnancy WHERE ClientID = " + client.ClientId;
                using (SQLiteCommand sqlQuery = new SQLiteCommand(queryString, _dbConnection))
                {
                    SQLiteDataReader reader = sqlQuery.ExecuteReader();

                    while (reader.Read())
                    {
                        client.Pregnancy = new Pregnancy()
                        {
                            ClientId = Database.ConvertValue<int>(reader["ClientID"]),
                            PregnancyId = Database.ConvertValue<int>(reader["PregnancyID"]),
                            PartnerId = Database.ConvertValue<int>(reader["PartnerID"]),
                            ReturnPregnancy = Database.ConvertValue<bool>(reader["ReturnPregnancy"]),
                            VerifiedPregnancy = Database.ConvertValue<bool>(reader["VerifiedPregnancy"]),
                            NeedPregnancyTest = Database.ConvertValue<bool>(reader["NeedPregnancyTest"]),
                            SignedReleaseForTest = Database.ConvertValue<bool>(reader["SignedReleaseForTest"]),
                            DueDate = Database.ConvertValue<DateTime>(reader["DueDate"]),
                            CarriedToTerm = Database.ConvertValue<bool>(reader["CarriedToTerm"]),
                            Intentions = Database.ConvertValue<string>(reader["Intentions"]),
                            BirthControl = Database.ConvertValue<bool>(reader["BirthControl"]),
                            MedInsurance = Database.ConvertValue<bool>(reader["MedInsurance"]),
                            Ob = Database.ConvertValue<string>(reader["OB"]),
                            PrenatalVitamin = Database.ConvertValue<bool>(reader["PrenatalVitamin"]),
                            Wic = Database.ConvertValue<bool>(reader["WIC"]),
                            Hugs = Database.ConvertValue<bool>(reader["HUGS"]),
                            HugsNurse = Database.ConvertValue<bool>(reader["HUGS_Nurse"]),
                            FoodStamps = Database.ConvertValue<bool>(reader["FoodStamps"]),
                            FamiliesFirst = Database.ConvertValue<bool>(reader["FamiliesFirst"]),
                            LifeBridgeFss = Database.ConvertValue<bool>(reader["LifeBridge_FSS"]),
                            ResultOfPregnancy = Database.ConvertValue<string>(reader["ResultOfPregnancy"]),
                            StaffId = Database.ConvertValue<int>(reader["StaffID"])
                        };
                    }
                }
            }
        }

        public static void AddClientPregnancy(int clientId, Pregnancy clientPregInfo)
        {
            if (_dbConnection != null)
            {
                string statement =
                    "INSERT INTO Pregnancy (PregnancyID, ClientID, PartnerID, ReturnPregnancy, VerifiedPregnancy, NeedPregnancyTest, " +
                    "SignedReleaseForTest, DueDate, CarriedToTerm, Intentions, BirthControl, MedInsurance, OB, PrenatalVitamin, WIC, " +
                    "HUGS, HUGS_Nurse, FoodStamps, FamiliesFirst, LifeBridge_FSS, ResultOfPregnancy, StaffID) " +
                    "VALUES (@PregnancyID, @ClientID, @PartnerID, @ReturnPregnancy, @VerifiedPregnancy, @NeedPregnancyTest, " +
                    "@SignedReleaseForTest, @DueDate, @CarriedToTerm, @Intentions, @BirthControl, @MedInsurance, @OB, @PrenatalVitamin, @WIC, " +
                    "@HUGS, @HUGS_Nurse, @FoodStamps, @FamiliesFirst, @LifeBridge_FSS, @ResultOfPregnancy, @StaffID);";

                using (SQLiteCommand cmd = new SQLiteCommand(statement, _dbConnection))
                {

                    cmd.Parameters.Add(new SQLiteParameter(@"PregnancyID", DbType.Int32) { Value = DBNull.Value });
                    cmd.Parameters.Add(new SQLiteParameter(@"ClientID", DbType.Int32) { Value = clientId });
                    cmd.Parameters.Add(new SQLiteParameter(@"PartnerID", DbType.Int32) { Value = clientPregInfo.PartnerId });
                    cmd.Parameters.Add(new SQLiteParameter(@"ReturnPregnancy", DbType.Boolean) { Value = clientPregInfo.ReturnPregnancy });
                    cmd.Parameters.Add(new SQLiteParameter(@"VerifiedPregnancy", DbType.Boolean) { Value = clientPregInfo.VerifiedPregnancy });
                    cmd.Parameters.Add(new SQLiteParameter(@"NeedPregnancyTest", DbType.Boolean) { Value = clientPregInfo.NeedPregnancyTest });
                    cmd.Parameters.Add(new SQLiteParameter(@"SignedReleaseForTest", DbType.Boolean) { Value = clientPregInfo.SignedReleaseForTest });
                    cmd.Parameters.Add(new SQLiteParameter(@"DueDate", DbType.DateTime) { Value = clientPregInfo.DueDate });
                    cmd.Parameters.Add(new SQLiteParameter(@"CarriedToTerm", DbType.String) { Value = clientPregInfo.CarriedToTerm });
                    cmd.Parameters.Add(new SQLiteParameter(@"Intentions", DbType.String) { Value = clientPregInfo.Intentions });
                    cmd.Parameters.Add(new SQLiteParameter(@"BirthControl", DbType.Boolean) { Value = clientPregInfo.BirthControl });
                    cmd.Parameters.Add(new SQLiteParameter(@"MedInsurance", DbType.Boolean) { Value = clientPregInfo.MedInsurance });
                    cmd.Parameters.Add(new SQLiteParameter(@"OB", DbType.String) { Value = clientPregInfo.Ob });
                    cmd.Parameters.Add(new SQLiteParameter(@"PrenatalVitamin", DbType.Boolean) { Value = clientPregInfo.PrenatalVitamin });
                    cmd.Parameters.Add(new SQLiteParameter(@"WIC", DbType.Boolean) { Value = clientPregInfo.Wic });
                    cmd.Parameters.Add(new SQLiteParameter(@"HUGS", DbType.Boolean) { Value = clientPregInfo.Hugs });
                    cmd.Parameters.Add(new SQLiteParameter(@"HUGS_Nurse", DbType.Boolean) { Value = clientPregInfo.HugsNurse });
                    cmd.Parameters.Add(new SQLiteParameter(@"FoodStamps", DbType.Boolean) { Value = clientPregInfo.FoodStamps });
                    cmd.Parameters.Add(new SQLiteParameter(@"FamiliesFirst", DbType.Boolean) { Value = clientPregInfo.FamiliesFirst });
                    cmd.Parameters.Add(new SQLiteParameter(@"LifeBridge_FSS", DbType.Boolean) { Value = clientPregInfo.LifeBridgeFss });
                    cmd.Parameters.Add(new SQLiteParameter(@"ResultOfPregnancy", DbType.String) { Value = clientPregInfo.ResultOfPregnancy });
                    cmd.Parameters.Add(new SQLiteParameter(@"StaffID", DbType.Int32) { Value = clientPregInfo.StaffId });


                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void UpdatePregnancy(Pregnancy pregnancy)
        {
            if (_dbConnection != null)
            {
                string stmtToUpdatePregnancy = string.Format("UPDATE Pregnancy SET ReturnPregnancy = \'" + Database.BoolToSqLite(pregnancy.ReturnPregnancy) + "\', " +
                                                                                  "VerifiedPregnancy = \'" + Database.BoolToSqLite(pregnancy.VerifiedPregnancy) + "\', " +
                                                                                  "NeedPregnancyTest = \'" + Database.BoolToSqLite(pregnancy.NeedPregnancyTest) + "\', " +
                                                                                  "SignedReleaseForTest = \'" + Database.BoolToSqLite(pregnancy.SignedReleaseForTest) + "\', " +
                                                                                  "DueDate = \'" + Database.DateTimeToSqLite(pregnancy.DueDate) + "\', " +
                                                                                  "CarriedToTerm = \'" + pregnancy.CarriedToTerm + "\', " +
                                                                                  "Intentions = \'" + pregnancy.Intentions + "\', " +
                                                                                  "BirthControl = \'" + Database.BoolToSqLite(pregnancy.BirthControl) + "\', " +
                                                                                  "MedInsurance = \'" + Database.BoolToSqLite(pregnancy.MedInsurance) + "\', " +
                                                                                  "OB = \'" + pregnancy.Ob + "\', " +
                                                                                  "PrenatalVitamin = \'" + Database.BoolToSqLite(pregnancy.PrenatalVitamin) + "\', " +
                                                                                  "WIC = \'" + Database.BoolToSqLite(pregnancy.Wic) + "\', " +
                                                                                  "HUGS = \'" + Database.BoolToSqLite(pregnancy.Hugs) + "\', " +
                                                                                  "HUGS_Nurse = \'" + Database.BoolToSqLite(pregnancy.HugsNurse) + "\', " +
                                                                                  "FoodStamps = \'" + Database.BoolToSqLite(pregnancy.FoodStamps) + "\', " +
                                                                                  "FamiliesFirst = \'" + Database.BoolToSqLite(pregnancy.FamiliesFirst) + "\', " +
                                                                                  "LifeBridge_FSS = \'" + Database.BoolToSqLite(pregnancy.LifeBridgeFss) + "\', " +
                                                                                  "ResultOfPregnancy = \'" + pregnancy.ResultOfPregnancy + "\', " +
                                                                                  "StaffID = \'" + pregnancy.StaffId + "\' WHERE PregnancyID = " + pregnancy.PregnancyId);
                using (SQLiteCommand sqlQuery = new SQLiteCommand(stmtToUpdatePregnancy, _dbConnection))
                {
                    sqlQuery.ExecuteNonQuery();
                }
            }
        }
    }
}
