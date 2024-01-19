using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tic_Tac_Toe_Machine_Problem
{

    class Settings //encapsulates time settings related to the game which allows access through private and public properties
    {
        private int timePerTurn3 = 3, timePerTurn6 = 6, timePerTurn10 = 9; //time settings
        private int gameTime3 = 1, gameTime6 = 3, gameTime10 = 5; //game time

        //Properties for time per turn settings
        public int TimePerTurn3 { get { return timePerTurn3; } set { timePerTurn3 = value; } }
        public int TimePerTurn6 { get { return timePerTurn6; } set { timePerTurn6 = value; } }
        public int TimePerTurn10 { get { return timePerTurn10; } set { timePerTurn10 = value; } }

        //Properties for game time settings
        public int GameTime3 { get { return gameTime3; } set { gameTime3 = value; } }
        public int GameTime6 { get { return gameTime6; } set { gameTime6 = value; } }
        public int GameTime10 { get { return gameTime10; } set { gameTime10 = value; } }


        public void GameTimerCustom() //method for customizing game timer
        {
            char clarifyTimer; bool validTimer, timerBreak = true;
            int changeValue;


            do //Loop to check if input is valid for adjusting game time
            {
                try
                {
                    Console.WriteLine($"\nDefault Values: (Easy - {gameTime3} || Normal - {gameTime6} || Hard - {gameTime10})");
                    Console.Write("Adjust Game Time for (1 - Easy, 2 - Normal, 3 - Hard): ");
                    changeValue = int.Parse(Console.ReadLine());

                    if (changeValue != 1 && changeValue != 2 && changeValue != 3) //check if entered value is within range
                    {
                        throw new FormatException(); //if invalid input
                    }
                    else
                    {

                        break;
                    }

                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid Input...");
                }

            } while (true); //loops until user enters valid input

            do // loop to change Game timer
            {
                Console.Write("\nChange Game Time: ");
                validTimer = int.TryParse(Console.ReadLine(), out int changeGameTime); //validate user input

                if (validTimer)
                {
                    if (changeGameTime <= 0 || changeGameTime >= 60)
                    {
                        Console.WriteLine("Game Time is not in the range between 3 and 30."); //invalid input
                    }
                    else
                    {
                        do //confirm adjustment of game time
                        {
                            try
                            {

                                Console.Write("\nDo you want to readjust your Game Time? ( Y / N ): ");
                                clarifyTimer = Console.ReadLine()[0];

                                if (char.ToUpper(clarifyTimer) == 'Y')
                                {
                                    timerBreak = true; //break loop for changing game time
                                    break;
                                }
                                else if (char.ToUpper(clarifyTimer) == 'N') //continue loop for changing game time
                                {
                                    timerBreak = false;
                                    //Adjust game time based on difficulty
                                    if (changeValue == 1) { GameTime3 = changeGameTime; }
                                    else if (changeValue == 2) { GameTime6 = changeGameTime; }
                                    else if (changeValue == 3) { GameTime10 = changeGameTime; }

                                    break;
                                }
                                else //Input is not Y or N
                                {
                                    Console.WriteLine("Input must be Y or N");
                                }

                            }
                            catch (IndexOutOfRangeException)
                            {
                                Console.WriteLine("Invalid Input...");
                            }
                        }
                        while (true);//loop until valid input
                    }
                }
                else //input is not a valid integer
                {
                    Console.WriteLine("Your Input must be a digit/number.");
                    break;
                }
            }while (!validTimer || timerBreak == true);//loop until valid input or user chooses to not change time
        }


        public void TimerPerTurnCustom() //method for customizing timer per turn
        {
            char clarifyTPT, upperTPT; int changeValue;
            bool validTPT, breakTPT = true;
            do //loop to ensure valid choice
            {
                try
                {
                    Console.WriteLine($"\nDefault Values: (Easy - {timePerTurn3} || Normal - {timePerTurn6} || Hard - {timePerTurn10})");
                    Console.Write("Adjust Time Per Turn for (1 - Easy, 2 - Normal, 3 - Hard): ");
                    changeValue = int.Parse(Console.ReadLine());

                    if (changeValue != 1 && changeValue != 2 && changeValue != 3)//validate entered value
                    {
                        throw new FormatException();//throw exception for invalid input
                    }
                    else
                    {
                        break;//if input is valid
                    }

                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid Input...");
                }

            } while (true);//continue until valid input

            do//loop to change timer per turn
            {
                Console.Write("\nChange Timer Per Turn: ");
                validTPT = int.TryParse(Console.ReadLine(), out int timePerTurn);

                while (true)//user input for timer adjustment
                {
                    if (!validTPT)
                    {
                        Console.WriteLine("Input must be a digit/number.");//prompt to enter valid number
                        break;
                    }
                    else if (timePerTurn > 30 || timePerTurn <= 3)
                    {
                        Console.WriteLine("Inputted Time per Turn is not in the range between 3 and 30.");//check if input is within range
                        break;
                    }
                    else
                    {
                        Console.Write("\nDo you want to readjust your Timer Per Turn? ( Y / N ): ");
                        clarifyTPT = Console.ReadLine()[0];
                        upperTPT = char.ToUpper(clarifyTPT);

                        if (upperTPT == 'Y') //user wants to adjust timer
                        {
                            break; //exit loop to adjust timer
                        }
                        else if (upperTPT == 'N') //user does not want to adjust timer
                        {
                            breakTPT = false;
                            //set timer based on difficulty level
                            if (changeValue == 1) { TimePerTurn3 = timePerTurn; }
                            else if (changeValue == 2) { TimePerTurn6 = timePerTurn; }
                            else if (changeValue == 3) { TimePerTurn10 = timePerTurn; }

                            break;
                        }
                        else
                        {
                            Console.WriteLine("Wrong Input.\n");//prompt for correct input
                        }
                    }
                }
            }while (!validTPT || breakTPT == true);//continue until valid input or if user does not want to adjust timer
        }
    }
}
