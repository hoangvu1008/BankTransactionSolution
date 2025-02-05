using AutoMapper;
using BankTransactionSolution.Data.Abtract;
using BankTransactionSolution.Domain.Entities;
using BankTransactionSolution.Domain.Model;
using BankTransactionSolution.Services.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;

namespace BankTransactionSolution.Services.Imp
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<User> GetById(int id)
        {
            var includes = new Expression<Func<User, object>>[]
            {
                    t => t.bank_accounts,
            };

            var user = await _unitOfWork.user_repositoty
                                        .GetData(expression: t => t.id == id, includes: includes);

            var json = System.Text.Json.JsonSerializer.Serialize(user.First(), new JsonSerializerOptions { WriteIndented = true });

            Console.WriteLine(json);
            return user.First();
        }

        public async Task<User> GetUserWithConditionn(Expression<Func<User, bool>> expression)
        {
            var includes = new Expression<Func<User, object>>[]
            {
                    t => t.bank_accounts,
            };

            var user = await _unitOfWork.user_repositoty
                                        .GetData(expression: expression, includes: includes);

            var json = System.Text.Json.JsonSerializer.Serialize(user.First(), new JsonSerializerOptions { WriteIndented = true });

            Console.WriteLine(json);
            return user.First();
        }
    }
}
