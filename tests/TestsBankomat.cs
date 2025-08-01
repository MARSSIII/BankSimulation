using Abstractions.Repositories;
using Contracts.ApplicationContexts;
using Contracts.ResultType;
using Lab5.Application.Services;
using Models.Accounts;
using Moq;
using Xunit;

namespace Lab5.Tests;

public class TestsBankomat
{
    [Theory]
    [InlineData(9900, 17000, 7100, true)]
    [InlineData(17000, 9900, 0, false)]
    public void Test_MoneyWithdraw(int withdrawalAmount, int entryBalance, int balanceAfterOperation, bool isOperationSuccesed)
    {
        var mockAccountRepository = new Mock<IAccountRepository>();
        var mockContext = new Mock<IApplicationContext>();
        var withdrawalsAccountService = new WithdrawalAccountService(mockAccountRepository.Object);

        var account = new Account(123, 123, entryBalance, 12);
        WithdrawResult result = withdrawalsAccountService.WithdrawMoney(account, withdrawalAmount);

        if (isOperationSuccesed)
        {
            Assert.IsType<WithdrawResult.Success>(result);
            mockAccountRepository.Verify(
                repo => repo.UpdateAmount(
                    It.Is<Account>(acc => acc == account),
                    It.Is<int>(amount => amount == (withdrawalAmount * -1))),
                Times.Once);
        }
        else
        {
            Assert.IsType<WithdrawResult.NotEnoghtMoney>(result);
            mockAccountRepository.Verify(
                repo => repo.UpdateAmount(
                    It.Is<Account>(acc => acc == account),
                    It.Is<int>(amount => amount == balanceAfterOperation)),
                Times.Never);
        }
    }

    [Fact]
    public void Test_RefillBalance()
    {
        var mockAccountRepository = new Mock<IAccountRepository>();
        var mockContext = new Mock<IApplicationContext>();
        var refillAccountService = new RefillAccountService(mockAccountRepository.Object);

        var account = new Account(123, 123, 1000, 12);
        refillAccountService.Refill(account, 1000);

        mockAccountRepository.Verify(
                repo => repo.UpdateAmount(
                    It.Is<Account>(acc => acc == account),
                    It.Is<int>(amount => amount == 1000)),
                Times.Once);
    }
}
