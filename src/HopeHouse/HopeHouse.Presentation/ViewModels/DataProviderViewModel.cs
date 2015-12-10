using HopeHouse.Common.Interfaces;
using HopeHouse.Presentation.Commanding;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using HopeHouse.Common.EventFramework;
using HopeHouse.Core.Models;

namespace HopeHouse.Presentation.ViewModels
{
    public enum DataSearchBy
    {
        ShowDefault,
        Property,
        Value
    }

    public class DataProviderViewModel : ViewModelBase
    {
        #region Private Fields

        private readonly DataSearchBy[] _searchOptions =
        {
            DataSearchBy.Property,
            DataSearchBy.Value
        };
        private DataSearchBy _searchBy = DataSearchBy.ShowDefault;
        private string _searchString;
        private IDataProvider _dataProvider;
        private ICollection<DataDisplayWrapper> _displayData;
        private bool _isEditing;

        #endregion

        #region Properties

        public IEnumerable<DataSearchBy> SearchOptions
        {
            get
            {
                return _searchOptions;
            }
        }

        public DataSearchBy SearchBy
        {
            get
            {
                return _searchBy;
            }
            set
            {
                _searchBy = value;
                OnPropertyChanged(nameof(SearchBy));
                OnPropertyChanged(nameof(IsSearchStringTextBoxEnabled));
            }
        }

        public bool IsSearchStringTextBoxEnabled
        {
            get
            {
                return _searchBy != DataSearchBy.ShowDefault;
            }
        }

        public string SearchString
        {
            get
            {
                return _searchString;
            }
            set
            {
                _searchString = value;
                OnPropertyChanged(nameof(SearchString));
                UpdateDisplayData();
            }
        }

        public ICollection<DataDisplayWrapper> DisplayData
        {
            get
            {
                if (_displayData == null)
                {
                    _displayData = new ObservableCollection<DataDisplayWrapper>();
                }

                return _displayData;
            }
            set
            {
                _displayData = value;
                OnPropertyChanged(nameof(DisplayData));
            }
        }

        public bool IsEditing
        {
            get
            {
                return _isEditing;
            }
            set
            {
                _isEditing = value;
                OnPropertyChanged(nameof(IsEditing));
            }
        }

        #endregion

        #region Constructor

        public DataProviderViewModel(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
            
            _displayData = new ObservableCollection<DataDisplayWrapper>();

            foreach (KeyValuePair<string, object> dataPair in dataProvider.GetData())
            {
                _displayData.Add(new DataDisplayWrapper(dataPair.Key, dataPair.Value) { IsDisplayed = true });
            }
        }

        #endregion

        #region Commands

        private ICommand _editCommand;
        public ICommand EditCommand
        {
            get
            {
                if (_editCommand == null)
                {
                    _editCommand = new RelayCommand(x =>
                    {
                        IsEditing = true;
                    },
                    x => _dataProvider != null);
                }

                return _editCommand;
            }
        }

        private ICommand _saveChangesCommand;
        public ICommand SaveChangesCommand
        {
            get
            {
                if (_saveChangesCommand == null)
                {
                    _saveChangesCommand = new RelayCommand(x =>
                    {
                        foreach(DataDisplayWrapper wrapper in _displayData)
                        {
                            _dataProvider.SetDataProperty(wrapper.DisplayName, wrapper.Value);
                        }

                        _dataProvider.Update();
                        IsEditing = false;
                    },
                    x => true);
                }

                return _saveChangesCommand;
            }
        }

        private ICommand _viewObjectDataCommand;
        public ICommand ViewObjectDataCommand
        {
            get
            {
                if (_viewObjectDataCommand == null)
                {
                    _viewObjectDataCommand = new RelayCommand(x =>
                    {
                        if (x is IDataProvider)
                        {
                            IDataProvider dataProvider = x as IDataProvider;

                            if (dataProvider != null)
                            {
                                EventProvider.PublishAddDataProviderDisplay(dataProvider);
                            }
                        }
                        else if(x is IDataProviderAggregation)
                        {
                            IDataProviderAggregation dataProviderAggregation = x as IDataProviderAggregation;
                            Client client = _dataProvider as Client;

                            if(dataProviderAggregation != null && client != null)
                            {
                                EventProvider.PublishAddDataProviderAggregationDisplay(client.ClientId, dataProviderAggregation);
                            }
                        }
                    },
                    x => true);
                }

                return _viewObjectDataCommand;
            }
        }

        #endregion

        #region Public Methods

        public void UpdateDisplayData()
        { 
            if (_searchString != null)
            {
                if (_searchBy == DataSearchBy.Property)
                {
                    foreach (DataDisplayWrapper dataWrapper in _displayData)
                    {
                        if (dataWrapper.DisplayName.IndexOf(_searchString, StringComparison.InvariantCultureIgnoreCase) > -1)
                        {
                            dataWrapper.IsDisplayed = true;
                        }
                        else
                        {
                            dataWrapper.IsDisplayed = false;
                        }
                    }
                }
                else if (_searchBy == DataSearchBy.Value)
                {
                    foreach (DataDisplayWrapper dataWrapper in _displayData)
                    {
                        if (dataWrapper.Value == null)
                        {
                            dataWrapper.IsDisplayed = false;
                            continue;
                        }

                        if (dataWrapper.Value != null)
                        {
                            if (dataWrapper.Value.ToString().IndexOf(_searchString, StringComparison.InvariantCultureIgnoreCase) > -1)
                            {
                                dataWrapper.IsDisplayed = true;
                            }
                            else
                            {
                                dataWrapper.IsDisplayed = false;
                            }
                        }
                    }
                }
            }
        }

        #endregion
    }
}
