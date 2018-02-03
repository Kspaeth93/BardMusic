using BardMusic.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using WindowsInput;
using WindowsInput.Native;

namespace BardMusic.Models
{
    public class Song
    {
        public List<Note> NotesList = new List<Note>();

        public static Song Parse (TextReader inputStream)
        {
            Song song = new Song();

            //if (songLines[0] != "=====BEGIN=BARD=MUSIC=SONG=FILE=====" ||
            //   songLines[songLines.Length - 1] != "======END=BARD=MUSIC=SONG=FILE======")

            do
            {
                inputStream.ReadUntil('(');
                //must have hit the end
                if ((char)inputStream.Peek() != '(')
                {
                    break;
                }
                //otherwise, parse the note
                Note newNote = null;
                try
                {
                    newNote = Note.Parse(inputStream);
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                
                song.NotesList.Add(newNote);
            }
            while (true);

            return song;
        }

        public void Play(int interval)
        {
            if (NotesList == null || NotesList.Count < 1)
            {
                throw new ArgumentNullException("Song has no notes.");
            }
            else
            {
                foreach(var note in NotesList)
                {
                    var sendKeyStr = note.GetSendKeyString();

                    var inputSim = new InputSimulator();

                    if (sendKeyStr.Contains(Constants.KeyBinding.SHIFT))
                    {
                        inputSim.Keyboard.KeyDown(VirtualKeyCode.SHIFT);
                    }
                    if (sendKeyStr.Contains(Constants.KeyBinding.CTRL))
                    {
                        inputSim.Keyboard.KeyDown(VirtualKeyCode.CONTROL);
                    }

                    //inputSim.Keyboard.KeyPress(VirtualKeyCode)
                    SendKeys.SendWait(sendKeyStr);
                    Thread.Sleep(
                        (interval * 16) / note.Length
                    );
                }
            }
        }
    }
}