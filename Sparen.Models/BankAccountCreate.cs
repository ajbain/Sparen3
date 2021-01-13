using Sparen.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparen.Models
{
    public class BankAccountCreate
    {
       //public int AccountNumber { get; set; }
        public double Balance { get; set; }
        //public Guid OwnerId { get; set; }
        //public AccountType AccountType { get; set; }
        [Display(Name ="Here you can set a nickname for your account:")]
        public string NameGiven { get; set; }
        [Display(Name ="Please check the box for Account Level")]
        public AccountLevel AccountLevel { get; set; }
        [Display(Name = "Is this a Checking Account?")]
        public bool IsChecking { get; set; }
    }
}
