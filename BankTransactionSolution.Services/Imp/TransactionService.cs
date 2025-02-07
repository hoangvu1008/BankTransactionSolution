using AutoMapper;
using BankTransactionSolution.Data;
using BankTransactionSolution.Data.Abtract;
using BankTransactionSolution.Domain.Entities;
using BankTransactionSolution.Domain.Enum;
using BankTransactionSolution.Domain.Model;
using BankTransactionSolution.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BankTransactionSolution.Services.Imp
{
    public class TransactionService: ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TransactionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> UserTranfer(UserTranferModel userTranferModel)
        {
            if (userTranferModel.amount <= 0)
                throw new Exception("Số tiền chuyển phải lớn hơn 0");

            if (userTranferModel.from_account_id == userTranferModel.to_account_id)
                throw new Exception("Không thể chuyển tiền vào cùng một tài khoản");

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var bankAccounts = await _unitOfWork.bank_account_repositoty.GetSingleByCondition(t =>t.id == userTranferModel.from_account_id);
                if (bankAccounts == null)
                    throw new Exception("Tài khoản nguồn không hợp lệ");

                var bankAccountTranfer = bankAccounts;
                if (bankAccountTranfer.balance < userTranferModel.amount)
                    throw new Exception("Tài khoản không đủ số dư để thực hiện giao dịch");

                var recipientAccount = await _unitOfWork.bank_account_repositoty.GetSingleByCondition(t => t.id == userTranferModel.to_account_id);
                if (recipientAccount == null)
                    throw new Exception("Không tìm thấy tài khoản người nhận");

                if (bankAccountTranfer.currency != recipientAccount.currency)
                    throw new Exception("Không thể chuyển tiền giữa các tài khoản có đơn vị tiền tệ khác nhau");

                bankAccountTranfer.balance -= userTranferModel.amount;
                _unitOfWork.bank_account_repositoty.Update(bankAccountTranfer);

                recipientAccount.balance += userTranferModel.amount;
                _unitOfWork.bank_account_repositoty.Update(recipientAccount);

                var transaction = new Transaction(userTranferModel.from_account_id, userTranferModel.to_account_id, userTranferModel.amount, "VND", TransactionStatus.pending, userTranferModel.description);
                var transaction_id = await _unitOfWork.transaction_repositoty.Add(transaction);
                
                var settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };
                var transaction_logs = new TransactionLogs(transaction_id, TransactionStatus.pending, JsonConvert.SerializeObject(bankAccountTranfer, settings), JsonConvert.SerializeObject(recipientAccount, settings));
                await _unitOfWork.transaction_logs_repositoty.Add(transaction_logs);

                _unitOfWork.CommitTransaction();

                transaction.status = TransactionStatus.success;
                _unitOfWork.transaction_repositoty.Update(transaction);

            }
            catch (DbUpdateException dbEx)
            {
                _unitOfWork.RollbackTransaction();
                throw new Exception("Lỗi cơ sở dữ liệu trong quá trình chuyển tiền", dbEx);
            }
            catch (TimeoutException timeoutEx)
            {
                _unitOfWork.RollbackTransaction();
                throw new Exception("Quá trình chuyển tiền bị timeout", timeoutEx);
            } 
            catch (Exception ex)
            {
                _unitOfWork.RollbackTransaction();
                throw new Exception("Đã xảy ra lỗi trong quá trình chuyển tiền", ex);
            }

            return true;
        }

        public async Task<IEnumerable<TransactionHistoryModel>> ListBankAccountHistoryAsync(int bank_acccount_id)
        {
            var includes = new Expression<Func<Transaction, object>>[]
           {
                    t => t.from_account,
                    t => t.to_account,
           };
            var transactions = await _unitOfWork.transaction_repositoty.GetData(expression: t => t.from_account.id == bank_acccount_id || t.to_account.id == bank_acccount_id, includes: includes);

            var result = _mapper.Map<IEnumerable<TransactionHistoryModel>>(transactions);
            return result;
        }
    }
}

