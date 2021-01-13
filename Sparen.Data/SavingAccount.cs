using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparen.Data
{
    public class SavingAccount : BankAccount
    {
        public double Interest { get; set; }
        public int TransactionCount { get; set; }
    }
}
