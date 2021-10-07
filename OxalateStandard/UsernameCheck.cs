using System;
using System.Collections.Generic;
using System.Text;

namespace Oxalate.Standard
{
    public static class UsernameCheck
    {
        public static bool IsLegalUsername(string username)
        {
            foreach (char ch in username)
            {
                if (ch <= 48 || ch >= 123)
                    return false;
                if (ch >= 58 && ch <= 64)
                    return false;
                if (ch >= 91 && ch <= 96)
                    return false;
            }
            return true;
        }

        public static bool IsLegalNickname(string nickname)
        {
            foreach (char ch in nickname)
            {
                if (ch <= 31)
                    return false;
                if (ch == '$')
                    return false;
            }
            return true;
        }
    }
}
