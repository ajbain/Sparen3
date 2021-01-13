using Sparen.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Sparen.Models
{
    public class TransactionCreate
    {
        public Guid OwnerId { get; set; }
        [Display(Name="Choose the Account Number that corresponds with this transaction:")]
        public int AccountNumber { get; set; }
        [Display(Name = "Merchant Name")]
        public string StoreName { get; set; }
        public double Amount { get; set; }
        [Display(Name ="Is this a deposit?")]
        public bool IsDeposit { get; set; }
        [Display(Name = "Transaction Type:")]
        public double CurrentBalance { get; set; }
        [Display(Name ="Date of Transaction:")]
        public DateTime DateOfTransaction { get; set; }
        public BankAccount BankAccount { get; set; }
        public AccountType AccountType { get; set; }
        public double OldBalance { get; set; }
        public IEnumerable<SelectListItem> AccountsList { get; set; }
    }
}
