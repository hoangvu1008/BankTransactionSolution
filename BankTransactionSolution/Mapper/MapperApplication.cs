using AutoMapper;
using BankTransactionSolution.Domain.Entities;
using BankTransactionSolution.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTransactionSolution.Data.Mapper
{
    public class MapperApplication : Profile
    {
        public MapperApplication()
        {
            CreateMap<User, UserModel>()
                .ForMember(dest => dest.bank_accounts, opt => opt.MapFrom(src => src.bank_accounts != null));
        }
    }
}
