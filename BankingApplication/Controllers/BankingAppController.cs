using BankingApplication.Data;
using Microsoft.AspNetCore.Mvc;

namespace BankingApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class BankingAppController : ControllerBase
    {
        private readonly ILogger<BankingAppController> _logger;
        private readonly IBankingTransactionalData _bankingTransactionalData;

        public BankingAppController(ILogger<BankingAppController> logger, IBankingTransactionalData bankingTransactionalData)
        {
            _logger = logger;
            _bankingTransactionalData = bankingTransactionalData;
        }

        [HttpGet("GetAccountDetailsByID", Name = "GetAccountDetailsById")]
        public async Task<IActionResult> GetAccountDetailsById(int id)
        {
            try
            {
                var account = await _bankingTransactionalData.GetAccountDetailsByID(id);
                if (account == null)
                {
                    return NotFound($"Account with ID {id} not found.");
                }
                return Ok(account);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching account details for ID {AccountId}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetAllAccounts", Name ="GetAllAccounts")]
        public async Task<IActionResult> GetAllAccounts()
        {
            try
            {
                var accounts = await _bankingTransactionalData.GetListOfAllAcounts();
                if(accounts == null)
                {
                    return NotFound("No accounts found.");
                }
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all account details");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("Deposit", Name = "UpdatedAccountAfterDeposit")]
        public async Task<IActionResult> UpdatedAccountAfterDeposit(int accountId, decimal amount)
        {
            try
            {
                var updatedAccount = await _bankingTransactionalData.Deposit(accountId, amount);
                if (updatedAccount == null)
                {
                    return NotFound($"Account with ID {accountId} not found.");
                }
                return Ok(updatedAccount);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while depositing to account ID {AccountId}", accountId);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("Withdraw", Name = "UpdatedAccountAfterWithdraw")]
        public async Task<IActionResult> UpdatedAccountAfterWithdraw(int accountId, decimal amount)
        {
            try
            {
                var updatedAccount = await _bankingTransactionalData.Withdraw(accountId, amount);
                if (updatedAccount == null)
                {
                    return NotFound($"Account with ID {accountId} not found.");
                }
                return Ok(updatedAccount);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while withdrawing from account ID {AccountId}", accountId);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("Transfer", Name = "UpdatedAccountAfterTransfer")]
        public async Task<IActionResult> UpdatedAccountAfterTransfer(int fromAccountid, int toAccountId, decimal amount)
        {
            try
            {
                var updatedAccounts = await _bankingTransactionalData.Transfer(fromAccountid, toAccountId, amount);
                if(updatedAccounts == null)
                {
                    return NotFound("One or both accounts not found.");
                }
                return Ok(updatedAccounts);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error occurred while transferring from " +
                    "account ID {FromAccountId} to account ID {ToAccountId}", fromAccountid, toAccountId);

            return StatusCode(500, "Internal server error");
            }
        }
    }
}
