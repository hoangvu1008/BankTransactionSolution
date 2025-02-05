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
    public interface IUserService
    {
        Task<User> GetById(int id);
        Task<User> GetUserWithConditionn(Expression<Func<User, bool>> expression);
    }
}
