using Sparen.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparen.Models
{
    public class TransactionListItem
    { 

        public int TransactionId { get; set; }
        public Guid OwnerId { get; set; }
        public double Amount { get; set; }
        [Display(Name="Merchant Name")]
        public string StoreName { get; set; }
        [Display(Name="Balance after transaction:")]
        public double CurrentBalance { get; set; }
        [Display(Name="Type of transaction:")]
        //public string NewBalance { get; set; }
        public string TransactionType { get; set; }
        [Display(Name="Account Type")]
        public AccountType AccountType { get; set; }
        [Display(Name ="Date of Transaction:")]
        [DataType(DataType.Date)]
        public DateTime DateOfTransaction { get; set; }
        public double OldBalance { get; set; }
        

    }
}
