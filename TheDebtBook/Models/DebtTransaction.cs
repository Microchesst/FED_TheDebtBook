using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace TheDebtBook.Models
{

    public class DebtTransaction
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed]
        public int DebtorId { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public double Amount { get; set; }

        public DateTime Date { get; set; }

        public TransactionType Type { get; set; }
    }

    public enum TransactionType
    {
        Debit,
        Credit
    }


}
