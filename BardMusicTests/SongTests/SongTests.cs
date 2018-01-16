using BardMusic.Song;
using Const = BardMusic.Constants;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BardMusicTests.SongTests
{
    [TestClass]
    public class SongTests
    {
        [TestMethod]
        public void TestParseSong ()
        {
            String[] songLines = {
                "=====BEGIN=BARD=MUSIC=SONG=FILE=====",
                "(c,16);(c#,16);(f#+1,16);(d-1,16);",
                "======END=BARD=MUSIC=SONG=FILE======"
            };

            Song song = Song.Parse(songLines);
            Assert.AreEqual(Const.Pitch.C, song.NotesList[1].Pitch);
            Assert.AreEqual(Const.Modifier.SHARP, song.NotesList[2].Modifier);
            Assert.AreEqual(Const.Octave.LOW, song.NotesList[3].Octave);
        }
    }
}
