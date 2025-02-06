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
using System.Text;
using System.Threading.Tasks;

namespace BankTransactionSolution.Services.Imp
{
    public class TransactionService: ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
                var user = await _unitOfWork.user_repositoty.GetSingleByCondition(t => t.id == userTranferModel.id_user_tranfer);
                if (user == null) return false;

                var bankAccounts = await _unitOfWork.bank_account_repositoty.GetData(t => t.user_id == user.id && t.id == userTranferModel.from_account_id);
                if (!bankAccounts.Any())
                    throw new Exception("Tài khoản nguồn không hợp lệ");

                var bankAccountTranfer = bankAccounts.First();
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

                var transaction = new Transaction(userTranferModel.from_account_id, userTranferModel.to_account_id, userTranferModel.amount, "VND", TransactionStatus.pending);
                var transaction_id = await _unitOfWork.transaction_repositoty.Add(transaction);

                var transaction_logs = new TransactionLogs(transaction_id, TransactionStatus.pending, JsonConvert.SerializeObject(bankAccountTranfer), JsonConvert.SerializeObject(recipientAccount));
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


    }
}

