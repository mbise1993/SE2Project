using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using HopeHouse.Core.Models;

namespace HopeHouse.Core.DataAccess
{
    public static class ClientDatabaseHelper
    {
        private static SQLiteConnection _dbConnection = Database.GetConnection();

        #region 'Get' Methods

        public static int GetNumberOfClients()
        {
            int numberOfClients = 0;

            if (_dbConnection != null)
            {
                string queryForNumOfClients = "SELECT COUNT(*) FROM Client";

                using (SQLiteCommand sqlQuery = new SQLiteCommand(queryForNumOfClients, _dbConnection))
                {
                    numberOfClients = (int)((Int64)sqlQuery.ExecuteScalar());
                }
            }

            return numberOfClients;
        }

        public static List<Client> GetAllClients()
        {
            Client client;
            List<Client> clientList = new List<Client>();

            // Query the Users database table for all clients
            if (_dbConnection != null)
            {
                string queryString = "SELECT * FROM Client";
                using (SQLiteCommand sqlQuery = new SQLiteCommand(queryString, _dbConnection))
                {
                    SQLiteDataReader reader = sqlQuery.ExecuteReader();

                    while (reader.Read())
                    {
                        client = new Client
                        {
                            Abortion = Database.ConvertValue<int>(reader["Abortion"]),
                            Address = Database.ConvertValue<string>(reader["Address"]),
                            Adoption = Database.ConvertValue<int>(reader["Adoption"]),
                            ApplyingForDisability = Database.ConvertValue<bool>(reader["ApplyingForDisability"]),
                            BirthDate = Database.ConvertValue<DateTime>(reader["BirthDate"]),
                            CanContact = Database.ConvertValue<bool>(reader["CanContact"]),
                            CarriedToTerm = Database.ConvertValue<int>(reader["CarriedToTerm"]),
                            CellNumber = Database.ConvertValue<string>(reader["CellNumber"]),
                            Church = Database.ConvertValue<string>(reader["Church"]),
                            City = Database.ConvertValue<string>(reader["City"]),
                            ClientId = Database.ConvertValue<int>(reader["ClientID"]),
                            CurrentlyPregnant = Database.ConvertValue<bool>(reader["CurrentlyPregnant"]),
                            DateEntered = Database.ConvertValue<DateTime>(reader["DateEntered"]),
                            DriversLicense = Database.ConvertValue<bool>(reader["DriversLicense"]),
                            FirstName = Database.ConvertValue<string>(reader["FirstName"]),
                            FosterCare = Database.ConvertValue<bool>(reader["FosterCare"]),
                            Gender = Database.ConvertValue<string>(reader["Gender"]),
                            HomeNumber = Database.ConvertValue<string>(reader["HomeNumber"]),
                            IsActive = Database.ConvertValue<bool>(reader["IsActive"]),
                            LastName = Database.ConvertValue<string>(reader["LastName"]),
                            MaritalStatus = Database.ConvertValue<string>(reader["MaritalStatus"]),
                            MiddleInit = Database.ConvertValue<string>(reader["MiddleInit"]),
                            Miscarriage = Database.ConvertValue<int>(reader["Miscarriage"]),
                            NumberOfPregnancy = Database.ConvertValue<int>(reader["NumberOfPregnancy"]),
                            PartnerId = Database.ConvertValue<int>(reader["PartnerID"]),
                            Prayer = Database.ConvertValue<bool>(reader["Prayer"]),
                            Race = Database.ConvertValue<string>(reader["Race"]),
                            ReceivingDisability = Database.ConvertValue<bool>(reader["ReceivingDisability"]),
                            ReferralSource = Database.ConvertValue<string>(reader["ReferralSource"]),
                            SchoolId = Database.ConvertValue<int>(reader["SchoolID"]),
                            Smoke = Database.ConvertValue<bool>(reader["Smoke"]),
                            StaffUsername = Database.ConvertValue<string>(reader["StaffUsername"]),
                            State = Database.ConvertValue<string>(reader["State"]),
                            WorkId = Database.ConvertValue<int>(reader["WorkID"]),
                            Zip = Database.ConvertValue<string>(reader["Zip"])
                        };

                        ChildDatabaseHelper.GetClientChildren(client);
                        PartnerDatabaseHelper.GetClientPartner(client);
                        WorkDatabaseHelper.GetClientWork(client);
                        SchoolDatabaseHelper.GetClientSchool(client);
                        PointsDatabaseHelper.GetClientPoints(client);
                        ContactDatabaseHelper.GetClientContact(client);
                        HousingDatabaseHelper.GetClientHousing(client);
                        EducationHistoryDatabaseHelper.GetClientEducationHistory(client);
                        ServicesReceivedDatabaseHelper.GetClientServicesReceived(client);
                        ServicesRequestedDatabaseHelper.GetClientServicesRequested(client);

                        clientList.Add(client);
                    }
                }
                return clientList;
            }
            return null;
        }

        #endregion

        #region 'Add' Methods

        public static void AddClient ( Client client )
        {
            if (_dbConnection != null)
            {
                int clientId,
                    schoolId,
                    workId,
                    partnerId;

                string statement = "INSERT INTO Client (ClientID, FirstName, MiddleInit, LastName, BirthDate, Gender, " +
                                   "HomeNumber, CellNumber, Address, City, State, Zip, MaritalStatus, PartnerID, ReferralSource, " +
                                   "CurrentlyPregnant, NumberOfPregnancy, Race, FosterCare, DriversLicense, Smoke, Church, SchoolId, " +
                                   "WorkId, ReceivingDisability, ApplyingForDisability, CanContact, Prayer, DateEntered, " +
                                   "StaffUsername, IsActive, CarriedToTerm, Miscarriage, Adoption, Abortion) " +
                                   "VALUES (@ClientID, @FirstName, @MiddleInit, @LastName, @BirthDate, @Gender, " +
                                   "@HomeNumber, @CellNumber, @Address, @City, @State, @Zip, @MaritalStatus, @PartnerID, @ReferralSource, " +
                                   "@CurrentlyPregnant, @NumberOfPregnancy, @Race, @FosterCare, @DriversLicense, @Smoke, @Church, @SchoolId, " +
                                   "@WorkId, @ReceivingDisability, @ApplyingForDisability, @CanContact, @Prayer, @DateEntered, @StaffUsername, "+
                                   "@IsActive, @CarriedToTerm, @Miscarriage, @Adoption, @Abortion);";

                using (SQLiteCommand cmd = new SQLiteCommand ( statement, _dbConnection ))
                {

                    cmd.Parameters.Add ( new SQLiteParameter ( @"ClientID", DbType.Int32 ) { Value = DBNull.Value } );
                    cmd.Parameters.Add ( new SQLiteParameter ( @"FirstName", DbType.String ) { Value = client.FirstName } );
                    cmd.Parameters.Add ( new SQLiteParameter ( @"MiddleInit", DbType.String ) { Value = client.MiddleInit } );
                    cmd.Parameters.Add ( new SQLiteParameter ( @"LastName", DbType.String ) { Value = client.LastName } );
                    cmd.Parameters.Add ( new SQLiteParameter ( @"BirthDate", DbType.DateTime ) { Value = client.BirthDate } );
                    cmd.Parameters.Add ( new SQLiteParameter ( @"Gender", DbType.String ) { Value = client.Gender } );
                    cmd.Parameters.Add ( new SQLiteParameter ( @"HomeNumber", DbType.String ) { Value = client.HomeNumber } );
                    cmd.Parameters.Add ( new SQLiteParameter ( @"CellNumber", DbType.String ) { Value = client.CellNumber } );
                    cmd.Parameters.Add ( new SQLiteParameter ( @"Address", DbType.String ) { Value = client.Address } );
                    cmd.Parameters.Add ( new SQLiteParameter ( @"City", DbType.String ) { Value = client.City } );
                    cmd.Parameters.Add ( new SQLiteParameter ( @"State", DbType.String ) { Value = client.State } );
                    cmd.Parameters.Add ( new SQLiteParameter ( @"Zip", DbType.String ) { Value = client.Zip } );
                    cmd.Parameters.Add ( new SQLiteParameter ( @"MaritalStatus", DbType.String ) { Value = client.MaritalStatus } );
                    cmd.Parameters.Add ( new SQLiteParameter ( @"PartnerID", DbType.Int32 ) { Value = client.PartnerId } );
                    cmd.Parameters.Add ( new SQLiteParameter ( @"ReferralSource", DbType.String ) { Value = client.ReferralSource } );
                    cmd.Parameters.Add ( new SQLiteParameter ( @"CurrentlyPregnant", DbType.Boolean ) { Value = client.CurrentlyPregnant } );
                    cmd.Parameters.Add ( new SQLiteParameter ( @"NumberOfPregnancy", DbType.Int32 ) { Value = client.NumberOfPregnancy } );
                    cmd.Parameters.Add ( new SQLiteParameter ( @"Race", DbType.String ) { Value = client.Race } );
                    cmd.Parameters.Add ( new SQLiteParameter ( @"FosterCare", DbType.Boolean ) { Value = client.FosterCare } );
                    cmd.Parameters.Add ( new SQLiteParameter ( @"DriversLicense", DbType.Boolean ) { Value = client.DriversLicense } );
                    cmd.Parameters.Add ( new SQLiteParameter ( @"Smoke", DbType.Boolean ) { Value = client.Smoke } );
                    cmd.Parameters.Add ( new SQLiteParameter ( @"Church", DbType.String ) { Value = client.Church } );
                    cmd.Parameters.Add ( new SQLiteParameter ( @"SchoolID", DbType.Int32 ) { Value = client.SchoolId } );
                    cmd.Parameters.Add ( new SQLiteParameter ( @"WorkID", DbType.Int32 ) { Value = client.WorkId } );
                    cmd.Parameters.Add ( new SQLiteParameter ( @"ReceivingDisability", DbType.Boolean ) { Value = client.ReceivingDisability } );
                    cmd.Parameters.Add ( new SQLiteParameter ( @"ApplyingForDisability", DbType.Boolean ) { Value = client.ApplyingForDisability } );
                    cmd.Parameters.Add ( new SQLiteParameter ( @"CanContact", DbType.Boolean ) { Value = client.CanContact } );
                    cmd.Parameters.Add ( new SQLiteParameter ( @"Prayer", DbType.Boolean ) { Value = client.Prayer } );
                    cmd.Parameters.Add ( new SQLiteParameter ( @"DateEntered", DbType.DateTime) {Value = client.DateEntered } );
                    cmd.Parameters.Add ( new SQLiteParameter(@"StaffUsername", DbType.String) { Value = client.StaffUsername } );
                    cmd.Parameters.Add ( new SQLiteParameter(@"IsActive", DbType.Boolean) { Value = client.IsActive } );
                    cmd.Parameters.Add ( new SQLiteParameter(@"CarriedToTerm", DbType.Int32) { Value = client.CarriedToTerm } );
                    cmd.Parameters.Add ( new SQLiteParameter(@"Miscarriage", DbType.Int32 ) { Value = client.Miscarriage } );
                    cmd.Parameters.Add ( new SQLiteParameter(@"Abortion", DbType.Int32 ) { Value = client.Abortion } );
                    cmd.Parameters.Add ( new SQLiteParameter(@"Adoption", DbType.Int32 ) { Value = client.Adoption } );

                    cmd.ExecuteNonQuery ( );

                    cmd.CommandText = "SELECT last_insert_rowID()";
                    clientId = (int) (long)cmd.ExecuteScalar ( ); // the id of the client just added
                  }

                if (client.Partner != null)
                {
                    partnerId = PartnerDatabaseHelper.AddClientPartner(clientId, client.Partner);

                    if (partnerId != -1)
                    {
                        UpdateClientPartnerId(clientId, partnerId);
                        client.Pregnancy.PartnerId = partnerId;
                    }

                    if (client.Partner.Work != null)
                    {
                        int pworkId = WorkDatabaseHelper.AddPartnerWork(partnerId, client.Partner.Work);
                        if (pworkId != -1)
                        {
                            UpdatePartnerWorkId(partnerId, pworkId);
                            client.Partner.WorkID = pworkId;
                        }
                    }
                }

                if(client.EducationHistory != null)
                {
                    EducationHistoryDatabaseHelper.AddClientEducationHistory(clientId, client.EducationHistory);
                }

                if (client.Work != null)
                {
                    workId = WorkDatabaseHelper.AddClientWork(clientId, client.Work);

                    if (workId != -1)
                    {
                        UpdateClientWorkId ( clientId, workId ); 
                    }
                }

                if ( client.School !=null )
                {
                    schoolId = SchoolDatabaseHelper.AddClientSchool(clientId, client.School);

                    if (schoolId != -1)
                    {
                        UpdateClientSchoolId ( clientId, schoolId );
                    }
                }

                if(client.Housing != null)
                {
                    HousingDatabaseHelper.AddClientHousing(clientId, client.Housing);
                }

                if(client.Pregnancy != null)
                {
                    PregnancyDatabaseHelper.AddClientPregnancy(clientId, client.Pregnancy);
                }

                if (client.Children != null)
                {
                    ChildDatabaseHelper.AddClientChildren(clientId, client.Children);
                }

                //
                // TODO: Not sure if this is even needed...the intake form is simply a yes/no answer
                //
                if (client.ServicesReceived != null)
                {
                    ServicesReceivedDatabaseHelper.AddClientServicesReceived(clientId, client.ServicesReceived);
                }

                if (client.ServicesRequested != null)
                {
                    ServicesRequestedDatabaseHelper.AddClientServicesRequested(clientId, client.ServicesRequested);
                }
            }
        }

        #endregion

        #region 'Update' Methods

        //Method to update the passed client's database entry with new values
        public static void UpdateClient(Client client)
        {
            if (_dbConnection != null)
            {
                string stmtToUpdatePatient = string.Format("UPDATE Client SET FirstName = \'" + client.FirstName + "\', " +
                                                                                  "MiddleInit = \'" + client.MiddleInit + "\', " +
                                                                                  "LastName = \'" + client.LastName + "\', " +
                                                                                  "BirthDate = \'" + Database.DateTimeToSqLite(client.BirthDate) + "\', " +
                                                                                  "Gender = \'" + client.Gender + "\', " +
                                                                                  "HomeNumber = \'" + client.HomeNumber + "\', " +
                                                                                  "CellNumber = \'" + client.CellNumber + "\', " +
                                                                                  "Address = \'" + client.Address + "\', " +
                                                                                  "City = \'" + client.City + "\', " +
                                                                                  "State = \'" + client.State + "\', " +
                                                                                  "Zip = \'" + client.Zip + "\', " +
                                                                                  "MaritalStatus = \'" + client.MaritalStatus + "\', " +
                                                                                  "PartnerID = \'" + client.PartnerId + "\', " +
                                                                                  "ReferralSource = \'" + client.ReferralSource + "\', " +
                                                                                  "CurrentlyPregnant = \'" + Database.BoolToSqLite(client.CurrentlyPregnant) + "\', " +
                                                                                  "NumberOfPregnancy = \'" + client.NumberOfPregnancy + "\', " +
                                                                                  "Race = \'" + client.Race + "\', " +
                                                                                  "FosterCare = \'" + Database.BoolToSqLite(client.FosterCare) + "\', " +
                                                                                  "DriversLicense = \'" + Database.BoolToSqLite(client.DriversLicense) + "\', " +
                                                                                  "Smoke = \'" + Database.BoolToSqLite(client.Smoke) + "\', " +
                                                                                  "Church = \'" + client.Church + "\', " +
                                                                                  "SchoolID = \'" + client.SchoolId + "\', " +
                                                                                  "WorkID = \'" + client.WorkId + "\', " +
                                                                                  "ReceivingDisability = \'" + Database.BoolToSqLite(client.ReceivingDisability) + "\', " +
                                                                                  "ApplyingForDisability = \'" + Database.BoolToSqLite(client.ApplyingForDisability) + "\', " +
                                                                                  "CanContact = \'" + Database.BoolToSqLite(client.CanContact) + "\', " +
                                                                                  "Prayer = \'" + client.Prayer + "\' WHERE ClientID = " + client.ClientId + ";");
                using (SQLiteCommand sqlQuery = new SQLiteCommand(stmtToUpdatePatient, _dbConnection))
                {
                    sqlQuery.ExecuteNonQuery();
                }

                if (client.Pregnancy != null)
                {
                    PregnancyDatabaseHelper.UpdatePregnancy(client.Pregnancy);
                }
                if (client.Children != null)
                {
                    ChildDatabaseHelper.UpdateChildren(client.Children);
                }
                if (client.Partner != null)
                {
                    PartnerDatabaseHelper.UpdatePartner(client.Partner);
                }
                if (client.Work != null)
                {
                    WorkDatabaseHelper.UpdateWork(client.Work);
                }
                if (client.School != null)
                {
                    SchoolDatabaseHelper.UpdateSchool(client.School);
                }
                if (client.Points != null)
                {
                    PointsDatabaseHelper.UpdatePoints(client.Points);
                }
                if (client.Contact != null)
                {
                    ContactDatabaseHelper.UpdateContact(client.Contact);
                }
                if (client.Housing != null)
                {
                    HousingDatabaseHelper.UpdateHousing(client.Housing);
                }
                if (client.EducationHistory != null)
                {
                    EducationHistoryDatabaseHelper.UpdateEducationHistory(client.EducationHistory);
                }
                if (client.ServicesReceived != null)
                {
                    ServicesReceivedDatabaseHelper.UpdateServicesReceived(client.ServicesReceived);
                }
                if (client.ServicesRequested != null)
                {
                    ServicesRequestedDatabaseHelper.UpdateServicesRequested(client.ServicesRequested);
                }
            }
        }

        /// <summary>
        /// Work table entry is made after creating a client, so we need a way to go back and update the client with work id.
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="workId"></param>
        private static void UpdateClientWorkId(int clientId, int workId)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(_dbConnection))
            {
                cmd.CommandText = "UPDATE client SET workId = @work WHERE clientId = @client";

                cmd.Parameters.Add(new SQLiteParameter(@"work", DbType.Int32) {Value = workId});
                cmd.Parameters.Add(new SQLiteParameter(@"client", DbType.Int32) {Value = clientId});

                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// School table entry is made after creating a client, so we need a way to go back and update the client with school id.
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="schoolId"></param>
        private static void UpdateClientSchoolId ( int clientId, int schoolId )
        {
            using (SQLiteCommand cmd = new SQLiteCommand ( _dbConnection ))
            {
                cmd.CommandText = "UPDATE client SET schoolId = @school WHERE clientId = @client";

                cmd.Parameters.Add ( new SQLiteParameter ( @"school", DbType.Int32 ) { Value = schoolId } );
                cmd.Parameters.Add ( new SQLiteParameter ( @"client", DbType.Int32 ) { Value = clientId } );

                cmd.ExecuteNonQuery ( );
            }
        }

        /// <summary>
        /// Partner table entry is made after creating a client, so we need a way to go back and update the client with partner id.
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="partnerId"></param>
        private static void UpdateClientPartnerId ( int clientId, int partnerId )
        {
            using (SQLiteCommand cmd = new SQLiteCommand ( _dbConnection ))
            {
                cmd.CommandText = "UPDATE client SET partnerId = @partner WHERE clientId = @client";

                cmd.Parameters.Add ( new SQLiteParameter ( @"partner", DbType.Int32 ) { Value = partnerId } );
                cmd.Parameters.Add ( new SQLiteParameter ( @"client", DbType.Int32 ) { Value = clientId } );

                cmd.ExecuteNonQuery ( );
            }
        }

        /// <summary>
        /// Work table entry is made after creating a partner, so we need a way to go back and update the partner with work id.
        /// </summary>
        /// <param name="partnerId"></param>
        /// <param name="workId"></param>
        private static void UpdatePartnerWorkId ( int partnerId, int workId )
        {
            using (SQLiteCommand cmd = new SQLiteCommand ( _dbConnection ))
            {
                cmd.CommandText = "UPDATE partner SET workId = @work WHERE partnerId = @partner";

                cmd.Parameters.Add ( new SQLiteParameter ( @"work", DbType.Int32 ) { Value = workId } );
                cmd.Parameters.Add ( new SQLiteParameter ( @"partner", DbType.Int32 ) { Value = partnerId } );

                cmd.ExecuteNonQuery ( );
            }
        }

        #endregion

        #region 'Delete' Methods

        public static void DeleteClient(Client client)
        {
            int clientId = client.ClientId;
            string queryToDeleteClient = "DELETE FROM Client WHERE ClientID = " + clientId;
            string queryToDeleteClientSchool = "DELETE FROM School WHERE ClientID = " + clientId;
            string queryToDeleteClientWork = "DELETE FROM Work WHERE ClientID = " + clientId;
            string queryToDeleteClientPartner = "DELETE FROM Partner WHERE ClientID = " + clientId;
            string queryToDeleteClientChild = "DELETE FROM Child WHERE ClientID = " + clientId;
            string queryToDeleteClientEducationHistory = "DELETE FROM EducationHistory WHERE ClientID = " + clientId;
            string queryToDeleteClientHousing = "DELETE FROM Housing WHERE ClientID = " + clientId;
            string queryToDeleteClientServicesReceived = "DELETE FROM ServicesReceived WHERE ClientID = " + clientId;
            string queryToDeleteClientServicesRequested = "DELETE FROM ServicesRequested WHERE ClientID = " + clientId;
            string queryToDeleteClientContact = "DELETE FROM Contact WHERE ClientID = " + clientId;
            string queryToDeleteClientPoints = "DELETE FROM Points WHERE ClientID = " + clientId;

            using (SQLiteCommand sqlQuery = new SQLiteCommand(queryToDeleteClient, _dbConnection))
            {
                sqlQuery.ExecuteNonQuery();
                sqlQuery.CommandText = queryToDeleteClientChild;
                sqlQuery.ExecuteNonQuery();
                sqlQuery.CommandText = queryToDeleteClientSchool;
                sqlQuery.ExecuteNonQuery();
                sqlQuery.CommandText = queryToDeleteClientWork;
                sqlQuery.ExecuteNonQuery();
                sqlQuery.CommandText = queryToDeleteClientPartner;
                sqlQuery.ExecuteNonQuery();
                sqlQuery.CommandText = queryToDeleteClientEducationHistory;
                sqlQuery.ExecuteNonQuery();
                sqlQuery.CommandText = queryToDeleteClientHousing;
                sqlQuery.ExecuteNonQuery();
                sqlQuery.CommandText = queryToDeleteClientServicesReceived;
                sqlQuery.ExecuteNonQuery();
                sqlQuery.CommandText = queryToDeleteClientServicesRequested;
                sqlQuery.ExecuteNonQuery();
                sqlQuery.CommandText = queryToDeleteClientContact;
                sqlQuery.ExecuteNonQuery();
                sqlQuery.CommandText = queryToDeleteClientPoints;
                sqlQuery.ExecuteNonQuery();
            }
        }

        #endregion
    }
}
