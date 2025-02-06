using AutoMapper;
using BankTransactionSolution.Data.Abtract;
using BankTransactionSolution.Domain.Entities;
using BankTransactionSolution.Domain.Model;
using BankTransactionSolution.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BankTransactionSolution.Services.Imp
{
    public class BankAccountService : IBankAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BankAccountService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BankAccountListModel>> GetListBankForUser(string user_name)
        {
            var includes = new Expression<Func<BankAccount, object>>[]
           {
                    t => t.user,
           };
            var bankAccounts = await _unitOfWork.bank_account_repositoty.GetData(expression: t=>t.user.user_name == user_name, includes: includes);

            var result = _mapper.Map<IEnumerable<BankAccountListModel>>(bankAccounts);
            return result;
        }

        public async Task<BankAccountListModel> GetSingleByCondition(Expression<Func<BankAccount, bool>> expression)
        {
            var includes = new Expression<Func<BankAccount, object>>[]
              {
                        t => t.user,
              };

            var bankAccount = await _unitOfWork.bank_account_repositoty.GetSingleByCondition(expression, includes);
            var result = _mapper.Map<BankAccountListModel>(bankAccount);
            return result; 
        }

        public async Task<IEnumerable<BankAccountListModel>> ListBankAccountAsync()
        {
            var includes = new Expression<Func<BankAccount, object>>[]
           {
                    t => t.user,
           };
            var bankAccounts = await _unitOfWork.bank_account_repositoty.GetData(expression:null,includes:includes);

            var result = _mapper.Map<IEnumerable<BankAccountListModel>>(bankAccounts);
            return result;
        }
    }
}
