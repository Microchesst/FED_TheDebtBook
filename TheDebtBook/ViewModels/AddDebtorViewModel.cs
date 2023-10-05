using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TheDebtBook.Data;
using TheDebtBook.Models;

namespace TheDebtBook.ViewModels
{
    public partial class AddDebtorViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _debtorName;

        [ObservableProperty]
        private double _amountOwed;

        public ICommand AddDebtorCommand { get; }

        public AddDebtorViewModel()
        {
            AddDebtorCommand = new RelayCommand(OnAddDebtor);
        }

        private async void OnAddDebtor()
        {
            if (string.IsNullOrWhiteSpace(DebtorName) || AmountOwed <= 0)
            {
                return;
            }

            Debtor newDebtor = new Debtor
            {
                Name = DebtorName,
                TotalAmountOwed = AmountOwed
            };

            await DataBaseHelper.AddDebtorAsync(newDebtor);
            var addedDebtor = await DataBaseHelper.GetDebtorByNameAsync(DebtorName); // Assuming you have a method to get debtor by name

            DebtTransaction initialTransaction = new()
            {
                Description = "Initial amount",
                Amount = AmountOwed,
                Date = DateTime.Now,
                Type = AmountOwed > 0 ? TransactionType.Credit : TransactionType.Debit,
                DebtorId = addedDebtor.Id
            };

            await DataBaseHelper.AddDebtTransactionAsync(initialTransaction);

            await Shell.Current.GoToAsync("//MainPage");
        }



    }
}