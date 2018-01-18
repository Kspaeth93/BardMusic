using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardMusic.Extensions
{
    public static class TextReaderExtensions
    {

        public static void ReadUntil(this TextReader inputStream, char targetChar)
        {
            while(inputStream.ReadIf(c => c != targetChar))
            {
                if (inputStream.Peek() == -1)
                {
                    break;
                }
            }
        }

        public static bool ReadIf(this TextReader inputStream, Func<char, bool> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            if (predicate((char)inputStream.Peek()))
            {
                inputStream.Read();//consume
                return true;
            }

            return false;
        }

        public static bool ReadIfEqualTo(this TextReader inputStream, char compareChar)
        {
            return inputStream.ReadIf(c => c == compareChar);
        }

        public static void ReadToNextNonWhitespaceCharacter(this TextReader inputStream)
        {
            char curChar;
            do
            {
                curChar = (char)inputStream.Peek();
                if (!char.IsWhiteSpace(curChar) || curChar == -1)
                {
                    break;
                }
                inputStream.Read();//consume the whitespace!
            } while (true);
        }
    }
}
