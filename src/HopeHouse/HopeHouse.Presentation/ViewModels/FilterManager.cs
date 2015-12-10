using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using HopeHouse.Core.DataAccess;
using HopeHouse.Core.Models;

namespace HopeHouse.Presentation.ViewModels
{
    internal class FilterManager
    {
        #region Private Fields

        private Dictionary<string, FilterType> _stringToFilterTypeMap;
        private Dictionary<FilterType, Type> _filterTypeToTypeMap;
        private Dictionary<FilterType, List<string>> _filterTypeToFieldMap;
        private Filter _currentFilter;
        private List<Client> _currentFilterClients;
        private Dictionary<Filter, List<Client>> _appliedFilterToClients;
        private List<Client> _appliedFilterClients;

        #endregion

        #region Properties

        public Filter CurrentFilter
        {
            get
            {
                return _currentFilter;
            }
        }

        public List<Client> CurrentFilterClients
        {
            get
            {
                return _currentFilterClients;
            }
        } 

        public ICollection<Filter> AppliedFilters
        {
            get
            {
                if (_appliedFilterToClients == null)
                {
                    _appliedFilterToClients = new Dictionary<Filter, List<Client>>();
                }

                return _appliedFilterToClients.Keys.ToList();
            }
        }

        #endregion

        #region Constructor

        public FilterManager()
        {
            _stringToFilterTypeMap = new Dictionary<string, FilterType>();
            _filterTypeToTypeMap = new Dictionary<FilterType, Type>();
            _filterTypeToFieldMap = new Dictionary<FilterType, List<string>>();
            _currentFilterClients = new List<Client>();
            _appliedFilterToClients = new Dictionary<Filter, List<Client>>();

            _appliedFilterClients = new List<Client>(ClientManager.GetAllClients());

            PopulateStringToFilterTypeMap();
            PopulateFilterTypeToTypeMap();
            PopulateFilterTypeToFieldMap();
        }

        #endregion

        #region Public Methods

        public ICollection<string> GetFilterTypeOptions()
        {
            return _stringToFilterTypeMap.Keys;
        }

        public ICollection<string> GetFilterFieldOptions(string filterTypeString)
        {
            if (!string.IsNullOrEmpty(filterTypeString))
            {
                if (_stringToFilterTypeMap.ContainsKey(filterTypeString))
                {
                    FilterType filterType = _stringToFilterTypeMap[filterTypeString];
                    return _filterTypeToFieldMap[filterType];
                }
            }

            return new Collection<string>();
        }

        public bool IsPropertyType<T>(string filterTypeString, string filterFieldString)
        {
            if ((!string.IsNullOrEmpty(filterTypeString)) && (!string.IsNullOrEmpty(filterFieldString)))
            {
                if (_stringToFilterTypeMap.ContainsKey(filterTypeString))
                {
                    FilterType filterType = _stringToFilterTypeMap[filterTypeString];
                    Type type = _filterTypeToTypeMap[filterType];

                    PropertyInfo property = type.GetProperties().First(
                            x => x.GetCustomAttribute<DescriptionAttribute>()?.Description == filterFieldString);

                    if (property != null)
                    {
                        return property.PropertyType == typeof (T);
                    }
                }
            }

            return false;
        }

        public ICollection<Client> GetAllClients()
        {
            List<Filter> appliedFilters = _appliedFilterToClients.Keys.ToList();

            foreach (Filter filter in appliedFilters)
            {
                _appliedFilterToClients.Remove(filter);
            }

            _currentFilter = new AllClientsFilter();

            return new ObservableCollection<Client>(ClientManager.GetAllClients());
        } 

        public ICollection<Client> GetPresetFilteredClients(Filter presetFilter)
        {
            ICollection<Client> clients = new Collection<Client>();

            if (presetFilter != null)
            {
                _currentFilter = presetFilter;

                clients = new ObservableCollection<Client>(ClientManager.GetFilteredClients(_currentFilter, _appliedFilterClients));
            }

            return clients;
        } 

        public ICollection<Client> GetStringFilteredClients(string filterTypeString, string filterFieldString, string filterString)
        {
            ICollection<Client> clients = new Collection<Client>();

            if ((!string.IsNullOrEmpty(filterTypeString)) && (!string.IsNullOrEmpty(filterFieldString)))
            {
                if (_stringToFilterTypeMap.ContainsKey(filterTypeString))
                {
                    FilterType filterType = _stringToFilterTypeMap[filterTypeString];
                    _currentFilter = new StringFilter(filterType, filterFieldString, filterString);

                    clients = new ObservableCollection<Client>(ClientManager.GetFilteredClients(_currentFilter, _appliedFilterClients));
                }
            }

            return clients;
        }

        public ICollection<Client> GetDateFilteredClients(string filterTypeString, string filterFieldString, string filterDay,
            string filterMonth, string filterYear)
        {
            ICollection<Client> clients = new Collection<Client>();

            if ((!string.IsNullOrEmpty(filterTypeString)) && (!string.IsNullOrEmpty(filterFieldString)))
            {
                if (_stringToFilterTypeMap.ContainsKey(filterTypeString))
                {
                    FilterType filterType = _stringToFilterTypeMap[filterTypeString];
                    _currentFilter = new DateFilter(filterType, filterFieldString, filterDay, filterMonth, filterYear);

                    clients = new ObservableCollection<Client>(ClientManager.GetFilteredClients(_currentFilter, _appliedFilterClients));
                }
            }

            return clients;
        }

        public void AddAppliedFilter()
        {
            if ((_currentFilter != null) && (!_appliedFilterToClients.ContainsKey(_currentFilter)))
            {
                List<Client> excludedClients = ClientManager.GetAllClients().Except(
                    ClientManager.GetFilteredClients(_currentFilter)).ToList();

                _appliedFilterToClients[_currentFilter] = excludedClients;

                UpdateAppliedFilterClients();
            }
        }

        public ICollection<Client> DeleteAppliedFilter(Filter filter)
        {
            if (_appliedFilterToClients.ContainsKey(filter))
            {
                _appliedFilterToClients.Remove(filter);

                UpdateAppliedFilterClients();

                if (_currentFilter == null)
                {
                    return _appliedFilterClients;
                }

                return ClientManager.GetFilteredClients(_currentFilter, _appliedFilterClients);
            }

            return _appliedFilterClients;
        }

        #endregion

        #region Private Methods

        private void UpdateAppliedFilterClients()
        {
            _appliedFilterClients.Clear();
            _appliedFilterClients.AddRange(ClientManager.GetAllClients());

            foreach (List<Client> excludedClients in _appliedFilterToClients.Values)
            {
                _appliedFilterClients = _appliedFilterClients.Except(excludedClients).ToList();
            }
        } 

        private void PopulateStringToFilterTypeMap()
        {
            IEnumerable<FilterType> filterTypes = Enum.GetValues(typeof (FilterType)).Cast<FilterType>();

            foreach (FilterType filterType in filterTypes)
            {
                string filterTypeString = Enum.GetName(typeof (FilterType), filterType);
                DescriptionAttribute descriptionAttr = typeof (FilterType).GetField(filterTypeString)
                    .GetCustomAttribute<DescriptionAttribute>();

                if (descriptionAttr != null)
                {
                    _stringToFilterTypeMap[descriptionAttr.Description] = filterType;
                }
            }
        }

        private void PopulateFilterTypeToTypeMap()
        {
            foreach (FilterType filterType in _stringToFilterTypeMap.Values)
            {
                string filterTypeString = Enum.GetName(typeof (FilterType), filterType);
                Type type = Type.GetType("HopeHouse.Core.Models." + filterTypeString + ", HopeHouse.Core");

                if (type != null)
                {
                    _filterTypeToTypeMap[filterType] = type;
                }
            }
        }

        private void PopulateFilterTypeToFieldMap()
        {
            foreach (KeyValuePair<FilterType, Type> pair in _filterTypeToTypeMap)
            {
                PropertyInfo[] properties = pair.Value.GetProperties().Where(
                    x => x.GetCustomAttribute<FilterAttribute>() != null).ToArray();

                List<string> typeFields = new List<string>();

                foreach (PropertyInfo property in properties)
                {
                    DescriptionAttribute descriptionAttr = property.GetCustomAttribute<DescriptionAttribute>();

                    if (descriptionAttr != null)
                    {
                        typeFields.Add(descriptionAttr.Description);
                    }
                }

                _filterTypeToFieldMap[pair.Key] = typeFields;
            }
        }

        //private void UpdateAppliedFilterClients()
        //{
        //    if (_appliedFilterToClientsRemoved.Count > 0)
        //    {
        //        _appliedFilterClients.Clear();
        //        _appliedFilterClients.AddRange(ClientManager.GetFilteredClients(_appliedFilterToClientsRemoved.ToList()[0]));

        //        foreach (Filter filter in _appliedFilterToClientsRemoved)
        //        {
        //            ICollection<Client> filterClients = ClientManager.GetFilteredClients(filter);

        //            _appliedFilterClients = (from x in filterClients
        //                                     join y in _appliedFilterClients
        //                                         on x.ClientId equals y.ClientId
        //                                     select x).ToList();
        //        }
        //    }
        //}

        #endregion
    }
}
