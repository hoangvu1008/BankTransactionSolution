using BankTransactionSolution.Domain.Entities;
using BankTransactionSolution.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BankTransactionSolution.Services.Interface
{
    public interface IBankAccountService
    {
        Task<IEnumerable<BankAccountListModel>> GetListBankForUser(string user_name);
        Task<BankAccountListModel> GetSingleByCondition(Expression<Func<BankAccount, bool>> expression);
        Task<IEnumerable<BankAccountListModel>> ListBankAccountAsync(string user_name);
    }
}
