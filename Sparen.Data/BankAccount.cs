using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparen.Data
{
    public enum AccountType { Checking = 1, Savings, }
    public enum AccountLevel { Standard = 1, Premium, Emerald}
    public class BankAccount
    {
        [Key]
        public int AccountNumber { get; set; }
        public Guid OwnerId { get; set; }
        public double Balance { get; set; }
        public AccountType AccountType { get; set; }
        public string NameGiven { get; set; }
        public AccountLevel AccountLevel { get; set; }
        public List< Transaction> Transactions { get; set; }/* = new List<Transaction>();*/

    }
}
