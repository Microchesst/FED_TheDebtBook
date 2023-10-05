using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using TheDebtBook.Data;
using TheDebtBook.Models;

namespace TheDebtBook.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Debtor> _debtorsList;

        public ICommand NavigateToAddDebtorCommand { get; }
        public ICommand NavigateToDebtorDetailsCommand { get; }

        public MainPageViewModel()
        {
            LoadDebtors();
            NavigateToAddDebtorCommand = new RelayCommand(OnNavigateToAddDebtor);
            NavigateToDebtorDetailsCommand = new RelayCommand<int>(OnNavigateToDebtorDetails);
        }

        public async void LoadDebtors()
        {
            var debtors = await DataBaseHelper.GetAllDebtorsAsync();

            DebtorsList = new ObservableCollection<Debtor>(debtors);
        }

        private void OnNavigateToAddDebtor()
        {
            Shell.Current.GoToAsync("//AddDebtorPage");
        }

        private void OnNavigateToDebtorDetails(int debtorId)
        {
            WeakReferenceMessenger.Default.Send(new NavigateToDebtorDetailsMessage(debtorId));
            Shell.Current.GoToAsync("//DebtorDetailsPage");
        }


    }
}