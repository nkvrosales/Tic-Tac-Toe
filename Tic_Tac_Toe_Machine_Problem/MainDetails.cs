using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace Tic_Tac_Toe_Machine_Problem
{
    class MainDetails
    {

        public RollDice dice = new RollDice();
        public Settings SpeedSettings = new Settings();

        private string userName1 = "", userName2 = "", chosenMode = string.Empty;

        private string[] chosenAndPlayers = new string[3];


        public void SetPlayers()
        {

            string chosen_Player; string[] players = new string[2]; int mainChoice2; bool isValid = false;


            if (chosenMode.Contains("BOT"))
            {
                players[0] = "BOT"; players[1] = userName1;
                isValid = true;
            }
            else
            {
                do
                {

                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\n\tSecond Player Account\n");
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.WriteLine("1. Log-in/Sign-up");
                    Console.WriteLine("2. Exit");
                    Console.Write("\nEnter your choice here: ");
                    try
                    {
                        mainChoice2 = int.Parse(Console.ReadLine());

                        if (mainChoice2 == 2) { break; }
                        else if (mainChoice2 == 1)
                        {
                            isValid = LogInSignup(2);
                        }
                        else
                        {
                            throw new FormatException();
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid choice! please enter 1 or 2 only");
                        Console.WriteLine("Press Any Key to Continue....");
                        Console.ReadKey();
                    }

                } while (!isValid);
                
                players[0] = userName1; players[1] = userName2;
            }


            if (isValid)
            {
                Console.Clear();
                int dice1Total, dice2Total;

                dice.Run();
                dice1Total = dice.Total;

                if (dice1Total != -1)
                {
                    dice.Roll();
                    dice2Total = dice.Total;


                    Console.WriteLine("\nPlayer {0}'s Dice Value: {1}", players[0], dice1Total);
                    Console.WriteLine("Player {0}'s Dice Value: {1}\n", players[1], dice2Total);


                    if (dice1Total >= dice2Total) { chosen_Player = players[0]; }
                    else { chosen_Player = players[1]; }

                    Console.WriteLine("Player {0} will have the First Turn!", chosen_Player);

                    Console.WriteLine("Press Any Key to Continue..");
                    Console.ReadKey();
                    Console.Clear();

                    chosenAndPlayers[0] = chosen_Player;
                    chosenAndPlayers[1] = players[0];
                    chosenAndPlayers[2] = players[1];

                    
                }
                dice.Total = -1;
            }
            

        }
        public string FileNameCheck()
        {
            string fileName = "";
            switch (chosenMode)
            {
                case "EASYCASTWO":
                    fileName = "StandardEasyTwo.txt";
                    break;
                case "NORMALCASTWO":
                    fileName = "StandardNormalTwo.txt";
                    break;
                case "HARDCASTWO":
                    fileName = "StandardHardTwo.txt";
                    break;
                case "EASYCASBOT":
                    fileName = "StandardEasyBot.txt";
                    break;
                case "NORMALCASBOT":
                    fileName = "StandardNormalBot.txt";
                    break;
                case "HARDCASBOT":
                    fileName = "StandardHardBot.txt";
                    break;
                case "EASYSpeedTWO":
                    fileName = "SpeedEasyTwo.txt";
                    break;
                case "NORMALSpeedTWO":
                    fileName = "SpeedNormalTwo.txt";
                    break;
                case "HARDSpeedTWO":
                    fileName = "SpeedHardTwo.txt";
                    break;
                case "EASYSpeedBOT":
                    fileName = "SpeedEasyBot.txt";
                    break;
                case "NORMALSpeedBOT":
                    fileName = "SpeedNormalBot.txt";
                    break;
                case "HARDSpeedBOT":
                    fileName = "SpeedHardBot.txt";
                    break;
            }
            return fileName;
        }
        public void CheckHighScore(string[] gameInfo)
        {
            string fileName = FileNameCheck();


            if (gameInfo[1] != null)
            {
                string winnerName = gameInfo[0]; int score = int.Parse(gameInfo[1]); float gameTime = float.Parse(gameInfo[2]);
                string newInfo = winnerName + "|" + score.ToString() + "|" + gameTime.ToString();

                List<string> newContents = new List<string>();


                FileStream infoCheckorCreate = new FileStream(fileName, FileMode.OpenOrCreate);


                StreamReader infoRead = new StreamReader(infoCheckorCreate);
                string perLine = infoRead.ReadLine();


                //Append Non-empty lines as string to list
                int numLines = 0;
                while (perLine != null && perLine != "")
                {
                    if (perLine.Contains("|"))
                    {
                        newContents.Add(perLine);
                        numLines++;
                    }
                    perLine = infoRead.ReadLine();

                }

                infoRead.Close();
                infoCheckorCreate.Close();


                string toremove = string.Empty; bool beatOldRecord = false, nameMatch = false;
                foreach (string a in newContents)
                {
                    if (winnerName == a.Split('|')[0])
                    {
                        nameMatch = true;
                        if (gameTime <= float.Parse(a.Split('|')[2]))
                        {
                            toremove = a;
                            beatOldRecord = true;
                        }
                        break;
                    }
                    else if (gameTime <= float.Parse(a.Split('|')[2]))
                    {
                        if(toremove == string.Empty || float.Parse(a.Split('|')[2]) <= float.Parse(toremove.Split('|')[2]))
                        {
                            if(numLines >= 3)
                            {
                                toremove = a;
                            }

                        }

                    }

                }

                if (toremove == string.Empty && !nameMatch && numLines >= 3)
                {
                    MessageBox.Show("You Did Not Make It to the TOP 3\nBetter Luck Next Time :>", "Don't Give Up!!");
                }
                else
                {
                    if (!beatOldRecord && nameMatch)
                    {
                        MessageBox.Show("You Made it to the TOP 3\nBut Did Not Surpass Personal Record", "So Close!!");
                    }
                    else 
                    {
                        newContents.Add(newInfo);
                        newContents.Remove(toremove);

                        // Sort from lowest to highest gametime
                        newContents = newContents.OrderByDescending(c => c.Split('|')[2]).ToList();

                        // Reverse the list to achieve highest to lowest
                        newContents.Reverse();

                        MessageBox.Show("CONGRATULATIONS!! You Made it to the TOP 3!!", "NEW RECORD!!!");
                    }
                }


                //Overwrite the contents of the txt file
                using (StreamWriter newTask = new StreamWriter(fileName, false))
                {
                    numLines = 0;
                    foreach (string lines in newContents)
                    {
                        if (numLines < 3)
                        {
                            newTask.WriteLine(lines);
                        }
                        else
                        {
                            break;
                        }
                        numLines++;
                    }
                }

                newContents.Clear();
            }


        }
        public void StandardMode()
        {
            string choice1; string choice2; bool pass1 = false; bool pass2 = false; string[] afterGameInfo = new string[3];
            while (!pass1)
            {
                Console.Write("1 - Single Player Mode (vs AI) || 2 - Two Player Mode || X - Exit ----> ");
                choice1 = Console.ReadLine();
                if (choice1.ToUpper() == "X")
                {
                    break;
                }
                switch (choice1)
                {
                    case "1":
                        while (!pass2)
                        {
                            Console.Write("ENTER DIFFICULTY (1-Easy, 2-Normal, 3-Hard, X-Exit): ");
                            choice2 = Console.ReadLine();
                            if (choice2.ToUpper() == "X")
                            {
                                break;
                            }
                            StandardVsBot casBot = new StandardVsBot();
                            Console.Clear();
                            switch (choice2)
                            {
                                case "1":
                                    chosenMode = "EASYCASBOT";
                                    SetPlayers();
                                    if (chosenAndPlayers[1] != null) { afterGameInfo = casBot.ThreeByThreeStandardBot(chosenAndPlayers[0], chosenAndPlayers[1], chosenAndPlayers[2]); }
                                    CheckHighScore(afterGameInfo);
                                    pass1 = true;
                                    pass2 = true;
                                    break;
                                case "2":
                                    chosenMode = "NORMALCASBOT";
                                    SetPlayers();
                                    if (chosenAndPlayers[1] != null) { afterGameInfo = casBot.SixBySixStandardBot(chosenAndPlayers[0], chosenAndPlayers[1], chosenAndPlayers[2]); }
                                    CheckHighScore(afterGameInfo);
                                    pass1 = true;
                                    pass2 = true;
                                    break;
                                case "3":
                                    chosenMode = "HARDCASBOT";
                                    SetPlayers();
                                    if (chosenAndPlayers[1] != null){afterGameInfo = casBot.TenByTenStandardBot(chosenAndPlayers[0], chosenAndPlayers[1], chosenAndPlayers[2]);}
                                    CheckHighScore(afterGameInfo);
                                    pass1 = true;
                                    pass2 = true;
                                    break;
                                default:
                                    Console.WriteLine("Enter Valid Input.");
                                    break;
                            }
                        }
                        break;

                    case "2":
                        while (!pass2)
                        {
                            Console.Write("ENTER DIFFICULTY (1-Easy, 2-Normal, 3-Hard, X-Exit): ");
                            choice2 = Console.ReadLine();
                            if (choice2.ToUpper() == "X")
                            {
                                break;
                            }
                            StandardTwoPlayer casTwo = new StandardTwoPlayer();
                            Console.Clear();
                            switch (choice2)
                            {
                                case "1":
                                    chosenMode = "EASYCASTWO";
                                    SetPlayers();

                                    if(userName2 != "" && chosenAndPlayers[1] != null) 
                                    { afterGameInfo = casTwo.ThreeByThreeStandard(chosenAndPlayers[0], chosenAndPlayers[1], chosenAndPlayers[2]); }
                                    
                                    CheckHighScore(afterGameInfo);
                                    pass1 = true;
                                    pass2 = true;
                                    break;
                                case "2":
                                    chosenMode = "NORMALCASTWO";
                                    SetPlayers();

                                    if (userName2 != "" && chosenAndPlayers[1] != null) 
                                    { afterGameInfo = casTwo.SixBySixStandard(chosenAndPlayers[0], chosenAndPlayers[1], chosenAndPlayers[2]); }

                                    CheckHighScore(afterGameInfo);
                                    pass1 = true;
                                    pass2 = true;
                                    break;
                                case "3":
                                    chosenMode = "HARDCASTWO";
                                    SetPlayers();

                                    if (userName2 != "" && chosenAndPlayers[1] != null) 
                                    { afterGameInfo = casTwo.TenByTenStandard(chosenAndPlayers[0], chosenAndPlayers[1], chosenAndPlayers[2]); }

                                    CheckHighScore(afterGameInfo);
                                    pass1 = true;
                                    pass2 = true;
                                    break;
                                default:
                                    Console.WriteLine("Enter Valid Input.");
                                    break;
                            }
                        }
                        break;
                    default:
                        Console.WriteLine("Enter Valid Input.");
                        break;
                }

            }

        }
        public void SpeedMode()
        {
            string choice1; string choice2; bool pass1 = false; bool pass2 = false; string[] afterGameInfo = new string[2];
            int gameTime3 = SpeedSettings.GameTime3, gameTime6 = SpeedSettings.GameTime6, gameTime10 = SpeedSettings.GameTime10;
            int timePerTurn3 = SpeedSettings.TimePerTurn3, timePerTurn6 = SpeedSettings.TimePerTurn6, timePerTurn10 = SpeedSettings.TimePerTurn10;


            while (!pass1)
            {
                Console.Write("1 - Single Player Mode (vs AI) || 2 - Two Player Mode || X - Exit ----> ");
                choice1 = Console.ReadLine();
                if (choice1.ToUpper() == "X")
                {
                    break;
                }
                switch (choice1)
                {
                    case "1":
                        while (!pass2)
                        {
                            Console.Write("ENTER DIFFICULTY (1-Easy, 2-Normal, 3-Hard, X-Exit): ");
                            choice2 = Console.ReadLine();
                            if (choice2.ToUpper() == "X")
                            {
                                break;
                            }
                            SpeedModeVsBot SpeedBot = new SpeedModeVsBot();
                            Console.Clear();
                            switch (choice2)
                            {
                                case "1":
                                    chosenMode = "EASYSpeedBOT";
                                    SetPlayers();
                                    if (chosenAndPlayers[1] != null)
                                    { afterGameInfo = SpeedBot.ThreeByThreeSpeedBot(chosenAndPlayers[0], chosenAndPlayers[1], chosenAndPlayers[2], timePerTurn3, gameTime3); }
                                    CheckHighScore(afterGameInfo);
                                    pass1 = true;
                                    pass2 = true;
                                    break;
                                case "2":
                                    chosenMode = "NORMALSpeedBOT";
                                    SetPlayers();
                                    if (chosenAndPlayers[1] != null)
                                    { afterGameInfo = SpeedBot.SixBySixSpeedBot(chosenAndPlayers[0], chosenAndPlayers[1], chosenAndPlayers[2], timePerTurn6, gameTime6); }
                                    CheckHighScore(afterGameInfo);
                                    pass1 = true;
                                    pass2 = true;
                                    break;
                                case "3":
                                    chosenMode = "HARDSpeedBOT";
                                    SetPlayers();
                                    if (chosenAndPlayers[1] != null)
                                    { afterGameInfo = SpeedBot.TenByTenBotSpeed(chosenAndPlayers[0], chosenAndPlayers[1], chosenAndPlayers[2], timePerTurn10, gameTime10); }
                                    CheckHighScore(afterGameInfo);
                                    pass1 = true;
                                    pass2 = true;
                                    break;
                                default:
                                    Console.WriteLine("Enter Valid Input.");
                                    break;
                            }
                        }
                        break;

                    case "2":
                        while (!pass2)
                        {   
                            Console.Write("ENTER DIFFICULTY (1-Easy, 2-Normal, 3-Hard, X-Exit): ");
                            choice2 = Console.ReadLine();
                            if (choice2.ToUpper() == "X")
                            {
                                break;
                            }
                            SpeedModeTwoPlayer SpeedTwo = new SpeedModeTwoPlayer();
                            Console.Clear();
                            switch (choice2)
                            {
                                case "1":
                                    chosenMode = "EASYSpeedTWO";
                                    SetPlayers();

                                    if (userName2 != "" && chosenAndPlayers[1] != null)
                                    { afterGameInfo = SpeedTwo.ThreeByThreeSpeed(chosenAndPlayers[0], chosenAndPlayers[1], chosenAndPlayers[2], timePerTurn3, gameTime3); }

                                    CheckHighScore(afterGameInfo);
                                    pass1 = true;
                                    pass2 = true;
                                    break;
                                case "2":
                                    chosenMode = "NORMALSpeedTWO";
                                    SetPlayers();

                                    if (userName2 != "" && chosenAndPlayers[1] != null) 
                                    { afterGameInfo = SpeedTwo.SixBySixSpeed(chosenAndPlayers[0], chosenAndPlayers[1], chosenAndPlayers[2], timePerTurn6, gameTime6); }

                                    CheckHighScore(afterGameInfo);
                                    pass1 = true;
                                    pass2 = true;
                                    break;
                                case "3":
                                    chosenMode = "HARDSpeedTWO";
                                    SetPlayers();

                                    if (userName2 != "" && chosenAndPlayers[1] != null) 
                                    { afterGameInfo = SpeedTwo.TenByTenSpeed(chosenAndPlayers[0], chosenAndPlayers[1], chosenAndPlayers[2], timePerTurn10, gameTime10); }

                                    CheckHighScore(afterGameInfo);
                                    pass1 = true;
                                    pass2 = true;
                                    break;
                                default:
                                    Console.WriteLine("Enter Valid Input.");
                                    break;
                            }
                        }
                        break;
                    default:
                        Console.WriteLine("Enter Valid Input.");
                        break;
                }
            }
            Console.Clear();
        }

        public void ShowLeaderboards()
        {
            int boardChoice1 = 0, boardChoice2 = 0, boardChoice3 = 0;
            bool validBoard1 = false, validBoard = false, validBoard2 = false;

            while (!validBoard)
            {
                try
                {
                    Console.WriteLine("\n1 - Standard Mode || 2 - Speed Mode || 3 - Exit");
                    Console.Write("Enter Your choice: ");
                    boardChoice1 = int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid Input, please enter a numeric option only");
                }
                finally
                {
                    if (boardChoice1 != 1 && boardChoice1 != 2 && boardChoice1 != 3)
                    {
                        Console.WriteLine("Invalid Choice, please enter 1, 2, or 3 only");
                    }
                    else
                    {
                        validBoard = true;
                    }
                }
            }

            if (boardChoice1 != 3)
            {
                while (!validBoard1)
                {
                    try
                    {
                        Console.WriteLine("\n1 - Two Player Leaderboard || 2 - VsBot leaderboard || 3 - Exit");
                        Console.Write("Enter Your choice: ");
                        boardChoice2 = int.Parse(Console.ReadLine());
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid Input, please enter a numeric option only");
                    }
                    finally
                    {
                        if (boardChoice2 != 1 && boardChoice2 != 2 && boardChoice2 != 3)
                        {
                            Console.WriteLine("Invalid Choice, please enter 1, 2, or 3 only");
                        }
                        else
                        {
                            validBoard1 = true;
                        }
                    }
                }

            }

            if (boardChoice1 != 3 && boardChoice2 != 3)
            {
                if (boardChoice1 == 1) { Console.ForegroundColor = ConsoleColor.Cyan; Console.WriteLine("\nSTANDARD LEADERBOARD"); Console.ForegroundColor = ConsoleColor.White; }
                else { Console.ForegroundColor = ConsoleColor.Magenta; Console.WriteLine("\nSPEED LEADERBOARDS"); Console.ForegroundColor = ConsoleColor.White; }

                if (boardChoice2 == 1)
                {
                    while (!validBoard2)
                    {
                        try
                        {
                            Console.WriteLine("\n1 - EasyTwoPlayer leaderboard || 2 - NormalTwoPlayer leaderboard || 3 - HardTwoPlayer leaderboard || 4 - Exit");
                            Console.Write("Enter Your choice: ");
                            boardChoice3 = int.Parse(Console.ReadLine());
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid Input, please enter a numeric option only");
                        }
                        finally
                        {
                            if (boardChoice3 != 1 && boardChoice3 != 2 && boardChoice3 != 3 && boardChoice3 != 4)
                            {
                                Console.WriteLine("Invalid Choice, please enter 1, 2, 3, or 4 only");
                            }
                            else
                            {
                                if (boardChoice1 == 1 && boardChoice3 == 1) { chosenMode = "EASYCASTWO"; }
                                else if (boardChoice1 == 1 && boardChoice3 == 2) { chosenMode = "NORMALCASTWO"; }
                                else if (boardChoice1 == 1 && boardChoice3 == 3) { chosenMode = "HARDCASTWO"; }
                                else if (boardChoice1 == 2 && boardChoice3 == 1) { chosenMode = "EASYSpeedTWO"; }
                                else if (boardChoice1 == 2 && boardChoice3 == 2) { chosenMode = "NORMALSpeedTWO"; }
                                else if (boardChoice1 == 2 && boardChoice3 == 3) { chosenMode = "HARDSpeedTWO"; }
                                validBoard2 = true;
                            }
                        }
                    }
                }
                else
                {
                    while (!validBoard2)
                    {
                        try
                        {
                            Console.WriteLine("\n1 - EasyVSBot leaderboard || 2 - NormalVSBot leaderboard || 3 - HardVsBot leaderboard || 4 - Exit");
                            Console.Write("Enter Your choice: ");
                            boardChoice3 = int.Parse(Console.ReadLine());
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid Input, please enter a numeric option only");
                        }
                        finally
                        {
                            if (boardChoice3 != 1 && boardChoice3 != 2 && boardChoice3 != 3 && boardChoice3 != 4)
                            {
                                Console.WriteLine("Invalid Choice, please enter 1, 2, 3, or 4 only");
                            }
                            else
                            {
                                if (boardChoice1 == 1 && boardChoice3 == 1) { chosenMode = "EASYCASBOT"; }
                                else if (boardChoice1 == 1 && boardChoice3 == 2) { chosenMode = "NORMALCASBOT"; }
                                else if (boardChoice1 == 1 && boardChoice3 == 3) { chosenMode = "HARDCASBOT"; }
                                else if (boardChoice1 == 2 && boardChoice3 == 1) { chosenMode = "EASYSpeedBOT"; }
                                else if (boardChoice1 == 2 && boardChoice3 == 2) { chosenMode = "NORMALSpeedBOT"; }
                                else if (boardChoice1 == 2 && boardChoice3 == 3) { chosenMode = "HARDSpeedBOT"; }
                                validBoard2 = true;
                            }
                        }
                    }
                }
            }

            if (boardChoice1 != 3 && boardChoice2 != 3 && boardChoice3 != 4)
            {
                try
                {
                    StreamReader leaderRead = new StreamReader(FileNameCheck());
                    string perLine = leaderRead.ReadLine();
                    string writtenScore, name, gameTime;

                    if (perLine == null || perLine == "") { leaderRead.Close(); throw new FileNotFoundException(); }

                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nLEADERBOARDS: ");
                    Console.ForegroundColor = ConsoleColor.White;

                    int num = 1;
                    while (perLine != null && perLine != "")
                    {
                        name = perLine.Split('|')[0];
                        writtenScore = perLine.Split('|')[1];
                        gameTime = perLine.Split('|')[2];

                        Console.WriteLine("\nTop " + num.ToString() + ": " + name + " || Score: " + writtenScore + " || Game Duration: " + gameTime);
                        perLine = leaderRead.ReadLine();
                        num++;
                    }

                    leaderRead.Close();

                }
                catch (FileNotFoundException)
                {

                    Console.WriteLine("\nNo Records available");
                }
                finally
                {
                    Console.WriteLine("\nPress any key to continue....");
                    Console.ReadKey();
                }
            }
        }

        public bool LogInSignup(int userNum)
        {
            string userNameTry = string.Empty, passWordTry = string.Empty;
            bool nameFound, passwordCorrect, validEntry = false, empty = false;


            DialogResult signup = MessageBox.Show("Do you have an existing account?", "Hello Player!!", MessageBoxButtons.YesNo);
            if (signup == DialogResult.Yes)
            {
                try
                {
                    StreamReader playerRead = new StreamReader("players.Txt");
                    
                    List<string> infoContainer = new List<string>();

                    string perLine = playerRead.ReadLine();
                    empty = false; nameFound = false; passwordCorrect = false;


                    if (perLine == null || perLine == "") { empty = true; }

                    if (empty)
                    {
                        playerRead.Close();
                        throw new FileNotFoundException();
                    }

                    else
                    {

                        while (perLine != null && perLine != "")
                        {
                            infoContainer.Add(perLine);
                            perLine = playerRead.ReadLine();
                        }
                        playerRead.Close();


                        Console.Write("\nEnter Player Username: ");
                        userNameTry = Console.ReadLine();


                        if (userNameTry == string.Empty) { Console.WriteLine("Empty Username Input, Try Again.."); }
                        else
                        {
                            foreach (string info in infoContainer)
                            {
                                if (info.Split('|')[0] == userNameTry)
                                {
                                    nameFound = true;
                                    break;
                                }
                            }

                            if (nameFound)
                            {
                                Console.Write("\nEnter Password: ");
                                passWordTry = Console.ReadLine();

                                if (passWordTry == string.Empty) { Console.WriteLine("Empty Password Input, Try Again.."); }
                                else
                                {
                                    foreach (string info in infoContainer)
                                    {
                                        if (info.Split('|')[0] == userNameTry)
                                        {
                                            if (info.Split('|')[1] == passWordTry)
                                            {
                                                passwordCorrect = true;
                                                validEntry = true;
                                                break;
                                            }

                                        }
                                    }

                                    if (userNum == 1) { userName1 = userNameTry; }
                                    else { userName2 = userNameTry; }

                                    if (passwordCorrect)
                                    {
                                        MessageBox.Show("Welcome " + userNameTry + "!" + "\n Good to see you!", "Hello Player!!");

                                    }
                                    else { Console.WriteLine("Password Incorrect, Try Again.."); }
                                }
                            }
                            else
                            {
                                Console.WriteLine("Username Not Found..");
                            }
                        }
                    }
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("No Records Found, Creating a new account.\n");
                    do
                    {
                        Console.Write("\nEnter Username: ");
                        userNameTry = Console.ReadLine();

                        Console.Write("\nEnter Password: ");
                        passWordTry = Console.ReadLine();

                        if (userNameTry == string.Empty || passWordTry == string.Empty)
                        {
                            Console.WriteLine("Invalid Input, must not be empty..");
                        }

                    } while (userNameTry == string.Empty || passWordTry == string.Empty);

                    using (StreamWriter newTask = new StreamWriter("players.Txt", false))
                    {
                        newTask.WriteLine(userNameTry + '|' + passWordTry);
                    }


                    if (userNum == 1) { userName1 = userNameTry; }
                    else { userName2 = userNameTry; }
                    validEntry = true;
                    MessageBox.Show("Welcome " + userNameTry + "!" + "\n Good to see you!", "Hello Player!!");
                }

                finally
                {
                    Console.WriteLine("\nClick Any Key To Continue...");
                    Console.ReadKey();
                }

            }

            else
            {
                try
                {
                    empty = false;
                    List<string> infoContainer = new List<string>();

                    StreamReader playerRead = new StreamReader("players.Txt");


                    string perLine = playerRead.ReadLine();

                    if (perLine == string.Empty) { empty = true; }

                    if (empty) { playerRead.Close(); throw new FileNotFoundException(); }

                    else
                    {
                        while (perLine != null && perLine != "")
                        {
                            infoContainer.Add(perLine);
                            perLine = playerRead.ReadLine();
                        }
                        playerRead.Close();

                        do
                        {
                            nameFound = false;

                            Console.Write("\nEnter Username: ");
                            userNameTry = Console.ReadLine();

                            foreach (string info in infoContainer)
                            {
                                if (info.Split('|')[0] == userNameTry)
                                {
                                    Console.WriteLine("UserName already Taken");
                                    nameFound = true;
                                    break;
                                }
                            }

                            if (!nameFound)
                            {
                                Console.Write("\nEnter Password: ");
                                passWordTry = Console.ReadLine();

                                if (userNameTry == string.Empty || passWordTry == string.Empty)
                                {
                                    Console.WriteLine("Invalid Input, must not be empty..");
                                }
                            }

                        } while (userNameTry == string.Empty || passWordTry == string.Empty || nameFound);
                    }
                }
                catch (FileNotFoundException)
                {
                    do
                    {
                        Console.Write("\nEnter Username: ");
                        userNameTry = Console.ReadLine();

                        Console.Write("\nEnter Password: ");
                        passWordTry = Console.ReadLine();

                        if (userNameTry == string.Empty || passWordTry == string.Empty)
                        {
                            Console.WriteLine("Invalid Input, must not be empty..");
                        }

                    } while (userNameTry == string.Empty || passWordTry == string.Empty);

                }
                finally
                {
                    using (StreamWriter newTask = new StreamWriter("players.Txt", true))
                    {
                        newTask.WriteLine(userNameTry + '|' + passWordTry);
                    }
                    validEntry = true;
                    if (userNum == 1) { userName1 = userNameTry; }
                    else { userName2 = userNameTry; }

                    MessageBox.Show("Welcome " + userNameTry + "!" + "\n Good to see you!", "Hello Player!!");
                }
            }
            return validEntry;
        }


        public void MenuSettingOptions() //Menu for changing speed mode settings
        {
            do
            {
                string choiceOptionMenu, choiceOptionSpeed;
                //Display Menu
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n\tSettings\n");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("1 - Speed");
                Console.WriteLine($"2 - Dice (Current setup is: {dice.DiceCount} dice of {dice.Sides} sides).");
                Console.WriteLine("X - Exit\n");
                Console.ForegroundColor = ConsoleColor.White;

                Console.Write("Enter choice: ");
                choiceOptionMenu = Console.ReadLine();

                if (choiceOptionMenu.ToUpper() == "X") //Exit
                {
                    Console.Clear();
                    break;
                }
                else if (choiceOptionMenu == "1") //Speed Settings
                {
                    do
                    {
                        //Display Sub-Menu
                        Console.Clear();
                        Console.WriteLine("-----Speed Settings-----");
                        Console.WriteLine("1 - Change Game Time");
                        Console.WriteLine("2 - Change Timer per Turn");
                        Console.WriteLine("X - Exit");

                        Console.Write("Enter choice: ");
                        choiceOptionSpeed = Console.ReadLine();

                        if (choiceOptionSpeed == "1")
                        {
                            SpeedSettings.GameTimerCustom(); //calls method in Settings
                            Console.WriteLine("Successfully Changed!\nEnter Any Key To Continue...");
                            Console.ReadKey();
                            break;
                        }
                        else if (choiceOptionSpeed == "2")
                        {
                            SpeedSettings.TimerPerTurnCustom(); //calls method in settings
                            Console.WriteLine("Successfully Changed!\nEnter Any Key To Continue...");
                            Console.ReadKey();
                            break;
                        }
                        else if (choiceOptionSpeed == "X")
                        {
                            Console.Clear();
                            break;
                        }
                        else //Invalid Input
                        {
                            Console.WriteLine("Your input should be either 1 or 2.");
                        }
                    }
                    while (true);

                }
                else if (choiceOptionMenu == "2")
                {
                    dice.Setup(); //calls method for dice settings
           
                }

            }while (true);

        }

        public void MainMenu()
        {
            //declare variables
            bool validEntry;
            string mainChoice1;
            int mainChoice2;
            
            do
            {
                //Display Main Menu
                
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n\tWELCOME TO TIC-TAC-TOE!\n"); //edit
                
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("1. Log-in/Sign-up");
                Console.WriteLine("2. Leaderboards");
                Console.WriteLine("3. Exit");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("\nEnter your choice here: ");
                Console.ForegroundColor = ConsoleColor.Green;

                try
                {
                    mainChoice2 = int.Parse(Console.ReadLine());
                }
                catch (FormatException) //if user enters invalid input
                {
                    do
                    {
                        Console.WriteLine("Invalid choice!");
                        Console.Write("\nEnter your choice here [1/2/3]: ");
                    } while (!int.TryParse(Console.ReadLine(), out mainChoice2)); //loop until valid integer is entered
                }

                if (mainChoice2 == 1)
                {
                    validEntry = LogInSignup(1); //calls method for log in and sign up

                    if (validEntry) //Successfull log in
                    {
                        //Display Sub-Menu
                        while (true)
                        {
                            Console.Clear();

                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.Write("Current Account: ");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(userName1);
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("\n\tWELCOME TO TIC-TAC-TOE!!!\n");
                            Console.ForegroundColor = ConsoleColor.DarkRed;


                            Console.WriteLine("\n1 - Standard Mode");
                            Console.WriteLine("2 - Speed Mode");
                            Console.WriteLine("3 - Leaderboards");
                            Console.WriteLine("4 - Settings");
                            Console.WriteLine("X - Log out ");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("\nEnter your choice here ----> ");
                            mainChoice1 = Console.ReadLine();

                            if (mainChoice1.ToUpper() == "X") //Log out option
                            {
                                Console.WriteLine("\nSuccessfully Logged Out.");
                                Console.WriteLine("Click Any Key to continue...");
                                Console.ReadKey();
                                break;
                            }

                            switch (mainChoice1) //Switch-case for different choices
                            {
                                case "1":
                                    StandardMode();
                                    break;
                                case "2":
                                    SpeedMode();
                                    break;
                                case "3":
                                    ShowLeaderboards();
                                    break;
                                case "4":
                                    MenuSettingOptions();
                                    break;
                                default:
                                    Console.WriteLine("Invalid Option...");
                                    break;
                            }

                            chosenAndPlayers[0] = null; chosenAndPlayers[1] = null; chosenAndPlayers[2] = null; //resets variables for next iteration
                        }
                    }
                }
                else if (mainChoice2 == 2)
                {
                    ShowLeaderboards(); //calls method for showing leaderboards
                }
                else if (mainChoice2 == 3) //Exit
                {
                    MessageBox.Show("Thank you for playing!", "Message");
                }
                Console.Clear();
            }
            while (mainChoice2 != 3); //loop until user chooses to exit
        }
    }

    abstract class GameInformation //inheritance to Standard two player, Abstraction; Base Class inherited by other classes
    {
        public int gameScore;
        public Timer GameTimer = new Timer();
        public Check_Win Win = new Check_Win();
        public Timer TimerPerTurn = new Timer();


        public string[] players = new string[2];
        public string[] infoResult = new string[3];

        public string[] easy_arr = new string[10];
        public string[] normal_arr = new string[37];
        public string[] hard_arr = new string[101];

        public abstract void Instructions();
        public abstract float GameResult(int status_Value, string player_Winner, string difficulty);
        

        public void Array_Initialize()
        {
            for (int num = 0; num < 10; num++) //initialize Easy elements index 0-9
            {
                easy_arr[num] = num.ToString();
            }
            for (int num = 0; num < 37; num++) //initialize Normal elements index 0-36 
            {
                normal_arr[num] = num.ToString();
            }
            for (int num = 0; num < 101; num++) //initialize Hard elements index 0-100
            {
                hard_arr[num] = num.ToString();
            }
        }

        public void ThreeByThree_Layout(string[] arr) //Makes 3x3 Layout for Easy Mode
        {
            int count = 1;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("3x3 Tic-Tac-Toe Table");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("_________________");
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("     |     |     ");
                for (int num = 0; num < 3; num++)
                {
                    if (arr[count] == "X")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else if (arr[count] == "O")
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

        public void SixBySix_Layout(string[] arr) //Makes 6x6 Layout for Normal Mode
        {
            int count = 1;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("6x6 Tic-Tac-Toe Table");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("___________________________________");
            for (int i = 0; i < 6; i++)
            {
                Console.WriteLine("     |     |     |     |     |     ");

                for (int num = 0; num < 6; num++)
                {
                    if (arr[count] == "X")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else if (arr[count] == "O")
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

        public void TenByTen_Layout(string[] arr) //Makes 10x10 Layout for Hard Mode
        {
            int count = 1;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("10x10 Tic-Tac-Toe Table");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("___________________________________________________________");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("     |     |     |     |     |     |     |     |     |     ");

                for (int num = 0; num < 10; num++)
                {
                    if (arr[count] == "X")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else if (arr[count] == "O")
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }

                    if (arr[count].Length == 1)
                    {
                        Console.Write("  {0}   ", arr[count]); // 2 before, 3 spaces after for 1 character long display
                    }
                    else if (arr[count].Length == 2)
                    {
                        Console.Write("  {0}  ", arr[count]); // 2 before, 2 spaces after for 2 characters long display
                    }
                    else
                    {
                        Console.Write(" {0}   ", arr[count]); // 1 before, 4 spaces after for 3 characters long display
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                    count++;

                }
                Console.Write("\n");
                Console.WriteLine("_____|_____|_____|_____|_____|_____|_____|_____|_____|_____");
            }
        }

        public void AccessPoints(float game_Time, string diffculty) //For scoring
        {

            switch (diffculty)
            {
                case "Easy":
                    gameScore += 5;
                    if (0 <= game_Time && game_Time <= 2)
                    {
                        gameScore += 10;
                    }
                    else if (2 < game_Time && game_Time <= 3)
                    {
                        gameScore += 5;
                    }
                    else if (3 < game_Time && game_Time <= 5)
                    {
                        gameScore += 2;
                    }

                    break;
                case "Normal":
                    gameScore += 10;
                    if (0 <= game_Time && game_Time <= 4)
                    {
                        gameScore += 10;
                    }
                    else if (4 < game_Time && game_Time <= 9)
                    {
                        gameScore += 5;
                    }
                    else if (9 < game_Time && game_Time <= 10)
                    {
                        gameScore += 2;
                    }
                    break;
                case "Hard":
                    gameScore += 15;
                    if (0 <= game_Time && game_Time <= 10)
                    {
                        gameScore += 10;
                    }
                    else if (10 < game_Time && game_Time <= 20)
                    {
                        gameScore += 5;
                    }
                    else if (20 < game_Time && game_Time <= 30)
                    {
                        gameScore += 2;
                    }
                    break;
            }
        }
    }
}
