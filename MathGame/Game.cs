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
        private int[] Numbers { get; set; }
        private List<string> GameHistory { get; set; }
        private readonly Random Random = new Random();

        public void PlayGame()
        {
            bool isGameRunning = true;
            GameHistory = new List<string>();

            while (isGameRunning)
            {
                Console.WriteLine("Welcome to the math game, choose which type would you like to play:");
                Console.WriteLine("A - addition game\nS - substraction game\nM - multiplication game\nD - division game\nV - view game history\nQ - quit");

                string gameSelected = Console.ReadLine().Trim().ToUpper();

                switch (gameSelected)
                {
                    case "A":
                        AdditionGame("You chose addition\n");
                        break;
                    case "S":
                        SubstractionGame("You chose substraction\n");
                        break;
                    case "M":
                        MultiplicationGame("You chose multiplication\n");
                        break;
                    case "D":
                        DivisionGame("You chose division\n");
                        break;
                    case "V":
                        ViewHistory();
                        break;
                    case "Q":
                        Console.WriteLine("Quitting game...\n");
                        isGameRunning = false;
                        break;
                };

            }

        }

        private void AdditionGame(string message)
        {
            int score = 0;
            Console.WriteLine(message);
            for (int i = 0; i < 6; i++)
            {
                GetTwoNumbersUpTo(100);
                Console.WriteLine("What's the result of this operation?");
                Console.WriteLine($"{Numbers[0]} + {Numbers[1]}\n");
                string sum = Console.ReadLine();
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
            AddToHistory("Addition", score);
            Console.WriteLine($"Game over, your score is {score}\n");
            StartNewGame();
        }

        private void SubstractionGame(string message)
        {
            int score = 0;
            Console.WriteLine(message);
            for (int i = 0; i < 6; i++)
            {
                GetTwoNumbersUpTo(100);
                Console.WriteLine("What's the result of this operation?");
                Console.WriteLine($"{Numbers[0]} - {Numbers[1]}\n");
                string difference = Console.ReadLine();
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
            AddToHistory("Substraction", score);
            Console.WriteLine($"Game over, your score is {score}\n");
            StartNewGame();
        }

        private void MultiplicationGame(string message)
        {
            int score = 0;
            Console.WriteLine(message);

            for (int i = 0; i < 6; i++)
            {
                GetTwoNumbersUpTo(40);
                Console.WriteLine("What's the result of this operation?");
                Console.WriteLine($"{Numbers[0]} * {Numbers[1]}\n");
                string product = Console.ReadLine();
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
            AddToHistory("Multiplication", score);
            Console.WriteLine($"Game over, your score is {score}\n");
            StartNewGame();
        }

        private void DivisionGame(string message)
        {
            int score = 0;
            Console.WriteLine(message);

            for (int i = 0; i < 6; i++)
            {
                GetTwoNumbersUpTo(40);
                while (Numbers[0] % Numbers[1] != 0)
                {
                    GetTwoNumbersUpTo(40);
                }

                Console.WriteLine("What's the result of this operation?");
                Console.WriteLine($"{Numbers[0]} / {Numbers[1]}\n");
                string quotient = Console.ReadLine();
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
            AddToHistory("Division", score);
            Console.WriteLine($"Game over, your score is {score}\n");
            StartNewGame();
        }

        private void ViewHistory()
        {
            foreach (string item in GameHistory)
            {
                Console.WriteLine(item);
                Console.WriteLine();
            }
        }

        private int[] GetTwoNumbersUpTo(int upperLimit)
        {
            Numbers = new int[2];
            Numbers[0] = Random.Next(1, upperLimit);
            Numbers[1] = Random.Next(1, upperLimit);

            return Numbers;
        }

        private void StartNewGame()
        {
            Console.WriteLine("Press any key to start a new game.");
            Console.ReadKey();
            Console.Clear();
        }
        private void AddToHistory(string gameType, int score)
        {            
            GameHistory.Add($"{DateTime.Now} - {gameType}: {score} points");
        }
    }
}
