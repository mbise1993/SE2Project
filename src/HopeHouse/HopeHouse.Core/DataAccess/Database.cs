using HopeHouse.Common.Helpers;
using HopeHouse.Common.Logging;
using HopeHouse.Core.Models;
using System;
using System.Data.SQLite;
using System.IO;

namespace HopeHouse.Core.DataAccess
{
    public static class Database
    {
        #region Private Fields

        private static string _databasePath = "../Database/HopeHouse.sqlite";
        private static SQLiteConnection _dbConnection;

        #endregion

        #region Public Methods

        public static bool Initialize(string databasePath = null)
        {
            string path;
            if (!string.IsNullOrEmpty(databasePath))
            {
                path = databasePath;
            }
            else
            {
                path = _databasePath;
            }

            if (!File.Exists(path))
            {
                return false;
            }

            string connectionString = $"Data Source={path};Version=3";

            try
            {
                _dbConnection = new SQLiteConnection(connectionString);
                _dbConnection.Open();
            }
            catch (Exception e)
            {
                Logger.WriteErrorLogEntry(new ErrorLogEntry(e));
                return false;
            }

            return true;
        }

        public static bool CreateDatabase(string pathWithName)
        {
            try
            {
                SQLiteConnection.CreateFile(pathWithName);
                SQLiteCommand createCommand;
                SQLiteConnection newDatabase = new SQLiteConnection(string.Format("Data Source={0};Version=3;", pathWithName));

                newDatabase.Open();
                createCommand = new SQLiteCommand(
                "CREATE TABLE Client(" +
                            "ClientID integer primary key," +
                            "FirstName text,MiddleInit text," +
                            "LastName text," +
                            "BirthDate text," +
                            "Gender text," +
                            "HomeNumber text," +
                            "CellNumber text," +
                            "Address text," +
                            "City text," +
                            "State text, " +
                            "Zip text," +
                            "MaritalStatus text," +
                            "PartnerID integer," +
                            "ReferralSource text," +
                            "CurrentlyPregnant integer," +
                            "NumberOfPregnancy integer," +
                            "Race text," +
                            "FosterCare integer," +
                            "DriversLicense integer," +
                            "Smoke integer," +
                            "Church text," +
                            "SchoolID integer," +
                            "WorkID integer," +
                            "ReceivingDisability integer," +
                            "ApplyingForDisability integer," +
                            "CanContact integer," +
                            "Prayer integer," +
                            "DateEntered text," +
                            "StaffUsername text," +
                            "IsActive integer," +
                            "FOREIGN KEY(PartnerID) REFERENCES Partner(PartnerID)," +
                            "foreign key(SchoolID) references School(SchoolID)," +
                            "foreign key(WorkID) references Work(WorkID));"
                + "CREATE TABLE EducationHistory(" +
                            "EducationID integer primary key," +
                            "ClientID integer," +
                            "HighSchoolGrad integer," +
                            "DropOutGrade real," +
                            "GED integer," +
                            "ObtainingGED integer," +
                            "CollegeGrad integer," +
                            "InCollege integer," +
                            "foreign key(ClientID) references Client(ClientID));"
                + "CREATE TABLE School(" +
                            "SchoolID integer primary key," +
                            "ClientID integer, SchoolName text," +
                            "CurrentlyEnroll integer," +
                            "HoursEnrolled integer," +
                            "Term text, foreign key(ClientID) references Client(ClientID));"
                + "CREATE TABLE Points(" +
                            "PointID integer primary key," +
                            "ClientID integer," +
                            "Amount integer," +
                            "Date text," +
                            "Reason text," +
                            "StaffID integer," +
                            "foreign key(ClientID) references Client(ClientID)," +
                            "foreign key(StaffID) references Staff(StaffID));"
                + "CREATE TABLE Staff(" +
                            "StaffID integer primary key," +
                            "FirstName text," +
                            "MiddleInit text," +
                            "LastName text," +
                            "Username text," +
                            "Password text," +
                            "PhoneNumber text," +
                            "IsAdministrator integer, " +
                            "IsActive integer);"
                + "CREATE TABLE Contact(" +
                            "ContactID integer primary key," +
                            "ClientID integer," +
                            "Client integer," +
                            "FirstName text," +
                            "MiddleInit text," +
                            "LastName text," +
                            "Reason text," +
                            "InOffice integer," +
                            "StaffID integer," +
                            "foreign key(ClientID) references Client(ClientID));"
                + "CREATE TABLE Child(" +
                            "ChildID integer primary key, " +
                            "ClientID integer," +
                            "PartnerID integer," +
                            "FirstName text," +
                            "MiddleInit text," +
                            "LastName text," +
                            "BirthDate text," +
                            "Pediatrician text," +
                            "ReceivingHealthCare integer," +
                            "InDaycare integer," +
                            "Disabled integer," +
                            "foreign key(ClientID) references Client(ClientID)," +
                            "foreign key(PartnerID) references Partner(PartnerID));"
                + "CREATE TABLE Pregnancy(" +
                            "PregnancyID integer primary key," +
                            "ClientID integer," +
                            "PartnerID integer," +
                            "ReturnPregnancy integer," +
                            "VerifiedPregnancy integer," +
                            "NeedPregnancyTest integer," +
                            "SignedReleaseForTest integer," +
                            "DueDate text, CarriedToTerm integer," +
                            "Intentions text," +
                            "BirthControl integer," +
                            "MedInsurance integer," +
                            "OB text," +
                            "PrenatalVitamin integer," +
                            "WIC integer," +
                            "HUGS integer," +
                            "HUGS_Nurse integer," +
                            "FoodStamps integer," +
                            "FamiliesFirst integer," +
                            "LifeBridge_FSS integer," +
                            "ResultOfPregnancy text," +
                            "StaffID integer," +
                            "foreign key(ClientID) references Client(ClientID)," +
                            "foreign key(PartnerID) references Partner(PartnerID)," +
                            "foreign key(StaffID) references Staff(StaffID));"
                + "CREATE TABLE Partner(" +
                            "PartnerID integer primary key," +
                            "ClientID integer," +
                            "FirstName text," +
                            "MiddleInit text," +
                            "LastName text," +
                            "BirthDate text," +
                            "Address text," +
                            "City text," +
                            "State text," +
                            "Zip text," +
                            "PhoneNumber text," +
                            "LiveTogether integer," +
                            "Supportive integer," +
                            "WorkID integer," +
                            "foreign key(ClientID) references Client(ClientID)," +
                            "foreign key(WorkID) references Work(WorkID));"
                + "CREATE TABLE Housing(" +
                            "HousingID integer primary key," +
                            "ClientID integer," +
                            "PartnerID integer," +
                            "HousingStatus text," +
                            "ResidentialProgram integer," +
                            "foreign key(ClientID) references Client(ClientID), " +
                            "foreign key(PartnerID) references Partner(PartnerID));"
                + "CREATE TABLE Work(" +
                            "WorkID integer primary key," +
                            "ClientID integer," +
                            "PartnerID integer," +
                            "Employer text," +
                            "PhoneNumber text," +
                            "WeeklyHours integer," +
                            "LiveableWage integer," +
                            "ReasonNotWorking text," +
                            "WillPursueWork integer," +
                            "foreign key(ClientID) references Client(ClientID)," +
                            "foreign key(PartnerID) references Partner(PartnerID));"
                + "CREATE TABLE ServicesRequested(" +
                            "RequestID integer primary key," +
                            "ClientID integer," +
                            "Date text," +
                            "Description text," +
                            "StaffID integer," +
                            "foreign key(ClientID) references Client(ClientID)," +
                            "foreign key(StaffID) references Staff(StaffID));"
                + "CREATE TABLE ServicesReceived(" +
                            "ReceivedID integer primary key," +
                            "ClientID integer," +
                            "Date text," +
                            "Description text," +
                            "StaffID integer," +
                            "foreign key(ClientID) references Client(ClientID)," +
                            "foreign key(StaffID) references Staff(StaffID));"
                            , newDatabase
             );

                createCommand.ExecuteNonQuery();

                string encryptedPassword = PasswordEncrypt.EncryptPassword("HopeHouseAdmin");

                string addStaffCommand = $"INSERT INTO Staff VALUES(NULL, 'Staff', 'A', 'Admin', 'admin', '{encryptedPassword}', " +
                    "'1234567890', 1, 1)";

                using (SQLiteCommand command = new SQLiteCommand(addStaffCommand, newDatabase))
                {
                    command.ExecuteNonQuery();
                }

                newDatabase.Close();
            }
            catch (Exception e)
            {
                Logger.WriteErrorLogEntry(new ErrorLogEntry(e));
                return false;
            }

            return true;
        }

        public static void Close()
        {
            if(_dbConnection != null)
            {
                _dbConnection.Close();
            }
        }

        public static SQLiteConnection GetConnection()
        {
            return _dbConnection;
        }

        // Determines if the value from the database is of the type that we expect. If it is,
        // cast the value to that type and return. Otherwise, return default value for that type.
        // If we expect an int, then cast the value to Int64 first to comply with SQLite datatypes.
        public static T ConvertValue<T>(object dbValue)
        {
            if (dbValue is DBNull)
            {
                return default(T);
            }

            if (typeof(T) == typeof(int))
            {
                Int64 valueAsInt64 = (Int64) dbValue;
                return (T) Convert.ChangeType(valueAsInt64, typeof (T));
            }

            if (typeof (T) == typeof (bool))
            {
                Int64 valueAsInt64 = (Int64)dbValue;
                bool valueAsBool = SqLiteToBool((int) valueAsInt64);
                return (T) Convert.ChangeType(valueAsBool, typeof (T));
            }

            if (typeof (T) == typeof (DateTime))
            {
                DateTime valueAsDateTime = SqLiteToDateTime((string) dbValue);
                return (T) Convert.ChangeType(valueAsDateTime, typeof (T));
            }

            return (T)dbValue;
        }

        // Converts SQLite date into .NET DateTime
        public static DateTime SqLiteToDateTime(string sqLiteDate)
        {
            if (!string.IsNullOrEmpty(sqLiteDate))
            {
                return Convert.ToDateTime(sqLiteDate);
            }

            return new DateTime();
        }

        // Converts the .NET DateTime structure into proper format for being recognized by SQLite
        public static string DateTimeToSqLite(DateTime datetime)
        {
            return string.Format("{0:s}", datetime);
        }

        // Converts SQLite boolean to bool
        public static bool SqLiteToBool(int sqLiteBoolean)
        {
            if (sqLiteBoolean == 0)
            {
                return false;
            }

            return true;
        }

        // Converts bool to SQLite boolean
        public static int BoolToSqLite(bool boolean)
        {
            if (!boolean)
            {
                return 0;
            }

            return 1;
        }

        #endregion
    }
}
