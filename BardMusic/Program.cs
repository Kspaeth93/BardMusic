using Const = BardMusic.Constants;
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using BardMusic.Models;

namespace BardMusic
{
    class Program
    {
        [DllImport("User32.dll")]
        protected static extern int SetForegroundWindow(IntPtr point);

        // FFXIV processing interval
        protected static int currentInterval = Const.Interval.RECOMMENDED;

        static void Main (string[] args)
        {
            Program program = new Program();

            Song song = program.ReadSongFile(@"..\..\Test Files\test_song1.song");

            Process process = program.GetFinalFantasyProcess();
 
            if (process != null)
            {
                program.SetProcessToForeground(process);
                song.Play(currentInterval);
            }


            // TODO: Remove debug
            
            foreach(var note in song.NotesList)
            {
                Console.WriteLine("Note: (" + note.Pitch + note.Modifier + note.Octave + "," + note.Length + ")");
                Console.WriteLine("Key Binding: " + note.GetSendKeyString());
            }
            
        }

        protected Process GetFinalFantasyProcess ()
        {
            return Process.GetProcessesByName("ffxiv_dx11").FirstOrDefault();
        }

        protected void SetProcessToForeground (Process process)
        {
            if (process == null)
            {
                throw new ArgumentNullException("Process is undefined.");
            }
            else
            {
                IntPtr mainWindowIntPtr = process.MainWindowHandle;
                SetForegroundWindow(mainWindowIntPtr);
            }
        }

        protected void SetDefaultInterval()
        {
            currentInterval = Const.Interval.RECOMMENDED;
        }

        protected void SetCustomInterval (int customInterval)
        {
            if (customInterval < Const.Interval.MINIMUM || customInterval > Const.Interval.MAXIMUM)
            {
                throw new ArgumentOutOfRangeException("Custom interval is outside of the allowed range.");
            }
            else
            {
                currentInterval = customInterval;
            }
        }

        protected Song ReadSongFile (String filePath)
        {
            TextReader tr = new StreamReader(new FileStream(filePath, FileMode.Open));

            return Song.Parse(tr);
        }
    }
}
