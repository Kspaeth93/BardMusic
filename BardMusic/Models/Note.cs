using Const = BardMusic.Constants;
using System;
using System.IO;
using BardMusic.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace BardMusic.Models
{
    public class Note
    {
        public char Pitch
        {
            get
            {
                char pitch = (char)('A' + NoteIndex);

                return NoteIndex != -1 ? pitch : Const.Pitch.Rest;
            }
            set
            {
                if (value == Const.Pitch.Rest)
                {
                    NoteIndex = -1;
                    return;
                }

                if (value >= Const.Pitch.A && value <= Const.Pitch.G)
                {
                    NoteIndex = value - Const.Pitch.A;
                }
                else
                {
                    throw new ArgumentOutOfRangeException($"Specified pitch {value} is out of range. Must be A-G or R.");
                }
            }
        }

        public Const.Modifiers Modifier = Const.Modifiers.NATURAL;

        /// <summary>
        /// 0 = A,...,7 = G.
        /// -1 = R (Rest)
        /// </summary>
        protected int NoteIndex = 0;

        protected int length = 1;
        public int Length
        {
            get
            {
                return length;
            }
            set
            {
                if (value >= 1 && value <= 5 || value == 8 || value == 16)
                {
                    length = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Note length can only be 1, 2, 3, 4, 5, 8 or 16.");
                }
            }
        }

        protected int octave = Const.Octave.NATURAL;
        public int Octave
        {
            get
            {
                return octave;
            }
            set
            {
                if (value < -1 || value > 2)
                {
                    throw new ArgumentOutOfRangeException($"Out of range [-1, 2], try to set to {value}");
                }
                octave = value;
            }
        }

        public static Note Parse(TextReader inputStream)
        {
            var newNote = new Note();

            if ((char)inputStream.Read() != '(')
            {
                throw new InvalidOperationException("Stream must start with ( to process a note!");
            }
            else
            {
                inputStream.ReadToNextNonWhitespaceCharacter();//move to next input to process
            }

            if (TryParseNoteOffset(newNote, inputStream))
            {
                inputStream.ReadToNextNonWhitespaceCharacter();
            }
            else
            {
                throw new FormatException("Notes must start with a valid pitch identifier.");
            }

            //Only need to keep processing the pitch if it isn't a rest
            if (newNote.Pitch != Const.Pitch.Rest)
            {
                //optional, defaults to natural if not specified
                TryParseNoteModifier(newNote, inputStream);

                inputStream.ReadToNextNonWhitespaceCharacter();

                //optional, defauls to zero if not specified
                TryParseNoteOctave(newNote, inputStream);

                inputStream.ReadToNextNonWhitespaceCharacter();
            }

            if (!inputStream.ReadIfEqualTo(','))
            {
                var badChar = (char)inputStream.Peek();
                throw new FormatException($"Expected a comma after pitch-octave identifier. Instead got '{badChar}'.");
            }

            inputStream.ReadToNextNonWhitespaceCharacter();

            if (TryParseNoteLength(newNote, inputStream))
            {
                inputStream.ReadToNextNonWhitespaceCharacter();
            }
            else
            {
                throw new FormatException("Expected a valid note length after pitch-octave identifier.");
            }

            //consume to move the stream off this note
            inputStream.ReadIfEqualTo(')');
            inputStream.ReadIfEqualTo(';');

            return newNote;
        }

        private static bool TryParseNoteLength(Note newNote, TextReader inputStream)
        {
            var noteLengthSet = false;
            var intLength = -1;

            if (noteLengthSet = TryParseIntFromStream(inputStream, out intLength))
            {
                newNote.Length = intLength;
            }


            return noteLengthSet;
        }

        private static bool TryParseNoteOctave(Note newNote, TextReader _inputStream)
        {
            var noteOctaveSet = false;
            var curChar = (char)_inputStream.Peek();
            var negate = false;

            if (curChar == '-' || curChar == '+')
            {
                if (curChar == '-') { negate = true; }
                _inputStream.Read();//consume the + or -
                curChar = (char)_inputStream.Peek();
            }

            if (char.IsDigit(curChar))
            { 
                if (noteOctaveSet = TryParseIntFromStream(_inputStream, out int octave))
                {
                    newNote.Octave = octave * (negate ? -1 : 1);
                }
            }

            return noteOctaveSet;
        }

        private static bool TryParseNoteModifier(Note note, TextReader inputStream)
        {
            var setModifier = false;
            var curChar = (char)inputStream.Peek();

            switch (curChar.ToString())
            {
                case Const.Modifier.FLAT:
                    note.Modifier = Const.Modifiers.FLAT;
                    setModifier = true;
                    break;
                case Const.Modifier.SHARP:
                    note.Modifier = Const.Modifiers.SHARP;
                    setModifier = true;
                    break;
            }

            if (setModifier) { inputStream.Read(); }

            return setModifier;
        }

        private static bool TryParseNoteOffset(Note note, TextReader inputStream)
        {
            var processedNoteOffset = false;
            var curChar = (char)inputStream.Peek();

            var alphaCharOffset = char.ToUpper(curChar) - 'A';
            if (alphaCharOffset >= 0 && alphaCharOffset <= 'G' - 'A')
            {
                note.NoteIndex = alphaCharOffset;
                processedNoteOffset = true;
            }
            else if (curChar == Const.Pitch.Rest)
            {
                note.Pitch = Const.Pitch.Rest;
                processedNoteOffset = true;
            }

            if (processedNoteOffset) { inputStream.Read(); }

            return processedNoteOffset;
        }

        protected static bool TryParseIntFromStream(TextReader inputStream, out int parsedInt)
        {
            var intStr = "";
            char curChar;

            do
            {
                curChar = (char)inputStream.Peek();
                if (char.IsDigit(curChar))
                {
                    intStr += curChar;
                    inputStream.Read();
                }
                else
                {
                    break;
                }
            }
            while (true);

            return int.TryParse(intStr, out parsedInt);
        }

        protected string[] keyOrdering = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "-", "="};
        protected Dictionary<char, int> pitchOffsetMapping =
            new Dictionary<char, int>()
            {
                {Const.Pitch.C, 0},
                {Const.Pitch.D, 2},
                {Const.Pitch.E, 4},
                {Const.Pitch.F, 5},
                {Const.Pitch.G, 7},
                {Const.Pitch.A, 9},
                {Const.Pitch.B, 11},
                {Const.Pitch.Rest, -1}
            };
        protected char[] pitchesThatDontAllowFlats = new char[] { Const.Pitch.C, Const.Pitch.F };
        protected char[] pitchesThatDontAllowSharps = new char[] { Const.Pitch.E, Const.Pitch.B };

        public string GetSendKeyString()
        {
            var sendKeyStr = "";

            if (pitchOffsetMapping.ContainsKey(Pitch))
            {
                var offset = GetPitchKeyOffset(Pitch);
                var modifierKey = "";

                switch(Octave)
                {
                    case Const.Octave.NATURAL:
                        modifierKey = Const.KeyBinding.CTRL;
                        break;
                    case Const.Octave.HIGH:
                        modifierKey = Const.KeyBinding.SHIFT;
                        break;
                    case Const.Octave.TOP:
                        modifierKey = Const.KeyBinding.ALT;
                        break;
                }

                if (Octave == Const.Octave.TOP && (Pitch != Const.Pitch.C || Modifier != Const.Modifiers.NATURAL))
                {
                    throw new ArgumentException("Only C natural is supported in the top octave.");
                }

                sendKeyStr = modifierKey + keyOrdering[offset];
            }

            return sendKeyStr;
        }

        private int GetPitchKeyOffset(char pitch)
        {
            var offset = pitchOffsetMapping[Pitch];

            if (offset >= 0 && offset < keyOrdering.Length)
            {
                if (Modifier == Const.Modifiers.FLAT)
                {
                    if (pitchesThatDontAllowFlats.Any(p => p == Pitch))
                    {
                        throw new ArgumentException($"Flats are not allowed for {Pitch} notes.");
                    }
                    else
                    {
                        offset--;
                    }
                }
                else if (Modifier == Const.Modifiers.SHARP)
                {
                    if (pitchesThatDontAllowSharps.Any(p => p == Pitch))
                    {
                        throw new ArgumentException($"Sharps are not allowed for {Pitch} notes.");
                    }
                    else
                    {
                        offset++;
                    }
                }
            }

            return offset;
        }
    }
}