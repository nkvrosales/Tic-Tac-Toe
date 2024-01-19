using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tic_Tac_Toe_Machine_Problem
{

    public class RollDice
    {
        private const int maxDice = 100;
        private const int minDice = 1;
        private const int maxSides = 100;
        private const int minSides = 2;

        private static int diceCount = 4;
        private static int sides = 6;

        static int total = -1;

        public int DiceCount
        {
            get { return diceCount; }
            set { diceCount = value; }

        }

        public int Sides
        {
            get { return sides; }
            set { sides = value; }

        }

        public int Total
        {
            get { return total; }
            set { total = value; }

        }

        private Dice[] _dices;

        private List<string> _menuItems = new List<string>
        {
        "Exit",
        $"Setup (Current setup is: {diceCount} dice of { sides } sides).",
        "Roll"
        };

        public static int ReadInteger(string prompt, int min, int max)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int option) && option >= min && option <= max)
                    return option;
                Console.WriteLine($"Please enter a number from {min} to {max}!");
            }
        }

        public void Run()
        {
            bool exit = false;

            _dices = CreateDice(diceCount, sides);
            while (!exit)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\tRoll the Dice");
                Console.ForegroundColor = ConsoleColor.White;
                ShowMenu(_menuItems);
                switch (ReadInteger("Enter your choice here: ", 0, 2))
                {
                    case 0:
                        exit = true;
                        break;
                    case 1:
                        Setup();
                        Console.Clear();
                        break;
                    case 2:
                        Roll();
                        if (_dices != null) { exit = true; }
                        break;

                    default:
                        Console.WriteLine("Invalid menu input!");
                        break;
                }
            }
        }



        public void Roll()
        {
            int initial_Total = 0;

            if (_dices == null)
            {
                Console.WriteLine("Please setup the Roll Dice first!");
                return;
            }
            

            Console.WriteLine("\nRolling...");
            for (int i = 0; i < _dices.Length; i++)
            {
                _dices[i].Roll();
            }
            Console.WriteLine($"Rolled: {string.Join(", ", (object[])_dices)}");


            for (int i = 0; i < _dices.Length; i++)
            {
                initial_Total += _dices[i].Value;
            }
            Console.WriteLine($"The Total Sum rolled is: {initial_Total}");
            Total = initial_Total;

        }

        public void Setup()
        {
            DiceCount = ReadInteger("How many dice will be thrown?: ", minDice, maxDice);
            Sides = ReadInteger("How many sides does each dice have?: ", minSides, maxSides);
            _menuItems[1] = $"Roll Dice Setup. (Current setup is: {diceCount} dice of {sides} sides).";
            _dices = CreateDice(diceCount, sides);
        }



        private Dice[] CreateDice(int diceCount, int sides)
        {
            Dice[] dices = new Dice[diceCount];
            for (int i = 0; i < diceCount; i++)
            {
                dices[i] = new Dice(sides);
            }
            return dices;
        }

        private void ShowMenu(List<string> items)
        {
            Console.WriteLine();
            for (int i = 1; i < items.Count; i++)
                Console.WriteLine($"[{i}] {items[i]}");
            Console.WriteLine($"[0] {items[0]}");
        }
    }

    public class Dice
    {
        private static Random rndGen = new Random();

        private int _sides;

        public int Value { get; private set; }

        public Dice(int sides)
        {
            _sides = sides;
        }

        public void Roll()
        {
            Value = rndGen.Next(0, _sides) + 1;
        }

        public override string ToString()
        {
            return $"[{Value}]";
        }
    }
}
