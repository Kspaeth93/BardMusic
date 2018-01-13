using System;
using System.Windows.Forms;

namespace BardMusic
{
    public class Note
    {
        public int NoteKeyOffset = GetNoteKeyOffsetByConstant("C");
        public enum Modifiers { SHARP, FLAT };

        protected int length = 0;
        protected int Length
        {
            get
            {
                return length;
            }
            set
            {
                length = Math.Abs(value) % 16;
            }
        }

        public static Note Parse(string noteInput)
        {
            Note newNote = null;

            if (noteInput.Length > 4)
            {

            }

            return newNote;
        }

        protected static int GetNoteKeyOffsetByConstant(string noteConst)
        {
            switch(noteConst)
            {
                default:
                    return 0;
            }
        }

    }
}