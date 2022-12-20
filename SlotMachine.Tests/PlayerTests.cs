using SlotMachine.Models;
using SlotMachine.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotMachine.Tests
{
    public class PlayerTests
    {
        private const string NAME_OF_THE_PLAYER = "Angel";

        [Test]
        public void TestPlayerConstructor()
        {
            var player = new Player("Angel");

            Assert.That(player.Name, Is.EqualTo(NAME_OF_THE_PLAYER));
            Assert.Throws<ArgumentException>(() => new Player(null));
        }

        [Test]
        public void TestPlayerBet()
        {
            const decimal initialWalletDeposit = 100m;
            const decimal regularBet = 29.30m;
            const decimal negativeBet = -33.28m;
            const decimal moreThanIHaveBet = decimal.MinValue;

            var player = new Player(NAME_OF_THE_PLAYER);

            player.Deposit(initialWalletDeposit);
            player.Bet(regularBet);

            Assert.That(player.Wallet.Balance, Is.EqualTo(initialWalletDeposit - regularBet));
            Assert.Throws<ArgumentException>(() => player.Bet(negativeBet));
            Assert.Throws<ArgumentException>(() => player.Bet(moreThanIHaveBet));
        }

        [Test]
        public void TestPlayerWalletDeposit()
        {
            const decimal initialWalletBalance = 0;
            const decimal walletNegativeDeposit = -66.66m;
            const decimal walletPositiveDepositTestDeposit = 99.99m;

            var wallet = new Wallet();

            Assert.That(wallet.Balance, Is.EqualTo(initialWalletBalance));
            Assert.Throws<ArgumentException>(() => wallet.Deposit(walletNegativeDeposit));

            wallet.Deposit(walletPositiveDepositTestDeposit);

            Assert.That(wallet.Balance, Is.EqualTo(walletPositiveDepositTestDeposit));
        }

        [Test]
        public void TestPlayerWalletDepositFromWinningBet()
        {
            const decimal initialWalletDeposit = 53.33m;
            const decimal regularBet = 2.9m;
            const decimal profit = 77.77m;

            var player = new Player(NAME_OF_THE_PLAYER);
            var expectedBalance = initialWalletDeposit - regularBet + profit;

            player.Deposit(initialWalletDeposit);
            player.Bet(regularBet);
            player.DepositFromWinningBet(profit);

            Assert.That(player.Wallet.Balance, Is.EqualTo(expectedBalance));
        }
    }
}
