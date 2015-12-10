using System;
using System.Collections.Generic;
using System.Data.OleDb;
namespace HopeHouse.Core.DataAccess
{
    class ParserHelper
    {
        private readonly OleDbDataReader _reader;
        #region Parser Indices

        private const int Name = 0;
        private const int ClientCont = 1;
        private const int AppDate = 3;
        private const int ClientStatus = 4;
        private const int ReturningPreg = 5;
        private int _pregHistory = 6;
        private const int MartialStatus = 13;
        private const int AgeRange = 17;
        private const int Ethnicity = 23;
        private const int CurrentSchoolEnrollment = 30;
        private const int EducationalBackground = 38;
        private const int SmokingStatus = 46;
        private const int ClassAttend = 48;
        private const int BetterWay = 49;
        private const int HrClass = 50;
        private const int MonthRangeStart = 51;
        private const int MonthRangeEnd = 63;
        private const int IntDot = 67;
        #endregion

        public ParserHelper(OleDbDataReader streamReader)
        {
            _reader = streamReader;
        }

        public string GetClientName()
        {
            var userName = _reader[Name].ToString();
            return userName;
        }

        public string GetClientFirstName()
        {
            var userName = _reader[Name].ToString();
            var splitName = userName.Split(' ');
            return splitName[0];
        }

        public string GetClientLastName()
        {
            var userName = _reader[Name].ToString();
            var splitName = userName.Split(' ');
            if (splitName.Length == 2)
            {
                return splitName[1];
            }
            else if (splitName.Length == 3)
            {
                return splitName[2];
            }
            else
            {
                return userName;
            }
        }

        public string GetClientMiddleInit()
        {
            var userName = _reader[Name].ToString();
            var splitName = userName.Split(' ');
            if (splitName.Length == 2)
            {
                return null;
            }
            else if (splitName.Length == 3)
            {
                return splitName[1];
            }
            else
            {
                return null;
            }
        }

        public bool IsClientContinuing()
        {
            var contInput = _reader[ClientCont].ToString();
            return contInput == "1";
        }

        public DateTime GetClientAppDate()
        {
            var appDate = _reader[AppDate].ToString();
            return Convert.ToDateTime(appDate);
        }

        public string GetClientStatus()
        {
            var clientStatus = _reader[ClientStatus].ToString();
            return clientStatus == "" ? "No additional information" : clientStatus;
        }

        public bool IsClientAReturningPregnancy()
        {
            var returning = _reader[ReturningPreg].ToString();
            return returning == "1";
        }

        public bool IsClientCurrentlyPregnant()
        {
            Dictionary<string, bool> pregDictionary = GetClientPregnancyHistory();
            bool pregnantFirst = pregDictionary["Pregnant-First"];
            bool pregWithChildren = pregDictionary["Preg with children"];
            return pregnantFirst || pregWithChildren;
        }

        public Dictionary<string, bool> GetClientPregnancyHistory()
        {
            _pregHistory = 6;
            string[] typesOfPregnancy = new string[] { "Pregnant-First", "Baby already born", "Preg with children", "NP more than 1 child", "Grandparent", "Father", "Other" };
            Dictionary<string, bool> pregHistory = new Dictionary<string, bool>();
            foreach (var pregnancyType in typesOfPregnancy)
            {
                var pregInfo = _reader[_pregHistory].ToString();
                pregHistory.Add(pregnancyType, pregInfo == "1");
                _pregHistory++;
            }
            return pregHistory;
        }

        public string GetClientMartialStatus()
        {
            for (int currentReaderValue = MartialStatus; currentReaderValue < AgeRange; currentReaderValue++)
            {
                var martialStatus = _reader[currentReaderValue].ToString();
                if (martialStatus == "1")
                {
                    switch (currentReaderValue)
                    {
                        case 13:
                            return "Single";
                        case 14:
                            return "Married";
                        case 15:
                            return "Seperated";
                        case 16:
                            return "?";
                    }
                }
            }
            return "?";
        }

        public string GetClientsAgeRange()
        {
            for (int currentReaderValue = AgeRange; currentReaderValue < Ethnicity; currentReaderValue++)
            {
                var ageRange = _reader[currentReaderValue].ToString();
                if (ageRange == "1")
                {
                    switch (currentReaderValue)
                    {
                        case 17:
                            return "Under 15";
                        case 18:
                            return "15-19";
                        case 19:
                            return "20-24";
                        case 20:
                            return "25-29";
                        case 21:
                            return "30 or over";
                        case 22:
                            return "?";
                    }
                }
            }
            return "Not a new client";
        }

        public string GetClientEthnicity()
        {
            for (int currentReaderValue = Ethnicity; currentReaderValue < CurrentSchoolEnrollment; currentReaderValue++)
            {
                var ethnicity = _reader[currentReaderValue].ToString();
                if (ethnicity != "1") continue;
                switch (currentReaderValue)
                {
                    case 23:
                        return "Caucasian";
                    case 24:
                        return "African American";
                    case 25:
                        return "Hispanic";
                    case 26:
                        return "Asian";
                    case 27:
                        return "Native American";
                    case 28:
                        return "Other";
                    case 29:
                        return "?";
                }
            }
            return "?";
        }

        public string GetClientCurrentSchoolEnrollment()
        {
            for (int currentReaderValue = CurrentSchoolEnrollment; currentReaderValue < EducationalBackground; currentReaderValue++)
            {
                var schoolEnroll = _reader[currentReaderValue].ToString();
                if (schoolEnroll != "1") continue;
                switch (currentReaderValue)
                {
                    case 30:
                        return "JH";
                    case 31:
                        return "HS";
                    case 32:
                        return "Home";
                    case 33:
                        return "GED";
                    case 34:
                        return "College";
                    case 35:
                        return "Trade";
                    case 36:
                        return "Not";
                    case 37:
                        return "?";
                }
            }
            return "?";
        }

        public string GetClientEducationalBackGround()
        {
            for (int currentReaderValue = EducationalBackground; currentReaderValue < SmokingStatus; currentReaderValue++)
            {
                var educationHistory = _reader[currentReaderValue].ToString();
                if (educationHistory != "1") continue;
                switch (currentReaderValue)
                {
                    case 38:
                        return "N/A";
                    case 39:
                        return "Drop Out";
                    case 40:
                        return "High School Grad";
                    case 41:
                        return "GED";
                    case 42:
                        return "Some College";
                    case 43:
                        return "College Grad";
                    case 44:
                        return "Tech Training";
                    case 45:
                        return "?";
                }
            }
            return "?";
        }

        public bool DoesClientSmoke()
        {
            for (int currentReaderValue = SmokingStatus; currentReaderValue < ClassAttend; currentReaderValue++)
            {
                var smoker = _reader[currentReaderValue].ToString();
                if (smoker == "1")
                {
                    return true;
                }
            }
            return false;
        }

        public bool DidClientAttendAClass()
        {
            var attendClass = _reader[ClassAttend].ToString();
            return attendClass == "1";
        }

        public bool IsClientInBetterWayProgram()
        {
            var betterWay = _reader[BetterWay].ToString();
            return betterWay == "1";
        }

        public bool IsClientInHrcClass()
        {
            var hrcClassStatus = _reader[HrClass].ToString();
            return hrcClassStatus == "1";
        }

        public Dictionary<int, int> GetClientsClassAttendance()
        {
            int janAttendanceCount = 0;
            int febAttendanceCount = 0;
            int aprilAttendanceCount = 0;
            int mayAttendanceCount = 0;
            int marchAttendanceCount = 0;
            int juneAttendanceCount = 0;
            int julyAttendanceCount = 0;
            int augAttendanceCount = 0;
            int septAttendanceCount = 0;
            int octAttendanceCount = 0;
            int novAttendanceCount = 0;
            int decAttendanceCount = 0;
            int[] monthAttendanceCounts = new int[]
            {

                janAttendanceCount,
                febAttendanceCount,
                marchAttendanceCount,
                aprilAttendanceCount,
                mayAttendanceCount,
                juneAttendanceCount,
                julyAttendanceCount,
                augAttendanceCount,
                septAttendanceCount,
                octAttendanceCount,
                novAttendanceCount,
                decAttendanceCount
            };
            Dictionary<int, int> classAttendance = new Dictionary<int, int>();
            for (int currentReadingField = MonthRangeStart, currentMonth = 0; currentReadingField < MonthRangeEnd; currentReadingField++, currentMonth++)
            {
                bool validated = Int32.TryParse(_reader[currentReadingField].ToString(), out monthAttendanceCounts[currentMonth]);
                classAttendance.Add(currentMonth, validated ? monthAttendanceCounts[currentMonth] : 0);
            }
            return classAttendance;
        }

        public int GetClientIndTot()
        {
            var indTotString = _reader[IntDot].ToString();
            int parsedIndTot;
            bool validated = Int32.TryParse(indTotString, out parsedIndTot);
            return validated ? parsedIndTot : 0;
        }
    }
}
