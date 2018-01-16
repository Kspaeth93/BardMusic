using System;
using System.Collections.Generic;

namespace BardMusic.Song
{
    public class Song
    {
        public List<Note> NotesList;

        public static Song Parse (String[] songLines)
        {
            if (songLines == null || songLines.Length < 3)
            {
                throw new ArgumentNullException("Song lines are undefined.");
            }
            else
            {
                Song song = new Song();
                List<Note> notesList = new List<Note>();

                if (songLines[0] != "=====BEGIN=BARD=MUSIC=SONG=FILE=====" ||
                    songLines[songLines.Length - 1] != "======END=BARD=MUSIC=SONG=FILE======")
                {
                    throw new Exception("Song is not enclosed by tags.");
                }
                else
                {
                    for (int i = 1; i < songLines.Length - 1; i++)
                    {
                        if (songLines[i] != null && songLines[i].Length > 1)
                        {
                            String[] noteStrings = songLines[i].Split(';');

                            if (noteStrings != null && noteStrings.Length > 1)
                            {
                                for (int j = 0; j < noteStrings.Length; j++)
                                {
                                    if (noteStrings[j] != null && noteStrings[j].Length > 1)
                                    {
                                        notesList.Add(Note.Parse(noteStrings[j]));
                                    }
                                }
                            }
                        }
                    }

                    song.NotesList = notesList;
                    return song;
                }
            }
        }
    }
}