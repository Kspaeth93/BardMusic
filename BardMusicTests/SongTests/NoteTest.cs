using Const = BardMusic.Constants;
using BardMusic.Song;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BardMusicTests.SongTests
{
    [TestClass]
    public class NoteTest
    {
        [TestMethod]
        public void TestParse()
        {
            Note note = new Note();

            note = Note.Parse("(c#+1,8)");
            Assert.AreEqual(Const.Pitch.C, note.Pitch);
            Assert.AreEqual(Const.Modifier.SHARP, note.Modifier);
            Assert.AreEqual(Const.Octave.HIGH, note.Octave);
            Assert.AreEqual(Const.Length.EIGHTH, note.Length);
        }
        [TestMethod]
        public void TestGetKeyBinding()
        {
            String keyBinding;
            Note note = new Note();

            note = Note.Parse("(c#+1,8)");
            keyBinding = Note.GetKeyBinding(note);
            Assert.AreEqual("+2", keyBinding);
        }
    }
}
