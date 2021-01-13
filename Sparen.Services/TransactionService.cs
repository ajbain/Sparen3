using Sparen.Data;
using Sparen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparen.Services
{
    public class TransactionService
    {
        private readonly Guid _userId;
        public TransactionService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateTransaction(TransactionCreate model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                BankAccount bankAcct = ctx.Accounts.Where(x => x.AccountNumber == model.AccountNumber).Single();
                if (model.IsDeposit == true)
                {
                    var entity = new Credit()
                    {

                        OwnerId = _userId,
                        OldBalance = bankAcct.Balance,
                        AccountNumber = model.AccountNumber,
                        StoreName = model.StoreName,
                        Amount = model.Amount,
                        TransactionType= "Credit",
                        Balance = bankAcct.Balance + model.Amount,
                        AccountType = bankAcct.AccountType,
                        DateOfTransaction = model.DateOfTransaction
                        
                    };
                    ctx.Credits.Add(entity);
                    bankAcct.Balance = bankAcct.Balance + model.Amount;
                }
                else
                {
                    var entity = new Debit()
                    {
                        OwnerId = _userId,
                        OldBalance = bankAcct.Balance,
                        AccountNumber = model.AccountNumber,
                        StoreName = model.StoreName,
                        Amount = model.Amount,
                        Balance = bankAcct.Balance - model.Amount,
                        AccountType = bankAcct.AccountType,
                        TransactionType = "Debit",
                        DateOfTransaction = model.DateOfTransaction

                    };
                    ctx.Debits.Add(entity);
                    bankAcct.Balance = bankAcct.Balance - model.Amount;
                }
                return ctx.SaveChanges() == 2;
            }
        }
        public IEnumerable<TransactionListItem> GetSavingsTransactions()
        {
            using (var ctx = new ApplicationDbContext())
            {
                //var checkingTransactions = ctx.Transactions.Where(e => e.BankAccount.AccountType == AccountType.Checking).ToList();
                //var savingsTransactions = ctx.Transactions.Where(e => e.BankAccount.AccountType == AccountType.Savings).ToList();
                var query = ctx.Transactions
                    .Where(e => e.OwnerId == _userId).Where(e=> e.BankAccount.AccountType == AccountType.Savings)
                    .Select(e => new TransactionListItem
                    {
                        Amount = e.Amount,
                        StoreName = e.StoreName,
                        CurrentBalance = e.Balance,
                        

                        //TransactionType = e.TransactionType,
                    });//.ToList().Sort();
                //ctx.SaveChanges();
                return query.ToArray();
            }
        }
        public IEnumerable<TransactionListItem> GetCheckingTransactions()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Transactions
                    .Where(e=> e.OwnerId == _userId).Where (e => e.BankAccount.AccountType == AccountType.Checking)
                    .Select(e => new TransactionListItem
                    {
                        Amount = e.Amount,
                        StoreName = e.StoreName,
                        CurrentBalance = e.Balance,
                    });
                return query.ToArray();
            }
        }
        public IEnumerable<TransactionListItem> GetTransactions()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Transactions.Where(e => e.OwnerId == _userId)
                    .Select(e => new TransactionListItem
                    {
                        TransactionId = e.TransactionId,
                        Amount = e.Amount,
                        StoreName = e.StoreName,
                        CurrentBalance = e.Balance,
                        AccountType = e.AccountType,
                        TransactionType = e.TransactionType ,
                        DateOfTransaction = e.DateOfTransaction,
                        OldBalance = e.OldBalance
                        
                        
                    });
                
                
                return query.ToArray();
            }
        }

        public double CreditBalance(double amount)
        {
            BankAccount bankAccount = new BankAccount();
            var newBalance = bankAccount.Balance += amount;
            return newBalance;
        }
        public double DebitBalance(double amount)
        {
            BankAccount bankAccount = new BankAccount();
            var newBalance = bankAccount.Balance -= amount;
            return newBalance;
        }

        public double BalanceUpdate(double amount)
        {
            Transaction tx = new Transaction();
            if (tx is Debit)
            {

                tx.Balance -= amount;
                return tx.Balance;
            }
            else
            {
                tx.Balance += amount;
                return tx.Balance;
            }
        }
      
        //public TransactionDetail GetDetail(int id)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var entity = ctx.Transactions.SingleOrDefault(e => e.TransactionId == id && e.OwnerId == _userId);/*Where(e => e.TransactionId == id);*//*DefaultIfEmpty(e => e.TransactionId == id && e.OwnerId == _userId);*/
        //        return
        //            new TransactionDetail
        //            {
        //                TransactionId = entity.TransactionId,
        //                AccountType = entity.AccountType,
        //                Balance = entity.Balance,
        //                StoreName = entity.StoreName,

        //            };
        //    }
        //}

        public TransactionDetail GetTransactionById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx.Transactions.Single(e => e.TransactionId == id && e.OwnerId == _userId);
                BankAccount bankAcct = ctx.Accounts.Where(x => x.AccountNumber == entity.AccountNumber).Single();
                return new TransactionDetail
                {
                    AccountNumber = entity.AccountNumber,
                    TransactionId = entity.TransactionId,
                    StoreName = entity.StoreName,
                    Amount = entity.Amount,
                    Balance = entity.Balance,
                    OldBalance = entity.OldBalance,
                    DateOfTransaction = entity.DateOfTransaction,
                    

                };
            }
        }

        public bool UpdateTransaction (TransactionEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                BankAccount bankAcct = ctx.Accounts.Where(x => x.AccountNumber == model.AccountNumber).Single();
                var entity  = ctx.Transactions
                    .Single(e=> e.TransactionId == model.TransactionId && e.OwnerId == _userId);

                bankAcct.AccountNumber = model.AccountNumber;
                entity.StoreName = model.StoreName;
                entity.Amount = model.Amount;
                //entity.OldBalance = model.OldBalance;
                entity.DateOfTransaction = model.DateOfTransaction;
                if(entity is Debit) { 
                bankAcct.Balance = entity.OldBalance - model.Amount;
                }
                else
                {
                    bankAcct.Balance = entity.OldBalance + model.Amount;
                }
                entity.Balance = bankAcct.Balance;

                return ctx.SaveChanges() == 2;

            }
        }
        public bool DeleteTransaction (int transactionId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Transactions
                    .Single(e => e.TransactionId == transactionId && e.OwnerId == _userId);
                BankAccount bankAcct = ctx.Accounts.Where(x => x.AccountNumber == entity.AccountNumber).Single();
                bankAcct.Balance = entity.OldBalance;

                ctx.Transactions.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
