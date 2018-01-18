using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Song;

namespace BardMusicTests.SongTests
{
    [TestClass]
    public class GetSendKeysTests
    {
        [TestMethod]
        public void GetSendKeys_ValidNote_C0()
        {
            var note = 
                new Note()
                {
                    Pitch = Constants.Pitch.C,
                    Octave = 0
                };

            Assert.AreEqual(Constants.KeyBinding.CTRL + Constants.KeyBinding.ONE, note.GetSendKeyString());
        }

        [TestMethod]
        public void GetSendKeys_ValidNote_C2()
        {
            var note =
                new Note()
                {
                    Pitch = Constants.Pitch.C,
                    Octave = Constants.Octave.TOP
                };

            Assert.AreEqual(Constants.KeyBinding.ALT + Constants.KeyBinding.ONE, note.GetSendKeyString());
        }

        [TestMethod]
        public void GetSendKeys_ValidNote_F1()
        {
            var note =
                new Note()
                {
                    Pitch = Constants.Pitch.F,
                    Octave = Constants.Octave.HIGH
                };

            Assert.AreEqual(Constants.KeyBinding.SHIFT + Constants.KeyBinding.SIX, note.GetSendKeyString());
        }

        [TestMethod]
        public void GetSendKeys_ValidNote_E0()
        {
            var note =
                new Note()
                {
                    Pitch = Constants.Pitch.E,
                    Octave = Constants.Octave.NATURAL
                };

            Assert.AreEqual(Constants.KeyBinding.CTRL + Constants.KeyBinding.FIVE, note.GetSendKeyString());
        }

        [TestMethod]
        public void GetSendKeys_ValidNote_CMinus1()
        {
            var note =
                new Note()
                {
                    Pitch = Constants.Pitch.C,
                    Octave = Constants.Octave.LOW
                };

            Assert.AreEqual(Constants.KeyBinding.ONE, note.GetSendKeyString());
        }

        [TestMethod]
        public void GetSendKeys_InvalidNote_Cb0()
        {
            var threwEx = false;
            var note =
                new Note()
                {
                    Pitch = Constants.Pitch.C,
                    Modifier = Constants.Modifiers.FLAT,
                    Octave = 0
                };

            try
            {
                note.GetSendKeyString();
            }
            catch(ArgumentException ae)
            {
                Trace.WriteLine(ae.Message);
                threwEx = true;
            }

            Assert.IsTrue(threwEx);
        }

        [TestMethod]
        public void GetSendKeys_InvalidNote_Fb1()
        {
            var threwEx = false;
            var note =
                new Note()
                {
                    Pitch = Constants.Pitch.F,
                    Modifier = Constants.Modifiers.FLAT,
                    Octave = 1
                };

            try
            {
                note.GetSendKeyString();
            }
            catch (ArgumentException ae)
            {
                Trace.WriteLine(ae.Message);
                threwEx = true;
            }

            Assert.IsTrue(threwEx);
        }

        [TestMethod]
        public void GetSendKeys_InvalidNote_Cs2()
        {
            var threwEx = false;
            var note =
                new Note()
                {
                    Pitch = Constants.Pitch.C,
                    Modifier = Constants.Modifiers.SHARP,
                    Octave = Constants.Octave.TOP
                };

            try
            {
                note.GetSendKeyString();
            }
            catch (ArgumentException ae)
            {
                Trace.WriteLine(ae.Message);
                threwEx = true;
            }

            Assert.IsTrue(threwEx);
        }

        [TestMethod]
        public void GetSendKeys_InvalidNote_BsMinus1()
        {
            var threwEx = false;
            var note =
                new Note()
                {
                    Pitch = Constants.Pitch.B,
                    Modifier = Constants.Modifiers.SHARP,
                    Octave = Constants.Octave.LOW
                };

            try
            {
                note.GetSendKeyString();
            }
            catch (ArgumentException ae)
            {
                Trace.WriteLine(ae.Message);
                threwEx = true;
            }

            Assert.IsTrue(threwEx);
        }
    }
}
