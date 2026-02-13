using BankingApplication.Controllers;
using BankingApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankingApplication.Data
{
    public class BankingTransactionalData: IBankingTransactionalData
    {
        private readonly List<BankingAccountModel> _listOfAccountsWithBalance = new List<BankingAccountModel>();

        public BankingTransactionalData()
        {
            _listOfAccountsWithBalance.Add(new BankingAccountModel()
            {
                Id = 111,
                AccountHolderName = "Test1",
                Balance = 10000.00m
            });
            _listOfAccountsWithBalance.Add(new BankingAccountModel()
            {
                Id = 222,
                AccountHolderName = "Test2",
                Balance = 15000.00m

            });
            _listOfAccountsWithBalance.Add(new BankingAccountModel()
            {
                Id = 333,
                AccountHolderName = "Test3",
                Balance = 20000.00m

            });
            _listOfAccountsWithBalance.Add(new BankingAccountModel()
            {
                Id = 444,
                AccountHolderName = "Test4",
                Balance = 25000.00m
            });
        }

        public Task<IEnumerable<BankingAccountModel>> GetAccountDetailsByID(int id)
        {
            var account = _listOfAccountsWithBalance.FirstOrDefault(x => x.Id == id);
            if(account == null)
            {
                return Task.FromResult(Enumerable.Empty<BankingAccountModel>());
            }

            return Task.FromResult(new List<BankingAccountModel> { account }.AsEnumerable());
        }

        public Task<IEnumerable<BankingAccountModel>> GetListOfAllAcounts()
        {
            var accounts = _listOfAccountsWithBalance.AsEnumerable();
            return Task.FromResult(accounts);
        }

        public Task<BankingAccountModel> Deposit(int accountId, decimal amount)
        {
            var account = _listOfAccountsWithBalance.FirstOrDefault(x => x.Id == accountId);
            if (account != null)
            {
                account.Balance += amount;
            }
            return Task.FromResult(account);
        }

        public Task<BankingAccountModel> Withdraw(int accountId, decimal amount)
        {
            var account = _listOfAccountsWithBalance.FirstOrDefault(x => x.Id == accountId);
            if (account != null && account.Balance >= amount)
            {
                account.Balance -= amount;
            }
            if(account != null && account.Balance < amount)
            {
                throw new InvalidOperationException("Insufficient funds for withdrawal.");
            }
            return Task.FromResult(account);
        }

        public Task<IEnumerable<BankingAccountModel>> Transfer(int fromAccountId, int toAccountId, decimal amount)
        {
            var fromAccount = _listOfAccountsWithBalance.FirstOrDefault(x => x.Id == fromAccountId);
            var toAccount = _listOfAccountsWithBalance.FirstOrDefault(x => x.Id == toAccountId);

            if(fromAccount!= null && toAccount!= null && fromAccount.Balance >= amount)
            {
                fromAccount.Balance -= amount;
                toAccount.Balance += amount;
            }
            if(fromAccount != null && fromAccount.Balance < amount)
            {
                throw new InvalidOperationException("Insufficient funds for transfer.");
            }
            return Task.FromResult(new List<BankingAccountModel> { fromAccount, toAccount }.AsEnumerable());
        }
    }

}
