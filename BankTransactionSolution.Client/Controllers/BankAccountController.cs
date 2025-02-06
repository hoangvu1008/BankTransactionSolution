using BankTransactionSolution.Domain.Entities;
using BankTransactionSolution.Domain.Model;
using BankTransactionSolution.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankTransactionSolution.Client.Controllers
{
    public class BankAccountController : Controller
    {
        private readonly IBankAccountService _bankAccountService;

        public BankAccountController(IBankAccountService bankAccountService)
        {
            _bankAccountService = bankAccountService;
        }

        public async Task<IActionResult> GetBankAccount()
        {
            var bankAccountList = await _bankAccountService.ListBankAccountAsync();


            return Ok(bankAccountList);
        }

        public async Task<IActionResult> Index()
        {
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return View("Index");
            }
            return View();
        }

        public async Task<BankAccountListModel> GetBankAccountDetail(int id)
        {
            var bankAccount = await _bankAccountService.GetSingleByCondition(t => t.id == id);
            return bankAccount;
        }

        public async Task<IEnumerable<BankAccountListModel>> GetListBankForUser()
        {
            var userName = Request.Cookies["user_name"];

            if (string.IsNullOrEmpty(userName))
            {
               throw new Exception("user_name invalid");
            }
            var bankAccount = await _bankAccountService.GetListBankForUser(userName);
            return bankAccount;
        }
    }
}
