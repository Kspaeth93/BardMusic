using System.Collections.Generic;
using System.IO;

namespace BardMusic
{
    public class BardMusicSong
    {
        protected List<Note> Notes = new List<Note>();

        public void AddNote(Note n)
        {
            Notes.Add(n);
        }

        public static BardMusicSong Parse(StreamReader _inputStream)
        {
            var song = new BardMusicSong();
            var curNote = new Note();

            char curChar;
            while ((curChar = (char)_inputStream.Read()) != -1)
            {
                switch (curChar)
                {
                    case '(':
                        curNote = Note.Parse(_inputStream, curChar);
                        song.AddNote(curNote);
                        break;
                }

                return song;
            }
            return song;
        }
    }
}