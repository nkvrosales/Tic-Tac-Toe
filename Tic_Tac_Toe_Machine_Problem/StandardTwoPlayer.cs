using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Tic_Tac_Toe_Machine_Problem
{//inheritance from class GameInformation
    class StandardTwoPlayer: GameInformation
    {
        public override void Instructions()
        {
            Console.WriteLine("Here are the rules for Standard Mode\n");
            Console.WriteLine("1. The first player will always have the 'X' mark");
            Console.WriteLine("2. The second player shall have the 'O' mark");
            Console.WriteLine("3. Players will take turns to choose a tile number where they will put their mark");
            Console.WriteLine("4. The player who manages to create a row either vertically, diagonally, or horizontally wins the game");
            Console.WriteLine("5. The post game scores are based on the time the game is finished\n");
            Console.WriteLine("Press any key to start the game");
            Console.ReadKey();
            Console.Clear();
        }

        public virtual string[] TileAndMarkStandard(string[] arr, string currentMark, string currentPlayer)
        {
            int temp; bool isValid; bool isValid2 = true; string[] value_Arr = new string[3];

            switch (currentMark)
            {
                case "X":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case "O":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
            }
   
            do
            {
                if(currentMark == "X") { Console.ForegroundColor = ConsoleColor.Red; } else if (currentMark == "O") { Console.ForegroundColor = ConsoleColor.Blue; }
                Console.Write("Player {0}'s turn ", currentPlayer);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\nEnter Tile number: ");
                isValid = int.TryParse(Console.ReadLine(), out temp);
                
                
                if (!isValid)
                {
                    Console.WriteLine("Invalid Tile number");
                }
                else
                {
                    isValid = !(temp <= 0 || temp > arr.Length -1);
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
            }

            return game_Time;
        }

        public string[] ThreeByThreeStandard(string currentPlayer, string fplayer, string splayer)
        {
            string currentMark = "X"; int status_Value; int tile_Num; string[] turnInfo_arr;
            bool timer_Status = false; string difficulty = "Easy"; players[0] = fplayer; players[1] = splayer;
            Array_Initialize();
            Instructions();

            ThreeByThree_Layout(easy_arr);
            Console.WriteLine("Timer Starts once first move is done");
            do
            {
                turnInfo_arr = TileAndMarkStandard(easy_arr, currentMark, currentPlayer);
                if (!timer_Status)
                {
                    GameTimer.CreateandRunTimer();
                    timer_Status = true;
                }

                tile_Num = int.Parse(turnInfo_arr[0]);
                easy_arr[tile_Num] = currentMark;
                currentMark = turnInfo_arr[1];
                currentPlayer = turnInfo_arr[2];

                status_Value = Win.EasyWin_Check(easy_arr);
                Console.Clear();
                ThreeByThree_Layout(easy_arr);
                
            } while (status_Value ==0);

            foreach (string name in players) { if (currentPlayer != name) { currentPlayer = name; break; } }
            infoResult[2] = GameResult(status_Value, currentPlayer, difficulty).ToString();

            infoResult[0] = currentPlayer;
            infoResult[1] = gameScore.ToString();
            
            return infoResult;
        }
        public string[] SixBySixStandard(string currentPlayer, string fplayer, string splayer)
        {
            string currentMark = "X"; int status_Value; int tile_Num; string[] turnInfo_arr;
            bool timer_Status = false; string difficulty = "Normal"; players[0] = fplayer; players[1] = splayer;
            Array_Initialize();
            Instructions();

            SixBySix_Layout(normal_arr);
            do
            {
                turnInfo_arr = TileAndMarkStandard(normal_arr, currentMark, currentPlayer);
                if (!timer_Status)
                {
                    GameTimer.CreateandRunTimer();
                    timer_Status = true;
                }

                tile_Num = int.Parse(turnInfo_arr[0]);
                normal_arr[tile_Num] = currentMark;
                currentMark = turnInfo_arr[1];
                currentPlayer = turnInfo_arr[2];

                status_Value = Win.NormalWin_Check(normal_arr);
                Console.Clear();
                SixBySix_Layout(normal_arr);
            } while (status_Value == 0);

            foreach (string name in players) { if (currentPlayer != name) { currentPlayer = name; break; } }
            infoResult[2] = GameResult(status_Value, currentPlayer, difficulty).ToString();

            infoResult[0] = currentPlayer;
            infoResult[1] = gameScore.ToString();

            return infoResult;
        }

        public string[] TenByTenStandard(string currentPlayer, string fplayer, string splayer)
        {
            string currentMark = "X"; int status_Value; int tile_Num; string[] turnInfo_arr;
            bool timer_Status = false; string difficulty = "Hard"; players[0] = fplayer; players[1] = splayer;
            Array_Initialize();
            Instructions();

            TenByTen_Layout(hard_arr);
            do
            {
                turnInfo_arr = TileAndMarkStandard(hard_arr, currentMark, currentPlayer);
                if (!timer_Status)
                {
                    GameTimer.CreateandRunTimer();
                    timer_Status = true;
                }

                tile_Num = int.Parse(turnInfo_arr[0]);
                hard_arr[tile_Num] = currentMark;
                currentMark = turnInfo_arr[1];
                currentPlayer = turnInfo_arr[2];

                status_Value = Win.HardWin_Check(hard_arr);
                Console.Clear();
                TenByTen_Layout(hard_arr);
            } while (status_Value == 0);

            foreach (string name in players) { if (currentPlayer != name) { currentPlayer = name; break; } }
            infoResult[2] = GameResult(status_Value, currentPlayer, difficulty).ToString();

            infoResult[0] = currentPlayer;
            infoResult[1] = gameScore.ToString();

            return infoResult;
        }
    }
}
