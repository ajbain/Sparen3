using Sparen.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparen.Models
{
    public class TransactionDetail
    {
        [Display(Name ="Transaction ID:")]
        public int TransactionId { get; set; }
        [Display(Name ="Amount of Transaction:")]
        public double Amount { get; set; }
        [Display(Name ="Balance after Transaction:")]
        public double Balance { get; set; }
        [Display(Name="Account Type")]
        public AccountType AccountType { get; set; }
        [Display(Name ="Merchant Name")]
        public string StoreName { get; set; }
        [Display(Name = "Date of Transaction:")]
        public DateTime DateOfTransaction { get; set; }
        public int AccountNumber { get; set; }
        public BankAccount BankAccount { get; set; }
        public double OldBalance { get; set; }
    }
}
