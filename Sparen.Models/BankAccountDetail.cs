using Sparen.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparen.Models
{
    public class BankAccountDetail
    {
        [Display(Name=" Account Number")]
        public int AccountNumber { get; set; }

        [Display(Name =" Account Type")]
        public AccountType AccountType { get; set; }
        [Display(Name = "Account NickName")]
        public string NameGiven { get; set; }
        [Display(Name = "Account Level")]
        public AccountLevel AccountLevel { get; set; }
        public double Interest { get; set; }
        //public virtual List<Transaction> Transactions { get; set; } /*= new List<TransactionListItem>();*/
    }
}
