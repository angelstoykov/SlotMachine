using AutoFixture;
using FluentAssertions;
using SlotMachine.Common.Messages;
using SlotMachine.Models.Account;
using SlotMachine.Models.Wallets;
using SlotMachine.Models.Wallets.Contracts;
using SlotMachine.Tests.Extensions;
using System;
using Xunit;

namespace SlotMachine.Tests
{
    public class WalletTests
    {
        private string nameOfThePlayer = string.Empty;
        private Fixture fixture;
        private IWallet wallet;

        public WalletTests()
        {
            this.fixture = new Fixture();
            this.wallet = new Wallet();

            this.nameOfThePlayer = fixture.Create<string>();
        }

        [Fact]
        public void OnWalletInitializationBalanceIsZero()
        {
            var initialWalletBalance = 0m;

            var wallet = new Wallet();

            wallet.Balance.Should().Be(initialWalletBalance);
        }

        [Fact]
        public void OnWalletDepositBalanceIsCorrect()
        {
            var depositAmount = fixture.CreateDecimalInRange(1m, decimal.MaxValue);

            var wallet = new Wallet();

            wallet.Deposit(depositAmount);

            wallet.Balance.Should().Be(depositAmount);
        }

        [Fact]
        public void OnWalletDepositNegativeAmountThrowsException()
        {
            var wallet = new Wallet();
            var negativeAmount = fixture.CreateDecimalInRange(decimal.MinValue, -1m);

            var act = () => wallet.Deposit(negativeAmount);

            act.Should()
                .Throw<ArgumentException>()
                .WithMessage(ExceptionMessages.CAN_NOT_ADD_ZERO_OR_NEGATIVE_DEPOSIT);
        }

        [Fact]
        public void PlayerWalletDepositFromWinningBetBalanceIsCorrect()
        {
            var initialWalletDeposit = fixture.CreateDecimalInRange(1m, decimal.MaxValue);
            var regularBet = fixture.CreateDecimalInRange(1m, initialWalletDeposit);
            var profit = fixture.CreateDecimalInRange(1m, decimal.MaxValue);

            var player = new Player(nameOfThePlayer, wallet);
            var expectedBalance = initialWalletDeposit - regularBet + profit;

            player.Deposit(initialWalletDeposit);
            player.Bet(regularBet);
            player.DepositFromWinningBet(profit);

            player.Wallet.Balance.Should().Be(expectedBalance);
        }


        [Fact]
        public void OnWalletWithdrawBetBalanceIsCorrect()
        {
            var initialWalletDeposit = fixture.CreateDecimalInRange(1m, decimal.MaxValue);
            var regularBet = fixture.CreateDecimalInRange(1m, initialWalletDeposit);
            var expectedBalance = initialWalletDeposit - regularBet;

            wallet.Deposit(initialWalletDeposit);
            wallet.WithdrawBet(regularBet);
            var checkoutReturnValue = wallet.Checkout();
            

            wallet.Balance.Should().Be(expectedBalance);
            wallet.Balance.Should().Be(checkoutReturnValue);
        }
    }
}
