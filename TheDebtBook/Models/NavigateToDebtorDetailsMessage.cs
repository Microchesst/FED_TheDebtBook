using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDebtBook.Models
{
    internal class NavigateToDebtorDetailsMessage
    {
        public int DebtorId { get; }

        public NavigateToDebtorDetailsMessage(int debtorId)
        {
            DebtorId = debtorId;
        }
    }
}
