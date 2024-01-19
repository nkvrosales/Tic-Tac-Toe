using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace Tic_Tac_Toe_Machine_Problem
{

    class Timer
    {
        public static Stopwatch stopw = new Stopwatch();
        public float turnConverted;

        public void CreateandRunTimer()
        {
            
            Console.WriteLine("Game Timer Started...");
            stopw.Start();

        }

        public bool GiveGameTime(float gameTimeSet)
        {
            string game_TimeMinutes; float converted_Time; bool validTurn = false;

            game_TimeMinutes = stopw.Elapsed.TotalMinutes.ToString("f2");


            converted_Time = float.Parse(game_TimeMinutes);

            if (converted_Time > gameTimeSet)
            {
                validTurn = false;
            }
            else if (converted_Time <= gameTimeSet)
            {
                validTurn = true;
            }

            return validTurn;
        }

        public float StopandGiveTime()
        {
            string game_TimeMinutes; float converted_Time;

            stopw.Stop();

            game_TimeMinutes = stopw.Elapsed.TotalMinutes.ToString("f2");

            stopw.Reset();

            converted_Time = float.Parse(game_TimeMinutes);


            return converted_Time;
        }

        public void SpeedTurnTimer(float timerSet)
        {
            Console.Write("{0} seconds Timer per turn had started!! || ", timerSet);
            stopw.Start();

        }

        public bool StopandGiveTurnValid(float timerSet)
        {
            string game_TurnSeconds; bool validTurn = false ;
            stopw.Stop();

            game_TurnSeconds = stopw.Elapsed.TotalSeconds.ToString("f2");
            turnConverted = float.Parse(game_TurnSeconds);

            if (turnConverted > timerSet)
            {
                validTurn = false;
            }
            else if (turnConverted <= timerSet)
            {
                validTurn = true;
            }

            stopw.Reset();

            return validTurn;

        }



        
    
    }
}
