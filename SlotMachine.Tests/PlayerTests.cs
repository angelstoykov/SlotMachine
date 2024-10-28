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
            var negativeBet = fixture.CreateDecimalInRange(-1000m, -1m);

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

        //[Test]
        //public void TestPlayerWalletDeposit()
        //{
        //    const decimal initialWalletBalance = 0;
        //    const decimal walletNegativeDeposit = -66.66m;
        //    const decimal walletPositiveDepositTestDeposit = 99.99m;

        //    var wallet = new Wallet();

        //    Assert.That(wallet.Balance, Is.EqualTo(initialWalletBalance));
        //    Assert.Throws<ArgumentException>(() => wallet.Deposit(walletNegativeDeposit));

        //    wallet.Deposit(walletPositiveDepositTestDeposit);

        //    Assert.That(wallet.Balance, Is.EqualTo(walletPositiveDepositTestDeposit));
        //}

        //[Test]
        //public void TestPlayerWalletDepositFromWinningBet()
        //{
        //    // TODO: Refactor, check if it is still accurate
        //    return;
        //    const decimal initialWalletDeposit = 53.33m;
        //    const decimal regularBet = 2.9m;
        //    const decimal profit = 77.77m;

        //    var player = new Player(nameOfThePlayer, wallet);
        //    var expectedBalance = initialWalletDeposit - regularBet + profit;

        //    player.Deposit(initialWalletDeposit);
        //    player.Bet(regularBet);
        //    player.DepositFromWinningBet(profit);

        //    Assert.That(player.Wallet.Balance, Is.EqualTo(expectedBalance));
        //}
    }
}
