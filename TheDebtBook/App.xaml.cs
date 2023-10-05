using TheDebtBook.Data;

namespace TheDebtBook
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Task.Run(async () => await DataBaseHelper.InitializeDatabaseAsync()).Wait();

            MainPage = new AppShell();
        }
    }
}