using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SlotMachine.Core;
using SlotMachine.Core.Contracts;
using SlotMachine.IO;
using SlotMachine.IO.Contracts;
using SlotMachine.Models.Account;
using SlotMachine.Models.Players.Contracts;
using SlotMachine.Models.Wallets;
using SlotMachine.Models.Wallets.Contracts;
using SlotMachine.Services.Contracts;
using SlotMachine.Services.Settlement;

namespace SlotMachine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Game game = new Game();
            //game.Play();

            IHost host = Host.CreateDefaultBuilder().ConfigureServices(services =>
                {
                    services.AddSingleton<IGame, Game>();
                    services.AddSingleton<IReader, Reader>();
                    services.AddSingleton<IWriter, Writer>();
                    services.AddSingleton<ISpinGenerator, SpinGenerator>();
                    services.AddSingleton<IWallet, Wallet>();
                    services.AddSingleton<ISettlement, Settlement>();
                    services.AddSingleton<IPrizeGenerator, PrizeGenerator>();
                    services.AddSingleton<IPlayer, Player>(serviceProvider =>
                        new Player("Angel", serviceProvider.GetRequiredService<IWallet>()));
                })
            .Build();

            var app = host.Services.GetRequiredService<IGame>();
            app.Play();
        }
    }
}