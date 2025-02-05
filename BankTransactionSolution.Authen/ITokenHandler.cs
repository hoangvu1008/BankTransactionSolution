

using BankTransactionSolution.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace BankTransactionSolution.Authentication
{
    public interface ITokenHandler
    {
        Task<(string, DateTime)> CreateAccessToken(User user);
        Task ValidateToken(TokenValidatedContext context);
    }
}