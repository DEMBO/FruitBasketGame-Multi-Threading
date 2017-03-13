using FruitBasketGame.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitBasketGame
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var players = ReadPlayers();

                if (players.IsEmpty)
                {
                    return;
                }
                
                var game = new FruitBasketGame(players.Value, new Tuple<int, int>(40, 140));

                var answer = game.GenerateNewValue();

                Console.WriteLine(String.Format("The weight of the basket is: {0}",answer));

                var result = game.Start();

                Console.WriteLine(result);
            }
        }

        static Maybe<IEnumerable<FruitBasketPlayer>> ReadPlayers()
        {
            Console.WriteLine("Steps to start the game:");
            Console.WriteLine("1. Add 2-8 players <unique_player_name> <type> (e.g. TestPlayer1 r)");
            Console.WriteLine("Players' types - r Random player; m Memory; t Thorough; c Cheater; tc Thorough cheater");
            Console.WriteLine("Type 's' to start the game and 'e' to exit");

            var players = new List<FruitBasketPlayer>();

            var startTheGame = false;
            while (!startTheGame)
            {
                var input = Console.ReadLine();
                switch (input)
                {
                    case "s":
                        if (players.Count() < 2)
                        {
                            Console.WriteLine("Number of players should be at least two");
                        }
                        startTheGame = true;
                        break;
                    case "e":
                        return new Maybe<IEnumerable<FruitBasketPlayer>>();
                    default:
                        if (players.Count() == 8)
                        {
                            Console.WriteLine("Too many players");
                        }
                        else
                        {

                            Maybe<FruitBasketPlayer> player = new Maybe<FruitBasketPlayer>();
                            var inputData = input.Split(new char[] { ' ' }).ToArray();
                            if (inputData.Length == 2 && !String.IsNullOrEmpty(inputData[0]))
                            {
                                player = GetPlayerByType(inputData[0], inputData[1]);
                                if (!player.IsEmpty)
                                {
                                    if (players.Any(p => p.Name == player.Value.Name))
                                    {
                                        Console.WriteLine("Player with such name already exists");
                                    }
                                    else
                                    {
                                        players.Add(player.Value);
                                        Console.WriteLine("Player added");
                                    }
                                }
                            }

                            if (player.IsEmpty)
                            {
                                Console.WriteLine("incorrect input");
                            }
                        }
                        break;
                }
            }

            return new Maybe<IEnumerable<FruitBasketPlayer>>(players);
        }

        static Maybe<FruitBasketPlayer> GetPlayerByType(string name, string type)
        {
            Maybe<FruitBasketPlayer> player;

            switch (type)
            {
                case "r":
                    player = new Maybe<FruitBasketPlayer>(new RandomPlayer(name));
                    break;
                case "m":
                    player =  new Maybe<FruitBasketPlayer>(new MemoryPlayer(name));
                    break;
                case "t":
                    player = new Maybe<FruitBasketPlayer>(new ThoroughPlayer(name));
                    break;
                case "c":
                    player = new Maybe<FruitBasketPlayer>(new CheaterPlayer(name));
                    break;
                case "tc":
                    player = new Maybe<FruitBasketPlayer>(new ThoroughCheaterPlayer(name));
                    break;
                default:
                    player = new Maybe<FruitBasketPlayer>();
                    break;
            }

            return player;
        }
    }
}
