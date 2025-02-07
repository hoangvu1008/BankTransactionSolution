using BankTransactionSolution.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTransactionSolution.Services.Interface
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionHistoryModel>> ListBankAccountHistoryAsync(int bank_acccount_id);
        Task<bool> UserTranfer(UserTranferModel userTranferModel);
    }
}
