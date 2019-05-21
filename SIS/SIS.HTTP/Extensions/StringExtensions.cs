using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.HTTP.Extensions
{
    public static class StringExtensions
    {
        public static string Capitalize(this string text)
        {
            string result = char.ToUpper(text[0]) + text.Substring(1).ToLower();
            return result;
        }
    }
}
