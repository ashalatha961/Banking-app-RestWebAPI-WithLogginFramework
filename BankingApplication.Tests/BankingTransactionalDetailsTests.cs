using BankingApplication.Data;
using BankingApplication.Models;
using BankingApplication.Controllers;
using NSubstitute;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NSubstitute.ExceptionExtensions;
using Microsoft.Extensions.Logging;
{
    
}

namespace BankingApplication.Tests
{
    public class BankingTransactionalDetailsTests
    {
        private readonly IBankingTransactionalData bankingTransactionalData;
        private readonly BankingAppController bankingAppController;
        
        public BankingTransactionalDetailsTests()
        {
            bankingTransactionalData = Substitute.For<IBankingTransactionalData>();
            var logger = Substitute.For<ILogger<BankingAppController>>();
            bankingAppController = new BankingAppController(logger, bankingTransactionalData);
        }

        [Fact]
        public async Task GetAccountDetailsById_ValidId_ReturnsOkResult()
        {
            // Arrange
            int accountId = 111;
            var account = new BankingAccountModel { Id = accountId, AccountHolderName = "Test1", Balance = 10000.00m };
            bankingTransactionalData.GetAccountDetailsByID(accountId).Returns(new List<BankingAccountModel> { account });
            // Act
            var result = await bankingAppController.GetAccountDetailsById(accountId);
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<BankingAccountModel>>(okResult.Value);
            Assert.Single(returnValue);
            Assert.Equal(accountId, returnValue[0].Id);
        }

        [Fact]
        public async Task GetAccountDetailsById_InvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            int accountId = 999; // Assuming this ID does not exist
            bankingTransactionalData.GetAccountDetailsByID(accountId).Returns(Enumerable.Empty<BankingAccountModel>());
            // Act
            var result = await bankingAppController.GetAccountDetailsById(accountId);
            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal($"Account with ID {accountId} not found.", notFoundResult.Value);
        }

        [Fact]
        public async Task GetAccountDetailsById_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            int accountId = 111;
            bankingTransactionalData.GetAccountDetailsByID(accountId).Throws(new Exception("Database error"));
            // Act
            var result = await bankingAppController.GetAccountDetailsById(accountId);
            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal("Internal server error", statusCodeResult.Value);
        }

        [Fact]
        public async Task GetAllAccounts_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var accounts = new List<BankingAccountModel>
            {
                new BankingAccountModel { Id = 111, AccountHolderName = "Test1", Balance = 10000.00m },
                new BankingAccountModel { Id = 222, AccountHolderName = "Test2", Balance = 15000.00m }
            };
            bankingTransactionalData.GetListOfAllAcounts().Returns(accounts);
            // Act
            var result = await bankingAppController.GetAllAccounts();
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<BankingAccountModel>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetAllAccounts_NoAccountsFound_ReturnsNotFoundResult()
        {
            // Arrange
            bankingTransactionalData.GetListOfAllAcounts().Returns(Enumerable.Empty<BankingAccountModel>());
            // Act
            var result = await bankingAppController.GetAllAccounts();
            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("No accounts found.", notFoundResult.Value);
        }
        [Fact]
        public async Task GetAllAccounts_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            bankingTransactionalData.GetListOfAllAcounts().Throws(new Exception("Database error"));
            // Act
            var result = await bankingAppController.GetAllAccounts();
            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal("Internal server error", statusCodeResult.Value);
        }

        [Fact]
        public async Task UpdatedAccountAfterDeposit_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            int accountId = 111;
            decimal depositAmount = 5000.00m;
            var updatedAccount = new BankingAccountModel { Id = accountId, AccountHolderName = "Test1", Balance = 15000.00m };
            bankingTransactionalData.Deposit(accountId, depositAmount).Returns(updatedAccount);
            // Act
            var result = await bankingAppController.UpdatedAccountAfterDeposit(accountId, depositAmount);
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<BankingAccountModel>(okResult.Value);
            Assert.Equal(accountId, returnValue.Id);
            Assert.Equal(15000.00m, returnValue.Balance);
        }

        [Fact]
        public async Task UpdatedAccountAfterDeposit_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            int accountId = 111;
            decimal depositAmount = 5000.00m;
            bankingTransactionalData.Deposit(accountId, depositAmount).Throws(new Exception("Database error"));
            // Act
            var result = await bankingAppController.UpdatedAccountAfterDeposit(accountId, depositAmount);
            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal("Internal server error", statusCodeResult.Value);
        }

        [Fact]
        public async Task UpdatedAccountAfterWithdraw_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            int accountId = 111;
            decimal withdrawAmount = 3000.00m;
            var updatedAccount = new BankingAccountModel { Id = accountId, AccountHolderName = "Test1", Balance = 7000.00m };
            bankingTransactionalData.Withdraw(accountId, withdrawAmount).Returns(updatedAccount);
            // Act
            var result = await bankingAppController.UpdatedAccountAfterWithdraw(accountId, withdrawAmount);
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<BankingAccountModel>(okResult.Value);
            Assert.Equal(accountId, returnValue.Id);
            Assert.Equal(7000.00m, returnValue.Balance);
        }



    }
}