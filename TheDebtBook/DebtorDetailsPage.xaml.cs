using TheDebtBook.ViewModels;
using Microsoft.Maui.Controls;

namespace TheDebtBook
{
    public partial class DebtorDetailsPage : ContentPage
    {
        public DebtorDetailsPage()
        {
            InitializeComponent();
            BindingContext = new DebtorDetailsViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext is DebtorDetailsViewModel vm)
            {
                vm.LoadTransactions();
            }
        }
    }
}