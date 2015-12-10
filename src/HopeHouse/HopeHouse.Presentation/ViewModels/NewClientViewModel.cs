using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using HopeHouse.Presentation.Commanding;
using System.Windows.Input;
using HopeHouse.Core.Models;
using HopeHouse.Core.DataAccess;
using System.Text.RegularExpressions;
using System.Windows;
using HopeHouse.Common.Logging;
// ReSharper disable InconsistentNaming

namespace HopeHouse.Presentation.ViewModels
{
    public class NewClientViewModel : ViewModelBase
    {
        private const string MATERNITY_CLOTHES = "Maternity Clothes";
        private const string PERSONAL_CARE = "Personal Care";
        private const string FOOD = "Food";
        private const string PREGNANCY_INFO = "Pregnancy Information"; 
        private const string PARENTING_INFO = "Parenting Information";
        private const string ADOPTION_INFO = "Adoption Information";
        private const string ABORTION_INFO = "Abortion Information";
        private const string EMOTIONAL_SUPPORT = "Emotional Support"; 
        private const string CHRISTIANITY_INFO = "Christianity Information";
        private const string HOUSING_INFO = "Housing Information";
        private const string ABSTINENCE_INFO = "Abstinence Information";
        private const string NUTRUTION_INFO = "Nutrution Information";
        private const string EMPLOYMENT_INFO = "Employment Information";
        private const string EDUCATION_REFERRALS = "Education Referrals";
        private const string MEDICAL_REFERRALS = "Medical Referrals";
        private const string PROFESSIONAL_COUNSEL = "Professional Counseling Referrals";
        private const string FINANCIAL_INFO = "Financial Information";
        private const string BREASTFEEDING_INFO = "Breastfeeding Information";
        private const string FRESH_START = "Fresh Start Program";
        private const string POINTS_PROGRAM = "Points Program";
        private const string HOPE_HOUSE = "Hope House Classes";
        private const string LIFE_BRIDGE = "Life Bridge Program";
        private const string PREGNANCY_TEST = "Pregnancy Test";

        #region Private Backing Fields
        /****
        * Private Fields
        *****/
        private readonly Regex _phonePattern = new Regex ( @"^((\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]*\d{3}[\s.-]*\d{4})$|^\d{10}$" );
        private readonly Regex _zipPattern = new Regex( @"(?<Zip>\d{5})(?<Sub>-\d{4})?" );
        private string _errorMessage;
        private bool _saveComplete;

        private readonly Staff _staffMember;

        // Client
        private string _address;
        private bool _applyingForDisability;
        private int _age;
        private DateTime _birthDate;
        private bool _canContact;
        private string _cellNumber;
        private string _church;
        private string _city;
        private bool _currentlyPregnant;
        private bool _driversLicense;
        private string _firstName;
        private bool _fosterCare;
        private string _gender;
        private string _homeNumber;
        private string _lastName;
        private string _maritalStatus;
        private string _middleInitial;
        private int _numOfPregnancy;
        private bool _prayer;
        private string _race;
        private bool _receivingDisability;
        private string _referralSource;
        private bool _smoke;
        private string _state;
        private string _zip;
        private ICollection<Child> _children;

        // School
        private string _schoolName;
        private bool _currentlyEnroll;
        private int _hoursEnrolled;
        private string _term;

        // Work
        private string _employer;
        private string _phoneNumber;
        private int _weeklyHours;
        private bool _liveableWage;
        private string _reasonNotWorking;
        private bool _willPursueWork;

        // Partner
        private string _partnerFirstName;
        private string _partnerMiddleInit;
        private string _partnerLastName;
        private DateTime _partnerBirthDate;
        private string _partnerAddress;
        private string _partnerCity;
        private string _partnerState;
        private string _partnerZip;
        private string _partnerPhone;
        private bool _liveTogether;
        private bool _supportive;
        private string _partnerWork;

        // Child
        private string _childFirstName;
        private string _childMiddleInit;
        private string _childLastName;
        private DateTime? _childBirthDate;
        private string _childPediatrician;
        private bool? _receivingHealthcare;
        private bool? _inDaycare;
        private bool? _disabled;

        // Education History
        private string _education;
        private bool _highSchoolGrad;
        private int _dropOutGrade;
        private bool _ged;
        private bool _obtainingGed;
        private bool _collegeGrad;
        private bool _inCollege;

        // Housing
        private string _housingStatus;
        private bool _residentialProgram;

        // Pregnancy History
        private int _carriedToTerm,
            _miscarriage,
            _abortion,
            _adoption;

        // Pregnancy Info
        private bool _returnPregnancy;
        private bool _verifiedPregnancy;
        private bool _needPregnancyTest;
        private bool _signedForRelease;
        private DateTime _dueDate;
        private string _intention;
        private bool _birthControl;
        private bool _medInsurance;
        private string _ob;
        private bool _prenatalVitamins;
        private bool _wic;
        private bool _hugs;
        private bool _hugsNurse;
        private bool _foodStamps;
        private bool _familiesFirst;
        private bool _lifeBridge;
        private string _resultOfPregnancy;

        // Services Requested
        private readonly Dictionary<string, bool> _services;
        private string _otherService;

        private bool _receivedServicesBefore;
        #endregion

        #region Client Properties

        public string FirstName { set { _firstName = value; } }

        public string MiddleInitial { set { _middleInitial = value; } }

        public string LastName { set {_lastName = value; } }

        public DateTime BirthDate { set { _birthDate = value; } }

        public int Age { set { _age = value; } }

        public string Gender { set { _gender = value; } }

        public string HomeNumber
        {
            get { return _homeNumber; }
            set
            {
                if (_phonePattern.IsMatch(value))
                {
                    try
                    {
                        Regex r = new Regex ( @"[^\d]" );
                        _homeNumber = Regex.Replace( r.Replace ( value, "" ), @"(\d{3})(\d{3})(\d{4})", @"($1) $2-$3");
                        OnPropertyChanged(nameof(HomeNumber));
                    }
                    catch (ArgumentException ex)
                    {
                        //TODO exception here
                        throw;
                    }
                }
                else
                {
                    // ToDo give some error
                    _homeNumber = "Invalid Number Format!";
                }
            }
        }

        public string CellNumber
        {
            get { return _cellNumber; }
            set
            {
                if (_phonePattern.IsMatch(value))
                {
                    try
                    {
                        Regex r = new Regex ( @"[^\d]" );
                        _cellNumber = Regex.Replace ( r.Replace ( value, "" ), @"(\d{3})(\d{3})(\d{4})", @"($1) $2-$3" );
                        OnPropertyChanged ( nameof ( CellNumber ) );
                    }
                    catch (ArgumentException ex)
                    {
                        //TODO exception here
                        throw;
                    }
                }
                else
                {
                    // ToDo give some error
                    _cellNumber = "Invalid Number Format!";
                }
            }
        }

        public string Address
        {
            set
            {
                _address = value;
            }
        }

        public string City { set { _city = value; } }

        public string State { set { _state = value.ToUpper(); } }

        public string Zip
        {
            set
            {
                if (_zipPattern.IsMatch(value))
                {
                    _zip = value;
                }
                else
                {
                    // ToDo some error
                    _errorMessage = "Invalid Zip Format!";
                }
            }
        }

        public string MaritalStatus { set { _maritalStatus = StripComboBoxText(value); } }

        public string ReferralSource { set { _referralSource = value; } }

        public bool CurrentlyPregnant { set { _currentlyPregnant = value; } }

        public int NumOfPregnancy { set { _numOfPregnancy = value; } }

        public string Race { set { _race = value; } }

        public bool FosterCare { set { _fosterCare = value; } }

        public bool DriversLicense { set { _driversLicense = value; } }

        public bool Smoke { set { _smoke = value; } }

        public string Church { set { _church = value; } }

        public bool ReceivingDisability { set { _receivingDisability = value; } }

        public bool ApplyingForDisability { set { _applyingForDisability = value; } }

        public bool CanContact { set { _canContact = value; } }

        public bool Prayer { set { _prayer = value; } }

        #endregion

        #region School Properties

        public string SchoolName { set { _schoolName = value; } }
        public bool CurrentlyEnrolled { set { _currentlyEnroll = value; } }
        public int HoursEnrolled { set { _hoursEnrolled = value; } }
        public string Term { set { _term = value; } }
        #endregion

        #region Work Properties

        public string Employer { set { _employer = value; } }
        public string PhoneNumber { set { _phoneNumber = value; } }
        public int WeeklyHours { set { _weeklyHours = value; } }
        public bool LiveableWage { set { _liveableWage = value; } }
        public string ReasonNotWorking { set { _reasonNotWorking = value; } }
        public bool WillPursueWork { set { _willPursueWork = value; } }
        #endregion

        #region Partner Properties

        public string PartnerFirstName { set { _partnerFirstName = value; } }
        public string PartnerMiddleInit { set { _partnerMiddleInit = value; } }
        public string PartnerLastName { set { _partnerLastName = value; } }
        public DateTime PartnerBirthDate { set { _partnerBirthDate = value; } }
        public string PartnerAddress { set { _partnerAddress = value; } }
        public string PartnerCity { set { _partnerCity = value; } }
        public string PartnerState { set { _partnerState = value; } }
        public string PartnerZip { set { _partnerZip = value; } }
        public string PartnerPhone { set { _partnerPhone = value; } }
        public bool LiveTogether { set { _liveTogether = value; } }
        public bool Supportive { set { _supportive = value; } }
        public string PartnerWork { set { _partnerWork = value; } }
        #endregion

        #region Child Properties

        public string ChildFirstName
        {
            set
            {
                _childFirstName = value;
            }
        }
        public string ChildMiddleInit { set { _childMiddleInit = value; } }
        public string ChildLastName { set { _childLastName = value; } }
        public DateTime? ChildBirthDate { set { _childBirthDate = value; } }
        public string ChildPediatrician { set { _childPediatrician = value; } }
        public bool? ReceivingHealthcare { set { _receivingHealthcare = value; } }
        public bool? InDaycare { set { _inDaycare = value; } }
        public bool? Disabled { set { _disabled = value; } }

        public ICollection<Child> Children
        {
            get
            {
                if (_children == null)
                {
                    _children = new ObservableCollection<Child>();
                }

                return _children;
            }
            set
            {
                _children = value;
                OnPropertyChanged(nameof(Children));
            }
        }

        public bool HasChildren
        {
            get
            {
                return Children.Count > 0;
            }
        }

        #endregion

        #region Education History Properties

        public string Education
        {
            set
            {
                _education = StripComboBoxText(value).ToLower();
                if (_education.Equals("graduated highschool"))
                {
                    _highSchoolGrad = true;
                }
                else if (_education.Equals("earned ged"))
                {
                    _ged = true;
                }
                else if (_education.Equals("in ged program"))
                {
                    _obtainingGed = true;
                }
                else if (_education.Equals("in college"))
                {
                    _inCollege = true;
                }
                else if (_education.Equals("graduated college"))
                {
                    _collegeGrad = true;
                }
            }
        }
        
        public int DropOutGrade { set { _dropOutGrade = value; } }
        #endregion

        #region Housing Properties

        public string HousingStatus { set { _housingStatus = StripComboBoxText(value); } }
        public bool ResidentialProgram { set { _residentialProgram = value; } }

        #endregion

        #region Pregnancy History Properties

        public int CarriedToTerm { set { _carriedToTerm = value; } }
        public int Miscarriage { set { _miscarriage = value; } }
        public int Abortion { set { _abortion = value; } }
        public int Adoption { set { _adoption = value; } }
        #endregion

        #region Pregnancy Properties

        public bool ReturnPregnancy { set { _returnPregnancy = value; } }
        public bool VerifiedPregnancy { set { _verifiedPregnancy = value; } }
        public bool NeedPregnancyTest { set { _needPregnancyTest = value; } }
        public bool SignedForRelease { set { _signedForRelease = value; } }
        public DateTime DueDate { set { _dueDate = value; } }
        public string Intention { set { _intention = StripComboBoxText(value); } }
        public bool BirthControl { set { _birthControl = value; } }
        public bool MedInsurance { set { _medInsurance = value; } }
        public string OB { set { _ob = value; } }
        public bool PrenatalVitamins { set { _prenatalVitamins = value; } }
        public bool WIC { set { _wic = value; } }
        public bool Hugs { set { _hugs = value; } }
        public bool HugsNurse { set { _hugsNurse = value; } }
        public bool FoodStamps { set { _foodStamps = value; } }
        public bool FamiliesFirst { set { _familiesFirst = value; } }
        public bool LifeBridge { set { _lifeBridge = value; } }
        public string ResultOfPregnancy { set { _resultOfPregnancy = value; } }
        #endregion

        #region Services Requested
        
        public bool MaternityClothes { set { _services[MATERNITY_CLOTHES] = value; } }
        public bool PersonalCare { set { _services[PERSONAL_CARE] = value; } }
        public bool Food { set { _services[FOOD] = value; } }
        public bool PregnancyInfo { set { _services[PREGNANCY_INFO] = value; } }
        public bool ParentingInfo { set { _services[PARENTING_INFO] = value; } }
        public bool AdoptionInfo { set { _services[ADOPTION_INFO] = value; } }
        public bool AbortionInfo { set { _services[ABORTION_INFO] = value; } }
        public bool EmotionalSupport { set { _services[EMOTIONAL_SUPPORT] = value; } }
        public bool ChristianityInfo { set { _services[CHRISTIANITY_INFO] = value; } }
        public bool HousingInfo { set { _services[HOUSING_INFO] = value; } }
        public bool AbstinenceInfo { set { _services[ABSTINENCE_INFO] = value; } }
        public bool NutritionInfo { set { _services[NUTRUTION_INFO] = value; } }
        public bool EmploymentInfo { set { _services[EMPLOYMENT_INFO] = value; } }
        public bool EducationReferrals { set { _services[EDUCATION_REFERRALS] = value; } }
        public bool MedicalReferrals { set { _services[MEDICAL_REFERRALS] = value; } }
        public bool ProCounselingReferral { set { _services[PROFESSIONAL_COUNSEL] = value; } }
        public bool FinancialInfo { set { _services[FINANCIAL_INFO] = value; } }
        public bool BreastFeedingInfo { set { _services[BREASTFEEDING_INFO] = value; } }
        public bool FreshStartProgram { set { _services[FRESH_START] = value; } }
        public bool PointsProgram { set { _services[POINTS_PROGRAM] = value; } }
        public bool HopeHouseClasses { set { _services[HOPE_HOUSE] = value; } }
        public bool LifeBridgeProgram { set { _services[LIFE_BRIDGE] = value; } }
        public bool PregnancyTest { set { _services[PREGNANCY_TEST] = value; } }
        public string Other { set { _otherService = value; } }
        #endregion

        public bool ReceivedServicesBefore { set { _receivedServicesBefore = value; } }

        public string ErrorMessage
        {
            get { return _errorMessage; }
        }

        public NewClientViewModel(Staff staff)
        {
            _staffMember = staff;
            _saveComplete = false;

            _services = new Dictionary<string, bool>();
            _services.Add ( MATERNITY_CLOTHES, false );
            _services.Add ( PERSONAL_CARE, false );
            _services.Add ( FOOD, false );
            _services.Add ( PREGNANCY_INFO, false );
            _services.Add ( PARENTING_INFO, false );
            _services.Add ( ADOPTION_INFO, false );
            _services.Add ( ABORTION_INFO, false );
            _services.Add ( EMOTIONAL_SUPPORT, false );
            _services.Add ( CHRISTIANITY_INFO, false );
            _services.Add ( HOUSING_INFO, false );
            _services.Add ( ABSTINENCE_INFO, false );
            _services.Add ( NUTRUTION_INFO, false );
            _services.Add ( EMPLOYMENT_INFO, false );
            _services.Add ( EDUCATION_REFERRALS, false );
            _services.Add ( MEDICAL_REFERRALS, false );
            _services.Add ( PROFESSIONAL_COUNSEL, false );
            _services.Add ( FINANCIAL_INFO, false );
            _services.Add ( BREASTFEEDING_INFO, false );
            _services.Add ( FRESH_START, false );
            _services.Add ( POINTS_PROGRAM, false );
            _services.Add ( HOPE_HOUSE, false );
            _services.Add ( LIFE_BRIDGE, false );
            _services.Add ( PREGNANCY_TEST, false );
        }

        #region Commands
        /****
        * Commands
        *****/
        private ICommand _addClientCommand;

        public ICommand AddClientCommand
        {
            get
            {
                if (_addClientCommand == null)
                {
                    _addClientCommand = new RelayCommand(arg =>
                    {
                        var newClient = CreateClient();
                        ClientManager.AddClient(newClient);
                        _saveComplete = true;


                        UserActionLogEntry logEntry = new UserActionLogEntry(_staffMember.ToString(), _staffMember.Username,
                            $"Added client {newClient.ToString()}");
                        Logger.WriteUserActionLogEntry(logEntry);

                        MessageBox.Show($"Information for {_firstName} {_lastName} has been saved", "Success");
                    },
                    arg =>
                    {
                        return (_firstName != null) && (_lastName != null) && (_saveComplete == false);
                    });
                }
                return _addClientCommand;
            }
        }

        private ICommand _addChildCommand;

        public ICommand AddChildCommand
        {
            get
            {
                if (_addChildCommand == null)
                {
                    _addChildCommand = new RelayCommand(arg =>
                    {
                        Children.Add(CreateChild());
                        OnPropertyChanged(nameof(HasChildren));
                    },
                    arg => (!string.IsNullOrEmpty(_childFirstName))
                            && (!string.IsNullOrEmpty(_childLastName))
                            && (!string.IsNullOrEmpty(_childMiddleInit))
                            && (_childBirthDate != null)
                            && (_receivingHealthcare != null)
                            && (_inDaycare != null)
                            && (_disabled != null));
                }

                return _addChildCommand;
            }
        }

        private ICommand _deleteChildCommand;

        public ICommand DeleteChildCommand
        {
            get
            {
                if (_deleteChildCommand == null)
                {
                    _deleteChildCommand = new RelayCommand(arg =>
                    {
                        Child argAsChild = arg as Child;
                        if (argAsChild != null)
                        {
                            Children.Remove(argAsChild);
                            OnPropertyChanged(nameof(HasChildren));
                        }
                    },
                    arg => true);
                }

                return _deleteChildCommand;
            }
        }
        #endregion


        #region Helpers
        private Client CreateClient()
        {
            Client client = new Client();
            client.StaffUsername = _staffMember.Username;
            client.FirstName = _firstName;
            client.MiddleInit = _middleInitial;
            client.LastName = _lastName;
            client.BirthDate = _birthDate;
            client.Gender = _gender;
            client.HomeNumber = _homeNumber;
            client.CellNumber = _cellNumber;
            client.Address = _address;
            client.City = _city;
            client.State = _state;
            client.Zip = _zip;
            client.MaritalStatus = _maritalStatus;
            client.ReferralSource = _referralSource;
            client.CurrentlyPregnant = _currentlyPregnant;
            client.NumberOfPregnancy = _numOfPregnancy;
            client.Race = _race;
            client.FosterCare = _fosterCare;
            client.DriversLicense = _driversLicense;
            client.Smoke = _smoke;
            client.Church = _church;
            client.ReceivingDisability = _receivingDisability;
            client.ApplyingForDisability = _applyingForDisability;
            client.CanContact = _canContact;
            client.Prayer = _prayer;
            client.DateEntered = DateTime.Now;

            client.CarriedToTerm = _carriedToTerm;
            client.Miscarriage = _miscarriage;
            client.Abortion = _abortion;
            client.Adoption = _adoption;

            CreateEducation(client);
            CreateSchool(client);
            CreateWork(client);
            CreatePartner(client);
            CreateServicesReq(client);
            CreateHousing(client);
            CreatePregnancyInfo(client);
            CreateChildren(client);

            return client;
        }

        private void CreateChildren ( Client client )
        {
            if (Children.Count == 0)
            {
                return;
            }

            client.Children = new ChildAggregation();

            foreach (Child child in Children)
            {
                client.Children.Add(child);
            }
        }

        private void CreateEducation ( Client client )
        {
            client.EducationHistory = new EducationHistory();
            client.EducationHistory.HighSchoolGrad = _highSchoolGrad;
            client.EducationHistory.DropOutGrade = _dropOutGrade;
            client.EducationHistory.Ged = _ged;
            client.EducationHistory.ObtainingGed = _obtainingGed;
            client.EducationHistory.CollegeGrad = _collegeGrad;
            client.EducationHistory.InCollege = _inCollege;
        }

        private void CreateSchool( Client client )
        {
            if (String.IsNullOrEmpty(_schoolName))
            {
                client.School = null;
                return;
            }

            client.School = new School();
            client.School.SchoolName = _schoolName;
            client.School.CurrentlyEnrolled = _currentlyEnroll;
            client.School.HoursEnrolled = _hoursEnrolled;
            client.School.Term = _term;
        }

        private void CreateWork( Client client )
        {
            client.Work = new Work();
            client.Work.Employer = _employer;
            client.Work.PhoneNumber = _phoneNumber;
            client.Work.WeeklyHours = _weeklyHours;
            client.Work.LivableWage = _liveableWage;
            client.Work.ReasonNotWorking = _reasonNotWorking;
            client.Work.WillPursueWork = _willPursueWork;
        }

        private void CreatePartner( Client client )
        {
            if (String.IsNullOrEmpty(_partnerFirstName))
            {
                client.Partner = null;
                return;
            }

            client.Partner = new Partner();
            client.Partner.FirstName = _partnerFirstName;
            client.Partner.MiddleInit = _partnerMiddleInit;
            client.Partner.LastName = _partnerLastName;
            client.Partner.Birthday = _partnerBirthDate;
            client.Partner.Address = _partnerAddress;
            client.Partner.City = _partnerCity;
            client.Partner.State = _partnerState;
            client.Partner.Zip = _partnerZip;
            client.Partner.PhoneNumber = _partnerPhone;
            client.Partner.LiveTogether = _liveTogether;
            client.Partner.Supportive = _supportive;
            if (!String.IsNullOrEmpty(_partnerWork))
            {
                client.Partner.Work = new Work ( );
                client.Partner.Work.Employer = _partnerWork; 
            }
            else
            {
                client.Partner.Work = null;
            }

            if (_liveTogether)
            {
                client.Partner.Address = _address;
                client.Partner.City = _city;
                client.Partner.State = _state;
                client.Partner.Zip = _zip;
            }
        }

        private Child CreateChild()
        {
            return new Child
            {
                FirstName = _childFirstName,
                LastName = _childLastName,
                MiddleInit = _childMiddleInit,
                BirthDate = (DateTime)_childBirthDate,
                Pediatrician = _childPediatrician,
                ReceivingHealthcare = (bool)_receivingHealthcare,
                InDaycare = (bool)_inDaycare,
                Disabled = (bool)_disabled
            };
        }

        private void CreateServicesReq(Client client)
        {
            client.ServicesRequested = new ServicesRequestedAggregation();

            Service request;

            foreach (KeyValuePair<string, bool> service in _services)
            {
                request = (service.Value == true) ? new Service() : null;
                setServiceInfo(client, request, service.Key);
            }

            if (!String.IsNullOrEmpty(_otherService))
            {
                request = new Service();
                setServiceInfo(client, request, _otherService);
            }
        }

        private void setServiceInfo(Client client, Service request, string description)
        {
            if (request == null)
            {
                return;
            }

            request.Description = description;
            request.Date = DateTime.Today;
            request.StaffId = _staffMember.StaffId;

            client.ServicesRequested.Add(request);
        }

        private void CreateHousing( Client client )
        {
            client.Housing = new Housing();
            client.Housing.HousingStatus = _housingStatus;
            client.Housing.ResidentialProgram = _residentialProgram;
        }

        private void CreatePregnancyInfo(Client client)
        {
            client.Pregnancy = new Pregnancy();
            client.Pregnancy.ReturnPregnancy = _returnPregnancy;
            client.Pregnancy.VerifiedPregnancy = _verifiedPregnancy;
            client.Pregnancy.NeedPregnancyTest = _needPregnancyTest;
            client.Pregnancy.SignedReleaseForTest = _signedForRelease;
            client.Pregnancy.DueDate = _dueDate;
            client.Pregnancy.Intentions = _intention;
            client.Pregnancy.BirthControl = _birthControl;
            client.Pregnancy.MedInsurance = _medInsurance;
            client.Pregnancy.Ob = _ob;
            client.Pregnancy.PrenatalVitamin = _prenatalVitamins;
            client.Pregnancy.Wic = _wic;
            client.Pregnancy.Hugs = _hugs;
            client.Pregnancy.HugsNurse = _hugsNurse;
            client.Pregnancy.FoodStamps = _foodStamps;
            client.Pregnancy.FamiliesFirst = _foodStamps;
            client.Pregnancy.LifeBridgeFss = _lifeBridge;
            client.Pregnancy.ResultOfPregnancy = _resultOfPregnancy;
        }

        private string StripComboBoxText(string input)
        {
            int start = input.IndexOf(" ", StringComparison.Ordinal) + 1;
            int end = input.Length - input.IndexOf(" ", StringComparison.Ordinal) - 1;
            return input.Substring ( start, end );
        }
#endregion
    }
}