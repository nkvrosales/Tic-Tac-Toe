using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tic_Tac_Toe_Machine_Problem
{
    class SpeedModeTwoPlayer: StandardTwoPlayer 
    {
        public override void Instructions()
        {
            Console.WriteLine("\nWelcome!! Here is a short recap of the rules in Speed Mode");
            Console.WriteLine("\n1. The First Player will always have the 'X' mark");
            Console.WriteLine("2. In relation, the Second Player shall have the 'O' mark");
            Console.WriteLine("3. Players will take turn to choose a tile number to put their mark");
            Console.WriteLine("4. As set in the settings, each side shall have N seconds to make their move (Unable to do so will result to a LOSS of the respective side)");
            Console.WriteLine("5. Accordingly, the game shall concluded within the time frame set, failure to do so will result to a DRAW");
            Console.WriteLine("6. The side who managed to create a row of their marks vertically, diagonally, or horizotally will win!!");
            Console.WriteLine("7. Post game scores are reliant to the time the game was finished :>");
            Console.WriteLine("8. After these instructions, the game shall start immediately, so be ready!!");
            Console.WriteLine("\nPress Any Key to Start the game.....");
            Console.ReadKey();
            Console.Clear();
        }

        public string[] TileAndMarkSpeed(string[] arr, string currentMark, string currentPlayer, float perTurnValue, float gameTimeValue)
        {
            int temp; bool isValid; bool isValid2 = true; string[] value_Arr = new string[5];
            bool turnValid; bool gameTimeValid = GameTimer.GiveGameTime(gameTimeValue);

            switch (currentMark)
            {
                case "X":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case "O":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
            }
            
            TimerPerTurn.SpeedTurnTimer(perTurnValue);

            do
            {
                if (currentMark == "X") { Console.ForegroundColor = ConsoleColor.Red; } else if (currentMark == "O") { Console.ForegroundColor = ConsoleColor.Blue; }
                Console.Write("Player {0}'s turn  ", currentPlayer);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\nEnter Tile number: ");
                isValid = int.TryParse(Console.ReadLine(), out temp);


                if (!isValid)
                {
                    Console.WriteLine("Invalid Tile number");
                }
                else
                {
                    isValid = !(temp <= 0 || temp > arr.Length - 1);
                    if (!isValid)
                    {
                        Console.WriteLine("Invalid Tile number");
                    }
                    else
                    {
                        isValid2 = arr[temp] == "X" || arr[temp] == "O";
                        if (isValid2)
                        {
                            Console.WriteLine("Tile Already Marked");
                        }
                    }
                }

            } while (!isValid || isValid2);

            turnValid = TimerPerTurn.StopandGiveTurnValid(perTurnValue);

            value_Arr[0] = temp.ToString();
            switch (currentMark)
            {
                case "X":
                    value_Arr[1] = "O";
                    break;
                case "O":
                    value_Arr[1] = "X";
                    break;

            }
            foreach (string name in players) { if (currentPlayer != name) { currentPlayer = name; break; } }
            value_Arr[2] = currentPlayer;

            value_Arr[3] = turnValid.ToString();

            value_Arr[4] = gameTimeValid.ToString();
            
            return value_Arr;
        }

        public override float GameResult(int status_Value, string player_Winner, string difficulty)
        {
            float game_Time = GameTimer.StopandGiveTime();
            

            switch (status_Value)
            {
                case 1:
                    AccessPoints(game_Time, difficulty);
                    MessageBox.Show((player_Winner + " WINS!!" + "\nGame Time: " + game_Time + "\nTotal Points: " + gameScore), "Game Result");
                    break;
                case -1:
                    MessageBox.Show("The game is a DRAW!!", "Game Result");
                    break;
                case -2:
                    foreach (string name in players) { if (player_Winner != name) { player_Winner = name; break; } }

                    MessageBox.Show(TimerPerTurn.turnConverted +" seconds Exceeded the Turn Time \nPlayer " + player_Winner + " WINS", "Game Result");
                    break;
                case -3:

                    MessageBox.Show(game_Time + " mins Exceeded the Turn Time \nIt's a DRAW!!", "Game Result");
                    break;
            }

            return game_Time;
        }

        public string[] ThreeByThreeSpeed(string currentPlayer, string fplayer, string splayer, float timePerTurn, float gameTime)
        {
            string currentMark = "X"; int status_Value; int tile_Num; string[] turnInfo_arr;
            bool timer_Status = false, turnValid, gameTimeValid; string difficulty = "Easy"; players[0] = fplayer; players[1] = splayer;
            Array_Initialize();
            Instructions();

            ThreeByThree_Layout(easy_arr);
            Console.WriteLine("Timer Starts once first move is done");
            do
            {
                turnInfo_arr = TileAndMarkSpeed(easy_arr, currentMark, currentPlayer, timePerTurn, gameTime);
                if (!timer_Status)
                {
                    GameTimer.CreateandRunTimer();
                    timer_Status = true;
                }

                tile_Num = int.Parse(turnInfo_arr[0]);
                easy_arr[tile_Num] = currentMark;
                currentMark = turnInfo_arr[1];
                currentPlayer = turnInfo_arr[2];
                turnValid = bool.Parse(turnInfo_arr[3]);
                gameTimeValid = bool.Parse(turnInfo_arr[4]);

                status_Value = Win.EasyWin_Check(easy_arr);
                Console.Clear();
                ThreeByThree_Layout(easy_arr);

            } while (status_Value == 0 && turnValid && gameTimeValid);

            if (!turnValid)
            {
                status_Value = -2;
            }
            else if (!gameTimeValid)
            {
                status_Value = -3;
            }

            foreach (string name in players) { if (currentPlayer != name) { currentPlayer = name; break; } }
            infoResult[2] = GameResult(status_Value, currentPlayer, difficulty).ToString();


            infoResult[0] = currentPlayer;
            infoResult[1] = gameScore.ToString();


            return infoResult;

        }
        public string[] SixBySixSpeed(string currentPlayer, string fplayer, string splayer, float timePerTurn, float gameTime)
        {
            string currentMark = "X"; int status_Value; int tile_Num; string[] turnInfo_arr;
            bool timer_Status = false, turnValid, gameTimeValid; string difficulty = "Normal"; players[0] = fplayer; players[1] = splayer;
            Array_Initialize();
            Instructions();

            SixBySix_Layout(normal_arr);
            Console.WriteLine("Timer Starts once first move is done");
            do
            {
                turnInfo_arr = TileAndMarkSpeed(normal_arr, currentMark, currentPlayer, timePerTurn, gameTime);
                if (!timer_Status)
                {
                    GameTimer.CreateandRunTimer();
                    timer_Status = true;
                }

                tile_Num = int.Parse(turnInfo_arr[0]);
                normal_arr[tile_Num] = currentMark;
                currentMark = turnInfo_arr[1];
                currentPlayer = turnInfo_arr[2];
                turnValid = bool.Parse(turnInfo_arr[3]);
                gameTimeValid = bool.Parse(turnInfo_arr[4]);

                status_Value = Win.NormalWin_Check(normal_arr);
                Console.Clear();
                SixBySix_Layout(normal_arr);

            } while (status_Value == 0 && turnValid && gameTimeValid);

            if (!turnValid)
            {
                status_Value = -2;
            }
            else if (!gameTimeValid)
            {
                status_Value = -3;
            }

            foreach (string name in players) { if (currentPlayer != name) { currentPlayer = name; break; } }
            infoResult[2] = GameResult(status_Value, currentPlayer, difficulty).ToString();


            infoResult[0] = currentPlayer;
            infoResult[1] = gameScore.ToString();


            return infoResult;
        }


        public string[] TenByTenSpeed(string currentPlayer, string fplayer, string splayer, float timePerTurn, float gameTime)
        {
            string currentMark = "X"; int status_Value; int tile_Num; string[] turnInfo_arr;
            bool timer_Status = false, turnValid, gameTimeValid; string difficulty = "Hard"; players[0] = fplayer; players[1] = splayer;
            Array_Initialize();
            Instructions();

            TenByTen_Layout(hard_arr);
            Console.WriteLine("Timer Starts once first move is done");
            do
            {
                turnInfo_arr = TileAndMarkSpeed(hard_arr, currentMark, currentPlayer, timePerTurn, gameTime);
                if (!timer_Status)
                {
                    GameTimer.CreateandRunTimer();
                    timer_Status = true;
                }

                tile_Num = int.Parse(turnInfo_arr[0]);
                hard_arr[tile_Num] = currentMark;
                currentMark = turnInfo_arr[1];
                currentPlayer = turnInfo_arr[2];
                turnValid = bool.Parse(turnInfo_arr[3]);
                gameTimeValid = bool.Parse(turnInfo_arr[4]);


                status_Value = Win.HardWin_Check(hard_arr);
                Console.Clear();
                TenByTen_Layout(hard_arr);

            } while (status_Value == 0 && turnValid && gameTimeValid);

            if (!turnValid)
            {
                status_Value = -2;
            }
            else if (!gameTimeValid)
            {
                status_Value = -3;
            }

            foreach (string name in players) { if (currentPlayer != name) { currentPlayer = name; break; } }
            infoResult[2] = GameResult(status_Value, currentPlayer, difficulty).ToString();


            infoResult[0] = currentPlayer;
            infoResult[1] = gameScore.ToString();

            return infoResult;
        }
    }
}
