using HopeHouse.Common.EventFramework;
using HopeHouse.Common.Interfaces;
using HopeHouse.Core.Models;
using HopeHouse.Presentation.Commanding;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Linq;

namespace HopeHouse.Presentation.ViewModels
{
    public class DataProviderAggregationViewModel : ViewModelBase
    {
        #region Private Fields

        private int _ownerId;
        private IDataProviderAggregation _dataProviderAggregation;
        private ICollection<DataDisplayWrapper> _displayData;

        #endregion

        #region Properties

        public ICollection<DataDisplayWrapper> DisplayData
        {
            get
            {
                if(_displayData == null)
                {
                    _displayData = new ObservableCollection<DataDisplayWrapper>();
                }

                return _displayData;
            }
        }

        public string DataProviderType
        {
            get
            {
                return _dataProviderAggregation.GetDataProviderType();
            }
        }

        #endregion

        #region Constructor

        public DataProviderAggregationViewModel(int ownerId, IDataProviderAggregation dataProviderAggregation)
        {
            _ownerId = ownerId;
            _dataProviderAggregation = dataProviderAggregation;

            _displayData = new ObservableCollection<DataDisplayWrapper>();

            GetDisplayData();
        }

        #endregion

        #region Commands

        private ICommand _viewObjectDataCommand;
        public ICommand ViewObjectDataCommand
        {
            get
            {
                if (_viewObjectDataCommand == null)
                {
                    _viewObjectDataCommand = new RelayCommand(x =>
                    {
                        IDataProvider dataProvider = x as IDataProvider;

                        if (dataProvider != null)
                        {
                            EventProvider.PublishAddDataProviderDisplay(dataProvider);
                        }
                    },
                    x => true);
                }

                return _viewObjectDataCommand;
            }
        }

        private ICommand _deleteObjectCommand;
        public ICommand DeleteObjectCommand
        {
            get
            {
                if(_deleteObjectCommand == null)
                {
                    _deleteObjectCommand = new RelayCommand(arg =>
                    {
                        IDataProvider dataProvider = arg as IDataProvider;

                        if (dataProvider != null)
                        {
                            MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete {dataProvider.GetIdentifier()} ?",
                                "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                            if (result == MessageBoxResult.Yes)
                            {
                                _dataProviderAggregation.DeleteDataProvider(dataProvider);

                                DataDisplayWrapper remove = _displayData.First(x => x.Value == dataProvider);
                                _displayData.Remove(remove);
                                OnPropertyChanged(nameof(DisplayData));
                            }
                        }
                    },
                    x => true);
                }

                return _deleteObjectCommand;
            }
        }

        private ICommand _addObjectCommand;
        public ICommand AddObjectCommand
        {
            get
            {
                if(_addObjectCommand == null)
                {
                    _addObjectCommand = new RelayCommand(arg =>
                    {
                        IDataProvider dataProviderToAdd = null;

                        switch(_dataProviderAggregation.GetDataProviderType())
                        {
                            case "Child":
                                dataProviderToAdd = new Child()
                                {
                                    FirstName = "New",
                                    LastName = "Child"
                                };
                                break;
                            case "Points":
                                dataProviderToAdd = new Points()
                                {
                                    Reason = "New Points"
                                };
                                break;
                            case "Service Received":
                            case "Service Requested":
                                dataProviderToAdd = new Service()
                                {
                                    Description = "New Service"
                                };
                                break;
                        }

                        if (dataProviderToAdd != null)
                        {
                            _dataProviderAggregation.AddDataProvider(_ownerId, dataProviderToAdd);

                            _displayData.Clear();
                            GetDisplayData();
                            OnPropertyChanged(nameof(DisplayData));
                        }
                    },
                    arg => true);
                }

                return _addObjectCommand;
            }
        }

        #endregion

        #region Private Methods

        private void GetDisplayData()
        {
            foreach (KeyValuePair<string, IDataProvider> dataPair in _dataProviderAggregation.GetDataProviders())
            {
                _displayData.Add(new DataDisplayWrapper(dataPair.Key, dataPair.Value) { IsDisplayed = true });
            }
        }

        #endregion
    }
}
