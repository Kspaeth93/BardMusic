using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BardMusic.Models;
using Const = BardMusic.Constants;

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
                    Pitch = Const.Pitch.C,
                    Octave = 0
                };

            Assert.AreEqual(Const.KeyBinding.CTRL + Const.KeyBinding.ONE, note.GetSendKeyString());
        }

        [TestMethod]
        public void GetSendKeys_ValidNote_C2()
        {
            var note =
                new Note()
                {
                    Pitch = Const.Pitch.C,
                    Octave = Const.Octave.TOP
                };

            Assert.AreEqual(Const.KeyBinding.ALT + Const.KeyBinding.ONE, note.GetSendKeyString());
        }

        [TestMethod]
        public void GetSendKeys_ValidNote_F1()
        {
            var note =
                new Note()
                {
                    Pitch = Const.Pitch.F,
                    Octave = Const.Octave.HIGH
                };

            Assert.AreEqual(Const.KeyBinding.SHIFT + Const.KeyBinding.SIX, note.GetSendKeyString());
        }

        [TestMethod]
        public void GetSendKeys_ValidNote_E0()
        {
            var note =
                new Note()
                {
                    Pitch = Const.Pitch.E,
                    Octave = Const.Octave.NATURAL
                };

            Assert.AreEqual(Const.KeyBinding.CTRL + Const.KeyBinding.FIVE, note.GetSendKeyString());
        }

        [TestMethod]
        public void GetSendKeys_ValidNote_CMinus1()
        {
            var note =
                new Note()
                {
                    Pitch = Const.Pitch.C,
                    Octave = Const.Octave.LOW
                };

            Assert.AreEqual(Const.KeyBinding.ONE, note.GetSendKeyString());
        }

        [TestMethod]
        public void GetSendKeys_InvalidNote_Cb0()
        {
            var threwEx = false;
            var note =
                new Note()
                {
                    Pitch = Const.Pitch.C,
                    Modifier = Const.Modifiers.FLAT,
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
                    Pitch = Const.Pitch.F,
                    Modifier = Const.Modifiers.FLAT,
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
                    Pitch = Const.Pitch.C,
                    Modifier = Const.Modifiers.SHARP,
                    Octave = Const.Octave.TOP
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
                    Pitch = Const.Pitch.B,
                    Modifier = Const.Modifiers.SHARP,
                    Octave = Const.Octave.LOW
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
