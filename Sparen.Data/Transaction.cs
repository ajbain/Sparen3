using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparen.Data
{
    public class Transaction

    {
        [Key]
        public int TransactionId { get; set; }
        public Guid OwnerId { get; set; }
        //foreign key is account number???
        public int AccountNumber { get; set; }
        public virtual BankAccount BankAccount { get; set; }
        public double Amount { get; set; }
        public double Balance { get; set; }
        public string TransactionType { get; set; }
        public AccountType AccountType { get; set; }
        public string StoreName { get; set; } 
        public DateTime DateOfTransaction { get; set; }
        public double OldBalance { get; set; }
        

    }
}
