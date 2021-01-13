using Sparen.Data;
using Sparen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparen.Services
{
    public class BankAccountService
    {
        private readonly Guid _userId;
        public BankAccountService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateBankAccount (BankAccountCreate model)
        {
            if (model.IsChecking == true)
            {
                var entity = new CheckingAccount()
                {
                    OwnerId = _userId,
                    AccountNumber = GenerateAccountNumber(),
                    AccountLevel = model.AccountLevel,
                    Balance = model.Balance,
                    AccountType = AccountType.Checking,
                    NameGiven = model.NameGiven
                };
                using (var ctx = new ApplicationDbContext())
                {
                    ctx.CheckingAccounts.Add(entity);
                    return ctx.SaveChanges() == 1;
                }
            }
            else
            {
                var entity = new SavingAccount()
                {
                    OwnerId = _userId,
                    AccountNumber = GenerateAccountNumber(),
                    AccountType = AccountType.Savings,
                    AccountLevel = model.AccountLevel,
                    Balance = model.Balance,
                    NameGiven = model.NameGiven,
                    Interest = 0.01,
                    TransactionCount = 0,

                };
                using (var ctx = new ApplicationDbContext())
                {
                    ctx.SavingAccounts.Add(entity);
                    return ctx.SaveChanges() == 1;
                }
            }
        }
        public IEnumerable<BankAccountListItem> GetBankAccounts()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Accounts
                    .Where(e=> e.OwnerId == _userId)
                    .Select(e => new BankAccountListItem
                {
                    AccountNumber = e.AccountNumber,
                    AccountLevel = e.AccountLevel,
                    AccountType = e.AccountType,
                    Balance = e.Balance,
                    NameGiven = e.NameGiven,
                });
                return query.ToArray();
            }
        }
        public BankAccountDetail GetDetails(int? id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Accounts.Single(e => e.AccountNumber == id && e.OwnerId == _userId);
                return
                    new BankAccountDetail
                    {
                        AccountNumber = entity.AccountNumber,
                        AccountType = entity.AccountType,
                        NameGiven = entity.NameGiven,
                        AccountLevel= entity.AccountLevel,
                        
              
                        
                        
                    }; 
            }
        }

        public BankAccountDetail GetBankAccountById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx.Accounts.Single(e => e.AccountNumber == id && e.OwnerId == _userId);
                return new BankAccountDetail
                {
                    AccountNumber = entity.AccountNumber,
                    AccountType = entity.AccountType,
                    
                };
            }
        }
        public bool UpdateBankAccount (BankAccountEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Accounts.Single(e => e.AccountNumber == model.AccountNumber && e.OwnerId == _userId);
                entity.NameGiven = model.NameGiven;
                entity.AccountLevel = model.AccountLevel;
                

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteBankAccount(int bankAccount)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Accounts
                    .Single(e => e.AccountNumber == bankAccount && e.OwnerId == _userId);
                ctx.Accounts.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        // probably do want to have a detail data page, because i would like a list of transactions per account on that item. 

        public int GenerateAccountNumber()
        {
            Random randy = new Random();
            int accountNumber = randy.Next(100000000, 999999999);
            return accountNumber;

            // get checking acct to start at a higher number and climb//
        }


    }
}
