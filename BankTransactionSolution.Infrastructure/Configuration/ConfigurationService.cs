using BankTransactionSolution.Authentication;
using BankTransactionSolution.Data;
using BankTransactionSolution.Data.Abtract;
using BankTransactionSolution.Services.Imp;
using BankTransactionSolution.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SomoTaskManagement.Authentication;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTransactionSolution.Infrastructure.Configuration
{
    public static class ConfigurationService
    {
        public static void RegisterContextDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BankTransactionSolutionContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("MyDB"),
                    options => options.MigrationsAssembly("SomoTaskManagement.Data"));
            });
        }

        public static void RegisterDI(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped<ITokenHandler, TokenHandler>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBankAccountService, BankAccountService>();
        }
    }
}
