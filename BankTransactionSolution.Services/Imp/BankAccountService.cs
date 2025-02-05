using BankTransactionSolution.Data.Abtract;
using BankTransactionSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BankTransactionSolution.Services.Imp
{
    public class BankAccountService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BankAccountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable< BankAccount>> GetBankAccountAsync(int id_user)
        {
            var bankAccounts = await _unitOfWork.bank_account_repositoty.GetData(expression: t => t.user_id == id_user);
            return bankAccounts;
        }
    }
}
