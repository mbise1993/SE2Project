using System;
using System.Collections.Generic;
using HopeHouse.Core.Models;

namespace HopeHouse.Core.DataAccess
{
    public static class ClientGenerator
    {
        #region Private Fields

        private static Random _random = new Random();

        private static readonly string[] HousingOptions =
        {
            "Low income",
            "Living with friend",
            "Living with relative",
            "Owns home",
            "Rents home"
        };

        private static readonly string[] RaceOptions =
        {
            "White",
            "African American",
            "Hispanic",
            "Asian",
            "Native American",
            "Other"
        };

        #endregion

        #region Generate Clients

        public static List<Client> GenerateClients(int numClients)
        {
            List<Client> clients = new List<Client>(numClients);

            for (int i = 0; i < numClients; i++)
            {
                Client newClient = new Client()
                {
                    Abortion = _random.Next(0, 4),
                    Adoption = _random.Next(0, 4),
                    Address = "123 Address Rd.",
                    ApplyingForDisability = GetRandomBool(),
                    IsActive = GetRandomBool(),
                    BirthDate = GetRandomDateTime(),
                    CanContact = GetRandomBool(),
                    CarriedToTerm = _random.Next(0, 4),
                    CellNumber = "123-456-7890",
                    Church = "Cool Church",
                    City = "City",
                    CurrentlyPregnant = GetRandomBool(),
                    Children = GenerateChildren(),
                    Contact = GenerateContact(),
                    DriversLicense = GetRandomBool(),
                    DateEntered = GetRandomBool() == true ? 
                    new DateTime(DateTime.Now.Year, GetRandomDateTime().Month, GetRandomDateTime().Day)
                    : GetRandomDateTime(),
                    EducationHistory = GenerateEducationHistory(),
                    FirstName = $"Client{i}First",
                    FosterCare = GetRandomBool(),
                    Gender = GetRandomBool() ? "Female" : "Male",
                    HomeNumber = "123-456-7890",
                    Housing = GenerateHousing(),
                    LastName = $"Client{i}Last",
                    MiddleInit = "A",
                    Miscarriage = _random.Next(0, 4),
                    MaritalStatus = GetRandomBool() ? "Married" : "Single",
                    NumberOfPregnancy = _random.Next(0, 6),
                    Prayer = GetRandomBool(),
                    Partner = GeneratePartner(),
                    Points = GeneratePoints(),
                    Pregnancy = GeneratePregnancy(),
                    ReferralSource = "Telegram",
                    ReceivingDisability = GetRandomBool(),
                    Race = RaceOptions[_random.Next(0, RaceOptions.Length)],
                    State = "TN",
                    School = GenerateSchool(),
                    Smoke = GetRandomBool(),
                    ServicesRequested = GenerateServicesRequested(),
                    ServicesReceived = GenerateServicesReceived(),
                    Work = GenerateWork(),
                    Zip = "12345"
                };

                clients.Add(newClient);
            }

            return clients;
        }

        #endregion

        #region Generate Children

        private static ChildAggregation GenerateChildren()
        {
            ChildAggregation children = new ChildAggregation();

            int numChildren = _random.Next(0, 6);
            for (int i = 0; i < numChildren; i++)
            {
                Child newChild = new Child()
                {
                    BirthDate = GetRandomDateTime(),
                    Disabled = GetRandomBool(),
                    FirstName = $"Child{i}First",
                    InDaycare = GetRandomBool(),
                    LastName = $"Child{i}Last",
                    MiddleInit = "A",
                    Pediatrician = "Pediatrician Inc.",
                    ReceivingHealthcare = GetRandomBool()
                };

                children.Add(newChild);
            }

            return children;
        }

        #endregion

        #region Generate Contact

        private static Contact GenerateContact()
        {
            return new Contact()
            {
                FirstName = "ContactFirst",
                LastName = "ContactLast",
                MiddleInit = "A",
                Reason = "Reason",
                ContactType = "Just chillin'",
                StaffId = _random.Next()
            };
        }

        #endregion

        #region Generate Education History

        private static EducationHistory GenerateEducationHistory()
        {
            return new EducationHistory()
            {
                CollegeGrad = GetRandomBool(),
                DropOutGrade = _random.Next(9, 13),
                Ged = GetRandomBool(),
                HighSchoolGrad = GetRandomBool(),
                InCollege = GetRandomBool(),
                ObtainingGed = GetRandomBool()
            };
        }

        #endregion

        #region Generate Housing

        private static Housing GenerateHousing()
        {
            return new Housing()
            {
                HousingStatus = HousingOptions[_random.Next(0, HousingOptions.Length)],
                ResidentialProgram = GetRandomBool()
            };
        }

        #endregion

        #region Generate Partner

        private static Partner GeneratePartner()
        {
            return new Partner()
            {
                Address = "123 Address Rd.",
                Birthday = GetRandomDateTime(),
                City = "City",
                FirstName = "PartnerFirst",
                LastName = "PartnerLast",
                LiveTogether = GetRandomBool(),
                MiddleInit = "A",
                PhoneNumber = "123-456-7890",
                State = "TN",
                Supportive = GetRandomBool(),
                Zip = "12345"
            };
        }

        #endregion

        #region Generate Points

        private static PointsAggregation GeneratePoints()
        {
            PointsAggregation points = new PointsAggregation();

            for(int i = 0; i < 5; i++)
            {
                Points pointsEntry = new Points()
                {
                    Amount = _random.Next(0, 1001),
                    Date = GetRandomDateTime(),
                    Reason = "Found out what the staging area is for in GIT"
                };

                points.Add(pointsEntry);
            }

            return points;
        }

        #endregion

        #region Generate Pregnancy

        private static Pregnancy GeneratePregnancy()
        {
            return new Pregnancy()
            {
                BirthControl = GetRandomBool(),
                CarriedToTerm = GetRandomBool(),
                DueDate = GetRandomDateTime(),
                FamiliesFirst = GetRandomBool(),
                FoodStamps = GetRandomBool(),
                Hugs = GetRandomBool(),
                HugsNurse = GetRandomBool(),
                Intentions = GetRandomBool() ? "Keep it!" : "Abortion",
                LifeBridgeFss = GetRandomBool(),
                MedInsurance = GetRandomBool(),
                NeedPregnancyTest = GetRandomBool(),
                Ob = "Best O. Bgyn",
                PrenatalVitamin = GetRandomBool(),
                ResultOfPregnancy = GetRandomBool() ? "Kept it!" : "Aborted mission",
                ReturnPregnancy = GetRandomBool(),
                SignedReleaseForTest = GetRandomBool(),
                VerifiedPregnancy = GetRandomBool(),
                Wic = GetRandomBool()
            };
        }

        #endregion

        #region Generate School

        private static School GenerateSchool()
        {
            return new School()
            {
                CurrentlyEnrolled = GetRandomBool(),
                HoursEnrolled = _random.Next(0, 20),
                SchoolName = "U of Phoenix Online",
                Term = GetRandomBool() ? "First" : "Last"
            };
        }

        #endregion

        #region Generate Services Requested

        private static ServicesRequestedAggregation GenerateServicesRequested()
        {
            ServicesRequestedAggregation servicesRequested = new ServicesRequestedAggregation();

            int numServices = _random.Next(0, 11);
            for (int i = 0; i < numServices; i++)
            {
                Service newService = new Service()
                {
                    Date = GetRandomDateTime(),
                    Description = $"ServiceRequested{i}"
                };

                servicesRequested.Add(newService);
            }

            return servicesRequested;
        }

        #endregion

        #region Generate Services Received

        private static ServicesReceivedAggregation GenerateServicesReceived()
        {
            ServicesReceivedAggregation servicesReceived = new ServicesReceivedAggregation();

            int numServices = _random.Next(0, 11);
            for (int i = 0; i < numServices; i++)
            {
                Service newService = new Service()
                {
                    Date = GetRandomDateTime(),
                    Description = $"ServiceReceived{i}"
                };

                servicesReceived.Add(newService);
            }

            return servicesReceived;
        }

        #endregion

        #region Generate Work

        private static Work GenerateWork()
        {
            return new Work()
            {
                Employer = "Employer",
                LivableWage = GetRandomBool(),
                PhoneNumber = "123-456-7890",
                ReasonNotWorking = GetRandomBool() ? "Lazy" : "N/A",
                WeeklyHours = _random.Next(0, 51),
                WillPursueWork = GetRandomBool()
            };
        }

        #endregion

        #region Random Helpers

        private static bool GetRandomBool()
        {
            return Database.SqLiteToBool(_random.Next(0, 2));
        }

        private static DateTime GetRandomDateTime()
        {
            int year = _random.Next(1950, DateTime.Now.Year + 1);
            int month = _random.Next(1, 12);
            int day;

            if (month == 2)
            {
                day = _random.Next(1, 29);
            }
            else
            {
                day = _random.Next(1, 31);
            }

            return new DateTime(year, month, day);
        }

        #endregion
    }
}
