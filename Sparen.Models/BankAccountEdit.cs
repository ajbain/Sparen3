using Sparen.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparen.Models
{
    public class BankAccountEdit
    {
        public int AccountNumber { get; set; }
        public string NameGiven { get; set; }
        public AccountLevel AccountLevel { get; set; }
    }
}
