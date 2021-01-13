using Microsoft.AspNet.Identity;
using Sparen.Data;
using Sparen.Models;
using Sparen.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sparen.Controllers
{
    public class TransactionController : Controller
    {
        // GET: Transaction
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new TransactionService(userId);
            var model = service.GetTransactions();
            return View(model);
        }

        //GET: FOR CREATE TRANSACTIONS
        public ActionResult Create()
        {
            var _context = new ApplicationDbContext();
            var bankAccounts = _context.Accounts.Select(b => b.AccountNumber);
            var accountView = new TransactionCreate
            {
                AccountsList = new SelectList(bankAccounts)
            };
            return View(accountView);
        }

        //POST: FOR CREATE TRANSACTIONS
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TransactionCreate model)
        {
            if (!ModelState.IsValid) return View(model);
            var service = CreateTransactionService();
            if (service.CreateTransaction(model))

            {
                ViewBag.SaveResult = "Your transaction was created!";
                return RedirectToAction("Index");

            };
            ModelState.AddModelError("", "Transaction couldn't be created.");
            return View(model);
        }
        private TransactionService CreateTransactionService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new TransactionService(userId);
            return service;
        }

        public ActionResult Details(int id)
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new TransactionService(userId);
            var model = service.GetTransactionById(id);
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateTransactionService();
            var detail = service.GetTransactionById(id);
            var model = new TransactionEdit
            {
                BankAccount = detail.BankAccount,
                AccountNumber = detail.AccountNumber,
                TransactionId = detail.TransactionId,
                Amount = detail.Amount,
                StoreName = detail.StoreName,
                Balance = detail.Balance
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit (int id, TransactionEdit model)
        {
            if (!ModelState.IsValid) return View(model);
            if (model.TransactionId != id)
            {
                ModelState.AddModelError("", "Transaction Mismatch");
                return View(model);
            }
            var service = CreateTransactionService();
            if (service.UpdateTransaction(model))
            {
                TempData["SaveResult"] = "Your transaction was updated";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Your transaction couldn't be updated");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var userid = Guid.Parse(User.Identity.GetUserId());
            var service = new TransactionService(userid);
            var model = service.GetTransactionById(id);
            return View(model);
        }

        [HttpPost]
        [ActionName ("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTransaction(int id)
        {
            var service = CreateTransactionService();
            service.DeleteTransaction(id);
            TempData["SaveResult"] = "Your transaction was deleted";
            return RedirectToAction("Index");
        }

    }
}