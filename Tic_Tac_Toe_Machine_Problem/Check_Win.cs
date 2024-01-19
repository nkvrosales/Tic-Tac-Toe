using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tic_Tac_Toe_Machine_Problem
{

    class Check_Win
    {

        public int EasyWin_Check(string[] easy_arr)
        {
            // 1- WIN, -1 - DRAW, 0 - no conditions found(Continue Game)

            //Horzontal Winning Check
            if (new[] {easy_arr[1], easy_arr[2], easy_arr[3]}.All(value => value == easy_arr[1]))
            {
                return 1;
            }
            else if (new[] {easy_arr[4], easy_arr[5], easy_arr[6] }.All(value => value == easy_arr[4]))
            {
                return 1;
            }
            else if (new[] {easy_arr[7], easy_arr[8], easy_arr[9] }.All(value => value == easy_arr[7]))
            {
                return 1;
            }
            
            //Vertical Winning Condtion
            else if (new[] { easy_arr[1], easy_arr[4], easy_arr[7] }.All(value => value == easy_arr[1]))
            {
                return 1;
            }
            else if (new[] { easy_arr[2], easy_arr[5], easy_arr[8] }.All(value => value == easy_arr[2]))
            {
                return 1;
            }
            else if (new[] { easy_arr[3], easy_arr[6], easy_arr[9] }.All(value => value == easy_arr[3]))
            {
                return 1;
            }


            // Diagonal Win Check
            else if (new[] { easy_arr[1], easy_arr[5], easy_arr[9] }.All(value => value == easy_arr[1]))
            {
                return 1;
            }
            else if (new[] { easy_arr[3], easy_arr[5], easy_arr[7] }.All(value => value == easy_arr[3]))
            {
                return 1;
            }


            //Check if draw
            else
            {
                //Create bool indicator
                bool num_Detect = false;

                for (int i = 1; i < easy_arr.Length; i++)
                {
                    // if all values of easy_arr are not a number, return value would be -1
                    if (easy_arr[i] == i.ToString())
                    {
                    num_Detect = true;
                    break;
                    }
                        
                }
               
                if (num_Detect)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
                
            }
        }

        public int NormalWin_Check(string[] ave_arr)
        {
            // 1- WIN, -1 - DRAW, 0 - no conditions found(Continue Game)

            //Horzontal Winning Check
            if (new[] { ave_arr[1], ave_arr[2], ave_arr[3], ave_arr[4], ave_arr[5], ave_arr[6] }.All(value => value == ave_arr[1]))
            {
                return 1;
            }
            else if (new[] { ave_arr[7], ave_arr[8], ave_arr[9], ave_arr[10], ave_arr[11], ave_arr[12] }.All(value => value == ave_arr[7]))
            {
                return 1;
            }
            else if (new[] { ave_arr[13], ave_arr[14], ave_arr[15], ave_arr[16], ave_arr[17], ave_arr[18] }.All(value => value == ave_arr[13]))
            {
                return 1;
            }
            else if (new[] { ave_arr[19], ave_arr[20], ave_arr[21], ave_arr[22], ave_arr[23], ave_arr[24] }.All(value => value == ave_arr[19]))
            {
                return 1;
            }
            else if (new[] { ave_arr[25], ave_arr[26], ave_arr[27], ave_arr[28], ave_arr[29], ave_arr[30] }.All(value => value == ave_arr[25]))
            {
                return 1;
            }
            else if (new[] { ave_arr[31], ave_arr[32], ave_arr[33], ave_arr[34], ave_arr[35], ave_arr[36] }.All(value => value == ave_arr[31]))
            {
                return 1;
            }


            //Vertical Winning Check
            else if (new[] { ave_arr[1], ave_arr[7], ave_arr[13], ave_arr[19], ave_arr[25], ave_arr[31] }.All(value => value == ave_arr[1]))
            {
                return 1;
            }
            else if (new[] { ave_arr[2], ave_arr[8], ave_arr[14], ave_arr[20], ave_arr[26], ave_arr[32] }.All(value => value == ave_arr[2]))
            {
                return 1;
            }
            else if (new[] { ave_arr[3], ave_arr[9], ave_arr[15], ave_arr[21], ave_arr[27], ave_arr[33] }.All(value => value == ave_arr[3]))
            {
                return 1;
            }
            else if (new[] { ave_arr[4], ave_arr[10], ave_arr[16], ave_arr[22], ave_arr[28], ave_arr[34] }.All(value => value == ave_arr[4]))
            {
                return 1;
            }
            else if (new[] { ave_arr[5], ave_arr[11], ave_arr[17], ave_arr[23], ave_arr[29], ave_arr[35] }.All(value => value == ave_arr[5]))
            {
                return 1;
            }
            else if (new[] { ave_arr[6], ave_arr[12], ave_arr[18], ave_arr[24], ave_arr[30], ave_arr[36] }.All(value => value == ave_arr[6]))
            {
                return 1;
            }


            //Diagonal Winning Check
            else if (new[] { ave_arr[1], ave_arr[8], ave_arr[15], ave_arr[22], ave_arr[29], ave_arr[36] }.All(value => value == ave_arr[1]))
            {
                return 1;
            }
            else if (new[] { ave_arr[6], ave_arr[11], ave_arr[16], ave_arr[21], ave_arr[26], ave_arr[31] }.All(value => value == ave_arr[6]))
            {
                return 1;
            }



            //Check if Draw

            else
            {
                //Create bool indicator
                bool num_Detect = false;

                for (int i = 1; i < ave_arr.Length; i++)
                {
                    // if all values of ave_arr are not a number, return value would be -1
                    if (ave_arr[i] == i.ToString())
                    {
                        num_Detect = true;
                        break;
                    }

                }

                if (num_Detect)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }

        }

        public int HardWin_Check(string[] hard_arr)
        {
            // 1- WIN, -1 - DRAW, 0 - no conditions found(Continue Game)

            //Horzontal Winning Check
            if (new[] { hard_arr[1], hard_arr[2], hard_arr[3], hard_arr[4], hard_arr[5], hard_arr[6], hard_arr[7], hard_arr[8], hard_arr[9], hard_arr[10] }.All(value => value == hard_arr[1]))
            {
                return 1;
            }
            else if (new[] { hard_arr[11], hard_arr[12], hard_arr[13], hard_arr[14], hard_arr[15], hard_arr[16], hard_arr[17], hard_arr[18], hard_arr[19], hard_arr[20] }.All(value => value == hard_arr[11]))
            {
                return 1;
            }
            else if (new[] { hard_arr[21], hard_arr[22], hard_arr[23], hard_arr[24], hard_arr[25], hard_arr[26], hard_arr[27], hard_arr[28], hard_arr[29], hard_arr[30] }.All(value => value == hard_arr[21]))
            {
                return 1;
            }
            else if (new[] { hard_arr[31], hard_arr[32], hard_arr[33], hard_arr[34], hard_arr[35], hard_arr[36], hard_arr[37], hard_arr[38], hard_arr[39], hard_arr[40] }.All(value => value == hard_arr[31]))
            {
                return 1;
            }
            else if (new[] { hard_arr[41], hard_arr[42], hard_arr[43], hard_arr[44], hard_arr[45], hard_arr[46], hard_arr[47], hard_arr[48], hard_arr[49], hard_arr[50] }.All(value => value == hard_arr[41]))
            {
                return 1;
            }
            else if (new[] { hard_arr[51], hard_arr[52], hard_arr[53], hard_arr[54], hard_arr[55], hard_arr[56], hard_arr[57], hard_arr[58], hard_arr[59], hard_arr[60] }.All(value => value == hard_arr[51]))
            {
                return 1;
            }
            else if (new[] { hard_arr[61], hard_arr[62], hard_arr[63], hard_arr[64], hard_arr[65], hard_arr[66], hard_arr[67], hard_arr[68], hard_arr[69], hard_arr[70] }.All(value => value == hard_arr[61]))
            {
                return 1;
            }
            else if (new[] { hard_arr[71], hard_arr[72], hard_arr[73], hard_arr[74], hard_arr[75], hard_arr[76], hard_arr[77], hard_arr[78], hard_arr[79], hard_arr[80] }.All(value => value == hard_arr[71]))
            {
                return 1;
            }
            else if (new[] { hard_arr[81], hard_arr[82], hard_arr[83], hard_arr[84], hard_arr[85], hard_arr[86], hard_arr[87], hard_arr[88], hard_arr[89], hard_arr[90] }.All(value => value == hard_arr[81]))
            {
                return 1;
            }
            else if (new[] { hard_arr[91], hard_arr[92], hard_arr[93], hard_arr[94], hard_arr[95], hard_arr[96], hard_arr[97], hard_arr[98], hard_arr[99], hard_arr[100] }.All(value => value == hard_arr[91]))
            {
                return 1;
            }


            //Vertical Check Win
            else if (new[] { hard_arr[1], hard_arr[11], hard_arr[21], hard_arr[31], hard_arr[41], hard_arr[51], hard_arr[61], hard_arr[71], hard_arr[81], hard_arr[91] }.All(value => value == hard_arr[1]))
            {
                return 1;
            }
            else if (new[] { hard_arr[2], hard_arr[12], hard_arr[22], hard_arr[32], hard_arr[42], hard_arr[52], hard_arr[62], hard_arr[72], hard_arr[82], hard_arr[92] }.All(value => value == hard_arr[2]))
            {
                return 1;
            }
            else if (new[] { hard_arr[3], hard_arr[13], hard_arr[23], hard_arr[33], hard_arr[43], hard_arr[53], hard_arr[63], hard_arr[73], hard_arr[83], hard_arr[93] }.All(value => value == hard_arr[3]))
            {
                return 1;
            }
            else if (new[] { hard_arr[4], hard_arr[14], hard_arr[24], hard_arr[34], hard_arr[44], hard_arr[54], hard_arr[64], hard_arr[74], hard_arr[84], hard_arr[94] }.All(value => value == hard_arr[4]))
            {
                return 1;
            }
            else if (new[] { hard_arr[5], hard_arr[15], hard_arr[25], hard_arr[35], hard_arr[45], hard_arr[55], hard_arr[65], hard_arr[75], hard_arr[85], hard_arr[95] }.All(value => value == hard_arr[5]))
            {
                return 1;
            }
            else if (new[] { hard_arr[6], hard_arr[16], hard_arr[26], hard_arr[36], hard_arr[46], hard_arr[56], hard_arr[66], hard_arr[76], hard_arr[86], hard_arr[96] }.All(value => value == hard_arr[6]))
            {
                return 1;
            }
            else if (new[] { hard_arr[7], hard_arr[17], hard_arr[27], hard_arr[37], hard_arr[47], hard_arr[57], hard_arr[67], hard_arr[77], hard_arr[87], hard_arr[97] }.All(value => value == hard_arr[7]))
            {
                return 1;
            }
            else if (new[] { hard_arr[8], hard_arr[18], hard_arr[28], hard_arr[38], hard_arr[48], hard_arr[58], hard_arr[68], hard_arr[78], hard_arr[88], hard_arr[98] }.All(value => value == hard_arr[8]))
            {
                return 1;
            }
            else if (new[] { hard_arr[9], hard_arr[19], hard_arr[29], hard_arr[39], hard_arr[49], hard_arr[59], hard_arr[69], hard_arr[79], hard_arr[89], hard_arr[99] }.All(value => value == hard_arr[9]))
            {
                return 1;
            }
            else if (new[] { hard_arr[10], hard_arr[20], hard_arr[30], hard_arr[40], hard_arr[50], hard_arr[60], hard_arr[70], hard_arr[80], hard_arr[90], hard_arr[100] }.All(value => value == hard_arr[10]))
            {
                return 1;
            }



            //Diagonal Win Check
            else if (new[] { hard_arr[1], hard_arr[12], hard_arr[23], hard_arr[34], hard_arr[45], hard_arr[56], hard_arr[67], hard_arr[78], hard_arr[89], hard_arr[100] }.All(value => value == hard_arr[1]))
            {
                return 1;
            }
            else if (new[] { hard_arr[10], hard_arr[19], hard_arr[28], hard_arr[37], hard_arr[45], hard_arr[54], hard_arr[63], hard_arr[72], hard_arr[81], hard_arr[90] }.All(value => value == hard_arr[10]))
            {
                return 1;
            }

            //Check if Draw

            else
            {
                //Create bool indicator
                bool num_Detect = false;

                for (int i = 1; i < hard_arr.Length; i++)
                {
                    // if all values of hard_arr are not a number, return value would be -1
                    if (hard_arr[i] == i.ToString())
                    {
                        num_Detect = true;
                        break;
                    }

                }

                if (num_Detect)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }

  

        }
        
    }
}
