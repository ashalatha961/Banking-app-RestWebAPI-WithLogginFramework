using BankingApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BankingApplication.Data
{
  public interface IBankingTransactionalData
    {
        public Task<IEnumerable<BankingAccountModel>> GetAccountDetailsByID(int accountId);

        public Task<IEnumerable<BankingAccountModel>> GetListOfAllAcounts(); 

        public Task<BankingAccountModel> Deposit(int accountId, decimal amount);
        
        public Task<BankingAccountModel> Withdraw(int accountId, decimal amount);
        public Task<IEnumerable<BankingAccountModel>> Transfer(int fromAccountId, int toAccountId, decimal amount);

    }

}
