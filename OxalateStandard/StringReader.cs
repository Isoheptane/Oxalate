using System;
using System.Collections.Generic;
using System.Text;

namespace Oxalate.Standard
{
    public static class StringReader
    {
        static bool IsSpace(char ch)
        {
            return ch <= 32;
        }
        static void IgnoreSpaces(ref string str, ref int ptr)
        {
            while (IsSpace(str[ptr])) ptr++;
        }
        static string ReadString(ref string str, ref int ptr)
        {
            IgnoreSpaces(ref str, ref ptr);
            StringBuilder ret = new StringBuilder();
            if (str[ptr] != '\"')
            {
                while (ptr < str.Length && str[ptr] != ' ')
                    ret.Append(str[ptr++]);
                return ret.ToString();
            }
            ptr++;
            while (str[ptr] != '\"')
            {
                if (str[ptr] == '\\')
                {
                    switch (str[ptr + 1])
                    {
                        case '\\': { ret.Append('\\'); break; }
                        case '\"': { ret.Append('\"'); break; }
                        case 'n': { ret.Append('\n'); break; }
                        case 'b': { ret.Append('\b'); break; }
                        case '0': { ret.Append('\0'); break; }
                        case 'a': { ret.Append('\a'); break; }
                        case 'f': { ret.Append('\f'); break; }
                        case 'r': { ret.Append('\r'); break; }
                        case 't': { ret.Append('\t'); break; }
                        case 'v': { ret.Append('\v'); break; }
                    }
                    ptr += 2;
                    continue;
                }
                ret.Append(str[ptr]);
                ptr++;
            }
            ptr++;
            return ret.ToString();
        }
        public static string[] ReadStringArray(string source)
        {
            int pointer = 0;
            List<string> strings = new List<string>();
            while (pointer < source.Length)
                strings.Add(ReadString(ref source, ref pointer));
            return strings.ToArray();
        }
    }
}
