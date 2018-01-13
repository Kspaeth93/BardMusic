using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BardMusic
{
    class Program
    {
        [DllImport("User32.dll")]
        private static extern int SetForegroundWindow(IntPtr point);

        // Keybinds
        private const string ALT = "%";
        private const string CTRL = "^";
        private const string SHIFT = "+";
        private const string ONE = "1";
        private const string TWO = "2";
        private const string THREE = "3";
        private const string FOUR = "4";
        private const string FIVE = "5";
        private const string SIX = "6";
        private const string SEVEN = "7";
        private const string EIGHT = "8";
        private const string NINE = "9";
        private const string ZERO = "0";
        private const string MINUS = "-";
        private const string EQUALS = "=";

        // Note Codes
        private const string C_MINUS_1 = "c-1";
        private const string C_SHARP_MINUS_1 = "c#-1";
        private const string D_MINUS_1 = "d-1";
        private const string E_FLAT_MINUS_1 = "e@-1";
        private const string E_MINUS_1 = "e-1";
        private const string F_MINUS_1 = "f-1";
        private const string F_SHARP_MINUS_1 = "f#-1";
        private const string G_MINUS_1 = "g-1";
        private const string G_SHARP_MINUS_1 = "g#-1";
        private const string A_MINUS_1 = "a-1";
        private const string B_FLAT_MINUS_1 = "b@-1";
        private const string B_MINUS_1 = "b-1";
        private const string C = "c";
        private const string C_SHARP = "c#";
        private const string D = "d";
        private const string E_FLAT = "e@";
        private const string E = "e";
        private const string F = "f";
        private const string F_SHARP = "f#";
        private const string G = "g";
        private const string G_SHARP = "g#";
        private const string A = "a";
        private const string B_FLAT = "b@";
        private const string B = "b";
        private const string C_PLUS_1 = "c+1";
        private const string C_SHARP_PLUS_1 = "c#+1";
        private const string D_PLUS_1 = "d+1";
        private const string E_FLAT_PLUS_1 = "e@+1";
        private const string E_PLUS_1 = "e+1";
        private const string F_PLUS_1 = "f+1";
        private const string F_SHARP_PLUS_1 = "f#+1";
        private const string G_PLUS_1 = "g+1";
        private const string G_SHARP_PLUS_1 = "g#+1";
        private const string A_PLUS_1 = "a+1";
        private const string B_FLAT_PLUS_1 = "b@+1";
        private const string B_PLUS_1 = "b+1";
        private const string C_PLUS_2 = "c+2";
        private const string REST = "r";

        // Frequency Codes
        private const string SIXTEENTH = "16";
        private const string EIGHTH = "8";
        private const string FIFTH = "5";
        private const string QUARTER = "4";
        private const string THIRD = "3";
        private const string HALF = "2";
        private const string FULL = "1";
        
        // Frequency Intervals
        private const int MINIMUM_INTERVAL = 165;
        private const int SUGGESTED_INTERVAL = 175;
        private const int MAXIMUM_INTERVAL = 185;

        // Note Lengths
        private int sixteenthNote = SUGGESTED_INTERVAL;
        private int eighthNote = SUGGESTED_INTERVAL * 2;
        private int fifthNote = (SUGGESTED_INTERVAL * 16) / 5;
        private int quarterNote = SUGGESTED_INTERVAL * 4;
        private int thirdNote = (SUGGESTED_INTERVAL * 16) / 3;
        private int halfNote = SUGGESTED_INTERVAL * 8;
        private int fullNote = SUGGESTED_INTERVAL * 16;

        // Rest Lengths
        private int sixteenthRest = SUGGESTED_INTERVAL;
        private int eighthRest = SUGGESTED_INTERVAL * 2;
        private int fifthRest = (SUGGESTED_INTERVAL * 16) / 5;
        private int quarterRest = SUGGESTED_INTERVAL * 4;
        private int thirdRest = (SUGGESTED_INTERVAL * 16) / 3;
        private int halfRest = SUGGESTED_INTERVAL * 8;
        private int fullRest = SUGGESTED_INTERVAL * 16;

        static void Main(string[] args)
        {
            Program program = new Program();
            Process finalFantasyProcess = program.GetFinalFantasyProcess();
            program.SetProcessToForeground(finalFantasyProcess);
        }

        private void SetDefaultInterval()
        {
            sixteenthNote = SUGGESTED_INTERVAL;
            eighthNote = SUGGESTED_INTERVAL * 2;
            fifthNote = (SUGGESTED_INTERVAL * 16) / 5;
            quarterNote = SUGGESTED_INTERVAL * 4;
            thirdNote = (SUGGESTED_INTERVAL * 16) / 3;
            halfNote = SUGGESTED_INTERVAL * 8;
            fullNote = SUGGESTED_INTERVAL * 16;

            sixteenthRest = SUGGESTED_INTERVAL;
            eighthRest = SUGGESTED_INTERVAL * 2;
            fifthRest = (SUGGESTED_INTERVAL * 16) / 5;
            quarterRest = SUGGESTED_INTERVAL * 4;
            thirdRest = (SUGGESTED_INTERVAL * 16) / 3;
            halfRest = SUGGESTED_INTERVAL * 8;
            fullRest = SUGGESTED_INTERVAL * 16;
        }

        private void SetCustomInterval(int customInterval)
        {
            if (customInterval >= MINIMUM_INTERVAL && customInterval >= MAXIMUM_INTERVAL)
            {
                sixteenthNote = customInterval;
                eighthNote = customInterval * 2;
                fifthNote = (customInterval * 16) / 5;
                quarterNote = customInterval * 4;
                thirdNote = (customInterval * 16) / 3;
                halfNote = customInterval * 8;
                fullNote = customInterval * 16;

                sixteenthRest = customInterval;
                eighthRest = customInterval * 2;
                fifthRest = (customInterval * 16) / 5;
                quarterRest = customInterval * 4;
                thirdRest = (customInterval * 16) / 3;
                halfRest = customInterval * 8;
                fullRest = customInterval * 16;
            }
        }

        private Process GetFinalFantasyProcess()
        {
            return Process.GetProcessesByName("ffxiv_dx11").FirstOrDefault();
        }

        private void SetProcessToForeground(Process process)
        {
            if (process != null)
            {
                IntPtr mainWindowIntPtr = process.MainWindowHandle;
                SetForegroundWindow(mainWindowIntPtr);
            }
        }
    }
}
