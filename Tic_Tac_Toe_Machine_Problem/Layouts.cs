using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tic_Tac_Toe_Machine_Problem
{
    class Layouts
    {
        
        public void ThreeByThree_Layout(string[] arr)
        {
            int count = 1;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("3x3 Tic Tac Toe Table");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("_________________");
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("     |     |     ");
                for (int num = 0; num < 3; num++)
                {
                    if (arr[count] == "O")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else if (arr[count] == "X")
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    Console.Write("  {0}   ", arr[count]);
                    Console.ForegroundColor = ConsoleColor.White;
                    count++;
                }
                Console.Write("\n");
                Console.WriteLine("_____|_____|_____");
            }
        }

        public void SixBySix_Layout(string[] arr)
        {
            int count = 1;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("6x6 Tic Tac Toe Table");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("___________________________________");
            for (int i = 0; i < 6; i++)
            {
                Console.WriteLine("     |     |     |     |     |     ");

                for (int num = 0; num < 6; num++)
                {
                    if (arr[count] == "O")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else if (arr[count] == "X")
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }

                    if (arr[count].Length == 1)
                    {
                        Console.Write("  {0}   ", arr[count]);
                    }
                    else
                    {
                        Console.Write(" {0}   ", arr[count]);
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                    count++;
                    
                }
                Console.Write("\n");
                Console.WriteLine("_____|_____|_____|_____|_____|_____");
            }
        }

        public void TenByTen_Layout(string[] arr)
        {
            int count = 1;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("10x10 Tic Tac Toe Table");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("___________________________________________________________");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("     |     |     |     |     |     |     |     |     |     ");

                for (int num = 0; num < 10; num++)
                {
                    if(arr[count] == "O")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else if(arr[count] == "X")
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }

                    if (arr[count].Length == 1)
                    {
                        Console.Write("  {0}   ", arr[count]);
                    }
                    else if(arr[count].Length == 2)
                    {
                        Console.Write("  {0}  ", arr[count]);
                    }
                    else
                    {
                        Console.Write(" {0}   ", arr[count]);
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                    count++;

                }
                Console.Write("\n");
                Console.WriteLine("_____|_____|_____|_____|_____|_____|_____|_____|_____|_____");
            }





        }
    }
}
