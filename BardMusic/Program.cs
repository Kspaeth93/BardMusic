using BardMusic.Song;
using Const = BardMusic.Constants;
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;

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

        static void Main (string[] args)
        {
            Program program = new Program();

            String[] songLines = program.ReadSongFile(@"C:\Users\1022414\Desktop\song.txt");
            Song.Song song = Song.Song.Parse(songLines);

            Process process = program.GetFinalFantasyProcess();
            if (process != null)
            {
                program.SetProcessToForeground(process);
                program.PlaySong(song);
            }

            // TODO: Remove debug
            for (int i = 0; i < song.NotesList.Count; i++)
            {
                Console.WriteLine("Note: (" + song.NotesList[i].Pitch + song.NotesList[i].Modifier + song.NotesList[i].Octave + "," + song.NotesList[i].Length + ")");
                Console.WriteLine("Key Binding: " + Note.GetKeyBinding(song.NotesList[i]));
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

        protected void SetDefaultInterval ()
        {
            sixteenthNote = Const.Interval.RECOMMENDED;
            eighthNote = Const.Interval.RECOMMENDED * 2;
            fifthNote = (Const.Interval.RECOMMENDED * 16) / 5;
            quarterNote = Const.Interval.RECOMMENDED * 4;
            thirdNote = (Const.Interval.RECOMMENDED * 16) / 3;
            halfNote = Const.Interval.RECOMMENDED * 8;
            fullNote = Const.Interval.RECOMMENDED * 16;
        }

        protected void SetCustomInterval (int customInterval)
        {
            if (customInterval < Const.Interval.MINIMUM || customInterval > Const.Interval.MAXIMUM)
            {
                throw new ArgumentOutOfRangeException("Custom interval is outside of the allowed range.");
            }
            else
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

        protected String[] ReadSongFile (String filePath)
        {
            if (filePath == null || filePath.Length < 1)
            {
                throw new ArgumentNullException("Invalid file path.");
            }
            else
            {
                return System.IO.File.ReadAllLines(filePath);
            }
        }

        protected void PlaySong (Song.Song song)
        {
            if (song == null)
            {
                throw new ArgumentNullException("Song is undefined.");
            }
            else
            {
                if (song.NotesList == null || song.NotesList.Count < 1)
                {
                    throw new ArgumentNullException("Song has no notes.");
                }
                else
                {
                    for (int i = 0; i < song.NotesList.Count; i++)
                    {
                        int noteLength;
                        String keyBinding = Note.GetKeyBinding(song.NotesList[i]);

                        switch (song.NotesList[i].Length)
                        {
                            case Const.Length.FULL:
                                noteLength = fullNote;
                                break;
                            case Const.Length.HALF:
                                noteLength = halfNote;
                                break;
                            case Const.Length.THIRD:
                                noteLength = thirdNote;
                                break;
                            case Const.Length.QUARTER:
                                noteLength = quarterNote;
                                break;
                            case Const.Length.FIFTH:
                                noteLength = fifthNote;
                                break;
                            case Const.Length.EIGHTH:
                                noteLength = eighthNote;
                                break;
                            default:
                                noteLength = sixteenthNote;
                                break;
                        }

                        SendKeys.SendWait(keyBinding);
                        Thread.Sleep(noteLength);
                    }
                }
            }
        }
    }
}
