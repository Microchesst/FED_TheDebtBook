using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDebtBook.Models
{
    public class Debtor
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(250)]
        public string Name { get; set; }

        public double TotalAmountOwed { get; set; }

        [Ignore]
        public ObservableCollection<DebtTransaction> Transactions { get; set; } = new ObservableCollection<DebtTransaction>();
        [Ignore]
        public ObservableCollection<Debtor> DebtorsList { get; set; } = new ObservableCollection<Debtor>();

    }

}
