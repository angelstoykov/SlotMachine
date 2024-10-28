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
    public class PlayerTests
    {
        private string nameOfThePlayer = string.Empty;
        private Fixture fixture;
        private IWallet wallet;

        public PlayerTests()
        {
            this.fixture = new Fixture();
            this.wallet = new Wallet();

            this.nameOfThePlayer = fixture.Create<string>();
        }

        [Fact]
        public void OnPlayerInstantiationNameShouldBeCorrect()
        {
            var player = new Player(nameOfThePlayer, wallet);

            player.Name.Should().Be(nameOfThePlayer);
        }

        [Fact]
        public void OnWrongConstructorArgumentsPlayerShouldThrowError()
        {
            Action act = () => new Player(null, null);

            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void OnValidBetWalletBalanceIsCorrect()
        {
            var initialWalletDeposit = fixture.Create<decimal>();

            // Create bet less or equal to wallet balance
            var regularBet = fixture.CreateDecimalInRange(0, initialWalletDeposit);
            
            var player = new Player(nameOfThePlayer, wallet);

            player.Deposit(initialWalletDeposit);
            player.Bet(regularBet);

            player.Wallet.Balance.Should().Be(initialWalletDeposit - regularBet);
        }

        [Fact]
        public void OnNegativeBetThrowsException()
        {
            var initialWalletDeposit = fixture.Create<decimal>();

            // Create bet less than zero
            var negativeBet = fixture.CreateDecimalInRange(decimal.MinValue, -1m);

            var player = new Player(nameOfThePlayer, wallet);

            player.Deposit(initialWalletDeposit);
            var act = () => player.Bet(negativeBet);

            act.Should()
                .Throw<ArgumentException>()
                .WithMessage(ExceptionMessages.CAN_NOT_BET_NEGATIVE_AMOUNT);
        }

        [Fact]
        public void OnBetBiggerThanWalletBalanceThrowsException()
        {
            var initialWalletDeposit = fixture.Create<decimal>();

            // Create bet more than wallet balance
            var moreThanIHaveBet = fixture.CreateDecimalInRange(initialWalletDeposit + 1, decimal.MaxValue);

            var player = new Player(nameOfThePlayer, wallet);
            player.Deposit(initialWalletDeposit);

            var expectedExceptionMessage = string.Format(ExceptionMessages.UNSUFFICIANT_FUNDS, player.Wallet.Balance);

            var act = () => player.Bet(moreThanIHaveBet);

            act.Should()
                .Throw<ArgumentException>()
                .WithMessage(expectedExceptionMessage);
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
    }
}
