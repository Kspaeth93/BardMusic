using Const = BardMusic.Constants;
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace BardMusic
{
    class Program
    {
        [DllImport("User32.dll")]
        protected static extern int SetForegroundWindow(IntPtr point);

        // Note Lengths
        protected int sixteenthNote = Const.Interval.RECOMMENDED;
        protected int eighthNote = Const.Interval.RECOMMENDED * 2;
        protected int fifthNote = (Const.Interval.RECOMMENDED * 16) / 5;
        protected int quarterNote = Const.Interval.RECOMMENDED * 4;
        protected int thirdNote = (Const.Interval.RECOMMENDED * 16) / 3;
        protected int halfNote = Const.Interval.RECOMMENDED * 8;
        protected int fullNote = Const.Interval.RECOMMENDED * 16;

        static void Main(string[] args)
        {
        }

        protected void SetDefaultInterval()
        {
            sixteenthNote = Const.Interval.RECOMMENDED;
            eighthNote = Const.Interval.RECOMMENDED * 2;
            fifthNote = (Const.Interval.RECOMMENDED * 16) / 5;
            quarterNote = Const.Interval.RECOMMENDED * 4;
            thirdNote = (Const.Interval.RECOMMENDED * 16) / 3;
            halfNote = Const.Interval.RECOMMENDED * 8;
            fullNote = Const.Interval.RECOMMENDED * 16;
        }

        protected void SetCustomInterval(int customInterval)
        {
            if (customInterval >= Const.Interval.MINIMUM && customInterval >= Const.Interval.MAXIMUM)
            {
                sixteenthNote = customInterval;
                eighthNote = customInterval * 2;
                fifthNote = (customInterval * 16) / 5;
                quarterNote = customInterval * 4;
                thirdNote = (customInterval * 16) / 3;
                halfNote = customInterval * 8;
                fullNote = customInterval * 16;
            }
        }

        protected Process GetFinalFantasyProcess()
        {
            return Process.GetProcessesByName("ffxiv_dx11").FirstOrDefault();
        }

        protected void SetProcessToForeground(Process process)
        {
            if (process != null)
            {
                IntPtr mainWindowIntPtr = process.MainWindowHandle;
                SetForegroundWindow(mainWindowIntPtr);
            }
        }
    }
}
