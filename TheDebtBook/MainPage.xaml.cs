using TheDebtBook.ViewModels;
using Microsoft.Maui.Controls;
using TheDebtBook.Data;
using TheDebtBook.Models;

namespace TheDebtBook
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnDebtorSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is Debtor selectedDebtor)
            {
                (BindingContext as MainPageViewModel)?.NavigateToDebtorDetailsCommand.Execute(selectedDebtor.Id);
            }

            // Deselect the item in the ListView
            ((ListView)sender).SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            (BindingContext as MainPageViewModel)?.LoadDebtors();
        }
        private async void OnResetClicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Confirm", "Are you sure you want to reset the database?", "Mega Yes", "Little No");
            if (confirm)
            {
                await DataBaseHelper.ClearDatabaseAsync();

                // Refresh the UI
                if (BindingContext is MainPageViewModel viewModel)
                {
                    viewModel.LoadDebtors();
                }
            }
        }
    }
}