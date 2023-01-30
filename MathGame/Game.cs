using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using static System.Formats.Asn1.AsnWriter;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MathGame
{
    public class Game
    {
        public int[] Numbers { get; set; } = new int[2];
        public List<string> GameHistory;

        public Game(int[] numbers, List<string> gameHistory)
        {
            Numbers = numbers;
            GameHistory = gameHistory;
        }
        public void PlayGame(List<string> gameHistory)
        {
            bool isGameRunning = true;

            while (isGameRunning)
            {
                Console.WriteLine("Welcome to the math game, choose which type would you like to play:");
                Console.WriteLine("A - addition game\nS - substraction game\nM - multiplication game\nD - division game\nV - view game history\nQ - quit");

                var gameSelected = Console.ReadLine().Trim().ToUpper();

                switch (gameSelected)
                {
                    case "A":
                        AdditionGame("You chose addition\n", gameHistory);
                        break;
                    case "S":
                        SubstractionGame("You chose substraction\n", gameHistory);
                        break;
                    case "M":
                        MultiplicationGame("You chose multiplication\n", gameHistory);
                        break;
                    case "D":
                        DivisionGame("You chose division\n", gameHistory);
                        break;
                    case "V":
                        ViewHistory(gameHistory);
                        break;
                    case "Q":
                        Console.WriteLine("Quitting game...\n");
                        Environment.Exit(0);
                        break;
                };

            }

        }



        public void AdditionGame(string message, List<string> gameHistory)
        {
            var score = 0;
            Console.WriteLine(message);
            for (int i = 0; i < 6; i++)
            {
                GetTwoNumbersBelowHundred();
                Console.WriteLine("What's the result of this operation?");
                Console.WriteLine($"{Numbers[0]} + {Numbers[1]}\n");
                var sum = Console.ReadLine();
                if (int.Parse(sum) == Numbers[0] + Numbers[1])
                {
                    Console.WriteLine("Your answer is correct.");
                    score++;
                }
                else
                {
                    Console.WriteLine($"Wrong answer. The correct answer was {Numbers[0] + Numbers[1]}");
                }

            }
            AddToHistory(gameHistory, "Addition", score);
            Console.WriteLine($"Game over, your score is {score}\n");
            StartNewGame();
        }
        public void SubstractionGame(string message, List<string> gameHistory)
        {
            var score = 0;
            Console.WriteLine(message);
            for (int i = 0; i < 6; i++)
            {
                GetTwoNumbersBelowHundred();
                Console.WriteLine("What's the result of this operation?");
                Console.WriteLine($"{Numbers[0]} - {Numbers[1]}\n");
                var difference = Console.ReadLine();
                if (int.Parse(difference) == Numbers[0] - Numbers[1])
                {
                    Console.WriteLine("Your answer is correct.");
                    score++;
                }
                else
                {
                    Console.WriteLine($"Wrong answer. The correct answer was {Numbers[0] - Numbers[1]}");
                }

            }
            AddToHistory(gameHistory, "Substraction", score);
            Console.WriteLine($"Game over, your score is {score}\n");
            StartNewGame();


        }
        public void MultiplicationGame(string message, List<string> gameHistory)
        {
            var score = 0;
            Console.WriteLine(message);

            for (int i = 0; i < 6; i++)
            {
                GetTwoNumbersBelowForty();
                Console.WriteLine("What's the result of this operation?");
                Console.WriteLine($"{Numbers[0]} * {Numbers[1]}\n");
                var product = Console.ReadLine();
                if (int.Parse(product) == Numbers[0] * Numbers[1])
                {
                    Console.WriteLine("Your answer is correct.");
                    score++;
                }
                else
                {
                    Console.WriteLine($"Wrong answer. The correct answer was {Numbers[0] * Numbers[1]}");
                }

            }
            AddToHistory(gameHistory, "Multiplication", score);
            Console.WriteLine($"Game over, your score is {score}\n");
            StartNewGame();
        }
        public void DivisionGame(string message, List<string> gameHistory)
        {
            var score = 0;
            Console.WriteLine(message);

            for (int i = 0; i < 6; i++)
            {
                GetTwoNumbersBelowForty();
                while (Numbers[0] % Numbers[1] != 0)
                {
                    GetTwoNumbersBelowForty();
                }

                Console.WriteLine("What's the result of this operation?");
                Console.WriteLine($"{Numbers[0]} / {Numbers[1]}\n");
                var quotient = Console.ReadLine();
                if (int.Parse(quotient) == Numbers[0] / Numbers[1])
                {
                    Console.WriteLine("Your answer is correct.");
                    score++;
                }
                else
                {
                    Console.WriteLine($"Wrong answer. The correct answer was {Numbers[0] / Numbers[1]}");
                }

            }
            AddToHistory(gameHistory, "Division", score);
            Console.WriteLine($"Game over, your score is {score}\n");
            StartNewGame();
        }
        public void ViewHistory(List<string> gameHistory)
        {
            foreach (string item in gameHistory)
            {
                Console.WriteLine(item);
            }
        }
        public int[] GetTwoNumbersBelowHundred()
        {
            var random = new Random();
            Numbers[0] = random.Next(0, 100);
            Numbers[1] = random.Next(0, 100);

            return Numbers;
        }
        public int[] GetTwoNumbersBelowForty()
        {
            var random = new Random();
            Numbers[0] = random.Next(1, 40);
            Numbers[1] = random.Next(1, 40);

            return Numbers;
        }

        public void StartNewGame()
        {
            Console.WriteLine("Press any key to start a new game.");
            Console.ReadKey();
            Console.Clear();

        }
        public void AddToHistory(List<string> gameHistory, string gameType, int score)
        {
            gameHistory.Add($"{DateTime.Now} - {gameType}: {score} points");
        }

    }
}
