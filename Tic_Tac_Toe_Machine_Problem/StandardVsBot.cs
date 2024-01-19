using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Tic_Tac_Toe_Machine_Problem
{
    class StandardVsBot: StandardTwoPlayer // Inherit the class StandardTwoPlayer
    {
        //all public private is encapsulation
        public List<int> chosenList = new List<int>();



        public string RandomPick(string[] arr)
        {
            List<int> numList =  new List<int>();

            foreach(string value in arr)
            {
                if(value =="X" || value == "O" || value == "0")
                {
                    continue;
                }
                else
                {
                    numList.Add(int.Parse(value));
                }
            }
            int randNum;
            
            do
            {
                Random rand = new Random();
                randNum = rand.Next(numList.Min(), numList.Max());
            } while (chosenList.Contains(randNum));


            chosenList.Add(randNum);

            return randNum.ToString();
        }


        public override string[] TileAndMarkStandard(string[] arr, string currentMark, string currentPlayer)
        {
            int temp; bool isValid; bool isValid2 = true; string[] value_Arr = new string[4];

            switch (currentMark)
            {
                case "X":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case "O":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
            }


            if (currentPlayer == "BOT")
            {
                Console.Write("Bot's turn || ");
                Console.ForegroundColor = ConsoleColor.White;
                Thread.Sleep(1000);
                
                
                if(BoardEvalutation(arr) == 0)
                {
                    temp = int.Parse(RandomPick(arr));
                }
                else
                {
                    temp = BoardEvalutation(arr);
                }

                foreach (string name in players) { if (currentPlayer != name) { currentPlayer = name; break; }}
                
            }
            else
            {

                do
                {
                    if (currentMark == "X") { Console.ForegroundColor = ConsoleColor.Red; } else if (currentMark == "O") { Console.ForegroundColor = ConsoleColor.Blue; }
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
                currentPlayer = "BOT";
                chosenList.Add(temp);

            }


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
            value_Arr[2] = currentPlayer;
            return value_Arr;

        }


        public override float GameResult(int status_Value, string player_Winner, string difficulty)
        {

            float game_Time = GameTimer.StopandGiveTime();

            switch (status_Value)
            {
                case 1:
                    if (player_Winner == "BOT")
                    {
                        MessageBox.Show(" YOU LOST, Bot WINS", "Game Result");
                        break;
                    }
                    else
                    {
                        AccessPoints(game_Time, difficulty);
                        MessageBox.Show((player_Winner + " WINS!!" + "\nGame Time: " + game_Time + "\nTotal Points: " + gameScore), "Game Result");
                        
                        break;
                    }
                case -1:
                    MessageBox.Show("The game is a DRAW!!", "GameResult");
                    break;
            }

            return game_Time;
        }

        public int BoardEvalutation(string[] arr)
        {
            int winning_Tile; int x_Count; int o_Count; int missing_tile = 0; bool not_Found = true;
            int horizontal_end = 0; int horizontal_start = 0; int horizontal_trav = 0;
            int vertical_end = 0; int vertical_start = 0; int vertical_increment = 0;
            int diagonal_end = 0; int diagonal_start = 1; int diagonal_adjustment = 0;
            int for_DiagonalIncrement = 0;//necessary for diagonal evaluation to check both diagonal lines
            int loop_Value = 0; int xy_Count = 0;
            switch (arr.Length)
            {
                case 10:
                    loop_Value = 3;
                    horizontal_end = 4; horizontal_start = 1; horizontal_trav = 3;
                    vertical_end = 8; vertical_start = 1; vertical_increment = 3;
                    diagonal_end = 10; for_DiagonalIncrement = 4; diagonal_adjustment = 2;
                    xy_Count = 2;
                    break;
                case 37:
                    loop_Value = 6;
                    horizontal_end = 7; horizontal_start = 1; horizontal_trav = 6;
                    vertical_end = 32; vertical_start = 1; vertical_increment = 6;
                    diagonal_end = 37; for_DiagonalIncrement = 7; diagonal_adjustment = 5;
                    xy_Count = 5;
                    break;
                case 101:
                    loop_Value = 10;
                    horizontal_end = 11; horizontal_start = 1; horizontal_trav = 10;
                    vertical_end = 92; vertical_start = 1; vertical_increment = 10;
                    diagonal_end = 101; for_DiagonalIncrement = 11; diagonal_adjustment = 9;
                    xy_Count = 9;
                    break;
            }

                    
             // Horizontal Count
            for(int num = 0; num != loop_Value; num++) 
            {
                // reset mark counts
                x_Count = 0; o_Count = 0;
                        
                for (int i = horizontal_start; i < horizontal_end; i++)
                {
                    //check per index if the value is O or X then increment the count
                    if (arr[i] == "X")
                    {
                        x_Count += 1;
                    }
                    else if (arr[i] == "O")
                    {
                        o_Count += 1;
                    }
                    else
                    {
                        missing_tile = i;
                    }

                    //check if either counts met the require value set, if yes then set the boolean to false
                    if (x_Count == xy_Count || o_Count == xy_Count)
                    {                    
                        not_Found = false;
                              
                    }
                            
                }
                        
                if (!not_Found) 
                {   // if the missing tile value is 0, all tiles in the row are marked
                    if (missing_tile == 0)
                    {
                        not_Found = true;
                    }
                    else // else, exit the loop
                    {
                        break;
                    }
                            
                }
                //redefine missing tile to zero, to indicate no winning row
                missing_tile = 0;
                horizontal_end += horizontal_trav; horizontal_start += horizontal_trav;
            }

            ///Vertical Evaluation, reset all values except missing tile
            x_Count = 0; o_Count = 0;  not_Found = true;

            if (missing_tile == 0)
            {
                for (int num = 0; num != loop_Value; num++)
                {
                    // reset mark counts
                    x_Count = 0; o_Count = 0;

                    for (int i = vertical_start; i < vertical_end; i += vertical_increment) // Incremental to assess each column
                    {
                        //check per index if the value is O or X then increment the count
                        if (arr[i] == "X")
                        {
                            x_Count += 1;
                        }
                        else if (arr[i] == "O")
                        {
                            o_Count += 1;
                        }
                        else
                        {
                            missing_tile = i;
                        }

                        //check if either counts met the value set, if yes then set the boolean to false
                        if (x_Count == xy_Count || o_Count == xy_Count)
                        {
                            not_Found = false;

                        }

                    }

                    if (!not_Found)
                    {   // if the missing tile value is 0, all tiles in the column are marked
                        if (missing_tile == 0)
                        {
                            not_Found = true;
                        }
                        else // else, exit the loop
                        {
                            break;
                        }

                    }
                    //redefine missing tile to zero, to indicate no winning column
                    missing_tile = 0;
                    vertical_end ++; vertical_start ++;
                }
            }

            //Digonal Evaluation
            x_Count = 0; o_Count = 0; not_Found = true; 

            if (missing_tile == 0)
            {
                for (int num = 0; num != 2; num++)
                {
                    // reset mark counts
                    x_Count = 0; o_Count = 0;

                    for (int i = diagonal_start; i < diagonal_end; i += for_DiagonalIncrement) // Incremental to assess diagonal line of tiles
                    {
                        //check per index if the value is O or X then increment the count
                        if (arr[i] == "X")
                        {
                            x_Count += 1;
                        }
                        else if (arr[i] == "O")
                        {
                            o_Count += 1;
                        }
                        else
                        {
                            missing_tile = i;
                        }

                        //check if either counts are 2, if yes then set the boolean to false
                        if (x_Count == xy_Count || o_Count == xy_Count)
                        {
                            not_Found = false;

                        }

                    }

                    if (!not_Found)
                    {   // if the missing tile value is 0, all tiles in the column are marked
                        if (missing_tile == 0)
                        {
                            not_Found = true;
                        }
                        else // else, exit the loop
                        {
                            break;
                        }

                    }
                    //redefine missing tile to zero, to indicate no winning column
                    missing_tile = 0;
                    diagonal_end -= diagonal_adjustment; diagonal_start += diagonal_adjustment; for_DiagonalIncrement = diagonal_adjustment; 
                    // Adjust the values to match diagonal tiles
                }
            }

            winning_Tile = missing_tile;


            return winning_Tile;
        }

        public string[] ThreeByThreeStandardBot(string currentPlayer, string fplayer, string splayer)
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

            } while (status_Value == 0);

            foreach (string name in players) { if (currentPlayer != name) { currentPlayer = name; break; } }
            infoResult[2] = GameResult(status_Value, currentPlayer, difficulty).ToString();

            if (status_Value == 1 && currentPlayer != "BOT")
            {
                infoResult[0] = currentPlayer;
                infoResult[1] = gameScore.ToString();

            }

            return infoResult;
        }
        public string[] SixBySixStandardBot(string currentPlayer, string fplayer, string splayer)
        {
            string currentMark = "X"; int status_Value; int tile_Num; string[] turnInfo_arr;
            bool timer_Status = false; string difficulty = "Average"; players[0] = fplayer; players[1] = splayer;
            Array_Initialize();
            Instructions();

            SixBySix_Layout(normal_arr);
            Console.WriteLine("Timer Starts once first move is done");

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

            if (status_Value == 1 && currentPlayer != "BOT")
            {
                infoResult[0] = currentPlayer;
                infoResult[1] = gameScore.ToString();

            }

            return infoResult;
        }

        public string[] TenByTenStandardBot(string currentPlayer, string fplayer, string splayer)
        {
            string currentMark = "X"; int status_Value; int tile_Num; string[] turnInfo_arr;
            bool timer_Status = false; string difficulty = "Average"; players[0] = fplayer; players[1] = splayer;
            Array_Initialize();
            Instructions();

            TenByTen_Layout(hard_arr);
            Console.WriteLine("Timer Starts once first move is done");

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

            if (status_Value == 1 && currentPlayer != "BOT")
            {
                infoResult[0] = currentPlayer;
                infoResult[1] = gameScore.ToString();

            }

            return infoResult;
        }
    }
}
