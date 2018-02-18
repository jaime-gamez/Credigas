namespace Credigas.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows.Input;
    using Credigas.Models;
    using GalaSoft.MvvmLight.Command;
    using Services;
    using System.Linq;
    using Xamarin.Forms;

    public class WorkRouteViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Services
        ApiService apiService;
        DataService dataService;
        DialogService dialogService;
        NavigationService navigationService;
        #endregion

        #region Constructors
        public WorkRouteViewModel()
        {
            apiService = new ApiService();
            dataService = new DataService();
            dialogService = new DialogService();
            navigationService = new NavigationService();

            _originals = new List<Customer>();

            LoadClients();


        }
        #endregion

        #region Properties
        private List<Customer> _originals;

        private Statistics _statistics;
        public Statistics CurrentStatistics
        {
            get => _statistics;
            set
            {
                _statistics = value;
                PropertyChanged?.Invoke(
                        this,
                    new PropertyChangedEventArgs(nameof(CurrentStatistics)));
            }
        }

        public ObservableCollection<Customer> Clients
        {
            get;
            set;
        }

        private string _searchedText;
        public string SearchedText
        {
            get { return _searchedText; }
            set { 
                _searchedText = value; 

                if((_searchedText == null || _searchedText.Length == 0) && _originals.Count() > 0){
                    //Clients.Clear();
                    CopyClientsToObservableCollection(_originals);
                }else{
                    List<Customer> temps = (from c in _originals
                                            where c.FullName.ToUpper().Contains(SearchedText.ToUpper())
                                             || c.Address.ToUpper().Contains(SearchedText.ToUpper())
                                            || c.CustomerId.ToString().ToUpper().Contains(SearchedText.ToUpper())
                                            || Abonado(c.Modified).Contains(SearchedText.ToUpper())
                                            select c).ToList<Customer>();
                    if (temps != null)
                    {
                        
                        CopyClientsToObservableCollection(temps);
                        //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SearchedText)));
                    }
                }

                PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(nameof(SearchedText)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Clients)));
            }
        }
        #endregion

        #region Commands
        public ICommand NavigateCommand
        {
            get
            {
                return new RelayCommand(Navigate);
            }
        }

        async void Navigate()
        {
            await dialogService.ShowMessage(
                    "Crédigas",
                    "Valide carga de ruta.");
        }

        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(FilterCustomer);
            }
        }


        void FilterCustomer()
        {
            if (SearchedText.Length > 0)
            {
                
                List<Customer> temps = (from c in _originals
                                        where c.FullName.ToUpper().Contains(SearchedText.ToUpper())
                                         || c.Address.ToUpper().Contains(SearchedText.ToUpper())
                                        || c.CustomerId.ToString().ToUpper().Contains(SearchedText.ToUpper())
                                        || Abonado(c.Modified).Contains(SearchedText.ToUpper())
                                        select c).ToList<Customer>();
                if (temps != null)
                {
                    CopyClientsToObservableCollection(temps);
                    //Clients.Clear();
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Clients)));
                }

            }
            else
            {
                CopyClientsToObservableCollection(_originals);
            }
        }
        #endregion

        #region Methods
        private string Abonado(bool modified){

            if(modified)
                return "ABONADO";

            return "PENDIENTE";
        }
        void LoadClients()
        {
            Clients = new ObservableCollection<Customer>();
            _originals = dataService.GetAllCustomers();
            CopyClientsToObservableCollection(_originals);


        }

        public void UpdateClient(Customer client){

            var item = (from c in Clients
                        where c.CustomerId == client.CustomerId
                        select c).FirstOrDefault();
            if(item != null){
                var original = (from c in _originals
                            where c.CustomerId == client.CustomerId
                            select c).FirstOrDefault();
                original.Modified = true;
                item.Modified = true;
                PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(nameof(Clients)));
            }
            
        }

        void CopyClientsToObservableCollection(List<Customer> clients)
        {
            
            if (clients != null)
            {
                Clients.Clear();
                Clients = new ObservableCollection<Customer>();
                foreach (var client in clients)
                {
                    Clients.Add(client);
                }
            }

        }
        #endregion
    }
}
