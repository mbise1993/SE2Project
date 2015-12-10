using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using HopeHouse.Common.Interfaces;
using HopeHouse.Core.DataAccess;

namespace HopeHouse.Core.Models
{
    public class Client : IDataProvider
    {
        #region Private Fields

        private int _clientId,
                    _partnerId,
                    _numberOfPregnancy,
                    _schoolId,
                    _workId,
                    _carriedToTerm,
                    _miscarriage,
                    _adoption,
                    _abortion;
        private string  _firstName, 
                        _lastName,
                        _middleInit,
                        _gender,
                        _homeNumber, 
                        _cellNumber,
                        _address, 
                        _city, 
                        _state,
                        _zip, 
                        _maritalStatus,
                        _referralSource, 
                        _race,
                        _church,
                        _staffUsername;

        private DateTime _birthDate,
                         _dateEntered;
        private bool    _currentlyPregnant,
                        _fosterCare,
                        _driversLicense,
                        _smoke,
                        _receivingDisability,
                        _applyingForDisability,
                        _canContact,
                        _prayer,
                        _isActive;
        private Pregnancy _pregnancy;
        private ChildAggregation _children;
        private Partner _partner;
        private Work _work;
        private School _school;
        private PointsAggregation _points;
        private Contact _contact;
        private Housing _housing;
        private EducationHistory _educationHistory;
        private ServicesReceivedAggregation _servicesReceived;
        private ServicesRequestedAggregation _servicesRequested;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor for Client class which initializes clientID, firstName, middleInit, and lastName
        /// </summary>
        /// <returns></returns>
        public Client()
        {
            _clientId = 0;
            _firstName = "";
            _middleInit = "";
            _lastName = "";
        }

        /// <summary>
        /// Constructor that takes the Clients ID, first name, middle initial and last name
        /// </summary>
        /// <returns></returns>
        public Client(int clientId, string firstName, string middleInit, string lastName)
        {
            _clientId = clientId;
            _firstName = firstName;
            _middleInit = middleInit;
            _lastName = lastName;
        }

        #endregion

        #region Properties

        public int ClientId
        {
            get
            {
                return _clientId;
            }
            set
            {
                _clientId = value;
            }
        }

        public int PartnerId
        {
            get
            {
                return _partnerId;
            }
            set
            {
                _partnerId = value;
            }
        }

        [Filter]
        [Description("Number of Pregnancies")]
        public int NumberOfPregnancy
        {
            get
            {
                return _numberOfPregnancy;
            }
            set
            {
                _numberOfPregnancy = value;
            }
        }

        public int SchoolId
        {
            get
            {
                return _schoolId;
            }
            set
            {
                _schoolId = value;
            }
        }

        public int WorkId
        {
            get
            {
                return _workId;
            }
            set
            {
                _workId = value;
            }
        }

        [Filter]
        [Description("First Name")]
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
            }
        }

        [Filter]
        [Description("Last Name")]
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
            }
        }

        [Filter]
        [Description("Middle Initial")]
        public string MiddleInit
        {
            get
            {
                return _middleInit;
            }
            set
            {
                _middleInit = value;
            }
        }

        [Filter]
        [Description("Gender")]
        public string Gender
        {
            get
            {
                return _gender;
            }
            set
            {
                _gender = value;
            }
        }

        [Filter]
        [Description("Birth Date")]
        public DateTime BirthDate
        {
            get
            {
                return _birthDate;
            }
            set
            {
                _birthDate = value;
            }
        }

        [Filter]
        [Description("Home Phone Number")]
        public string HomeNumber
        {
            get
            {
                return _homeNumber;
            }
            set
            {
                _homeNumber = value;
            }
        }

        [Filter]
        [Description("Cellphone Number")]
        public string CellNumber
        {
            get
            {
                return _cellNumber;
            }
            set
            {
                _cellNumber = value;
            }
        }

        [Filter]
        [Description("Address")]
        public string Address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
            }
        }

        [Filter]
        [Description("City")]
        public string City
        {
            get
            {
                return _city;
            }
            set
            {
                _city = value;
            }
        }

        [Filter]
        [Description("State")]
        public string State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
            }
        }

        [Filter]
        [Description("Zip Code")]
        public string Zip
        {
            get
            {
                return _zip;
            }
            set
            {
                _zip = value;
            }
        }

        [Filter]
        [Description("Marrital Status")]
        public string MaritalStatus
        {
            get
            {
                return _maritalStatus;
            }
            set
            {
                _maritalStatus = value;
            }
        }

        [Filter]
        [Description("Referred By")]
        public string ReferralSource
        {
            get
            {
                return _referralSource;
            }
            set
            {
                _referralSource = value;
            }
        }

        [Filter]
        [Description("Race")]
        public string Race
        {
            get
            {
                return _race;
            }
            set
            {
                _race = value;
            }
        }

        [Filter]
        [Description("Church")]
        public string Church
        {
            get
            {
                return _church;
            }
            set
            {
                _church = value;
            }
        }

        [Filter]
        [Description("Added By (Staff Member)")]
        public string StaffUsername
        {
            get
            {
                return _staffUsername;
            }
            set
            {
                _staffUsername = value;
            }
        }

        [Filter]
        [Description("Currently Pregnant?")]
        public bool CurrentlyPregnant
        {
            get
            {
                return _currentlyPregnant;
            }
            set
            {
                _currentlyPregnant = value;
            }
        }

        [Filter]
        [Description("Foster Care?")]
        public bool FosterCare
        {
            get
            {
                return _fosterCare;
            }
            set
            {
                _fosterCare = value;
            }
        }

        [Filter]
        [Description("Driver's License?")]
        public bool DriversLicense
        {
            get
            {
                return _driversLicense;
            }
            set
            {
                _driversLicense = value;
            }
        }

        [Filter]
        [Description("Smokes?")]
        public bool Smoke
        {
            get
            {
                return _smoke;
            }
            set
            {
                _smoke = value;
            }
        }

        [Filter]
        [Description("Receiving Disability?")]
        public bool ReceivingDisability
        {
            get
            {
                return _receivingDisability;
            }
            set
            {
                _receivingDisability = value;
            }
        }

        [Filter]
        [Description("Applying for Disability?")]
        public bool ApplyingForDisability
        {
            get
            {
                return _applyingForDisability;
            }
            set
            {
                _applyingForDisability = value;
            }
        }

        [Filter]
        [Description("Can Contact?")]
        public bool CanContact
        {
            get
            {
                return _canContact;
            }
            set
            {
                _canContact = value;
            }
        }

        [Filter]
        [Description("Prays?")]
        public bool Prayer
        {
            get
            {
                return _prayer;
            }
            set
            {
                _prayer = value;
            }
        }

        [Description("Pregnancy Status of Client")]
        public Pregnancy Pregnancy
        {
            get
            {
                return _pregnancy;
            }
            set
            {
                _pregnancy = value;
            }
        }

        [Description("Children")]
        public ChildAggregation Children
        {
            get
            {
                return _children;
            }
            set
            {
                _children = value;
            }
        }

        [Description("Partner")]
        public Partner Partner
        {
            get
            {
                return _partner;
            }
            set
            {
                _partner = value;
            }
        }

        [Description("Work")]
        public Work Work
        {
            get
            {
                return _work;
            }
            set
            {
                _work = value;
            }
        }

        [Description("School")]
        public School School
        {
            get
            {
                return _school;
            }
            set
            {
                _school = value;
            }
        }

        [Description("Points")]
        public PointsAggregation Points
        {
            get
            {
                return _points;
            }
            set
            {
                _points = value;
            }
        }

        [Description("Contact Information")]
        public Contact Contact
        {
            get
            {
                return _contact;
            }
            set
            {
                _contact = value;
            }
        }

        [Description("Housing")]
        public Housing Housing
        {
            get
            {
                return _housing;
            }
            set
            {
                _housing = value;
            }
        }

        [Description("Education History")]
        public EducationHistory EducationHistory
        {
            get
            {
                return _educationHistory;
            }
            set
            {
                _educationHistory = value;
            }
        }

        [Description("Services Received")]
        public ServicesReceivedAggregation ServicesReceived
        {
            get
            {
                return _servicesReceived;
            }
            set
            {
                _servicesReceived = value;
            }
        }

        [Description("Services Requested")]
        public ServicesRequestedAggregation ServicesRequested
        {
            get
            {
                return _servicesRequested;
            }
            set
            {
                _servicesRequested = value;
            }
        }

        [Filter]
        [Description ("Date Entered")]
        public DateTime DateEntered
        {
            get
            {
                return _dateEntered;
            }
            set
            {
                _dateEntered = value;
            }
        }

        [Filter]
        [Description("Is Active?")]
        public bool IsActive
        {
            get
            {
                return _isActive;
            }
            set
            {
                _isActive = value;
            }
        }

        [Filter]
        [Description("Number Carried to Term")]
        public int CarriedToTerm
        {
            get
            {
                return _carriedToTerm;
            }
            set
            {
                _carriedToTerm = value;
            }
        }

        [Filter]
        [Description("Number of Miscarriages")]
        public int Miscarriage
        {
            get
            {
                return _miscarriage;
            }
            set
            {
                _miscarriage = value;
            }
        }

        [Filter]
        [Description("Number of Adoptions")]
        public int Adoption
        {
            get
            {
                return _adoption;
            }
            set
            {
                _adoption = value;
            }
        }

        [Filter]
        [Description("Number of Abortions")]
        public int Abortion
        {
            get
            {
                return _abortion;
            }
            set
            {
                _abortion = value;
            }
        }

        #endregion

        #region Overridden Methods

        public override string ToString()
        {
            return _lastName + ", " + _firstName + " " + _middleInit + ".";
        }

        #endregion

        #region IDataProvider Implementation

        public void SetDataProperty(string propertyName, object value)
        {
            PropertyInfo[] properties = GetType().GetProperties().Where(
                x => x.GetCustomAttribute<DescriptionAttribute>() != null).ToArray();

            foreach (PropertyInfo property in properties)
            {
                DescriptionAttribute attr = property.GetCustomAttribute<DescriptionAttribute>();

                if (attr != null)
                {
                    if (attr.Description == propertyName)
                    {
                        if (property.PropertyType.Name.Equals("Int32"))
                            property.SetValue(this, Int32.Parse(value.ToString()));
                        else if (property.PropertyType.Name.Equals("DateTime"))
                            property.SetValue(this, DateTime.Parse(value.ToString()));
                        else
                            property.SetValue(this, value);
                    }
                }
            }
        }

        public Dictionary<string, object> GetData()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();

            PropertyInfo[] properties = GetType().GetProperties().Where(
                x => x.GetCustomAttribute<DescriptionAttribute>() != null).ToArray();

            foreach (PropertyInfo property in properties)
            {
                DescriptionAttribute descriptionAttr = property.GetCustomAttribute<DescriptionAttribute>();

                if (descriptionAttr != null)
                {
                    object propertyValue = property.GetValue(this, null);
                    data[descriptionAttr.Description] = propertyValue;
                }
            }

            return data;

            //return new Dictionary<string, object>
            //{
            //    { "Client ID", _clientId },
            //    { "Partner ID", _partnerId },
            //    { "School ID", _school },
            //    { "Work ID", _workId },
            //    { "Number of Pregnancies", _numberOfPregnancy },
            //    { "First Name", _firstName },
            //    { "Last Name", _lastName },
            //    { "Middle Initial", _middleInit },
            //    { "Gender", _gender },
            //    { "Home Phone Number", _homeNumber },
            //    { "Cell Phone Number", _cellNumber },
            //    { "Address", _address },
            //    { "City", _city },
            //    { "State", _state },
            //    { "Zip Code", _zip },
            //    { "Marrital Status", _maritalStatus },
            //    { "Referral Source", _referralSource },
            //    { "Race", _race },
            //    { "Church", _church },
            //    { "Birth Date", _birthDate },
            //    { "Currently Pregnany?", _currentlyPregnant },
            //    { "Foster Care?", _fosterCare },
            //    { "Driver's License?", _driversLicense },
            //    { "Smokes?", _smoke },
            //    { "Receiving Disability?", _receivingDisability },
            //    { "Applying for Disability?", _applyingForDisability },
            //    { "Can Contact?", _canContact },
            //    { "Prayer?", _prayer },
            //    { "Pregnancy", _pregnancy },
            //    { "Children", _children },
            //    { "Work", _work },
            //    { "School", _school },
            //    { "Points", _points },
            //    { "Contact", _contact },
            //    { "Housing", _housing },
            //    { "Education History", _educationHistory },
            //    { "Services Requested", _servicesRequested },
            //    { "Services Received", _servicesReceived },
            //    { "Date Entered", _dateEntered },
            //    { "Is Active?", _isActive }
            //};
        }

        public string GetIdentifier()
        {
            return _lastName + ", " + _firstName + " " + _middleInit;
        }

        public void Update()
        {
            ClientDatabaseHelper.UpdateClient(this);
        }

        #endregion
    }
}
