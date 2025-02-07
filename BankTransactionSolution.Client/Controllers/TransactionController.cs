using BankTransactionSolution.Domain.Model;
using BankTransactionSolution.Services.Imp;
using BankTransactionSolution.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BankTransactionSolution.Client.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }


        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] UserTranferModel request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest("Dữ liệu không hợp lệ.");
                }

                var result = await _transactionService.UserTranfer(request);

                return Ok(new { success = result });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "Lỗi hệ thống.");
            }
        }

        [HttpGet]
        public async Task<IEnumerable<TransactionHistoryModel>> GetTransactionHistories([FromQuery] int bank_account_id)
        {
            var result = await _transactionService.ListBankAccountHistoryAsync(bank_account_id);
            return result;
        }


        public IActionResult TransactionHistory()
        {
            return View();
        }
    }
}
