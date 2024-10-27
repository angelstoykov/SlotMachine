using AutoFixture;
using FluentAssertions;
using SlotMachine.Models.Account;
using SlotMachine.Models.Wallets;
using SlotMachine.Models.Wallets.Contracts;
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
        public void TestPlayerBet()
        {
            //const decimal initialWalletDeposit = 100m;
            var initialWalletDeposit = fixture.Create<decimal>();

            const decimal regularBet = 29.30m;
            const decimal negativeBet = -33.28m;
            const decimal moreThanIHaveBet = decimal.MinValue;

            var player = new Player(nameOfThePlayer, wallet);

            player.Deposit(initialWalletDeposit);
            player.Bet(regularBet);

            player.Wallet.Balance.Should().Be(initialWalletDeposit - regularBet);

            //Assert.Throws<ArgumentException>(() => player.Bet(negativeBet));
            //Assert.Throws<ArgumentException>(() => player.Bet(moreThanIHaveBet));
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
