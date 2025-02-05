
using BankTransactionSolution.Authentication;
using BankTransactionSolution.Domain.Entities;
using BankTransactionSolution.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SomoTaskManagement.Authentication
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public TokenHandler(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        public async Task<(string, DateTime)> CreateAccessToken(User user)
        {
            DateTime expiredAccessToken = DateTime.Now.AddYears(1);
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString(),ClaimValueTypes.String,_configuration["TokenBear:Issuer"]),
                new Claim(JwtRegisteredClaimNames.Iss,_configuration["TokenBear:Issuer"] ,ClaimValueTypes.String,_configuration["TokenBear:Issuer"]),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToString(),ClaimValueTypes.Integer64,_configuration["TokenBear:Issuer"]),
                new Claim(JwtRegisteredClaimNames.Aud, _configuration["TokenBear:Audience"],ClaimValueTypes.String,_configuration["TokenBear:Issuer"]),
                new Claim(JwtRegisteredClaimNames.Exp,expiredAccessToken.ToString("yyyy/MM/dd hh:mm:ss"),ClaimValueTypes.String,_configuration["TokenBear:Issuer"]),
                new Claim(ClaimTypes.Name, user.full_name,ClaimValueTypes.String,_configuration["TokenBear:Issuer"]),
                new Claim(ClaimTypes.Role, "User",ClaimValueTypes.String,_configuration["TokenBear:Issuer"]),
                new Claim("Id", user.id.ToString(),ClaimValueTypes.String,_configuration["TokenBear:Issuer"]),
                new Claim("UserName", user.user_name,ClaimValueTypes.String,_configuration["TokenBear:Issuer"]),
            };
            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["TokenBear:SignatureKey"]));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenInfo = new JwtSecurityToken(
                issuer: _configuration["TokenBear:Issuer"],
                audience: _configuration["TokenBear:Issuer"],
                claims: claims,
                notBefore: DateTime.Now,
                expires: expiredAccessToken,
                credential
            );

            string token = new JwtSecurityTokenHandler().WriteToken(tokenInfo);
            return await Task.FromResult((token, expiredAccessToken));
        }


        public async Task ValidateToken(TokenValidatedContext context)
        {
            var claims = context.Principal.Claims.ToList();
            if (claims.Count == 0)
            {
                context.Fail("This token contains no information");
                return;
            }

            var identity = context.Principal.Identity as ClaimsIdentity;
            if (identity.FindFirst(JwtRegisteredClaimNames.Iss) == null)
            {
                context.Fail("This token is not issued by point entry");
                return;
            }

            if (identity.FindFirst("UserName") != null)
            {
                string userName = identity.FindFirst("UserName").Value;
                var user = await _userService.GetUserWithConditionn(t=>t.user_name == userName);

                if (user == null)
                {
                    context.Fail("This token is invalid for user");
                    return;
                }
            }

            if (identity.FindFirst(JwtRegisteredClaimNames.Exp) == null)
            {
                var dateExp = identity.FindFirst(JwtRegisteredClaimNames.Exp).Value;

                long ticks = long.Parse(dateExp);
                var date = DateTimeOffset.FromUnixTimeSeconds(ticks).DateTime;
                var minutes = date.Subtract(DateTime.Now).TotalMinutes;

                context.Fail("This token is expired");
                return;
            }
        }
    }
}
