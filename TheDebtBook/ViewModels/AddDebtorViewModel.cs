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

            // Save the new debtor to the database and await it
            await DataBaseHelper.AddDebtorAsync(newDebtor);

            // Add the initial transaction for the debtor
            DebtTransaction initialTransaction = new DebtTransaction
            {
                Description = "Initial amount",
                Amount = AmountOwed,
                Date = DateTime.Now,
                Type = AmountOwed > 0 ? TransactionType.Credit : TransactionType.Debit,
                DebtorId = newDebtor.Id
            };

            // Save the new transaction to the database and await it
            await DataBaseHelper.AddDebtTransactionAsync(initialTransaction);

            // Navigate back to the main page after adding
            Shell.Current.GoToAsync("//MainPage");
        }


    }
}