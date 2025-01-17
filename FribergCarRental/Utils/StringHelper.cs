﻿namespace FribergCarRental.Utils
{
    public static class StringHelper
    {
        public static string Capitalize(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            return char.ToUpper(str[0]) + str.Substring(1);
        }
    }
}
