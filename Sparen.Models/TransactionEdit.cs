using Sparen.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparen.Models
{
    public class TransactionEdit
    {
        public int TransactionId { get; set; }
        [Display(Name="Amount of Transaction")]
        public double Amount { get; set; }
        [Display(Name="Merchant Name")]
        public string StoreName { get; set; }
        [Display(Name = "Date of Transaction:")]
        public DateTime DateOfTransaction { get; set; }
        public int AccountNumber { get; set; }
        public BankAccount BankAccount { get; set;  }
        public double OldBalance { get; set; }
        public double Balance { get; set; }

    }
}
