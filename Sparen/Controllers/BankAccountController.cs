using Microsoft.AspNet.Identity;
using Sparen.Data;
using Sparen.Models;
using Sparen.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Sparen.Controllers
{
    public class BankAccountController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();
        // GET: BankAccount
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new BankAccountService(userId);
            var model = service.GetBankAccounts();
            return View(model);
        }

        //GET: for CREATE BankAccount
        public ActionResult Create()
        {
            return View();
        }
        //POST: for CREATE BankAccount
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BankAccountCreate model)
        {
            if (!ModelState.IsValid) return View(model);
            var service = CreateBankAccountService();
            if (service.CreateBankAccount(model))
            {
                ViewBag.SaveResult = "Your account was created!";
                return RedirectToAction("Index");
            };
            ModelState.AddModelError("", "Bank Account couldn't be created.");
            return View(model);
        }
        private BankAccountService CreateBankAccountService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new BankAccountService(userId);
            return service;
        }

        /////COME BACK TO THIS, NEED TO FIND A WAY TO SEE DETAILS

        //GET DETAILS
        public ActionResult Details(int? id)
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new BankAccountService(userId);
            var model = service.GetDetails(id);
            return View(model);
        }

        //GET EDIT
        public ActionResult Edit(int id)
        {
            var service = CreateBankAccountService();
            var detail = service.GetBankAccountById(id);
            var model = new BankAccountEdit
            {
                AccountNumber = detail.AccountNumber,
                NameGiven = detail.NameGiven,
                AccountLevel = detail.AccountLevel,
            };
            return View(model);
        }
        //GET POST EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BankAccountEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.AccountNumber != id)
            {
                ModelState.AddModelError("", "Account Number Mismatch");
                return View(model);
            }
            var service = CreateBankAccountService();
            if (service.UpdateBankAccount(model))
            {
                TempData["SaveResult"] = "Your bank account information was updated";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Your bank information could not be updated.");
            return View(model);
        }

        //GET DELETE
        [ActionName("Delete")]
        public ActionResult Delete(int? id)
        {

            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new BankAccountService(userId);
            var model = service.GetDetails(id);
            return View(model);
        }

        //GET POST 
        [HttpPost]
        [ActionName ("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAccount(int id)
        {
            var service = CreateBankAccountService();
            service.DeleteBankAccount(id);
            TempData["SaveResult"] = "Your bank account was deleted";
            return RedirectToAction("Index");
        }

    }
}