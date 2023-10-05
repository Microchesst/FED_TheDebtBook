using System;
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
    public partial class DebtorDetailsViewModel : BaseViewModel
    {
        [ObservableProperty]
        private ObservableCollection<DebtTransaction> _transactionsList;

        [ObservableProperty]
        private string _newTransactionDescription;

        [ObservableProperty]
        private double _newTransactionAmount;

        [ObservableProperty]
        private string _debtorName;

        [ObservableProperty]
        private DateTime _transactionDate;
        
        [ObservableProperty]
        private double _totalAmountSpent;


        private int _debtorId;

        public ICommand AddTransactionCommand { get; }

        public DebtorDetailsViewModel()
        {
            WeakReferenceMessenger.Default.Register<NavigateToDebtorDetailsMessage>(this, OnReceivedMessage);
            AddTransactionCommand = new RelayCommand(OnAddTransaction);
        }


        private void OnReceivedMessage(object recipient, NavigateToDebtorDetailsMessage message)
        {
            Initialize(message.DebtorId);
        }

        public async void Initialize(int debtorId)
        {
            _debtorId = debtorId;
            var debtor = await DataBaseHelper.GetDebtorByIdAsync(debtorId);
            if (debtor != null)
            {
                DebtorName = debtor.Name;
            }
            LoadTransactions();
        }

        private async void LoadDebtorDetails()
        {
            var debtor = await DataBaseHelper.GetDebtorByIdAsync(_debtorId);
            if (debtor != null)
            {
                DebtorName = debtor.Name;
            }
        }

        public async void LoadTransactions()
        {
            TransactionsList = new ObservableCollection<DebtTransaction>(await DataBaseHelper.GetTransactionsForDebtorAsync(_debtorId));
            TotalAmountSpent = TransactionsList.Sum(t => t.Amount);
        }

        private async void OnAddTransaction()
        {
            if (string.IsNullOrWhiteSpace(NewTransactionDescription) || NewTransactionAmount == 0)
            {
                return;
            }

            DebtTransaction newTransaction = new DebtTransaction
            {
                Description = NewTransactionDescription,
                Amount = NewTransactionAmount,
                Date = DateTime.Now,
                Type = NewTransactionAmount > 0 ? TransactionType.Credit : TransactionType.Debit,
                DebtorId = _debtorId
            };

            // Save the new transaction to the database
            await DataBaseHelper.AddDebtTransactionAsync(newTransaction);

            // Reload the transactions
            LoadTransactions();

            // Clear the input fields
            NewTransactionDescription = string.Empty;
            NewTransactionAmount = 0;
        }
    }
}
